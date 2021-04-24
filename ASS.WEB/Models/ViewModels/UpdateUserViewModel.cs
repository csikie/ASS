using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASS.WEB.Models.ViewModels
{
    public class UpdateUserViewModel
    {
        [NotMapped]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), Name = "RealName")]
        public string RealName { get; set; }

        [NotMapped]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), Name = "UserName")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), ErrorMessageResourceName = "WrongEmail")]
        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.CreateUserViewModel), Name = "Roles")]
        public string[] Roles { get; set; }

        public UpdateUserViewModel() { }

        public UpdateUserViewModel(int id, string realName, string userName, string email, string[] roles)
        {
            Id = id;
            RealName = realName;
            UserName = userName;
            Email = email;
            Roles = roles;
        }
    }
}
