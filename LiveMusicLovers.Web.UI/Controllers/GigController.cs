using LiveMusicLovers.Web.UI.Core;
using LiveMusicLovers.Web.UI.Core.Dto;
using LiveMusicLovers.Web.UI.Core.Models;
using LiveMusicLovers.Web.UI.Core.ViewModels;
using LiveMusicLovers.Web.UI.Persistence;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace LiveMusicLovers.Web.UI.Controllers
{
    public class GigController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public ViewResult Mine()
        {
            var gigs = _unitOfWork.Gigs.GetUpcomingGigsByArtist(User.Identity.GetUserId());

            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var gigViewModel = new GigsViewModel
            {
                UpcomingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userId),
                showAction = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm going",
                Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.GigId),
            };

            return View("Gigs", gigViewModel);
        }

        [Authorize]
        public ActionResult GigForm()
        {
            var viewModel = new GigForViewModel
            {
                Genres = _unitOfWork.Genres.GetGenres(),
                Heading = "Add a Gig",
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigForViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm",viewModel);
            }

            var userId = User.Identity.GetUserId();
            var gig = new Gig
            {
                ArtistId = userId,
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue,
            };

            _unitOfWork.Gigs.AddGig(gig);
            _unitOfWork.Complete();

            //gig.Artist = _unitOfWork.Users.GetArtistById(userId);
            //gig.Artist.Followers = (ICollection<Relationship>) _unitOfWork.Relationships.GetFollowers(userId);

            gig.NotifyForANewGig(_unitOfWork.Relationships.GetFollowers(userId));

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gig");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigForViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = new ApplicationDbContext().Genres.ToList();
                return View("GigForm",viewModel);
            }

            var gig = _unitOfWork.Gigs.GetGigWithAttendace(viewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if(gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gig");
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTeam});
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var gigViewModel = new GigForViewModel
            {
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Venue = gig.Venue,
                Genre = gig.GenreId,
                Genres = new ApplicationDbContext().Genres.ToList(),
                Id = gig.Id,
                Heading = "Edit a Gig",
            };

            return View("GigForm", gigViewModel);
        }

        public ActionResult Details(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            var viewModel = new DetailsDto { Gig = gig };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.AmIFollowing = _unitOfWork.Relationships.IsFollowing(gig.ArtistId,userId) != null;

                viewModel.AmIGoing = _unitOfWork.Attendances.IsGoing(gig.Id, userId) != null;

                viewModel.AmI = (User.Identity.GetUserId() == gig.ArtistId) ? true : false;
            }

            return View("Details", viewModel);
        }
    }
}