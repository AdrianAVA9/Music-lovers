using LiveMusicLovers.Web.UI.Core;
using LiveMusicLovers.Web.UI.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace LiveMusicLovers.Web.UI.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private IUnitOfWork _unitOfWork { get; set; }

        public GigsController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var gig = _unitOfWork.Gigs.GetGigWithAttendace(id);

            if (gig == null || gig.IsCanceled)
                return NotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return Unauthorized();

            gig.Cancel();

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
