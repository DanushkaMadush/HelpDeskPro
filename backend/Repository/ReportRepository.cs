using backend.Data;
using backend.Interface;
using backend.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace backend.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly HelpDeskProDBContext _context;

        public ReportRepository(HelpDeskProDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReportDTOs.MonthlyTicketResponse>> GetMonthlyTicketsAsync(int year)
        {
            var result = new List<ReportDTOs.MonthlyTicketResponse>();
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("usp_GetMonthlyTickets", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Year", year);

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    result.Add(new ReportDTOs.MonthlyTicketResponse
                    {
                        MonthNumber = reader.GetInt32(0),
                        MonthName = reader.GetString(1),
                        TotalTickets = reader.GetInt32(2)
                    });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"SQL Error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }

            return result;
        }

        public async Task<IEnumerable<ReportDTOs.BranchTicketResponse>> GetBranchWiseTicketsAsync()
        {
            var result = new List<ReportDTOs.BranchTicketResponse>();
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("usp_GetBranchWiseTickets", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    result.Add(new ReportDTOs.BranchTicketResponse
                    {
                        BranchId = reader.GetInt32(0),
                        BranchName = reader.GetString(1),
                        TotalTickets = reader.GetInt32(2)
                    });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"SQL Error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }

            return result;
        }

        public async Task<IEnumerable<ReportDTOs.PendingSystemResponse>> GetPendingSystemsAsync()
        {
            var result = new List<ReportDTOs.PendingSystemResponse>();
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("usp_GetPendingSystems", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    result.Add(new ReportDTOs.PendingSystemResponse
                    {
                        SystemId = reader.GetInt32(0),
                        SystemName = reader.GetString(1),
                        PendingTickets = reader.GetInt32(2)
                    });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"SQL Error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}", ex);
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