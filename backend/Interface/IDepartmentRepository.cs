using backend.Models.DTOs;

namespace backend.Interface
{
    public interface IDepartmentRepository
    {
        Task<DepartmentDTOs.DepartmentCreateResponse> CreateAsync(DepartmentDTOs.DepartmentCreateRequest request);
        Task<IEnumerable<DepartmentDTOs.DepartmentResponse>> GetAllAsync();
    }
}
