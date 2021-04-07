using System.Collections.Generic;

namespace ASS.WEB.Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string RealName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
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
