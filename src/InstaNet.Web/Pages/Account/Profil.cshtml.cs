using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaNet.ApplicationCore.Entities;
using InstaNet.DataAccess.Data;
using InstaNet.DataAccess.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InstaNet.Web.Pages.Account
{
    [Authorize]
    public class ProfilModel : PageModel
    {
        private readonly RepositoryContext repositoryContext;
        private readonly UserManager<ApplicationUser> userManager;

        public ProfilModel(RepositoryContext repositoryContext, UserManager<ApplicationUser> userManager)
        {
            this.repositoryContext = repositoryContext;
            this.userManager = userManager;
        }

        [BindProperty]
        public Profil Profil { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            this.Profil = await this.repositoryContext.Profils.FindAsync(Guid.Parse(id));
            return Page();
        }

        public async Task<IActionResult> OnGetFollow(string id)
        {
            var currentUser = await this.userManager.FindByNameAsync(User.Identity.Name);

            var result = currentUser.Profil.Following
                .FirstOrDefault(x => x.FollowingId == currentUser.ProfilId && x.FollowerId == Guid.Parse(id));

            if (result == null)
            {
                await this.repositoryContext.Follows.AddAsync(new Follow
                {
                    Id = Guid.NewGuid(),
                    FollowerId = Guid.Parse(id),
                    FollowingId = currentUser.ProfilId,
                });
            } else
            {
                this.repositoryContext.Follows.Remove(result);
            }

            await this.repositoryContext.SaveChangesAsync();

            return RedirectToPage("/Account/Profil", new { Id = id });
        }
    }
}