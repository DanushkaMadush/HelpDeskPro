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
        private readonly IWebHostEnvironment _environment;

        public TicketController(ITicketService ticketService, IWebHostEnvironment environment)
        {
            _ticketService = ticketService;
            _environment = environment;
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

        [HttpPatch("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] TicketDTOs.UpdateTicketStatusRequest request)
        {
            var response = await _ticketService.UpdateTicketStatusAsync(request);

            if (!string.IsNullOrEmpty(response.ErrorMessage))
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDetails([FromBody] TicketDTOs.UpdateTicketDetailsRequest request)
        {
            var response = await _ticketService.UpdateTicketDetailsAsync(request);

            if (!string.IsNullOrEmpty(response.ErrorMessage))
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("{ticketId:int}")]
        public async Task<IActionResult> SoftDelete(
            int ticketId,
            [FromQuery] string updatedBy)
        {
            var response = await _ticketService.SoftDeleteTicketAsync(
                new TicketDTOs.SoftDeleteTicketRequest
                {
                    TicketId = ticketId,
                    UpdatedBy = updatedBy
                });

            if (!string.IsNullOrEmpty(response.ErrorMessage))
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("{ticketId:int}/media")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload(int ticketId, [FromForm] TicketDTOs.UploadMediaRequest request)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest("No file uploaded.");

            request.TicketId = ticketId;

            var uploadsRoot = Path.Combine(
                _environment.ContentRootPath,
                "uploads",
                "tickets",
                ticketId.ToString());

            if (!Directory.Exists(uploadsRoot))
                Directory.CreateDirectory(uploadsRoot);

            var storedFileName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";
            var filePath = Path.Combine(uploadsRoot, storedFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            var relativePath = Path.GetRelativePath(_environment.ContentRootPath, filePath);

            var mimeType = request.File.ContentType;
            var fileSize = request.File.Length;
            int? durationSeconds = null;

            var response = await _ticketService.UploadMediaAsync(
                request,
                storedFileName,
                relativePath,
                fileSize,
                mimeType,
                durationSeconds);

            if (!string.IsNullOrEmpty(response.ErrorMessage))
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("{ticketId:int}/media")]
        public async Task<IActionResult> GetByTicketId(int ticketId)
        {
            var media = await _ticketService.GetMediaByTicketIdAsync(ticketId);
            return Ok(media);
        }

        [HttpDelete("{ticketId:int}/media/{ticketMediaId:int}")]
        public async Task<IActionResult> SoftDelete(
            int ticketId,
            int ticketMediaId,
            [FromQuery] string updatedBy)
        {
            var response = await _ticketService.DeleteMediaAsync(
                new TicketDTOs.DeleteMediaRequest
                {
                    TicketMediaId = ticketMediaId,
                    UpdatedBy = updatedBy
                });

            if (!string.IsNullOrEmpty(response.ErrorMessage))
                return BadRequest(response);

            return Ok(response);
        }
    }
}
