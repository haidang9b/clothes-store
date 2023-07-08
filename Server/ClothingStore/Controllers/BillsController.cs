using ClothingStore.Data.Repositories;
using ClothingStore.Entities;
using ClothingStore.Entities.Dtos;
using ClothingStore.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClothingStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IBillRepository _billRepository;
        private readonly IUserRepository _userRepository;
        public BillsController(IBillRepository billRepository, IUserRepository userRepository)
        {
            _billRepository = billRepository;
            _userRepository = userRepository;
        }
        // GET: api/<BillsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new ApiResult();
            try
            {
                List<BillDto> billDtos = new List<BillDto>();
                var bills = await _billRepository.GetBills();
                foreach (var item in bills)
                {
                    billDtos.Add(new BillDto
                    {
                        Id = item.Id,
                        User = item.User,
                        UpdateDate = item.UpdateDate,
                        CreatedDate = item.CreatedDate,
                        Address = item.Address,
                        NumberPhone = item.NumberPhone,
                        NameReceiver = item.NameReceiver,
                        Status = item.Status,
                        TotalPrice = await _billRepository.GetTotalPrice(item.Id)
                    });
                }
                result.Data = billDtos;
                result.Message = "Get list bill is successfully";
            }
            catch (Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        [AllowAnonymous]
        // GET api/<BillsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = new ApiResult();
            try
            {
                var data = await _billRepository.GetBillDetail(id);
                if (data == null)
                {
                    result.Message = "Get bill is failed";
                    result.IsSuccess = false;
                }
                else
                {
                    result.Data = data;
                    result.Message = "Get bill is successfully";
                }

            }
            catch (Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        // POST api/<BillsController>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CartDto cart)
        {
            var result = new ApiResult();
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                var username = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                var user = await _userRepository.GetUserByUsername(username);
                await _billRepository.InsertBill(cart, user);
                var idLast = await _billRepository.GetLastBillByUserID(user.Id);
                result.Message = "Create bill is successfully";
                result.Data = idLast;
            }
            catch (Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        [Authorize(Roles = "Seller,Admin")]
        [HttpPut("change-status/{id}")]
        public async Task<IActionResult> ChangeStatus(int id, [FromForm] EStatusBill status)
        {
            var result = new ApiResult();
            try
            {
                if (await _billRepository.ChangeStatus(id, status))
                {
                    result.Message = "Change status of bill is successfully";
                }
                else
                {
                    result.Message = "Change status of bill is failed";
                    result.IsSuccess = false;
                }
            }
            catch (Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        [HttpPost("export-order")]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> ExportOrder([FromForm] ExportDataDto data)
        {
            var result = new ApiResult();
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Templates", "Export_Order.xlsx");
                var templateFile = new FileInfo(filePath);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                var package = new ExcelPackage(templateFile);
                var fileOut = await CreateOrderExportWorkSheet(package, data);
                var dtNow = DateTimeOffset.Now;
                var fileName = $"{dtNow.Day}{dtNow.Month}{dtNow.Day} - Orders.xlsx";
                return File(await fileOut.GetAsByteArrayAsync(), "application/octet-stream", fileName);
            }
            catch (Exception e)
            {
                result.InternalError();
                return Ok(result);
            }

        }
        #region Privates
        private async Task<ExcelPackage> CreateOrderExportWorkSheet(ExcelPackage package, ExportDataDto data)
        {
            var ws = package.Workbook.Worksheets[0];
            var result = await _billRepository.GetBillDetailByTime(data);
            var listData = result.ToList();
            for (int i = 0; i < listData.Count; i++)
            {
                int r = i + 2;
                ws.Cells[r, 1].Value = listData[i].Id;
                ws.Cells[r, 2].Value = listData[i].Username;
                ws.Cells[r, 3].Value = listData[i].CreatedDate;
                ws.Cells[r, 4].Value = listData[i].UpdateDate;
                ws.Cells[r, 5].Value = listData[i].NameReceiver;
                ws.Cells[r, 6].Value = listData[i].NumberPhone;
                ws.Cells[r, 7].Value = listData[i].Address;
                ws.Cells[r, 8].Value = listData[i].Status;
                ws.Cells[r, 9].Value = listData[i].TotalCost;
            }
            return package;

        }

        #endregion Privates
    }
}
