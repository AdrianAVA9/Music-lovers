using System.Collections.Generic;
using LiveMusicLovers.Web.UI.Core.Models;

namespace LiveMusicLovers.Web.UI.Core.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}