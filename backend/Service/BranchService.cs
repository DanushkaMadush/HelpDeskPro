using backend.Interface;
using backend.Models.DTOs;


namespace backend.Service
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _repository;

        public BranchService(IBranchRepository repository)
        {
            _repository = repository;
        }

        public Task<BranchDTOs.BranchCreateResponse> CreateBranchAsync(BranchDTOs.BranchCreateRequest request)
        {
            return _repository.CreateAsync(request);
        }

        public Task<IEnumerable<BranchDTOs.BranchResponse>> GetAllBranchesAsync()
        {
            return _repository.GetAllAsync();
        }

    }
}
