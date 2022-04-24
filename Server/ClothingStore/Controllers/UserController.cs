using ClothingStore.Data.Repositories;
using ClothingStore.Entities;
using ClothingStore.Entities.Dtos;
using ClothingStore.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly ITokenService _tokenService;
        public UserController(IUserRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok("OK");
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto user)
        {
            var result = new ApiResult();
            try
            {
                var userExist = await _repository.GetUserByUsername(user.Username);
                if (userExist == null)
                {
                    result.IsSuccess = false;
                    result.Message = "Username or password is invalid";
                }
                else
                {
                    if (BCrypt.Net.BCrypt.Verify(user.Password, userExist.Password))
                    {
                        result.IsSuccess = true;
                        result.Message = "Login is successfully";
                        var refresh = _tokenService.CreateTokenRefresh(userExist);
                        await _repository.AddRefreshToken(new RefreshToken { refreshToken = refresh, CreatedByIp = GetIPAddress(), user_id = userExist.Id, CreatedDate = DateTime.Now });
                        result.Data = new UserDto()
                        {
                            Username = userExist.Username,
                            Fullname = userExist.FullName,
                            Token = _tokenService.CreateToken(userExist),
                            RefreshToken = refresh
                        };
                         
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Username or password is invalid";
                    }
                }
            }
            catch (Exception e)
            {
                result.InternalError();
                result.Message = e.Message;
            }
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> PostRefreshToken([FromForm] RefreshTokenDto value) 
        {
            var result = new ApiResult();
            try
            {
                if (await _repository.ValidateRefreshToken(value.RefreshToken, GetIPAddress()))
                {
                    var validate = _tokenService.GetPayloadRefreshToken(value.RefreshToken);
                    var exist = await _repository.GetUserByUsername(validate.Issuer);
                    result.Data = _tokenService.CreateToken(exist);
                    result.IsSuccess = true;
                }
                else
                {
                    result.IsSuccess = false;
                }
            }
            catch(Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromForm] RefreshTokenDto value)
        {
            var result = new ApiResult();
            try
            {
                var rftk = new RefreshToken { CreatedByIp = GetIPAddress(), refreshToken = value.RefreshToken };
                if(await _repository.RemoveRefreshToken(rftk))
                {
                    result.IsSuccess = true;
                }
                else
                {
                    result.IsSuccess = false;
                }
            }
            catch(Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        [Authorize(Roles = "Seller,Admin")]
        [HttpGet("testuseradmin")]
        public IActionResult TestAll()
        {
            return Ok("OK Seller Admin");
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto user)
        {
            var result = new ApiResult();
            try
            {
                if (await _repository.GetUserByUsername(user.Username) != null)
                {
                    result.IsSuccess = false;
                    result.Message = "Username has exist in database";
                    result.HttpStatusCode = 400;
                }
                else if (await _repository.register(user))
                {
                    result.IsSuccess = true;
                    result.Message = "Register account is successfully";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Information invalid";
                    result.HttpStatusCode = 400;
                }

            }
            catch (Exception e)
            {
                result.InternalError();
                result.Message = e.Message;
            }
            return Ok(result);
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordDto value)
        {
            var result = new ApiResult();

            try
            {
                value.username = User.Identity.Name;
                var user = await _repository.GetUserByUsername(value.username);
                if(BCrypt.Net.BCrypt.Verify(value.passwordOld, user.Password) == false)
                {
                    result.IsSuccess = false;
                    result.Message = "Old password is incorrect";
                    return Ok(result);
                }

                if(await _repository.ChangePassword(value))
                {
                    result.IsSuccess = true;
                    result.Message = "Change password is successfully";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Change password is failed";
                }
            }
            catch(Exception e)
            {
                result.InternalError();
            }

            return Ok(result);
        }

        [HttpGet("get-users")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetUsers()
        {
            var result = new ApiResult();
            try
            {
                result.Data = await _repository.GetUsers();
                result.Message = "Get users is successfully";
            }
            catch(Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        [HttpGet("roles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRoles()
        {
            var result = new ApiResult();
            try
            {
                result.Data = await _repository.GetRoles();
                result.Message = "Get roles is successfully";
            }
            catch(Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        [HttpPost("change-roles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRoles([FromForm] ChangeRoleDto value)
        {
            var result = new ApiResult();
            try
            {
                if(await _repository.ChangeRole(value))
                {
                    result.Message = "Get roles is successfully";
                }
                else
                {
                    result.Message = "Get roles is failed";
                    result.IsSuccess = true;
                }
            }
            catch (Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        private string GetIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
