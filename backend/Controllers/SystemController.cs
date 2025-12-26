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
            var systems = await _service.GetAllSystemsAsync();
            return Ok(systems);
        }

        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetSystemsByUserIdAsync(string userId)
        {
            var systems = await _service.GetSystemsByUserIdAsync(new SystemDTOs.GetSystemsByUserIdRequest
            {
                UserId = userId
            });
            return Ok(systems);
        }

        [HttpGet("users-by-system/{systemId:int}")]
        public async Task<IActionResult> GetUsersBySystemIdAsync(int systemId)
        {
            var users = await _service.GetUsersBySystemIdAsync(new SystemDTOs.GetUsersBySystemIdRequest
            {
                SystemId = systemId
            });
            return Ok(users);
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
