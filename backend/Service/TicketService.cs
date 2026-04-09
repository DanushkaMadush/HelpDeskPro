using backend.Interface;
using backend.Models.DTOs;

namespace backend.Service
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly INotificationService _notificationService;
        private readonly ISystemService _systemService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TicketService(ITicketRepository ticketRepository, ISystemService systemService, INotificationService notificationService, IHttpContextAccessor httpContextAccessor)
        {
            _ticketRepository = ticketRepository;
            _systemService = systemService;
            _notificationService = notificationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TicketDTOs.CreateTicketResponse> CreateTicketAsync(TicketDTOs.CreateTicketRequest request)
        {
            var result = await _ticketRepository.CreateAsync(request);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
                return result;

            var systemUsers = await _systemService.GetUsersBySystemIdAsync(new SystemDTOs.GetUsersBySystemIdRequest
            {
                SystemId = request.SystemId
            });

            var developerUserIds = systemUsers
                .Select(user => user.UserId)
                .Distinct()
                .ToList();

            if (!developerUserIds.Any())
                return result;

            var notification = new NotificationDTOs.NotificationMessage
            {
                Title = "New Ticket Created",
                Message = $"A new ticket was created for system ID {request.SystemId}.",
                TicketId = result.Ticket?.TicketId,
                SystemId = request.SystemId
            };

            await _notificationService.NotifyUsersAsync(developerUserIds, notification);

            return result;
        }

        public async Task<IEnumerable<TicketDTOs.TicketResponse>> GetAllTicketsAsync()
        {
            return await _ticketRepository.GetAllAsync();
        }

        public async Task<TicketDTOs.TicketResponse?> GetTicketByIdAsync(TicketDTOs.GetTicketByIdRequest request)
        {
            return await _ticketRepository.GetByIdAsync(request);
        }

        public async Task<IEnumerable<TicketDTOs.TicketResponse>> GetTicketsBySystemIdAsync(TicketDTOs.GetAllTicketsBySystemIdRequest request)
        {
            return await _ticketRepository.GetAllBySystemIdAsync(request);
        }

        public async Task<IEnumerable<TicketDTOs.TicketResponse>> GetTicketsByUserIdAsync(TicketDTOs.GetAllTicketsByUserIdRequest request)
        {
            return await _ticketRepository.GetAllByUserIdAsync(request);
        }

        public async Task<TicketDTOs.UpdateTicketStatusResponse> UpdateTicketStatusAsync(TicketDTOs.UpdateTicketStatusRequest request)
        {
            var result = await _ticketRepository.UpdateStatusAsync(request);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
                return result;

            var ticket = await _ticketRepository.GetByIdAsync(new TicketDTOs.GetTicketByIdRequest
            {
                TicketId = request.TicketId
            });

            if (ticket != null && !string.IsNullOrEmpty(ticket.CreatedBy))
            {
                var notification = new NotificationDTOs.NotificationMessage
                {
                    Title = "Ticket Status Updated",
                    Message = $"Your ticket #{request.TicketId} status has been updated to \"{ticket.Status ?? request.StatusId.ToString()}\".",
                    TicketId = request.TicketId,
                    StatusId = request.StatusId
                };

                await _notificationService.NotifyUserAsync(ticket.CreatedBy, notification);
            }

            return result;
        }

        public async Task<TicketDTOs.SoftDeleteTicketResponse> SoftDeleteTicketAsync(TicketDTOs.SoftDeleteTicketRequest request)
        {
            return await _ticketRepository.SoftDeleteAsync(request);
        }

        public async Task<TicketDTOs.UpdateTicketDetailsResponse> UpdateTicketDetailsAsync(TicketDTOs.UpdateTicketDetailsRequest request)
        {
            return await _ticketRepository.UpdateDetailsAsync(request);
        }

        public async Task<TicketDTOs.UploadMediaResponse> UploadMediaAsync(
            int ticketId,
            TicketDTOs.UploadMediaRequest request,
            string storedFileName,
            string filePath,
            long fileSize,
            string mimeType,
            int? durationSeconds)
        {
            return await _ticketRepository.CreateMediaAsync(
                ticketId,
                request,
                storedFileName,
                filePath,
                fileSize,
                mimeType,
                durationSeconds);
        }

        public async Task<IEnumerable<TicketDTOs.TicketMediaResponse>> GetMediaByTicketIdAsync(int ticketId)
        {
            var mediaList = await _ticketRepository.GetMediaByTicketIdAsync(ticketId);

            var request = _httpContextAccessor.HttpContext?.Request;

            var baseUrl = $"{request?.Scheme}://{request?.Host}";

            return mediaList.Select(m => new TicketDTOs.TicketMediaResponse
            {
                TicketMediaId = m.TicketMediaId,
                TicketId = m.TicketId,
                FileName = m.FileName,
                OriginalFileName = m.OriginalFileName,
                FilePath = $"{baseUrl}/{m.FilePath.Replace("\\", "/")}",
                MimeType = m.MimeType,
                FileSize = m.FileSize,
                DurationSeconds = m.DurationSeconds,
                UploadedAt = m.UploadedAt
            });
        }

        public async Task<TicketDTOs.DeleteMediaResponse> DeleteMediaAsync(TicketDTOs.DeleteMediaRequest request)
        {
            return await _ticketRepository.DeleteMediaAsync(request);
        }
    }
}
