using Microsoft.AspNetCore.Mvc;
using backend.Interface;
using backend.Models.DTOs;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/departments")]

    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentDTOs.DepartmentCreateRequest request)
        {
            var result = await _service.CreateDepartmentAsync(request);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _service.GetAllDepartmentsAsync();
            return Ok(departments);
        }
    }
}
