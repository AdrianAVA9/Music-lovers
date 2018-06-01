using LiveMusicLovers.Web.UI.Core.Repositories;

namespace LiveMusicLovers.Web.UI.Core
{
    public interface IUnitOfWork
    {
        IGigRepository Gigs { get; }
        IAttendanceRepository Attendances { get; }
        IGenreRepository Genres { get; }
        IRelationshipRepository Relationships { get; }
        IUserRepository Users { get; }
        IUserNotificationRepository UserNotifications { get; }
        INotificationRepository Notification { get; }
        void Complete();
    }
}