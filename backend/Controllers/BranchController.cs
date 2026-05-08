using Microsoft.AspNetCore.Mvc;
using backend.Models.DTOs;
using backend.Interface;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/branches")]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _service;

        public BranchController(IBranchService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BranchDTOs.BranchCreateRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest(new { Message = "Invalid request body." });

                var result = await _service.CreateBranchAsync(request);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An error occurred while creating branch.",
                    Error = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var branches = await _service.GetAllBranchesAsync();
                return Ok(branches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Failed to retrieve branches.",
                    Error = ex.Message
                });
            }
        }
    }
}