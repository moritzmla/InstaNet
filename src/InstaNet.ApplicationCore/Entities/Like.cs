using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaNet.ApplicationCore.Entities
{
    public class Like : BaseEntity
    {
        private Profile _profile;
        private Post _post;

        public Like()
        {
        }

        private ILazyLoader LazyLoader { get; set; }

        public Like(ILazyLoader lazyLoader)
        {
            this.LazyLoader = lazyLoader;
        }

        public Guid? ProfileId { get; set; }
        public Profile Profile
        {
            get => this.LazyLoader.Load(this, ref _profile);
            set => _profile = value;
        }

        public Guid? PostId { get; set; }
        public Post Post
        {
            get => this.LazyLoader.Load(this, ref _post);
            set => _post = value;
        }
    }
}
