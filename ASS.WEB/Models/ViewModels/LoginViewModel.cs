using System;
using System.ComponentModel.DataAnnotations;

namespace ASS.WEB.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.LoginViewModel),ErrorMessageResourceName = "UsernameRequired")]
        [StringLength(maximumLength: 6, MinimumLength = 5, ErrorMessageResourceType = typeof(Resources.Models.ViewModels.LoginViewModel), ErrorMessageResourceName = "UsernameLengthMessage")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.LoginViewModel), Name = "Username")]
        public string Username { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.LoginViewModel), ErrorMessageResourceName = "PasswordRequired")]
        [StringLength(maximumLength: 30, ErrorMessageResourceType = typeof(Resources.Models.ViewModels.LoginViewModel),ErrorMessageResourceName = "PasswordLength")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.LoginViewModel), Name = "Password")]
        public string Password { get; set; }
    }
}
