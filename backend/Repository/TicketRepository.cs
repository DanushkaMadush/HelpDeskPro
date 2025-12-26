using backend.Models.DTOs;
using backend.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using backend.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace backend.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly HelpDeskProDBContext _context;

        public TicketRepository(HelpDeskProDBContext context)
        {
            _context = context;
        }

        public async Task<TicketDTOs.CreateTicketResponse> CreateAsync(TicketDTOs.CreateTicketRequest request)
        {
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using var command = new SqlCommand("usp_Ticket_Create", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add("@Title", SqlDbType.NVarChar, 200).Value = request.Title;
                command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = request.Description ?? (object)DBNull.Value;
                command.Parameters.Add("@BranchId", SqlDbType.Int).Value = request.BranchId;
                command.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = request.DepartmentId;
                command.Parameters.Add("@SystemId", SqlDbType.Int).Value = request.SystemId;
                command.Parameters.Add("@StatusId", SqlDbType.Int).Value = request.StatusId;
                command.Parameters.Add("@PriorityId", SqlDbType.Int).Value = request.PriorityId;
                command.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 100).Value = request.CreatedBy;

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

                await command.ExecuteNonQueryAsync();

                return new TicketDTOs.CreateTicketResponse
                {
                    SuccessMessage = successMessage.Value?.ToString(),
                    ErrorMessage = errorMessage.Value?.ToString()
                };
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error while creating ticket.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error while creating ticket.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
        }

        public async Task<IEnumerable<TicketDTOs.TicketResponse>> GetAllAsync()
        {
            var tickets = new List<TicketDTOs.TicketResponse>();
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using var command = new SqlCommand("usp_Ticket_GetAll", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    tickets.Add(MapTicket(reader));
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error while retrieving tickets.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error while retrieving tickets.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }

            return tickets;
        }

        public async Task<TicketDTOs.TicketResponse?> GetByIdAsync(TicketDTOs.GetTicketByIdRequest request)
        {
            TicketDTOs.TicketResponse? ticket = null;
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using var command = new SqlCommand("usp_Ticket_GetById", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add("@TicketId", SqlDbType.Int).Value = request.TicketId;

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    ticket = MapTicket(reader);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error while retrieving ticket.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error while retrieving ticket.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }

            return ticket;
        }

        public async Task<IEnumerable<TicketDTOs.TicketResponse>> GetAllBySystemIdAsync(TicketDTOs.GetAllTicketsBySystemIdRequest request)
        {
            var tickets = new List<TicketDTOs.TicketResponse>();
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using var command = new SqlCommand("usp_Ticket_GetAllBySystemId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@SystemId", request.SystemId);

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    tickets.Add(MapTicket(reader));
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error while retrieving tickets by system.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error while retrieving tickets by system.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }

            return tickets;
        }

        public async Task<IEnumerable<TicketDTOs.TicketResponse>> GetAllByUserIdAsync(TicketDTOs.GetAllTicketsByUserIdRequest request)
        {
            var tickets = new List<TicketDTOs.TicketResponse>();
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using var command = new SqlCommand("usp_Ticket_GetAllByUserId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@UserId", request.UserId);

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    tickets.Add(MapTicket(reader));
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error while retrieving tickets by user.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error while retrieving tickets by user.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }

            return tickets;
        }

        public async Task<TicketDTOs.UpdateTicketStatusResponse> UpdateStatusAsync(TicketDTOs.UpdateTicketStatusRequest request)
        {
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("usp_Ticket_UpdateStatus", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@TicketId", request.TicketId);
                command.Parameters.AddWithValue("@StatusId", request.StatusId);
                command.Parameters.AddWithValue("@UpdatedBy", request.UpdatedBy);

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

                return new TicketDTOs.UpdateTicketStatusResponse
                {
                    SuccessMessage = successMessage.Value?.ToString(),
                    ErrorMessage = errorMessage.Value?.ToString()
                };
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error while updating ticket status.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error while updating ticket status.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
        }

        public async Task<TicketDTOs.SoftDeleteTicketResponse> SoftDeleteAsync(TicketDTOs.SoftDeleteTicketRequest request)
        {
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("usp_Ticket_Delete", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@TicketId", request.TicketId);
                command.Parameters.AddWithValue("@UpdatedBy", request.UpdatedBy);

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

                return new TicketDTOs.SoftDeleteTicketResponse
                {
                    SuccessMessage = successMessage.Value?.ToString(),
                    ErrorMessage = errorMessage.Value?.ToString()
                };
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error while deleting ticket.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error while deleting ticket.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
        }

        public async Task<TicketDTOs.UpdateTicketDetailsResponse> UpdateDetailsAsync(TicketDTOs.UpdateTicketDetailsRequest request)
        {
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("usp_Ticket_UpdateDetails", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@TicketId", request.TicketId);
                command.Parameters.AddWithValue("@Title", request.Title);
                command.Parameters.AddWithValue("@Description", request.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@BranchId", request.BranchId);
                command.Parameters.AddWithValue("@DepartmentId", request.DepartmentId);
                command.Parameters.AddWithValue("@SystemId", request.SystemId);
                command.Parameters.AddWithValue("@PriorityId", request.PriorityId);
                command.Parameters.AddWithValue("@UpdatedBy", request.UpdatedBy);

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

                return new TicketDTOs.UpdateTicketDetailsResponse
                {
                    SuccessMessage = successMessage.Value?.ToString(),
                    ErrorMessage = errorMessage.Value?.ToString()
                };
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error while updating ticket details.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error while updating ticket details.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
        }

        private static TicketDTOs.TicketResponse MapTicket(SqlDataReader reader)
        {
            int GetOrdinal(string name) => reader.GetOrdinal(name);

            return new TicketDTOs.TicketResponse
            {
                TicketId = reader.GetInt32(GetOrdinal("ticketId")),
                Title = reader.IsDBNull(GetOrdinal("title")) ? null : reader.GetString(GetOrdinal("title")),
                Description = reader.IsDBNull(GetOrdinal("description")) ? null : reader.GetString(GetOrdinal("description")),
                BranchId = reader.GetInt32(GetOrdinal("branchId")),
                BranchName = reader.IsDBNull(GetOrdinal("branchName")) ? null : reader.GetString(GetOrdinal("branchName")),
                DepartmentId = reader.GetInt32(GetOrdinal("departmentId")),
                DepartmentName = reader.IsDBNull(GetOrdinal("departmentName")) ? null : reader.GetString(GetOrdinal("departmentName")),
                SystemId = reader.GetInt32(GetOrdinal("systemId")),
                SystemName = reader.IsDBNull(GetOrdinal("systemName")) ? null : reader.GetString(GetOrdinal("systemName")),
                StatusId = reader.GetInt32(GetOrdinal("statusId")),
                Status = reader.IsDBNull(GetOrdinal("status")) ? null : reader.GetString(GetOrdinal("status")),
                PriorityId = reader.GetInt32(GetOrdinal("priorityId")),
                Priority = reader.IsDBNull(GetOrdinal("priority")) ? null : reader.GetString(GetOrdinal("priority")),
                CreatedBy = reader.GetString(GetOrdinal("createdBy")),
                CreatedAt = reader.GetDateTime(GetOrdinal("createdAt")),
                UpdatedBy = reader.IsDBNull(GetOrdinal("updatedBy")) ? null : reader.GetString(GetOrdinal("updatedBy")),
                UpdatedAt = reader.IsDBNull(GetOrdinal("updatedAt")) ? null : reader.GetDateTime(GetOrdinal("updatedAt"))
            };
        }

        public async Task<TicketDTOs.UploadMediaResponse> CreateMediaAsync(TicketDTOs.UploadMediaRequest request,
            string storedFileName,
            string filePath,
            long fileSize,
            string mimeType,
            int? durationSeconds)
        {
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("usp_TicketMedia_Create", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@TicketId", request.TicketId);
                command.Parameters.AddWithValue("@FileName", storedFileName);
                command.Parameters.AddWithValue("@OriginalFileName", request.File.FileName);
                command.Parameters.AddWithValue("@FilePath", filePath);
                command.Parameters.AddWithValue("@MimeType", mimeType);
                command.Parameters.AddWithValue("@FileSize", fileSize);
                command.Parameters.AddWithValue("@DurationSeconds", durationSeconds ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@UploadedBy", request.UploadedBy);

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

                return new TicketDTOs.UploadMediaResponse
                {
                    SuccessMessage = successMessage.Value?.ToString(),
                    ErrorMessage = errorMessage.Value?.ToString()
                };
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error while saving ticket media.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error while saving ticket media.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
        }

        public async Task<IEnumerable<TicketDTOs.TicketMediaResponse>> GetMediaByTicketIdAsync(int ticketId)
        {
            var mediaList = new List<TicketDTOs.TicketMediaResponse>();
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("usp_TicketMedia_GetByTicketId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@TicketId", ticketId);

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
                    mediaList.Add(new TicketDTOs.TicketMediaResponse
                    {
                        TicketMediaId = reader.GetInt32(reader.GetOrdinal("ticketMediaId")),
                        TicketId = reader.GetInt32(reader.GetOrdinal("ticketId")),
                        FileName = reader.GetString(reader.GetOrdinal("fileName")),
                        OriginalFileName = reader.GetString(reader.GetOrdinal("originalFileName")),
                        FilePath = reader.GetString(reader.GetOrdinal("filePath")),
                        MimeType = reader.GetString(reader.GetOrdinal("mimeType")),
                        FileSize = reader.GetInt64(reader.GetOrdinal("fileSize")),
                        DurationSeconds = reader["durationSeconds"] as int?,
                        UploadedAt = reader.GetDateTime(reader.GetOrdinal("uploadedAt"))
                    });
                }

                return mediaList;
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error while retrieving ticket media.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error while retrieving ticket media.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
        }

        public async Task<TicketDTOs.DeleteMediaResponse> DeleteMediaAsync(TicketDTOs.DeleteMediaRequest request)
        {
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            try
            {
                using var command = new SqlCommand("usp_TicketMedia_SoftDelete", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@TicketMediaId", request.TicketMediaId);
                command.Parameters.AddWithValue("@UpdatedBy", request.UpdatedBy);

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

                return new TicketDTOs.DeleteMediaResponse
                {
                    SuccessMessage = successMessage.Value?.ToString(),
                    ErrorMessage = errorMessage.Value?.ToString()
                };
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error while deleting ticket media.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error while deleting ticket media.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
        }
    }
}
