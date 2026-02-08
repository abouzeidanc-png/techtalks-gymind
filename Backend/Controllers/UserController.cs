using GYMIND.API.DTOs;
using GYMIND.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace GYMIND.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
            => Ok(await _userService.GetAllUsersAsync());

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            try
            {
                var user = await _userService.CreateUserAsync(dto);
                return CreatedAtAction(nameof(GetUser), new { id = user.UserID }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user for email {Email}", dto?.Email);
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var token = await _userService.LoginAsync(dto);
            if (token == null) return Unauthorized("Invalid email or passowrd.");
            return Ok(new { token });

        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto dto)
        {
            var success = await _userService.UpdateUserAsync(id,
                                                             dto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpPut("{id:guid}/deactivate")]
        public async Task<IActionResult> DeactivateUser(Guid id)
        {
            var success = await _userService.DeactivateUserAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }


        [Authorize]
        [HttpPatch("edit-profile")]
        public async Task<IActionResult> EditProfile([FromForm] EditProfileDto dto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            try
            {
                await _userService.UpdateProfileAsync(userId, dto);
                return Ok("Profile updated!");
            }
            catch (Exception ex)
            {
                // This will now return the specific "Storage Error" or "Database Error" message
                return BadRequest(ex.Message);
            }
        }
    }
}
