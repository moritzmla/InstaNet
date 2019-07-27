using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaNet.ApplicationCore.Entities;
using InstaNet.DataAccess.Data;
using InstaNet.DataAccess.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InstaNet.Web.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly RepositoryContext repositoryContext;
        private readonly UserManager<ApplicationUser> userManager;

        public IndexModel(RepositoryContext repositoryContext, UserManager<ApplicationUser> userManager)
        {
            this.repositoryContext = repositoryContext;
            this.userManager = userManager;
        }

        [BindProperty]
        public IReadOnlyList<Post> Timeline { get; set; }

        [BindProperty]
        public Replay Replay { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var currentUser = await this.userManager.FindByNameAsync(User.Identity.Name);
            var following = currentUser.Profile.Following.Select(x => x.FollowerId);

            this.Timeline = this.repositoryContext.Posts
                .Where(x => following.Contains(x.ProfileId) || x.ProfileId == currentUser.ProfileId)
                .Take(20).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostReplay(string id)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await this.userManager.FindByNameAsync(User.Identity.Name);

                this.Replay.Id = Guid.NewGuid();
                this.Replay.Profile = currentUser.Profile;
                this.Replay.Post = this.repositoryContext.Posts.FirstOrDefault(x => x.Id.ToString() == id);

                await this.repositoryContext.Replays.AddAsync(Replay);
                await this.repositoryContext.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetLike(string id)
        {
            var currentUser = await this.userManager.FindByNameAsync(User.Identity.Name);

            var result = this.repositoryContext.Likes
                .FirstOrDefault(x => x.ProfileId == currentUser.ProfileId && x.PostId == Guid.Parse(id));

            if (result == null)
            {
                await this.repositoryContext.Likes.AddAsync(new Like
                {
                    Id = Guid.NewGuid(),
                    PostId = Guid.Parse(id),
                    ProfileId = currentUser.Profile.Id
                });
            } else
            {
                this.repositoryContext.Likes.Remove(result);
            }

            await this.repositoryContext.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetLogOut()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToPage("/Index");
        }
    }
}