using InstaNet.ApplicationCore.Entities;
using InstaNet.DataAccess.Data;
using InstaNet.DataAccess.Identity;
using InstaNet.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace InstaNet.Web.Pages.Content
{
    [Authorize, ValidateAntiForgeryToken]
    public class NewModel : PageModel
    {
        private readonly RepositoryContext repositoryContext;
        private readonly UserManager<ApplicationUser> userManager;

        public NewModel(RepositoryContext repositoryContext, UserManager<ApplicationUser> userManager)
        {
            this.repositoryContext = repositoryContext;
            this.userManager = userManager;
        }

        [BindProperty]
        public PostViewModel PostViewModel { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostNew(string id)
        {
            if (ModelState.IsValid)
            {
                var post = new Post
                {
                    Id = Guid.NewGuid(),
                    Caption = PostViewModel.Caption,
                    ProfileId = Guid.Parse(id),
                    Created = DateTime.Now,
                    Modified = DateTime.Now
                };

                await this.repositoryContext.Posts.AddAsync(post);

                var picture = await this.repositoryContext.Pictures.AddAsync(new Picture
                {
                    Id = Guid.NewGuid(),
                    File = FileToByteArray(PostViewModel.Picture),
                    PostId = post.Id,
                    Created = DateTime.Now,
                    Modified = DateTime.Now
                });

                await this.repositoryContext.SaveChangesAsync();

                return RedirectToPage("/Content/Details", new { Id = post.Id });
            }

            return Page();
        }

        private byte[] FileToByteArray(IFormFile _formFile)
        {
            using (MemoryStream _memoryStream = new MemoryStream())
            {
                _formFile.CopyTo(_memoryStream);
                return _memoryStream.ToArray();
            }
        }
    }
}