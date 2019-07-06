using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaNet.ApplicationCore.Entities
{
    public class Replay : BaseEntity
    {
        private Post _post;
        private Profil _profil;

        public Replay()
        {

        }

        private ILazyLoader LazyLoader { get; set; }

        public Replay(ILazyLoader lazyLoader)
        {
            this.LazyLoader = lazyLoader; 
        }

        public string Text { get; set; }

        public Guid? ProfilId { get; set; }
        public Profil Profil
        {
            get => this.LazyLoader.Load(this, ref _profil);
            set => _profil = value;
        }

        public Guid? PostId { get; set; }
        public Post Post
        {
            get => this.LazyLoader.Load(this, ref _post);
            set => _post = value;
        }

        public string TimeAgo()
        {
            string result = string.Empty;
            var timeSpan = DateTime.Now.Subtract(this.Created);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0} seconds ago", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ?
                    String.Format("{0} minutes ago", timeSpan.Minutes) :
                    "a minute ago";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ?
                    String.Format("{0} hours ago", timeSpan.Hours) :
                    "an hour ago";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ?
                    String.Format("{0} days ago", timeSpan.Days) :
                    "yesterday";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30 ?
                    String.Format("{0} months ago", timeSpan.Days / 30) :
                    "a month ago";
            }
            else
            {
                result = timeSpan.Days > 365 ?
                    String.Format("{0} years ago", timeSpan.Days / 365) :
                    "a year ago";
            }

            return result;
        }
    }
}
