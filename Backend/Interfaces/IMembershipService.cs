using GYMIND.API.DTOs;

namespace GYMIND.API.Interfaces
{
    public interface IMembershipService
    {
        Task<MembershipSummaryDto> IssueMembershipsAsync(Guid userId, CreateMembershipDto dto);
        Task<List<MembershipSummaryDto>> GetUserMembershipsAsync(Guid userId);
    }
}