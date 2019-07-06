using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaNet.ApplicationCore.Entities
{
    public class Post : BaseEntity
    {
        private ICollection<Picture> _pictures;
        private ICollection<Video> _videos;
        private ICollection<Replay> _replays;
        private ICollection<Like> _likes;
        private Profil _profil;

        public Post()
        {
        }

        private ILazyLoader LazyLoader { get; set; }

        public Post(ILazyLoader lazyLoader)
        {
            this.LazyLoader = lazyLoader;
        }

        public string Caption { get; set; }

        public Guid? ProfilId { get; set; }
        public Profil Profil
        {
            get => this.LazyLoader.Load(this, ref _profil);
            set => _profil = value;
        }

        public ICollection<Like> Likes
        {
            get => this.LazyLoader.Load(this, ref _likes);
            set => _likes = value;
        }

        public ICollection<Picture> Pictures
        {
            get => this.LazyLoader.Load(this, ref _pictures);
            set => _pictures = value;
        }

        public ICollection<Replay> Replays
        {
            get => this.LazyLoader.Load(this, ref _replays);
            set => _replays = value;
        }

        public ICollection<Video> Videos
        {
            get => this.LazyLoader.Load(this, ref _videos);
            set => _videos = value;
        }
    }
}
