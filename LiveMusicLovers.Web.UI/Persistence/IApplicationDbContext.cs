using LiveMusicLovers.Web.UI.Core.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace LiveMusicLovers.Web.UI.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Attendance> Attendances { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Gig> Gigs { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<Relationship> Relationships { get; set; }
        DbSet<UserNotification> UserNotifications { get; set; }
        IDbSet<ApplicationUser> GetDebDbSetApplictionUser();
        Database GetDatabase();
        DbEntityEntry GetEntry(object obj);
    }
}