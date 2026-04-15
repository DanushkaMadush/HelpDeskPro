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
            try
            {
                var result = await _service.GetKpiAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error retrieving KPI data.",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("tickets-by-status")]
        public async Task<IActionResult> GetTicketsByStatus()
        {
            try
            {
                var result = await _service.GetTicketsByStatusAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error retrieving tickets by status.",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("tickets-over-time")]
        public async Task<IActionResult> GetTicketsOverTime()
        {
            try
            {
                var result = await _service.GetTicketsOverTimeAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error retrieving tickets over time.",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("tickets-by-system")]
        public async Task<IActionResult> GetTicketsBySystem()
        {
            try
            {
                var result = await _service.GetTicketsBySystemAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error retrieving tickets by system.",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("tickets-by-branch")]
        public async Task<IActionResult> GetTicketsByBranch()
        {
            try
            {
                var result = await _service.GetTicketsByBranchAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error retrieving tickets by branch.",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("tickets-by-priority")]
        public async Task<IActionResult> GetTicketsByPriority()
        {
            try
            {
                var result = await _service.GetTicketsByPriorityAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error retrieving tickets by priority.",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("avg-resolution-time")]
        public async Task<IActionResult> GetAvgResolutionTime()
        {
            try
            {
                var result = await _service.GetAvgResolutionTimeAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error retrieving average resolution time.",
                    Error = ex.Message
                });
            }
        }
    }
}