using LiveMusicLovers.Web.UI.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace LiveMusicLovers.Web.UI.Persistence.EntityConfiguration
{
    public class GenreConfiguration: EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}