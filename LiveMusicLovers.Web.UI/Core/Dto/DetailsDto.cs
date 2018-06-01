using LiveMusicLovers.Web.UI.Core.Models;

namespace LiveMusicLovers.Web.UI.Core.Dto
{
    public class DetailsDto
    {
        public Gig Gig { get; set; }
        public bool AmI { get; set; }
        public bool AmIFollowing { get; set; }
        public bool AmIGoing { set; get; }
        public string path { get; set; }

        public DetailsDto()
        {
            AmIFollowing = false;
            AmIGoing = false;
            path = "~/Upload/Image/";
        }
    }
}