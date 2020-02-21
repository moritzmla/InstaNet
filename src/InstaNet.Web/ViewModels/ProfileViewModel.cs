using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace InstaNet.Web.ViewModels
{
    public class ProfileViewModel
    {
        public IFormFile Image { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public string Bio { get; set; }
    }
}
