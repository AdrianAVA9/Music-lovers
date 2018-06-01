using System;
using System.ComponentModel.DataAnnotations;

namespace LiveMusicLovers.Web.UI.Core.Models
{
    public class Notification
    {
        protected Notification() { }

        private  Notification(Gig gig, NotificationType type)
        {
            Gig = gig ?? throw new ArgumentNullException(nameof(gig));
            Type = type;
            DateTime = DateTime.Now;
        }

        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }
        [Required]
        public Gig Gig { get; private set; }

        public static Notification GigCreated(Gig gig, string originalVenue, DateTime dateTime)
        {
            var notification = new Notification(gig, NotificationType.GigCreated);
            notification.OriginalDateTime = dateTime;
            notification.OriginalVenue = originalVenue;

            return notification;
        }

        public static Notification GigUpdated(Gig gig, string originalVenue, DateTime originalDateTime)
        {
            var notification = new Notification(gig, NotificationType.GigUpdated);
            notification.OriginalVenue = originalVenue;
            notification.OriginalDateTime = originalDateTime;

            return notification;
        }

        public static Notification GigCanceleted(Gig gig)
        {
            var notification = new Notification(gig, NotificationType.GigCanceled);

            return notification;
        }
    }
}