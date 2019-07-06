using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaNet.DataAccess.Identity;
using InstaNet.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InstaNet.Web.Pages.Account
{
    [ValidateAntiForgeryToken]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (LoginViewModel.Email.IndexOf("@") > -1)
            {
                var user = await this.userManager.FindByEmailAsync(LoginViewModel.Email);

                if (user != null)
                {
                    var result = await this.signInManager.PasswordSignInAsync(user, LoginViewModel.Password, true, true);

                    if (result.Succeeded)
                    {
                        Console.WriteLine("succ");
                        return RedirectToPage("/Index");
                    } else
                    {
                        ModelState.AddModelError("", "Something dosen´t work");
                    }
                } else
                {
                    ModelState.AddModelError("", "User not exists");
                }
            }

            return Page();
        }
    }
}