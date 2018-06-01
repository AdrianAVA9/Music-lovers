using LiveMusicLovers.Web.UI.Core;
using LiveMusicLovers.Web.UI.Core.Dto;
using LiveMusicLovers.Web.UI.Core.Models;
using LiveMusicLovers.Web.UI.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace LiveMusicLovers.Web.UI.Controllers.Api
{
    [Authorize]
    public class RelationshipsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RelationshipsController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        public RelationshipsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {

            var relationship = _unitOfWork.Relationships.IsFollowing(id, User.Identity.GetUserId());

            if (relationship == null)
                return NotFound();

            _unitOfWork.Relationships.Unfollow(relationship);
            _unitOfWork.Complete();

            return Ok(id);
        }


        [HttpPost]
        public IHttpActionResult Relationship(RelationshipDto dto)
        {
            var userId = User.Identity.GetUserId();

            var result = _unitOfWork.Relationships.IsFollowing(dto.FolloweeId, userId);

            if (result != null)
                return BadRequest("Relationship already exists");

            var relationship = new Relationship()
            {
                FollowerId = userId,
                Followeeid = dto.FolloweeId,
            };

            _unitOfWork.Relationships.FollowArtist(relationship);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
