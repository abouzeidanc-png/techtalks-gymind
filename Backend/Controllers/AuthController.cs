using GYMIND.API.Interfaces;
using GYMIND.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GYMIND.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
                return BadRequest("Email and password are required.");

            var token = await _userService.LoginAsync(dto);

            if (token == null)
                return Unauthorized("Invalid credentials.");

            return Ok(new
            {
                token
            });
        }
    }
}
