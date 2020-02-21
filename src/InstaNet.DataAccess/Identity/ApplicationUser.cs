using InstaNet.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace InstaNet.DataAccess.Identity
{
    public class ApplicationUser : IdentityUser
    {
        private Profile _profile;

        public ApplicationUser()
        {
        }

        private ILazyLoader LazyLoader { get; set; }

        public ApplicationUser(ILazyLoader lazyLoader)
        {
            this.LazyLoader = lazyLoader;
        }

        public Guid? ProfileId { get; set; }
        public Profile Profile
        {
            get => this.LazyLoader.Load(this, ref _profile);
            set => _profile = value;
        }
    }
}
