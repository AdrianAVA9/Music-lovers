using LiveMusicLovers.Web.UI.Core.ViewModels;
using LiveMusicLovers.Web.UI.Persistence;
using LiveMusicLovers.Web.UI.Persistence.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LiveMusicLovers.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly GigRepository _gigReposiory;
        private readonly AttendanceRepository _attendanceRepository;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _gigReposiory = new GigRepository(_context);
            _attendanceRepository = new AttendanceRepository(_context);
        }

        public ActionResult Index(string query = null)
        {
            var userId = User.Identity.GetUserId();

            //var upcomingGigs = userId != null ? _gigReposiory.GetGigsUserAttending(userId) : _gigReposiory.GetGigs();

            var upcomingGigs = _gigReposiory.GetGigs();


            var attendances = _attendanceRepository.GetFutureAttendances(userId).ToLookup(a => a.GigId);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs =
                    upcomingGigs.Where(g => 
                        g.Artist.Name == query || 
                        g.Genre.Name == query || 
                        g.Venue == query);
            }

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                showAction = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTeam = query,
                Attendances = attendances,
            };

            return View("Gigs",viewModel);
        }

        public ActionResult Homepage()
        {
            return View("Homepage");
        }
    }
}