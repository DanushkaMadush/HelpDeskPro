using backend.Models.DTOs;

namespace backend.Interface
{
    public interface IBranchService
    {
        Task<BranchDTOs.BranchCreateResponse> CreateBranchAsync(BranchDTOs.BranchCreateRequest request);
        Task<IEnumerable<BranchDTOs.BranchResponse>> GetAllBranchesAsync();
    }
}
