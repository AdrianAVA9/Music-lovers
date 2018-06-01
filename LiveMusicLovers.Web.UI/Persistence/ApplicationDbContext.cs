using LiveMusicLovers.Web.UI.Core.Models;
using LiveMusicLovers.Web.UI.Persistence.EntityConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace LiveMusicLovers.Web.UI.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public Database GetDatabase()
        {
            return this.Database;
        }

        public DbEntityEntry GetEntry(object obj)
        {
            return this.Entry(obj);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<ApplicationUser> GetDebDbSetApplictionUser()
        {
            return new ApplicationDbContext().Users;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GigConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            modelBuilder.Configurations.Add(new NotificationConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}