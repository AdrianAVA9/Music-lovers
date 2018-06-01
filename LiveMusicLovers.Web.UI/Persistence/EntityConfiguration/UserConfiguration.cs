using LiveMusicLovers.Web.UI.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace LiveMusicLovers.Web.UI.Persistence.EntityConfiguration
{
    public class UserConfiguration: EntityTypeConfiguration<ApplicationUser>
    {
        public UserConfiguration()
        {
            Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            HasMany(u => u.UserNotifications)
                .WithRequired(u => u.User)
                .WillCascadeOnDelete(false);

            HasMany(f => f.Followees)
                .WithRequired(u => u.Follower)
                .WillCascadeOnDelete(false);

            HasMany(f => f.Followers)
                .WithRequired(u => u.Followee)
                .WillCascadeOnDelete(false);
        }
    }
}