using backend.Interface;
using backend.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Authorize]
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
            try
            {
                if (request == null)
                    return BadRequest(new { Message = "Invalid request body." });

                var response = await _ticketService.CreateTicketAsync(request);

                if (!string.IsNullOrEmpty(response.ErrorMessage))
                    return BadRequest(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating ticket.", Error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            try
            {
                var tickets = await _ticketService.GetAllTicketsAsync();
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Failed to retrieve tickets.", Error = ex.Message });
            }
        }

        [HttpGet("{ticketId:int}")]
        public async Task<IActionResult> GetTicketById(int ticketId)
        {
            try
            {
                var ticket = await _ticketService.GetTicketByIdAsync(
                    new TicketDTOs.GetTicketByIdRequest { TicketId = ticketId });

                if (ticket == null)
                    return NotFound(new { Message = "Ticket not found." });

                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error retrieving ticket.", Error = ex.Message });
            }
        }

        [HttpGet("system/{systemId:int}")]
        public async Task<IActionResult> GetTicketsBySystemId(int systemId)
        {
            try
            {
                var tickets = await _ticketService.GetTicketsBySystemIdAsync(
                    new TicketDTOs.GetAllTicketsBySystemIdRequest { SystemId = systemId });

                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error retrieving tickets by system.", Error = ex.Message });
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTicketsByUserId(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    return BadRequest(new { Message = "UserId is required." });

                var tickets = await _ticketService.GetTicketsByUserIdAsync(
                    new TicketDTOs.GetAllTicketsByUserIdRequest { UserId = userId });

                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error retrieving tickets by user.", Error = ex.Message });
            }
        }

        [HttpPatch("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] TicketDTOs.UpdateTicketStatusRequest request)
        {
            try
            {
                var response = await _ticketService.UpdateTicketStatusAsync(request);

                if (!string.IsNullOrEmpty(response.ErrorMessage))
                    return BadRequest(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error updating ticket status.", Error = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDetails([FromBody] TicketDTOs.UpdateTicketDetailsRequest request)
        {
            try
            {
                var response = await _ticketService.UpdateTicketDetailsAsync(request);

                if (!string.IsNullOrEmpty(response.ErrorMessage))
                    return BadRequest(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error updating ticket details.", Error = ex.Message });
            }
        }

        [HttpDelete("{ticketId:int}")]
        public async Task<IActionResult> SoftDelete(int ticketId, [FromQuery] string updatedBy)
        {
            try
            {
                var response = await _ticketService.SoftDeleteTicketAsync(
                    new TicketDTOs.SoftDeleteTicketRequest
                    {
                        TicketId = ticketId,
                        UpdatedBy = updatedBy
                    });

                if (!string.IsNullOrEmpty(response.ErrorMessage))
                    return BadRequest(response);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error deleting ticket.", Error = ex.Message });
            }
        }

        [HttpPost("{ticketId:int}/media")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload(int ticketId, [FromForm] TicketDTOs.UploadMediaRequest request)
        {
            try
            {
                if (request.File == null || request.File.Length == 0)
                    return BadRequest(new { Message = "No file uploaded." });

                var uploadsRoot = Path.Combine(_environment.ContentRootPath, "uploads", "tickets", ticketId.ToString());

                if (!Directory.Exists(uploadsRoot))
                    Directory.CreateDirectory(uploadsRoot);

                var storedFileName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";
                var filePath = Path.Combine(uploadsRoot, storedFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream);
                }

                var relativePath = Path.Combine("uploads", "tickets", ticketId.ToString(), storedFileName).Replace("\\", "/");

                var response = await _ticketService.UploadMediaAsync(
                    ticketId,
                    request,
                    storedFileName,
                    relativePath,
                    request.File.Length,
                    request.File.ContentType,
                    null);

                if (!string.IsNullOrEmpty(response.ErrorMessage))
                    return BadRequest(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error uploading file.", Error = ex.Message });
            }
        }

        [HttpGet("{ticketId:int}/media")]
        public async Task<IActionResult> GetByTicketId(int ticketId)
        {
            try
            {
                var media = await _ticketService.GetMediaByTicketIdAsync(ticketId);
                return Ok(media);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error retrieving media.", Error = ex.Message });
            }
        }

        [HttpDelete("{ticketId:int}/media/{ticketMediaId:int}")]
        public async Task<IActionResult> DeleteMedia(int ticketId, int ticketMediaId, [FromQuery] string updatedBy)
        {
            try
            {
                var response = await _ticketService.DeleteMediaAsync(
                    new TicketDTOs.DeleteMediaRequest
                    {
                        TicketMediaId = ticketMediaId,
                        UpdatedBy = updatedBy
                    });

                if (!string.IsNullOrEmpty(response.ErrorMessage))
                    return BadRequest(response);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error deleting media.", Error = ex.Message });
            }
        }
    }
}