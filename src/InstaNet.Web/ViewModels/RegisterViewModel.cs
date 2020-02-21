using System.ComponentModel.DataAnnotations;

namespace InstaNet.Web.ViewModels
{
    public class RegisterViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
