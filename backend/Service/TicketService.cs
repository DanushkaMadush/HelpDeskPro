using backend.Interface;
using backend.Models.DTOs;

namespace backend.Service
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly INotificationService _notificationService;
        private readonly ISystemService _systemService;

        public TicketService(ITicketRepository ticketRepository, ISystemService systemService, INotificationService notificationService)
        {
            _ticketRepository = ticketRepository;
            _systemService = systemService;
            _notificationService = notificationService;
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
            return await _ticketRepository.UpdateStatusAsync(request);
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

        public async Task<IEnumerable<TicketDTOs.TicketMediaResponse>> GetMediaByTicketIdAsync(
            int ticketId)
        {
            return await _ticketRepository.GetMediaByTicketIdAsync(ticketId);
        }

        public async Task<TicketDTOs.DeleteMediaResponse> DeleteMediaAsync(TicketDTOs.DeleteMediaRequest request)
        {
            return await _ticketRepository.DeleteMediaAsync(request);
        }
    }
}
