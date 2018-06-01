using LiveMusicLovers.Web.UI.Core.Models;
using LiveMusicLovers.Web.UI.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LiveMusicLovers.Web.UI.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly IApplicationDbContext _context;

        public GigRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public IEnumerable<Gig> GetGigs()
        {
            return _context.Gigs
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public IEnumerable<Gig> GetUpcomingGigsByArtist(string artistId)
        {
            return _context.Gigs
                .Where(g => g.ArtistId == artistId && g.DateTime > DateTime.Now && !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
        }

        public void AddGig(Gig gig)
        {
            _context.Gigs.Add(gig);
        }

        public Gig GetGigWithAttendace(int id)
        {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == id);
        }

        public Gig GetGig(int id)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == id);
        }
    }
}