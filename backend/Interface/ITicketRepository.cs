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
    }
}
