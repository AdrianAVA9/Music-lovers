using LiveMusicLovers.Web.UI.Core.Models;
using System.Collections.Generic;

namespace LiveMusicLovers.Web.UI.Core.Repositories
{
    public interface IUserNotificationRepository
    {
        IEnumerable<UserNotification> GetUserNotification(string userId);
    }
}