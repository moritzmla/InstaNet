using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaNet.ApplicationCore.Entities
{
    public class Like : BaseEntity
    {
        private Profil _profil;
        private Post _post;

        public Like()
        {
        }

        private ILazyLoader LazyLoader { get; set; }

        public Like(ILazyLoader lazyLoader)
        {
            this.LazyLoader = lazyLoader;
        }

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
    }
}
