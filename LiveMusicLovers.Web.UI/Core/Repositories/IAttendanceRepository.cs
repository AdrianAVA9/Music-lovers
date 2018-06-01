using LiveMusicLovers.Web.UI.Core.Models;
using System.Collections.Generic;

namespace LiveMusicLovers.Web.UI.Core.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        void CancelAttendance(Attendance attendance);
        Attendance IsGoing(int gigId, string userId);
        void Add(Attendance attendance);
    }
}