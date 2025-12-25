using backend.Data;
using backend.Interface;
using backend.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace backend.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HelpDeskProDBContext _context;

        public DepartmentRepository(HelpDeskProDBContext context)
        {
            _context = context;
        }

        public async Task<DepartmentDTOs.DepartmentCreateResponse> CreateAsync(DepartmentDTOs.DepartmentCreateRequest request)
        {
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("usp_Department_Create", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@DepartmentName", request.DepartmentName);
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

                return new DepartmentDTOs.DepartmentCreateResponse
                {
                    SuccessMessage = successMessage.Value?.ToString(),
                    ErrorMessage = errorMessage.Value?.ToString()
                };
            }
            catch (SqlException ex)
            {
                return new DepartmentDTOs.DepartmentCreateResponse
                {
                    ErrorMessage = $"SQL Error: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new DepartmentDTOs.DepartmentCreateResponse
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

        public async Task<IEnumerable<DepartmentDTOs.DepartmentResponse>> GetAllAsync()
        {
            var departments = new List<DepartmentDTOs.DepartmentResponse>();
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("usp_Department_GetAll", connection)
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
                    departments.Add(new DepartmentDTOs.DepartmentResponse
                    {
                        DepartmentId = reader.GetInt32(reader.GetOrdinal("departmentId")),
                        DepartmentName = reader.GetString(reader.GetOrdinal("departmentName")),
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

            return departments;

        }
    }
}
