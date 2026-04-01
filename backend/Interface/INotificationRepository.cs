using backend.Models.DTOs;
using static backend.Models.DTOs.NotificationDTOs;

namespace backend.Interface
{
    public interface INotificationRepository
    {
        Task CreateAsync(NotificationMessage message, string userId);
        Task<IEnumerable<GetNotificationByUser>> GetByUserIdAsync(string userId);
        Task<int> GetUnreadCountAsync(string userId);
        Task MarkAsReadAsync(int notificationId, string userId);
        Task MarkAllAsReadAsync(string userId);
    }
}
