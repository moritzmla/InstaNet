using System;
using System.ComponentModel.DataAnnotations;

namespace InstaNet.ApplicationCore.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public string TimeAgo()
        {
            string result = string.Empty;
            var timeSpan = DateTime.Now.Subtract(this.Created);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0} seconds", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ?
                    String.Format("{0} minutes", timeSpan.Minutes) :
                    "a minute ago";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ?
                    String.Format("{0} hours", timeSpan.Hours) :
                    "an hour ago";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ?
                    String.Format("{0} days", timeSpan.Days) :
                    "yesterday";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30 ?
                    String.Format("{0} months", timeSpan.Days / 30) :
                    "a month ago";
            }
            else
            {
                result = timeSpan.Days > 365 ?
                    String.Format("{0} years", timeSpan.Days / 365) :
                    "a year ago";
            }

            return result;
        }
    }
}
