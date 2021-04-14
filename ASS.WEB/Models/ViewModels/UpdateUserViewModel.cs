using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASS.WEB.Models.ViewModels
{
    public class UpdateUserViewModel
    {
        [NotMapped]
        public int Id { get; set; }

        [Required]
        public string RealName { get; set; }

        [NotMapped]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
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
