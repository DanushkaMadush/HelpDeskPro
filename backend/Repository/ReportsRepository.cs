using backend.Data;
using backend.Interface;
using backend.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace backend.Repository
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly HelpDeskProDBContext _context;

        public ReportsRepository(HelpDeskProDBContext context)
        {
            _context = context;
        }

        public async Task<ReportsDTOs.TicketSummaryResponse> GetTicketSummaryAsync()
        {
            var result = new ReportsDTOs.TicketSummaryResponse();
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("sp_Report_TicketSummary", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    result.Status.Add(new ReportsDTOs.StatusSummary
                    {
                        Status = reader.GetString(0),
                        Total = reader.GetInt32(1)
                    });
                }

                await reader.NextResultAsync();
                while (await reader.ReadAsync())
                {
                    result.Priority.Add(new ReportsDTOs.PrioritySummary
                    {
                        Priority = reader.IsDBNull(0) ? null : reader.GetString(0),
                        Total = reader.GetInt32(1)
                    });
                }

                await reader.NextResultAsync();
                while (await reader.ReadAsync())
                {
                    result.System.Add(new ReportsDTOs.SystemSummary
                    {
                        SystemName = reader.GetString(0),
                        Total = reader.GetInt32(1)
                    });
                }

                await reader.NextResultAsync();
                while (await reader.ReadAsync())
                {
                    result.Branch.Add(new ReportsDTOs.BranchSummary
                    {
                        BranchName = reader.GetString(0),
                        Total = reader.GetInt32(1)
                    });
                }
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }

            return result;
        }

        public async Task<IEnumerable<ReportsDTOs.TicketTrendResponse>> GetTicketTrendsAsync(DateTime? start, DateTime? end, int? systemId, int? branchId)
        {
            var list = new List<ReportsDTOs.TicketTrendResponse>();
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("sp_Report_TicketTrends", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@StartDate", (object?)start ?? DBNull.Value);
                command.Parameters.AddWithValue("@EndDate", (object?)end ?? DBNull.Value);
                command.Parameters.AddWithValue("@SystemId", (object?)systemId ?? DBNull.Value);
                command.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    list.Add(new ReportsDTOs.TicketTrendResponse
                    {
                        Date = reader.GetDateTime(0),
                        TotalTickets = reader.GetInt32(1)
                    });
                }
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }

            return list;
        }

        public async Task<ReportsDTOs.ResolutionPerformanceResponse> GetResolutionPerformanceAsync()
        {
            var result = new ReportsDTOs.ResolutionPerformanceResponse();
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("sp_Report_ResolutionPerformance", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    result.Details.Add(new ReportsDTOs.ResolutionDetail
                    {
                        TicketId = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        CreatedAt = reader.GetDateTime(2),
                        ResolvedAt = reader.GetDateTime(3),
                        ResolutionHours = reader.GetInt32(4)
                    });
                }

                await reader.NextResultAsync();
                while (await reader.ReadAsync())
                {
                    result.Summary.Add(new ReportsDTOs.ResolutionSummary
                    {
                        SystemName = reader.GetString(0),
                        AvgResolutionHours = Convert.ToDouble(reader.GetValue(1))
                    });
                }
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }

            return result;
        }
    }
}
