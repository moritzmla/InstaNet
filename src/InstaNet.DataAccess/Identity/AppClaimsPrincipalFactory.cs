using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InstaNet.DataAccess.Identity
{
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public AppClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor) { }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            if (user.ProfileId != null)
            {
                (principal.Identity as ClaimsIdentity).AddClaims(new[] {
                    new Claim("ProfileId", user.ProfileId.ToString()),
                    new Claim("AuthorName", user.Profile.UserName),
                    new Claim("ProfileImage", Convert.ToBase64String(user.Profile.Image))
                });
            }

            return principal;
        }
    }
}
