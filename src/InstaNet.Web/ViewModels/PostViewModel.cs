using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstaNet.Web.ViewModels
{
    public class PostViewModel
    {
        [Required]
        public IFormFile Picture { get; set; }

        public string Caption { get; set; }
    }
}
