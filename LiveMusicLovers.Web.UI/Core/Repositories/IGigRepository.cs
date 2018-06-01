using System.Collections.Generic;
using LiveMusicLovers.Web.UI.Core.Models;

namespace LiveMusicLovers.Web.UI.Core.Repositories
{
    public interface IGigRepository
    {
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string artistId);
        void AddGig(Gig gig);
        Gig GetGigWithAttendace(int id);
        Gig GetGig(int id);
    }
}