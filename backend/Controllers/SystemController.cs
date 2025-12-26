using backend.Interface;
using backend.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/systems")]

    public class SystemController : ControllerBase
    {
        private readonly ISystemService _service;

        public SystemController(ISystemService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(SystemDTOs.SystemCreateRequest request)
        {
            var result = await _service.CreateSystemAsync(request);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var branches = await _service.GetAllSystemsAsync();
            return Ok(branches);
        }

        [HttpGet("by-user")]
        public async Task<IActionResult> GetSystemsByUserIdAsync([FromQuery] SystemDTOs.GetSystemsByUserIdRequest request)
        {
            var branches = await _service.GetSystemsByUserIdAsync(request);
            return Ok(branches);
        }

        [HttpGet("users-by-system")]
        public async Task<IActionResult> GetUsersBySystemIdAsync([FromQuery] SystemDTOs.GetUsersBySystemIdRequest request)
        {
            var branches = await _service.GetUsersBySystemIdAsync(request);
            return Ok(branches);
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignSystemsToDevelopersAsync(SystemDTOs.SystemAssignRequest request)
        {
            var result = await _service.AssignSystemsToDevelopersAsync(request);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
                return BadRequest(result);

            return Ok(result);
        }
    }
}
