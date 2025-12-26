using backend.Models.DTOs;

namespace backend.Interface
{
    public interface ISystemRepository
    {
        Task<SystemDTOs.SystemCreateResponse> CreateAsync(SystemDTOs.SystemCreateRequest request);
        Task<IEnumerable<SystemDTOs.SystemResponse>> GetAllAsync();
        Task<IEnumerable<SystemDTOs.GetSystemsUsersResponse>> GetSystemsByUserIdAsync(SystemDTOs.GetSystemsByUserIdRequest request);
        Task<IEnumerable<SystemDTOs.GetSystemsUsersResponse>> GetUsersBySystemIdAsync(SystemDTOs.GetUsersBySystemIdRequest request);
        Task<SystemDTOs.SystemCreateResponse> AssignSystemsToDevelopersAsync(SystemDTOs.SystemAssignRequest request);
    }
}
