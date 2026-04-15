using backend.Interface;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsService _service;

        public ReportsController(IReportsService service)
        {
            _service = service;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var result = await _service.GetTicketSummaryAsync();
            return Ok(result);
        }

        [HttpGet("trends")]
        public async Task<IActionResult> GetTrends(
            DateTime? startDate,
            DateTime? endDate,
            int? systemId,
            int? branchId)
        {
            var result = await _service.GetTicketTrendsAsync(startDate, endDate, systemId, branchId);
            return Ok(result);
        }

        [HttpGet("resolution")]
        public async Task<IActionResult> GetResolution()
        {
            var result = await _service.GetResolutionPerformanceAsync();
            return Ok(result);
        }
    }
}
