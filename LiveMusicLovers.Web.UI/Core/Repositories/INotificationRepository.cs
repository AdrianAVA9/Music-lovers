using LiveMusicLovers.Web.UI.Core.Models;
using System.Collections.Generic;

namespace LiveMusicLovers.Web.UI.Core.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNewUserNotification(string userId);
    }
}