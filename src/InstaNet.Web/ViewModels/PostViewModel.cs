using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace InstaNet.Web.ViewModels
{
    public class PostViewModel
    {
        [Required]
        public IFormFile Picture { get; set; }

        public string Caption { get; set; }
    }
}
