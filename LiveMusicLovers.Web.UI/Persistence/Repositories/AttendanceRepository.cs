using LiveMusicLovers.Web.UI.Core.Models;
using LiveMusicLovers.Web.UI.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LiveMusicLovers.Web.UI.Persistence.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList();
        }

        public void Add(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
        }

        public void CancelAttendance(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }

        public Attendance IsGoing(int gigId, string userId)
        {
            return _context.Attendances
                .Include(a => a.Attendee)
                .Include(a => a.Gig)
                .SingleOrDefault(a => a.AttendeeId == userId && a.GigId == gigId);
        }
    }
}