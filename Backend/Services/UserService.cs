using GYMIND.API.DTOs;
using GYMIND.API.Interfaces;
using GYMIND.API.Entities;
using Microsoft.EntityFrameworkCore;
using GYMIND.API.Data;

using BCrypt.Net;

namespace GYMIND.API.GYMIND.Application.Service
{

    public class UserService : IUserService
    {
        private readonly SupabaseDbContext _context;
        private readonly ITokenService _tokenService;

        public UserService(SupabaseDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<string?> LoginAsync(LoginRequestDto dto)
        {
            var user = await _context.Users
                .Include(u => u.UserRole)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.IsActive);

            if (user == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) return null;

            return _tokenService.CreateToken(user, user.UserRole);
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Where(u => u.IsActive)
                .Select(u => new UserResponseDto
                {
                    UserID = u.UserID,
                    FullName = u.FullName,
                    Email = u.Email,
                    Phone = u.Phone,
                    CreatedAt = u.CreatedAt,
                    Roles = u.UserRole.Select(ur => ur.RoleID).ToList()

                })
                .ToListAsync();
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users
                .Include(u => u.UserRole)        // load the join table
                    .ThenInclude(ur => ur.Role)  // load the actual Role entity
                .FirstOrDefaultAsync(u => u.UserID == id && u.IsActive);

            if (user == null) return null;

            return new UserResponseDto
            {
                UserID = user.UserID,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                CreatedAt = user.CreatedAt,
                Roles = user.UserRole.Select(ur => ur.RoleID).ToList()  
            };
        }


        public async Task<UserResponseDto> CreateUserAsync(CreateUserDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                throw new Exception("Email already exists");

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
                
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            // attach role
            user.UserRole.Add(new UserRole
            {
                RoleID = 2 // default to member role
            });

            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw;
            }

            return new UserResponseDto
            {
                UserID = user.UserID,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                CreatedAt = user.CreatedAt,
                Roles = user.UserRole.Select(ur => ur.RoleID).ToList(),
            };
        }

        public async Task<bool> UpdateUserAsync(Guid id, UpdateUserDto dto)
        {
            var user = await _context.Users
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.UserID == id);

            if (user == null || !user.IsActive)
                return false;

            if (!string.IsNullOrEmpty(dto.FullName))
                user.FullName = dto.FullName;

            if (!string.IsNullOrEmpty(dto.Phone))
                user.Phone = dto.Phone;

            if (dto.RoleIDs != null)
            {
                _context.UserRole.RemoveRange(user.UserRole);

                user.UserRole = dto.RoleIDs.Select(roleId => new UserRole
                {
                    RoleID = roleId,
                    UserID = user.UserID
                }).ToList();
            }
                

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeactivateUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
