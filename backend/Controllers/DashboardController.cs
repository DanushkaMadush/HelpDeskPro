using backend.Interface;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController(IDashboardService service)
        {
            _service = service;
        }

        [HttpGet("kpi")]
        public async Task<IActionResult> GetKpi()
        {
            var result = await _service.GetKpiAsync();
            return Ok(result);
        }

        [HttpGet("tickets-by-status")]
        public async Task<IActionResult> GetTicketsByStatus()
        {
            var result = await _service.GetTicketsByStatusAsync();
            return Ok(result);
        }

        [HttpGet("tickets-over-time")]
        public async Task<IActionResult> GetTicketsOverTime()
        {
            var result = await _service.GetTicketsOverTimeAsync();
            return Ok(result);
        }

        [HttpGet("tickets-by-system")]
        public async Task<IActionResult> GetTicketsBySystem()
        {
            var result = await _service.GetTicketsBySystemAsync();
            return Ok(result);
        }

        [HttpGet("tickets-by-branch")]
        public async Task<IActionResult> GetTicketsByBranch()
        {
            var result = await _service.GetTicketsByBranchAsync();
            return Ok(result);
        }

        [HttpGet("tickets-by-priority")]
        public async Task<IActionResult> GetTicketsByPriority()
        {
            var result = await _service.GetTicketsByPriorityAsync();
            return Ok(result);
        }

        [HttpGet("avg-resolution-time")]
        public async Task<IActionResult> GetAvgResolutionTime()
        {
            var result = await _service.GetAvgResolutionTimeAsync();
            return Ok(result);
        }
    }
}
