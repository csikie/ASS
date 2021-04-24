using System;
using System.ComponentModel.DataAnnotations;

namespace ASS.WEB.Models.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), Name = "RealName")]
        public string RealName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), Name = "UserName")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), ErrorMessageResourceName = "WrongEmail")]
        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), Name = "Password")]
        [Compare("ConfirmPassword", ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), ErrorMessageResourceName = "ComparePasswordError")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), ErrorMessageResourceName = "ComparePasswordError")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), Name = "Roles")]
        public string[] Roles { get; set; }

        public CreateUserViewModel(){}
    }
}
