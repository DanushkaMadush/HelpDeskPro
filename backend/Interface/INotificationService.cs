using static backend.Models.DTOs.NotificationDTOs;

namespace backend.Interface
{
    public interface INotificationService
    {
        Task NotifyUserAsync(string userId, NotificationMessage message);
        Task NotifyUsersAsync(IEnumerable<string> userIds, NotificationMessage message);
    }
}
