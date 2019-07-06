using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaNet.ApplicationCore.Entities
{
    public class Follow : BaseEntity
    {
        private Profil _follower;
        private Profil _following;

        public Follow()
        {
        }

        private ILazyLoader LazyLoader { get; set; }

        public Follow(ILazyLoader lazyLoader)
        {
            this.LazyLoader = lazyLoader;
        }

        public Guid? FollowerId { get; set; }
        public Profil Follower
        {
            get => this.LazyLoader.Load(this, ref _follower);
            set => _follower = value;
        }

        public Guid? FollowingId { get; set; }
        public Profil Following
        {
            get => this.LazyLoader.Load(this, ref _following);
            set => _following = value;
        }
    }
}
