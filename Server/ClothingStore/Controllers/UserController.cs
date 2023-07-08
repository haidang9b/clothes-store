using ClothingStore.Data.Repositories;
using ClothingStore.Entities;
using ClothingStore.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClothingStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public UserController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto user)
        {
            var result = new ApiResult();
            try
            {
                var userExist = await _userRepository.GetUserByUsername(user.Username);
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
                        result.Data = new UserDto()
                        {
                            Username = userExist.Username,
                            Fullname = userExist.FullName,
                            Token = _tokenService.CreateToken(userExist)
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


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto user)
        {
            var result = new ApiResult();
            try
            {
                if (await _userRepository.GetUserByUsername(user.Username) != null)
                {
                    result.IsSuccess = false;
                    result.Message = "Username has exist in database";
                    result.HttpStatusCode = 400;
                }
                else if (await _userRepository.register(user))
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
                value.Username = User.Identity.Name;
                var user = await _userRepository.GetUserByUsername(value.Username);
                if (BCrypt.Net.BCrypt.Verify(value.PasswordOld, user.Password) == false)
                {
                    result.IsSuccess = false;
                    result.Message = "Old password is incorrect";
                    return Ok(result);
                }

                if (await _userRepository.ChangePassword(value))
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
            catch (Exception e)
            {
                result.InternalError();
            }

            return Ok(result);
        }

        [HttpGet("get-users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            var result = new ApiResult();
            try
            {
                result.Data = await _userRepository.GetUsers();
                result.Message = "Get users is successfully";
            }
            catch (Exception e)
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
                result.Data = await _userRepository.GetRoles();
                result.Message = "Get roles is successfully";
            }
            catch (Exception e)
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
                if (await _userRepository.ChangeRole(value))
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
    }
}
