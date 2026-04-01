
using backend.Models.DTOs;


namespace backend.Interface
{
    public interface IBranchRepository
    {
        Task<BranchDTOs.BranchCreateResponse> CreateAsync(BranchDTOs.BranchCreateRequest request);
        Task<IEnumerable<BranchDTOs.BranchResponse>> GetAllAsync();
    }
}
