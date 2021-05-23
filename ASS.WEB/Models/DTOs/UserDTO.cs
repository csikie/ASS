using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASS.WEB.Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(Resources.Models.DTOs.UserDTO), Name = "RealName")]
        public string RealName { get; set; }

        [Display(ResourceType = typeof(Resources.Models.DTOs.UserDTO), Name = "UserName")]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(Resources.Models.DTOs.UserDTO), Name = "Email")]
        public string Email { get; set; }

        [Display(ResourceType = typeof(Resources.Models.DTOs.UserDTO), Name = "Roles")]
        public string[] Roles { get; set; }

        public List<SolutionDTO> Solutions { get; set; }

        public UserDTO(string realName, string username, string email, string[] roles)
        {
            RealName = realName;
            UserName = username;
            Email = email;
            Roles = roles;
        }
    }
}
