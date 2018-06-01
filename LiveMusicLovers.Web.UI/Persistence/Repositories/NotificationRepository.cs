using LiveMusicLovers.Web.UI.Core.Models;
using LiveMusicLovers.Web.UI.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LiveMusicLovers.Web.UI.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private IApplicationDbContext _context;

        public NotificationRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetNewUserNotification(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();
        }
    }
}