using backend.Interface;
using backend.Models.DTOs;

namespace backend.Service
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<TicketDTOs.CreateTicketResponse> CreateTicketAsync(TicketDTOs.CreateTicketRequest request)
        {
            return await _ticketRepository.CreateAsync(request);
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
    }
}
