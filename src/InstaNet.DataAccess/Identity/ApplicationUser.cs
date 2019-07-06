using InstaNet.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaNet.DataAccess.Identity
{
    public class ApplicationUser : IdentityUser
    {
        private Profil _profil;

        public ApplicationUser()
        {
        }

        private ILazyLoader LazyLoader { get; set; }

        public ApplicationUser(ILazyLoader lazyLoader)
        {
            this.LazyLoader = lazyLoader;
        }

        public Guid? ProfilId { get; set; }
        public Profil Profil
        {
            get => this.LazyLoader.Load(this, ref _profil);
            set => _profil = value;
        }
    }
}
