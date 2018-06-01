using LiveMusicLovers.Web.UI.Core;
using LiveMusicLovers.Web.UI.Core.Repositories;
using LiveMusicLovers.Web.UI.Persistence.Repositories;

namespace LiveMusicLovers.Web.UI.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGigRepository Gigs { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IRelationshipRepository Relationships { get; private set; }
        public IUserRepository Users { get; }
        public IUserNotificationRepository UserNotifications { get; }
        public INotificationRepository Notification { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(_context);
            Attendances = new AttendanceRepository(_context);
            Genres = new GenreRepository(_context);
            Relationships = new RelationshipRepository(_context);
            Users = new UserRepository(_context);
            UserNotifications = new UserNotificationRepository(_context);
            Notification = new NotificationRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}