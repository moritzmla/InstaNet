using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace InstaNet.ApplicationCore.Entities
{
    public class Replay : BaseEntity
    {
        private Post _post;
        private Profile _profile;

        public Replay()
        {

        }

        private ILazyLoader LazyLoader { get; set; }

        public Replay(ILazyLoader lazyLoader)
        {
            this.LazyLoader = lazyLoader;
        }

        public string Text { get; set; }

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
