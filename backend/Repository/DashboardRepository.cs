using backend.Data;
using backend.Interface;
using backend.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace backend.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly HelpDeskProDBContext _context;

        public DashboardRepository(HelpDeskProDBContext context)
        {
            _context = context;
        }

        private SqlConnection GetConnection()
        {
            return (SqlConnection)_context.Database.GetDbConnection();
        }

        public async Task<DashboardDTOs.KpiResponse> GetKpiAsync()
        {
            var connection = GetConnection();

            try
            {
                using var command = new SqlCommand("usp_GetTicketKPI", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return new DashboardDTOs.KpiResponse
                    {
                        TotalTickets = reader.GetInt32("TotalTickets"),
                        PendingTickets = reader.GetInt32("PendingTickets"),
                        OngoingTickets = reader.GetInt32("OngoingTickets"),
                        CompletedTickets = reader.GetInt32("CompletedTickets")
                    };
                }

                return new DashboardDTOs.KpiResponse();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
        }

        public async Task<IEnumerable<DashboardDTOs.StatusChartResponse>> GetTicketsByStatusAsync()
        {
            var list = new List<DashboardDTOs.StatusChartResponse>();
            var connection = GetConnection();

            try
            {
                using var command = new SqlCommand("usp_GetTicketsByStatus", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    list.Add(new DashboardDTOs.StatusChartResponse
                    {
                        Status = reader["status"].ToString(),
                        TicketCount = Convert.ToInt32(reader["TicketCount"])
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

        public async Task<IEnumerable<DashboardDTOs.TimeSeriesResponse>> GetTicketsOverTimeAsync()
        {
            var list = new List<DashboardDTOs.TimeSeriesResponse>();
            var connection = GetConnection();

            try
            {
                using var command = new SqlCommand("usp_GetTicketsOverTime", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    list.Add(new DashboardDTOs.TimeSeriesResponse
                    {
                        TicketDate = Convert.ToDateTime(reader["TicketDate"]),
                        TicketCount = Convert.ToInt32(reader["TicketCount"])
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

        public async Task<IEnumerable<DashboardDTOs.SystemChartResponse>> GetTicketsBySystemAsync()
        {
            var list = new List<DashboardDTOs.SystemChartResponse>();
            var connection = GetConnection();

            try
            {
                using var command = new SqlCommand("usp_GetTicketsBySystem", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    list.Add(new DashboardDTOs.SystemChartResponse
                    {
                        SystemName = reader["systemName"].ToString(),
                        TicketCount = Convert.ToInt32(reader["TicketCount"])
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

        public async Task<IEnumerable<DashboardDTOs.BranchChartResponse>> GetTicketsByBranchAsync()
        {
            var list = new List<DashboardDTOs.BranchChartResponse>();
            var connection = GetConnection();

            try
            {
                using var command = new SqlCommand("usp_GetTicketsByBranch", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    list.Add(new DashboardDTOs.BranchChartResponse
                    {
                        BranchName = reader["branchName"].ToString(),
                        TicketCount = Convert.ToInt32(reader["TicketCount"])
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

        public async Task<IEnumerable<DashboardDTOs.PriorityChartResponse>> GetTicketsByPriorityAsync()
        {
            var list = new List<DashboardDTOs.PriorityChartResponse>();
            var connection = GetConnection();

            try
            {
                using var command = new SqlCommand("usp_GetTicketsByPriority", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    list.Add(new DashboardDTOs.PriorityChartResponse
                    {
                        Priority = reader["priority"].ToString(),
                        TicketCount = Convert.ToInt32(reader["TicketCount"])
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

        public async Task<DashboardDTOs.AvgResolutionResponse> GetAvgResolutionTimeAsync()
        {
            var connection = GetConnection();

            try
            {
                using var command = new SqlCommand("usp_GetAverageResolutionTime", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return new DashboardDTOs.AvgResolutionResponse
                    {
                        AvgResolutionHours = Convert.ToDouble(reader["AvgResolutionHours"])
                    };
                }

                return new DashboardDTOs.AvgResolutionResponse();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
        }
    }
}
