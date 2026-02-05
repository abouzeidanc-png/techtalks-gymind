using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GYMIND.API.Entities;
using GYMIND.API.GYMIND.API.DTOs;
using BCrypt.Net;
using System;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly SupabaseDbContext _context;

    public UsersController(SupabaseDbContext context)
    {
        _context = context;
    }


    //Get all active users       
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users
            .Where(u => u.IsActive)
            .Select(u => new
            {
                u.UserID,
                u.FullName,
                u.Email,
                u.Phone,
                u.CreatedAt,
                u.RoleID
            })
            .ToListAsync();

        return Ok(users);
    }

    //Get user by ID
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _context.Users
            .Where(u => u.UserID == id && u.IsActive)
            .Select(u => new
            {
                u.UserID,
                u.FullName,
                u.Email,
                u.Phone,
                u.CreatedAt,
                u.RoleID
            })
            .FirstOrDefaultAsync();

        if (user == null)
            return NotFound();

        return Ok(user);
    }




    //Create new user
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return BadRequest("Email already exists.");

        var user = new User
        {
            UserID = Guid.NewGuid(),
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            DateOfBirth = dto.DateOfBirth.HasValue
            ? DateTime.SpecifyKind(dto.DateOfBirth.Value, DateTimeKind.Utc) : null,

            Location = dto.Location,
            RoleID = 2, // => Default role as 'Member', Changed by admins through sqlqueries
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.UserID }, new
        {
            user.UserID,
            user.FullName,
            user.Email,
            user.Phone,
            user.DateOfBirth,
            user.Location,
            user.RoleID,
            user.CreatedAt
        });
    }

    //Update user information (except email and password)
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto dto)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null || !user.IsActive)
            return NotFound();

        if (!string.IsNullOrEmpty(dto.FullName))
            user.FullName = dto.FullName;

        if (!string.IsNullOrEmpty(dto.Phone))
            user.Phone = dto.Phone;

        if (dto.RoleID.HasValue)
            user.RoleID = dto.RoleID.Value;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Deactivate user 
    [HttpPut("{id:guid}/deactivate")]
    public async Task<IActionResult> DeactivateUser(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return NotFound();

        user.IsActive = false;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    
    
}


