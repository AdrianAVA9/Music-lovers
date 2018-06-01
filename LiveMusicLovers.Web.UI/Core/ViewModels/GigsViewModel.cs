using System.Collections.Generic;
using System.Linq;
using LiveMusicLovers.Web.UI.Core.Models;

namespace LiveMusicLovers.Web.UI.Core.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public bool showAction { get; set; }
        public string Heading { get; set; }
        public string SearchTeam { get; set; }
        public ILookup<int, Attendance> Attendances { get; set; }
    }
}