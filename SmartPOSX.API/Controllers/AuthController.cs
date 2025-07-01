using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPOSX.Core.DTOs.Employees;
using SmartPOSX.Core.Interfaces.Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartPOSX.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Core.DTOs.Employees.LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authService.Login(request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEmployeeDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authService.CreateEmployeeAccount(request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            return Ok();
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyAccount()
        {
            return Ok();
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword()
        {
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword()
        {
            return Ok();
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            return Ok();
        }
    }
}
