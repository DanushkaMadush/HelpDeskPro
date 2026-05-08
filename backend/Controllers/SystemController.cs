using backend.Interface;
using backend.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Create([FromBody] SystemDTOs.SystemCreateRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest(new { Message = "Invalid request body." });

                var result = await _service.CreateSystemAsync(request);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An error occurred while creating system.",
                    Error = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var systems = await _service.GetAllSystemsAsync();
                return Ok(systems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Failed to retrieve systems.",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetSystemsByUserIdAsync(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    return BadRequest(new { Message = "UserId is required." });

                var systems = await _service.GetSystemsByUserIdAsync(
                    new SystemDTOs.GetSystemsByUserIdRequest
                    {
                        UserId = userId
                    });

                return Ok(systems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error retrieving systems by user.",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("users-by-system/{systemId:int}")]
        public async Task<IActionResult> GetUsersBySystemIdAsync(int systemId)
        {
            try
            {
                if (systemId <= 0)
                    return BadRequest(new { Message = "Invalid systemId." });

                var users = await _service.GetUsersBySystemIdAsync(
                    new SystemDTOs.GetUsersBySystemIdRequest
                    {
                        SystemId = systemId
                    });

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error retrieving users by system.",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignSystemsToDevelopersAsync([FromBody] SystemDTOs.SystemAssignRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest(new { Message = "Invalid request body." });

                var result = await _service.AssignSystemsToDevelopersAsync(request);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error assigning systems to developers.",
                    Error = ex.Message
                });
            }
        }
    }
}