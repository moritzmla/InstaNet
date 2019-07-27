using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InstaNet.ApplicationCore.Entities;
using InstaNet.DataAccess.Data;
using InstaNet.DataAccess.Identity;
using InstaNet.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InstaNet.Web.Pages.Account
{
    [Authorize, ValidateAntiForgeryToken]
    public class SettingsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RepositoryContext repositoryContext;

        public SettingsModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RepositoryContext repositoryContext)
        {
            this.userManager = userManager;
            this.repositoryContext = repositoryContext;
        }

        [BindProperty]
        public Profile Profile { get; set; }

        [BindProperty]
        public ProfileViewModel ProfilViewModel { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            this.Profile = await this.repositoryContext.Profiles.FindAsync(Guid.Parse(id));
            var currentUser = await this.userManager.FindByNameAsync(User.Identity.Name);

            if (currentUser.ProfileId != this.Profile.Id)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSave(string id)
        {
            if (ModelState.IsValid)
            {
                var editProfil = await this.repositoryContext.Profiles.FindAsync(Guid.Parse(id));

                editProfil.DisplayName = this.ProfilViewModel.DisplayName;
                editProfil.Bio = this.ProfilViewModel.Bio;
                editProfil.Modified = DateTime.Now;

                if (this.ProfilViewModel.Image != null)
                {
                    editProfil.Image = FileToByteArray(this.ProfilViewModel.Image);
                }

                this.repositoryContext.Profiles.Update(editProfil);

                await this.repositoryContext.SaveChangesAsync();

                return RedirectToPage("/Account/Profile", new { Id = id });
            }
            return RedirectToPage("/Account/Settings", new { Id = id });
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