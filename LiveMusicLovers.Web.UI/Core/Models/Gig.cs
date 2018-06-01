using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace LiveMusicLovers.Web.UI.Core.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCanceled { get; private set; }

        public ApplicationUser Artist { get; set; }

        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public Genre Genre { get; set; }

        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public string getNameMonth()
        {
            DateTimeFormatInfo cultureInfo = new CultureInfo("en-US").DateTimeFormat;

            return cultureInfo.GetMonthName(DateTime.Month);
        }

        public string getDateTimeString()
        {
            DateTimeFormatInfo cultureInfo = new CultureInfo("en-US").DateTimeFormat;

            return string.Concat($"{DateTime.Day} {(cultureInfo.GetMonthName(DateTime.Month)).Substring(0,3)} {DateTime.Year}");
        }

        public void NotifyForANewGig(IEnumerable<Relationship> followers)
        {
            var notification = Notification.GigCreated(this, Venue, DateTime);

            if (followers == null) return;

            foreach (var relationship in followers)
            {
                relationship.Follower.Notify(notification);
            }

        }

        public void Cancel()
        {
            IsCanceled = true;

            var notification = Notification.GigCanceleted(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        public void Modify(DateTime dateTime, string venue, byte genre)
        {
            var notification = Notification.GigUpdated(this, Venue, DateTime);

            DateTime = dateTime;
            GenreId = genre;
            Venue = venue;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}