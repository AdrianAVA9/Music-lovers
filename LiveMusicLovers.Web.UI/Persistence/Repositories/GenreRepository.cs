using System.Collections.Generic;
using System.Linq;
using LiveMusicLovers.Web.UI.Core.Models;
using LiveMusicLovers.Web.UI.Core.Repositories;

namespace LiveMusicLovers.Web.UI.Persistence.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }
    }
}