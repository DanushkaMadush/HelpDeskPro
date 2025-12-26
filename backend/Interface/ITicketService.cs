using backend.Models.DTOs;

namespace backend.Interface
{
    public interface ITicketService
    {
        Task<TicketDTOs.CreateTicketResponse> CreateTicketAsync(TicketDTOs.CreateTicketRequest request);
        Task<IEnumerable<TicketDTOs.TicketResponse>> GetAllTicketsAsync();
        Task<TicketDTOs.TicketResponse?> GetTicketByIdAsync(TicketDTOs.GetTicketByIdRequest request);
        Task<IEnumerable<TicketDTOs.TicketResponse>> GetTicketsBySystemIdAsync(TicketDTOs.GetAllTicketsBySystemIdRequest request);
        Task<IEnumerable<TicketDTOs.TicketResponse>> GetTicketsByUserIdAsync(TicketDTOs.GetAllTicketsByUserIdRequest request);
        Task<TicketDTOs.UpdateTicketStatusResponse> UpdateTicketStatusAsync(TicketDTOs.UpdateTicketStatusRequest request);
        Task<TicketDTOs.SoftDeleteTicketResponse> SoftDeleteTicketAsync(TicketDTOs.SoftDeleteTicketRequest request);
        Task<TicketDTOs.UpdateTicketDetailsResponse> UpdateTicketDetailsAsync(TicketDTOs.UpdateTicketDetailsRequest request);
        Task<TicketDTOs.UploadMediaResponse> UploadMediaAsync(TicketDTOs.UploadMediaRequest request,
            string storedFileName,
            string filePath,
            long fileSize,
            string mimeType,
            int? durationSeconds);
        Task<IEnumerable<TicketDTOs.TicketMediaResponse>> GetMediaByTicketIdAsync(int ticketId);
        Task<TicketDTOs.DeleteMediaResponse> DeleteMediaAsync(TicketDTOs.DeleteMediaRequest request);
    }
}
