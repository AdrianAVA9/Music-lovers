using LiveMusicLovers.Web.UI.Core.Models;
using System.Collections.Generic;

namespace LiveMusicLovers.Web.UI.Core.Repositories
{
    public interface IRelationshipRepository
    {
        Relationship IsFollowing(string followeedId, string followerId);
        IEnumerable<Relationship> GetFollowers(string followee);
        void Unfollow(Relationship relationship);
        void FollowArtist(Relationship relationship);
    }
}