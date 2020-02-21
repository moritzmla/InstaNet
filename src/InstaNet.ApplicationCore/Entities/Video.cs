using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace InstaNet.ApplicationCore.Entities
{
    public class Video : BaseEntity
    {
        private Post _post;

        public Video()
        {

        }

        private ILazyLoader LazyLoader { get; set; }

        public Video(ILazyLoader lazyLoader)
        {
            this.LazyLoader = lazyLoader;
        }

        public byte[] File { get; set; }

        public Guid? PostId { get; set; }
        public Post Post
        {
            get => this.LazyLoader.Load(this, ref _post);
            set => _post = value;
        }
    }
}
