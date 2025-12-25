using Microsoft.AspNetCore.Mvc;
using backend.Models.DTOs;
using backend.Interface;


namespace backend.Controllers
{
    [ApiController]
    [Route("api/branches")]

    public class BranchController : ControllerBase
    {
        private readonly IBranchService _service;

        public BranchController(IBranchService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
        BranchDTOs.BranchCreateRequest request)
        {
            var result = await _service.CreateBranchAsync(request);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var branches = await _service.GetAllBranchesAsync();
            return Ok(branches);
        }
    }
}
