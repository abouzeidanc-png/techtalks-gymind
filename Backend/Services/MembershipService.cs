using GYMIND.API.Data;
using GYMIND.API.DTOs;
using GYMIND.API.Entities;
using GYMIND.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GYMIND.API.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly SupabaseDbContext _context;

        public MembershipService(SupabaseDbContext context)
        {
            _context = context;
        }

        public async Task<MembershipSummaryDto> IssueMembershipsAsync(Guid userId, CreateMembershipDto dto)
        {
            var membership = new Membership
            {
                MembershipID = Guid.NewGuid(),
                UserID = userId,
                GymID = dto.GymID,
                GymBranchID = dto.BranchID, // null if branch not specified
                IsMember = true,
                JoinedAt = DateTime.UtcNow,
                ExpiryDate = dto.ExpiryDate,
                Description = dto.Description
            };

            _context.Memberships.Add(membership);
            await _context.SaveChangesAsync();

            var gym = await _context.Gym.FindAsync(dto.GymID);

            return new MembershipSummaryDto
            {
                MembershipID = membership.MembershipID,
                GymName = gym?.Name ?? "Unknown Gym",
                IsActive = membership.IsMember,
                ExpiryDate = membership.ExpiryDate
            };
        }

        public async Task<List<MembershipSummaryDto>> GetUserMembershipsAsync(Guid userId)
        {
            
            var memberships = await _context.Memberships
                .Include(m => m.Gym)
                .Where(m => m.UserID == userId && m.RemovedAt == null)
                .ToListAsync();

            return memberships.Select(m => new MembershipSummaryDto
            {
                MembershipID = m.MembershipID,
                GymName = m.Gym.Name,
                IsActive = m.IsMember,
                ExpiryDate = m.ExpiryDate
            }).ToList();
        }
    }
}
