using AutoMapper;
using LiveMusicLovers.Web.UI.Controllers.Api;
using LiveMusicLovers.Web.UI.Core.Dto;
using LiveMusicLovers.Web.UI.Core.Models;

namespace LiveMusicLovers.Web.UI.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Gig, GigDto>();
            CreateMap<Notification, NotificationDto>();
        }

    }
}