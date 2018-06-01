using LiveMusicLovers.Web.UI.Core.Models;
using LiveMusicLovers.Web.UI.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LiveMusicLovers.Web.UI.Persistence.Repositories
{
    public class RelationshipRepository : IRelationshipRepository
    {
        private ApplicationDbContext _context;

        public RelationshipRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Relationship> GetFollowers(string followee)
        {
            return _context.Relationships
                .Where(f => f.Followeeid == followee)
                .Include(f => f.Follower)
                .ToList();
        }

        public void FollowArtist(Relationship relationship)
        {
            _context.Relationships.Add(relationship);
        }

        public Relationship IsFollowing(string followeedId, string followerId)
        {
            return _context.Relationships
                .SingleOrDefault(r => r.Followeeid == followeedId && r.FollowerId == followerId);
        }

        public void Unfollow(Relationship relationship)
        {
            _context.Relationships.Remove(relationship);
        }
    }
}