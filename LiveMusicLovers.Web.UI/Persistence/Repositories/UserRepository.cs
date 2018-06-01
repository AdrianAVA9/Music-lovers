using LiveMusicLovers.Web.UI.Core.Models;
using LiveMusicLovers.Web.UI.Core.Repositories;
using System.Data.SqlClient;
using System.Linq;

namespace LiveMusicLovers.Web.UI.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _context;

        public UserRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GetArtistById(string artistId)
        {
            return _context.GetDebDbSetApplictionUser()
                .SingleOrDefault(a => a.Id == artistId);
        }

        public void UpdateUser(string imageUrl, string artistId)
        {
            _context.GetDatabase()
                .ExecuteSqlCommand("UPDATE dbo.AspNetUsers SET image = @imageUrl WHERE Id = @userId" 
                ,new[] {
                    new SqlParameter("@imageUrl", imageUrl),
                    new SqlParameter("@userId",artistId)
                });
        }
    }
}