using backend.Models.DTOs;

namespace backend.Interface
{
    public interface IDepartmentService
    {
        Task<DepartmentDTOs.DepartmentCreateResponse> CreateDepartmentAsync(DepartmentDTOs.DepartmentCreateRequest request);
        Task<IEnumerable<DepartmentDTOs.DepartmentResponse>> GetAllDepartmentsAsync();
    }
}
