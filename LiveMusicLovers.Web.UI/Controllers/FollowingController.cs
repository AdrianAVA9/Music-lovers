using LiveMusicLovers.Web.UI.Persistence;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace LiveMusicLovers.Web.UI.Controllers
{
    public class FollowingController : Controller
    {
        private ApplicationDbContext _context { get; set; }

        public FollowingController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var artist = _context.Relationships
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();

            return View(artist);
        }
    }
}