using LiveMusicLovers.Web.UI.Core;
using LiveMusicLovers.Web.UI.Core.Dto;
using LiveMusicLovers.Web.UI.Core.Models;
using LiveMusicLovers.Web.UI.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace LiveMusicLovers.Web.UI.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult CancelAttendance(int id)
        {
            var userId = User.Identity.GetUserId();
            var attendance = _unitOfWork.Attendances.IsGoing(id, userId);

            if (attendance == null)
                return NotFound();

            if (attendance.Gig.IsCanceled)
                return BadRequest("The gig is canceled");

            _unitOfWork.Attendances.CancelAttendance(attendance);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            var attendance = _unitOfWork.Attendances.IsGoing(dto.GigId, userId);

            if (attendance != null)
                return BadRequest("The attendance already exists or gig does not exists");

            attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId,
            };

            _unitOfWork.Attendances.Add(attendance);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
