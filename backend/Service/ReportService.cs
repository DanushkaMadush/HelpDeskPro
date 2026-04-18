using backend.Interface;
using backend.Models.DTOs;

namespace backend.Service
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repository;

        public ReportService(IReportRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<ReportDTOs.MonthlyTicketResponse>> GetMonthlyTicketsAsync(int year)
        {
            return _repository.GetMonthlyTicketsAsync(year);
        }

        public Task<IEnumerable<ReportDTOs.BranchTicketResponse>> GetBranchWiseTicketsAsync()
        {
            return _repository.GetBranchWiseTicketsAsync();
        }

        public Task<IEnumerable<ReportDTOs.PendingSystemResponse>> GetPendingSystemsAsync()
        {
            return _repository.GetPendingSystemsAsync();
        }
    }
}