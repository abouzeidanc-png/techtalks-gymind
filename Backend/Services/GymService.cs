using GYMIND.API.Data;
using GYMIND.API.Interfaces;
using Microsoft.EntityFrameworkCore;
// using AutoMapper;
using GYMIND.API.DTOs;
using GYMIND.API.Entities;

namespace GYMIND.API.Service
{
    public class GymService : IGymService
    {
        private readonly SupabaseDbContext _context;
        // private readonly IMapper _mapper;
        // private readonly Supabase.Client _supabase;

        public GymService(SupabaseDbContext context)
        {
            _context = context;
            // _mapper = mapper;
            // _supabase = supabase;
        }
        
        public async Task<IEnumerable<GymDto>> GetAllGymsAsync()
        {
            return await _context.Gym
                .Select(g => new GymDto
                {
                    GymId = g.GymId,
                    Name = g.Name,
                    Description = _context.GymBranches
                        .Where(gb => gb.GymID == g.GymId)
                        .Select(gb => gb.ServiceDescription)
                        .FirstOrDefault()!, // Get description from the first branch
                    IsApproved = g.IsApproved,
                    CreatedAt = g.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<GymDto?> GetGymByIdAsync(Guid id)
        {
            var gym = await _context.Gym.FindAsync(id);
            if (gym == null || !gym.IsApproved)
                return null;

            return new GymDto
            {
                GymId = gym.GymId,
                Name = gym.Name,
                Description = _context.GymBranches.Where(gb => gb.GymID == gym.GymId).Select(gb => gb.ServiceDescription).FirstOrDefault()!,
                IsApproved = gym.IsApproved,
                CreatedAt = gym.CreatedAt
            };
        }

        public async Task<GymDto?> GetGymByNameAsync(string name)
        {
            var gym = await _context.Gym
                .Where(g => g.Name == name)
                .FirstOrDefaultAsync();

            if(gym == null) return null;

            return new GymDto
            {
                GymId = gym.GymId,
                Name = gym.Name,
                Description = _context.GymBranches.Where(gb => gb.GymID == gym.GymId).Select(gb => gb.ServiceDescription).FirstOrDefault()!,
                IsApproved = gym.IsApproved,
                CreatedAt = gym.CreatedAt
            };
        }

        public async Task<IEnumerable<GymDto?>> GetGymsByAddressAsync(string address)
        {
            var gyms = await _context.GymBranches  
                .Where(gb => gb.Location.City.Contains(address) && gb.Gym.IsApproved)
                .Select(gb => new GymDto
                {
                    GymId = gb.GymID,
                    Name = gb.Gym.Name,
                    Description = gb.ServiceDescription ?? string.Empty,
                    IsApproved = gb.Gym.IsApproved,
                    CreatedAt = gb.Gym.CreatedAt
                })
                .ToListAsync();

            return gyms;
        }

        public async Task<IEnumerable<GymDto>> GetGymsByApprovalStatusAsync(bool isApproved)
        {
            return await _context.Gym
                .Where(g => g.IsApproved == isApproved)
                .Select(g => new GymDto
                {
                    GymId = g.GymId,
                    Name = g.Name,
                    Description = _context.GymBranches
                        .Where(gb => gb.GymID == g.GymId)
                        .Select(gb => gb.ServiceDescription)
                        .FirstOrDefault()!,
                    IsApproved = g.IsApproved,
                    CreatedAt = g.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<GymDto> CreateGymAsync(GymDto dto)
        {
            var gym = new Gym
            {
                GymId = Guid.NewGuid(),
                Name = dto.Name,
                IsApproved = dto.IsApproved,
                CreatedAt = DateTime.UtcNow,
            };

            await _context.Gym.AddAsync(gym);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message); // returning message to be chaanged later for better error handling and security
                throw;
            }

            return new GymDto
            {
                GymId = gym.GymId,
                Name = gym.Name,
                IsApproved = gym.IsApproved,
                CreatedAt = gym.CreatedAt,
            };
        }

        public async Task<bool> UpdateGymAsync(Guid id, GymDto dto)
        {
            var gym = await _context.Gym.FindAsync(id);

            if (gym == null) return false;

            if (!string.IsNullOrWhiteSpace(dto.Name)) 
                gym.Name = dto.Name;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ApproveGymAsync(Guid id)
        {
            return true;
        }

        public async Task<GymBranchDto> CreateBranchAsync(Guid gymId, GymBranchDto dto)
        {
            var gymExists = await _context.Gym.AnyAsync(g => g.GymId == gymId);

            if (!gymExists)
                throw new Exception("Gym Not Found");
            
            var branch = new GymBranch
            {
                GymBranchID = Guid.NewGuid(),
                GymID = gymId,
                LocationID = dto.LocationID,
                Name = dto.Name,
                OperatingHours = dto.OperatingHours,
                ServiceDescription = dto.ServiceDescription,
                CoverImageUrl = dto.CoverImageUrl,
                IsActive = true
            };

            await _context.GymBranches.AddAsync(branch);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw;
            }

            dto.GymBranchID = branch.GymBranchID;

            return dto;
        }

        public async Task<bool> UpdateBranchAsync(Guid branchId, GymBranchDto dto)
        {
            var branch = await _context.GymBranches.FindAsync(branchId);

            if (branch == null)
                return false;
            
            branch.Name = dto.Name;
            branch.OperatingHours = dto.OperatingHours;
            branch.ServiceDescription = dto.ServiceDescription;
            branch.CoverImageUrl = dto.CoverImageUrl;
            
            await _context.SaveChangesAsync();
            return true;
        }
    }
}