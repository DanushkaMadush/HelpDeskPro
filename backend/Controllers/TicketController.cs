using backend.Interface;
using backend.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] TicketDTOs.CreateTicketRequest request)
        {
            var response = await _ticketService.CreateTicketAsync(request);

            if (!string.IsNullOrEmpty(response.ErrorMessage))
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return Ok(tickets);
        }

        [HttpGet("{ticketId:int}")]
        public async Task<IActionResult> GetTicketById(int ticketId)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(new TicketDTOs.GetTicketByIdRequest 
            { 
                TicketId = ticketId 
            });

            if (ticket == null)
                return NotFound(new { Message = "Ticket not found." });

            return Ok(ticket);
        }

        [HttpGet("system/{systemId:int}")]
        public async Task<IActionResult> GetTicketsBySystemId(int systemId)
        {
            var tickets = await _ticketService.GetTicketsBySystemIdAsync(new TicketDTOs.GetAllTicketsBySystemIdRequest 
            { 
                SystemId = systemId 
            });

            return Ok(tickets);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTicketsByUserId(string userId)
        {
            var tickets = await _ticketService.GetTicketsByUserIdAsync(new TicketDTOs.GetAllTicketsByUserIdRequest 
                { 
                    UserId = userId 
                });

            return Ok(tickets);
        }
    }
}
