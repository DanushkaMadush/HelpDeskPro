using backend.Data;
using backend.Interface;
using backend.Models.DTOs;
using backend.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace backend.Repository
{
    public class SystemRepository : ISystemRepository
    {
        private readonly HelpDeskProDBContext _context;

        public SystemRepository(HelpDeskProDBContext context)
        {
            _context = context;
        }

        public async Task<SystemDTOs.SystemCreateResponse> CreateAsync(SystemDTOs.SystemCreateRequest request)
        {
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("usp_System_Create", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@SystemName", request.SystemName);
                command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);

                var successMessage = new SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };

                var errorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(successMessage);
                command.Parameters.Add(errorMessage);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                return new SystemDTOs.SystemCreateResponse
                {
                    SuccessMessage = successMessage.Value?.ToString(),
                    ErrorMessage = errorMessage.Value?.ToString()
                };
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error while creating system.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error while creating system.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
        }

        public async Task<IEnumerable<SystemDTOs.SystemResponse>> GetAllAsync()
        {
            var systems = new List<SystemDTOs.SystemResponse>();
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("usp_System_GetAll", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var successMessage = new SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };

                var errorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(successMessage);
                command.Parameters.Add(errorMessage);

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    systems.Add(new SystemDTOs.SystemResponse
                    {
                        SystemId = reader.GetInt32(reader.GetOrdinal("systemId")),
                        SystemName = reader.GetString(reader.GetOrdinal("systemName"))
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

            return systems;

        }

        public async Task<IEnumerable<SystemDTOs.GetSystemsUsersResponse>> GetSystemsByUserIdAsync(SystemDTOs.GetSystemsByUserIdRequest request)
        {
            var systems = new List<SystemDTOs.GetSystemsUsersResponse>();
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("usp_System_GetAllSystemsByUserId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@UserId", request.UserId);

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    systems.Add(new SystemDTOs.GetSystemsUsersResponse
                    {
                        SysDevId = reader.GetInt32(reader.GetOrdinal("sysDevId")),
                        SystemId = reader.GetInt32(reader.GetOrdinal("systemId")),
                        UserId = reader.GetString(reader.GetOrdinal("userId")),
                        SystemName = reader.GetString(reader.GetOrdinal("systemName"))
                    });
                }

                return systems;
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error while fetching systems by user.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error while fetching systems by user.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
        }

        public async Task<IEnumerable<SystemDTOs.GetSystemsUsersResponse>> GetUsersBySystemIdAsync(SystemDTOs.GetUsersBySystemIdRequest request)
        {
            var systems = new List<SystemDTOs.GetSystemsUsersResponse>();
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("usp_System_GetAllUsersBySystemId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@SystemId", request.SystemId);

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    systems.Add(new SystemDTOs.GetSystemsUsersResponse
                    {
                        SysDevId = reader.GetInt32(reader.GetOrdinal("sysDevId")),
                        SystemId = reader.GetInt32(reader.GetOrdinal("systemId")),
                        UserId = reader.GetString(reader.GetOrdinal("userId")),
                        SystemName = reader.GetString(reader.GetOrdinal("systemName"))
                    });
                }

                return systems;
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error while fetching systems by user.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error while fetching systems by user.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
        }

        public async Task<SystemDTOs.SystemCreateResponse> AssignSystemsToDevelopersAsync(SystemDTOs.SystemAssignRequest request)
        {
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("usp_System_Assign", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@SystemId", request.SystemId);
                command.Parameters.AddWithValue("@UserId", request.UserId);

                var successMessage = new SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };

                var errorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(successMessage);
                command.Parameters.Add(errorMessage);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                return new SystemDTOs.SystemCreateResponse
                {
                    SuccessMessage = successMessage.Value?.ToString(),
                    ErrorMessage = errorMessage.Value?.ToString()
                };
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error while creating system.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error while creating system.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
        }
    }
}
