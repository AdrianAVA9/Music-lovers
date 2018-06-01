using LiveMusicLovers.Web.UI.Core.Models;
using LiveMusicLovers.Web.UI.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace LiveMusicLovers.Web.UI.Persistence.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private IApplicationDbContext _context;

        public UserNotificationRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserNotification> GetUserNotification(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();
        }
    }
}