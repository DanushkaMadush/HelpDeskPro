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
    }
}
