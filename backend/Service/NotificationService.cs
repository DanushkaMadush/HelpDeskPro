using backend.Hubs;
using backend.Interface;
using Microsoft.AspNetCore.SignalR;
using static backend.Models.DTOs.NotificationDTOs;

namespace backend.Service
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyUserAsync(string userId, NotificationMessage message)
        {
            var groupName = $"user_{userId}";

            await _hubContext
                .Clients
                .Group(groupName)
                .SendAsync("ReceiveNotification", message);
        }

        public async Task NotifyUsersAsync(IEnumerable<string> userIds, NotificationMessage message)
        {
            foreach (var userId in userIds.Distinct())
            {
                await NotifyUserAsync(userId, message);
            }
        }
    }
}
