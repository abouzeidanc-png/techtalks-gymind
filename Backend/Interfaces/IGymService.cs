using GYMIND.API.DTOs;

namespace GYMIND.API.Interfaces
{
    public interface IGymService // tii many tasks, not needed, implementation to be added in gymservice
    {
        Task<IEnumerable<GymDto>> GetAllGymsAsync(); // get all gyms, including unapproved ones for admin view
        Task<GymDto?> GetGymByIdAsync(Guid id); // okay
        Task<GymDto?> GetGymByNameAsync(string name); // okay
        Task<IEnumerable<GymDto?>> GetGymsByAddressAsync(string address); // location -based search, may return multiple gyms
        Task<IEnumerable<GymDto>> GetGymsByApprovalStatusAsync(bool isApproved);

        // editgymprofile task


        
        // for adminactions
        Task<GymDto> CreateGymAsync(GymDto dto); // create a new gym, initially unapproved  admin
        Task<bool> UpdateGymAsync(Guid id, GymDto dto); // update gym details, only if not approved yet  admin
        Task<bool> ApproveGymAsync(Guid id);// admin 
        Task<GymBranchDto> CreateBranchAsync(Guid gymId, GymBranchDto dto); // admin
        Task<bool> UpdateBranchAsync(Guid branchId, GymBranchDto dto); // admin -
        
    }
}