using backend.Data;
using backend.Interface;
using backend.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace backend.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly HelpDeskProDBContext _context;

        public BranchRepository(HelpDeskProDBContext context)
        {
            _context = context;
        }

        public async Task<BranchDTOs.BranchCreateResponse> CreateAsync(BranchDTOs.BranchCreateRequest request)
        {
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("usp_Branch_Create", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@BranchName", request.BranchName);
                command.Parameters.AddWithValue("@Address", request.Address);
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

                return new BranchDTOs.BranchCreateResponse
                {
                    SuccessMessage = successMessage.Value?.ToString(),
                    ErrorMessage = errorMessage.Value?.ToString()
                };
            }
            catch (SqlException ex)
            {
                return new BranchDTOs.BranchCreateResponse
                {
                    ErrorMessage = $"SQL Error: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new BranchDTOs.BranchCreateResponse
                {
                    ErrorMessage = $"Error: {ex.Message}"
                };
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
        }

        public async Task<IEnumerable<BranchDTOs.BranchResponse>> GetAllAsync()
        {
            var branches = new List<BranchDTOs.BranchResponse>();
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("usp_Branch_GetAll", connection)
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
                    branches.Add(new BranchDTOs.BranchResponse
                    {
                        BranchId = reader.GetInt32(reader.GetOrdinal("BranchId")),
                        BranchName = reader.GetString(reader.GetOrdinal("BranchName")),
                        Address = reader.GetString(reader.GetOrdinal("Address"))
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

            return branches;

        }
    }
}
