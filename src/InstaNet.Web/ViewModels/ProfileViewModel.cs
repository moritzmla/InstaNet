using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
