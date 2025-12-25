using backend.Interface;
using backend.Models.DTOs;

namespace backend.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public Task<DepartmentDTOs.DepartmentCreateResponse> CreateDepartmentAsync(DepartmentDTOs.DepartmentCreateRequest request)
        {
            return _repository.CreateAsync(request);
        }

        public Task<IEnumerable<DepartmentDTOs.DepartmentResponse>> GetAllDepartmentsAsync()
        {
            return _repository.GetAllAsync();
        }
    }
}
