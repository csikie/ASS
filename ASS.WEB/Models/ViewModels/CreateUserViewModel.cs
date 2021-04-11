using System;
using System.ComponentModel.DataAnnotations;

namespace ASS.WEB.Models.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        public string RealName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string[] Roles { get; set; }

        public CreateUserViewModel(){}
    }
}
