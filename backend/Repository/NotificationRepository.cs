using backend.Data;
using backend.Interface;
using backend.Models.DTOs;
using Microsoft.Data.SqlClient;
using static backend.Models.DTOs.NotificationDTOs;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly HelpDeskProDBContext _context;

        public NotificationRepository(HelpDeskProDBContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(NotificationMessage message, string userId)
        {
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            using var command = new SqlCommand("usp_Notification_Create", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@Title", message.Title);
            command.Parameters.AddWithValue("@Message", message.Message);
            command.Parameters.AddWithValue("@TicketId", message.TicketId ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@SystemId", message.SystemId ?? (object)DBNull.Value);

            var success = new SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 200)
            {
                Direction = ParameterDirection.Output
            };

            var error = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500)
            {
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(success);
            command.Parameters.Add(error);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }

        public async Task<IEnumerable<GetNotificationByUser>> GetByUserIdAsync(string userId)
        {
            var list = new List<GetNotificationByUser>();
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            using var command = new SqlCommand("usp_Notification_GetByUserId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@UserId", userId);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(new GetNotificationByUser
                {
                    NotificationId = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Message = reader.GetString(2),
                    TicketId = reader["ticketId"] as int?,
                    SystemId = reader["systemId"] as int?,
                    IsRead = reader.GetBoolean(5),
                    CreatedAt = reader.GetDateTime(6)
                });
            }

            await connection.CloseAsync();
            return list;
        }

        public async Task<int> GetUnreadCountAsync(string userId)
        {
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            using var command = new SqlCommand("usp_Notification_GetUnreadCount", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@UserId", userId);

            await connection.OpenAsync();
            var count = (int)await command.ExecuteScalarAsync();
            await connection.CloseAsync();

            return count;
        }

        public async Task MarkAsReadAsync(int notificationId, string userId)
        {
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            using var command = new SqlCommand("usp_Notification_MarkAsRead", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@NotificationId", notificationId);
            command.Parameters.AddWithValue("@UserId", userId);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }

        public async Task MarkAllAsReadAsync(string userId)
        {
            var connection = (SqlConnection)_context.Database.GetDbConnection();

            using var command = new SqlCommand("usp_Notification_MarkAllAsRead", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@UserId", userId);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }
    }
}
