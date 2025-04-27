using MenagMeWebApi.Application.Interfaces.ServiceInterfaces;
using MenagMeWebApi.Application.Objects.UserDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenagMeWebApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserCreateDTO>> Register(UserCreateDTO dto)
        {
            try
            {   
                var result = await _userService.CreateUser(dto);
                return Ok(new { message = "User created successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("log-in")]
        public async Task<ActionResult<UserDataDTO>> LogIn(string username, string password)
        {

            try
            {
                var result = await _userService.LogIn(username, password);
                if (result != null)
                {
                    var token = _tokenService.GenerateToken(result);
                    var refreshToken = _tokenService.GenerateRefreshToken();
                    return Ok(new { result, token, refreshToken });
                }
                else
                {
                    return BadRequest(new { message = "Invalid username or password" });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [Authorize]
        [HttpGet("get-user-list")]
        public async Task<ActionResult<List<UserDataDTO>>> GetUserList()
        {

            try
            {
                var result = await _userService.GetAllUsers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }


        [Authorize]
        [HttpGet("get-user")]
        public async Task<ActionResult<UserDataDTO>> GetUser(string id)
        {

            try
            {
                var result = await _userService.GetUser(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        

        [Authorize]
        [HttpGet("get-refresh-token")]
        public async Task<ActionResult<string>> GetRefreshToken(string id)
        {

            try
            {
                var result = _tokenService.GenerateRefreshToken();

                var user = await _userService.GetUser(id);
                
                var token = _tokenService.GenerateToken(user!);
                return Ok(new { result, token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
