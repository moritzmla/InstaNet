using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaNet.ApplicationCore.Entities;
using InstaNet.DataAccess.Data;
using InstaNet.DataAccess.Identity;
using InstaNet.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InstaNet.Web.Pages.Account
{
    [ValidateAntiForgeryToken]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RepositoryContext repositoryContext;

        public RegisterModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RepositoryContext repositoryContext)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.repositoryContext = repositoryContext;
        }

        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if ((RegisterViewModel.Email.IndexOf("@") > -1) && (RegisterViewModel.Password.Equals(RegisterViewModel.ConfirmPassword)))
            {
                var user = await this.userManager.FindByEmailAsync(RegisterViewModel.Email);

                if (user == null)
                {
                    Profil profil = new Profil
                    {
                        UserName = RegisterViewModel.UserName,
                        DisplayName = RegisterViewModel.UserName,
                        Bio = RegisterViewModel.UserName + "´s Bio",
                        Image = await System.IO.File.ReadAllBytesAsync(".//wwwroot//Images//User.PNG"),
                        Created = DateTime.Now,
                        Modified = DateTime.Now
                    };

                    await repositoryContext.Profils.AddAsync(profil);
                    await repositoryContext.SaveChangesAsync();

                    ApplicationUser applicationUser = new ApplicationUser
                    {
                        Email = RegisterViewModel.Email,
                        UserName = RegisterViewModel.UserName,
                        Profil = profil
                    };

                    var result = await this.userManager.CreateAsync(applicationUser, RegisterViewModel.Password);

                    if (result.Succeeded)
                    {
                        await this.signInManager.PasswordSignInAsync(applicationUser, RegisterViewModel.Password, true, true);
                        return RedirectToPage("/Index");
                    } else
                    {
                        ModelState.AddModelError("", "Something dosen´t work");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User already exists");
                }
            }
            else
            {
                ModelState.AddModelError("", "Passwords don´t match");
            }

            return Page();
        }
    }
}