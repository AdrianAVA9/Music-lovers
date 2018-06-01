using LiveMusicLovers.Web.UI.Core.Models;

namespace LiveMusicLovers.Web.UI.Core.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser GetArtistById(string artistId);
        void UpdateUser(string imageUrl, string artistId);
    }
}