using backend.Interface;
using backend.Models.DTOs;

namespace backend.Service
{
    public class SystemService : ISystemService
    {
        private readonly ISystemRepository _repository;

        public SystemService(ISystemRepository repository)
        {
            _repository = repository;
        }

        public Task<SystemDTOs.SystemCreateResponse> CreateSystemAsync(SystemDTOs.SystemCreateRequest request)
        {
            return _repository.CreateAsync(request);
        }

        public Task<IEnumerable<SystemDTOs.SystemResponse>> GetAllSystemsAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<IEnumerable<SystemDTOs.GetSystemsUsersResponse>>GetSystemsByUserIdAsync(SystemDTOs.GetSystemsByUserIdRequest request)
        {
            return _repository.GetSystemsByUserIdAsync(request);
        }

        public Task<IEnumerable<SystemDTOs.GetSystemsUsersResponse>>GetUsersBySystemIdAsync(SystemDTOs.GetUsersBySystemIdRequest request)
        {
            return _repository.GetUsersBySystemIdAsync(request);
        }

        public Task<SystemDTOs.SystemCreateResponse> AssignSystemsToDevelopersAsync(SystemDTOs.SystemAssignRequest request)
        {
            return _repository.AssignSystemsToDevelopersAsync(request);
        }
    }
}
