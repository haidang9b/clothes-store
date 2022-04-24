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
        private readonly IBillRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BillsController(IBillRepository repository, IUserRepository userRepository, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _userRepository = userRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: api/<BillsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new ApiResult();
            try
            {
                List<BillDto> billDtos = new List<BillDto>();
                var bills = await _repository.GetBills();
                foreach(var item in bills)
                {
                    billDtos.Add(new BillDto
                    {
                        id=item.id,
                        user = item.user,
                        updateDate = item.updateDate,
                        createdDate = item.createdDate,
                        address = item.address,
                        numberPhone = item.numberPhone,
                        nameReceiver = item.nameReceiver,
                        status = item.status,
                        totalPrice = await _repository.GetTotalPrice(item.id)
                    });
                }
                result.Data = billDtos;
                result.Message = "Get list bill is successfully";
            }
            catch(Exception e)
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
                var data = await _repository.GetBillDetail(id);
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
            catch(Exception e)
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
                await _repository.InsertBill(cart, user);
                var idLast = await _repository.GetLastBillByUserID(user.Id);
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
        public async Task<IActionResult> ChangeStatus(int id, [FromForm]EStatusBill status)
        {
            var result = new ApiResult();
            try
            {
                if(await _repository.ChangeStatus(id, status))
                {
                    result.Message = "Change status of bill is successfully";
                }
                else
                {
                    result.Message = "Change status of bill is failed";
                    result.IsSuccess = false;
                }
            }
            catch(Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        [HttpPost("export-order")]
        [Authorize(Roles ="Admin,Seller")]
        public async Task<IActionResult> ExportOrder([FromForm]ExportDataDto data)
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
            catch(Exception e)
            {
                result.InternalError();
                return Ok(result);
            }
            
        }
        #region Privates
        private async Task<ExcelPackage> CreateOrderExportWorkSheet(ExcelPackage package, ExportDataDto data)
        {
            var ws = package.Workbook.Worksheets[0];
            var result = await _repository.GetBillDetailByTime(data);
            var listData = result.ToList();
            for(int i = 0; i< listData.Count; i++)
            {
                int r = i + 2;
                ws.Cells[r, 1].Value = listData[i].id;
                ws.Cells[r, 2].Value = listData[i].username;
                ws.Cells[r, 3].Value = listData[i].createdDate;
                ws.Cells[r, 4].Value = listData[i].updateDate;
                ws.Cells[r, 5].Value = listData[i].nameReceiver;
                ws.Cells[r, 6].Value = listData[i].numberPhone;
                ws.Cells[r, 7].Value = listData[i].address;
                ws.Cells[r, 8].Value = listData[i].status;
                ws.Cells[r, 9].Value = listData[i].totalCost;
            }
            return package;

        }

        #endregion Privates
    }
}
