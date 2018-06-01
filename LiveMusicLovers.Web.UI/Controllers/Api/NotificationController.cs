using AutoMapper;
using LiveMusicLovers.Web.UI.Core;
using LiveMusicLovers.Web.UI.Core.Dto;
using LiveMusicLovers.Web.UI.Core.Models;
using LiveMusicLovers.Web.UI.Persistence;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebGrease.Css.Extensions;

namespace LiveMusicLovers.Web.UI.Controllers.Api
{
    [Authorize]
    public class NotificationController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        public NotificationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var notification = _unitOfWork.UserNotifications.GetUserNotification(User.Identity.GetUserId());

            notification.ForEach(n => n.Read());

            _unitOfWork.Complete();

            return Ok();
        }

        [HttpGet]
        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var notification = _unitOfWork.Notification.GetNewUserNotification(User.Identity.GetUserId());
            
            return notification.Select(Mapper.Map<Notification,NotificationDto>);
        }
    }
}
