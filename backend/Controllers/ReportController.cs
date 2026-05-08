using Microsoft.AspNetCore.Mvc;
using backend.Interface;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet("monthly-tickets")]
        public async Task<IActionResult> GetMonthlyTickets(int year)
        {
            try
            {
                var result = await _service.GetMonthlyTicketsAsync(year);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("branch-wise-tickets")]
        public async Task<IActionResult> GetBranchWiseTickets()
        {
            try
            {
                var result = await _service.GetBranchWiseTicketsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("pending-systems")]
        public async Task<IActionResult> GetPendingSystems()
        {
            try
            {
                var result = await _service.GetPendingSystemsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}