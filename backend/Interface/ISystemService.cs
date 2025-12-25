using backend.Models.DTOs;

namespace backend.Interface
{
    public interface ISystemService
    {
        Task<SystemDTOs.SystemCreateResponse> CreateSystemAsync(SystemDTOs.SystemCreateRequest request);
        Task<IEnumerable<SystemDTOs.SystemResponse>> GetAllSystemsAsync();
        Task<IEnumerable<SystemDTOs.GetSystemsUsersResponse>> GetSystemsByUserIdAsync(SystemDTOs.GetSystemsByUserIdRequest request);
        Task<IEnumerable<SystemDTOs.GetSystemsUsersResponse>> GetUsersBySystemIdAsync(SystemDTOs.GetUsersBySystemIdRequest request);
    }
}
