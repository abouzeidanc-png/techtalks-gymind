using GYMIND.API.DTOs;

namespace GYMIND.API.Interfaces
{
    public interface IGymService
    {
        Task<IEnumerable<GymDto>> GetAllGymsAsync();
        Task<GymDto?> GetGymByIdAsync(Guid id);
        Task<GymDto?> GetGymByNameAsync(string name);
        Task<IEnumerable<GymDto?>> GetGymsByAddressAsync(string address);
        Task<GymDto> CreateGymAsync(GymDto dto);
        Task<bool> UpdateGymAsync(Guid id, GymDto dto);
        Task<bool> DeleteGymAsync(Guid id);
        Task<bool> ApproveGymAsync(Guid id);
        Task<IEnumerable<GymDto>> GetGymsByApprovalStatusAsync(bool isApproved);

        Task<IEnumerable<GymBranchDto>> GetBranchesByGymIdAsync(Guid gymId);
        Task<GymBranchDto?> GetBranchByIdAsync(Guid branchId);
        Task<GymBranchDto?> GetBranchByNameAsync(Guid gymId, string name);
        Task<IEnumerable<GymBranchDto?>> GetBranchByLocationAsync(Guid gymId, Guid locationId);
        Task<GymBranchDto> CreateBranchAsync(Guid gymId, GymBranchDto dto);
        Task<bool> UpdateBranchAsync(Guid branchId, GymBranchDto dto);
        Task<bool> DeleteBranchAsync(Guid branchId);
        Task<bool> DeactivateBranchAsync(Guid branchId);
    }
}