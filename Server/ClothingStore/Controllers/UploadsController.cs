using ClothingStore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UploadsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("File upload api is running...");
        }

        [HttpPost("upload")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var result = new ApiResult();
            if(file.Length == 0)
            {
                result.IsSuccess = false;
                result.Message = "Not exist file";
                result.HttpStatusCode = 400;
                return Ok(result);
            }

            var response = await WriteFile(file);
            var fileTypesAccept = new string[]{ ".jpg", ".png", ".svg", ".jpeg", ".webp" };
            if (!fileTypesAccept.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                result.IsSuccess = false;
                result.Message = "Server can't accept file extension";
                result.HttpStatusCode = 400;
                return Ok(result);
            }
            if (response.isSuccess)
            {
                result.IsSuccess = true;
                result.Message = "Upload file is successfully";
                result.Data = response;
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Have error when upload your file";
            }
            return Ok(result);
        }
        #region Privates
        [NonAction] 
        private async Task<ResponseWriteFile> WriteFile(IFormFile file)
        {
            var result = new ResponseWriteFile();
            try
            {
                var extension = Path.GetExtension(file.FileName);
                var fileName = DateTime.Now.Ticks + extension;

                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Images");
                if (Directory.Exists(pathBuilt) == false)
                {
                    Directory.CreateDirectory(pathBuilt);
                }
                var url = Path.Combine(_configuration.GetSection("Backend:Url").Value, "Uploads", "Images", fileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads","Images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                result.isSuccess = true;
                result.pathFile = url;
            }
            catch(Exception e)
            {
                result.isSuccess = false;
            }
            return result;
        }
        #endregion
    }
    class ResponseWriteFile
    {
        public bool isSuccess { get; set; }
        public string pathFile { get; set; }
    }
}
