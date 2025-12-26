using backend.Models.DTOs;

namespace backend.Interface
{
    public interface ITicketRepository
    {
        Task<TicketDTOs.CreateTicketResponse> CreateAsync(TicketDTOs.CreateTicketRequest request);
        Task<IEnumerable<TicketDTOs.TicketResponse>> GetAllAsync();
        Task<TicketDTOs.TicketResponse?> GetByIdAsync(TicketDTOs.GetTicketByIdRequest request);
        Task<IEnumerable<TicketDTOs.TicketResponse>> GetAllBySystemIdAsync(TicketDTOs.GetAllTicketsBySystemIdRequest request);
        Task<IEnumerable<TicketDTOs.TicketResponse>> GetAllByUserIdAsync(TicketDTOs.GetAllTicketsByUserIdRequest request);
        Task<TicketDTOs.UpdateTicketStatusResponse> UpdateStatusAsync(TicketDTOs.UpdateTicketStatusRequest request);
        Task<TicketDTOs.SoftDeleteTicketResponse> SoftDeleteAsync(TicketDTOs.SoftDeleteTicketRequest request);
        Task<TicketDTOs.UpdateTicketDetailsResponse> UpdateDetailsAsync(TicketDTOs.UpdateTicketDetailsRequest request);
        Task<TicketDTOs.UploadMediaResponse> CreateMediaAsync(TicketDTOs.UploadMediaRequest request,
            string storedFileName,
            string filePath,
            long fileSize,
            string mimeType,
            int? durationSeconds);

        Task<IEnumerable<TicketDTOs.TicketMediaResponse>> GetMediaByTicketIdAsync(int ticketId);
        Task<TicketDTOs.DeleteMediaResponse> DeleteMediaAsync(TicketDTOs.DeleteMediaRequest request);
    }
}
