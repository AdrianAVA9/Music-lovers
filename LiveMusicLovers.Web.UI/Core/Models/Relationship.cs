using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiveMusicLovers.Web.UI.Core.Models
{
    public class Relationship
    {
        [Key]
        [Column(Order = 1)]
        public string Followeeid { get; set; }

        [Key]
        [Column(Order = 2)]
        public string FollowerId { get; set; }

        public ApplicationUser Followee { get; set; }
        public ApplicationUser Follower { get; set; }
    }
}