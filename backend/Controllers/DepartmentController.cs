using Microsoft.AspNetCore.Mvc;
using backend.Interface;
using backend.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Create([FromBody] DepartmentDTOs.DepartmentCreateRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest(new { Message = "Invalid request body." });

                var result = await _service.CreateDepartmentAsync(request);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An error occurred while creating department.",
                    Error = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var departments = await _service.GetAllDepartmentsAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Failed to retrieve departments.",
                    Error = ex.Message
                });
            }
        }
    }
}