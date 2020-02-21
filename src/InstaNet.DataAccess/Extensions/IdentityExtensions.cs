using System;
using System.Security.Claims;
using System.Security.Principal;

namespace InstaNet.DataAccess.Extensions
{
    public static class IdentityExtensions
    {
        public static Guid GetProfileId(this IIdentity identity)
        {
            var profileId = (identity as ClaimsIdentity).FindFirst("ProfileId");
            return (profileId != null) ? Guid.Parse(profileId.Value) : Guid.Empty;
        }

        public static string GetProfileName(this IIdentity identity)
        {
            var profileName = (identity as ClaimsIdentity).FindFirst("ProfileName");
            return (profileName != null) ? profileName.Value : string.Empty;
        }

        public static string GetProfileImage(this IIdentity identity)
        {
            var profileImage = (identity as ClaimsIdentity).FindFirst("ProfileImage");
            return (profileImage != null) ? profileImage.Value : string.Empty;
        }
    }
}
