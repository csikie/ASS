using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ASS.DAL.Models
{
    public class User : IdentityUser<int>
    {
        public User() : base() { }
        public User(string name) : base(name)
        {

        }

        public User(string neptunCode, string name) : base(neptunCode)
        {
            NormalizedUserName = name;
        }

        public ICollection<UserCourse> UserCourses { get; set; }
        public ICollection<UserSubject> UserSubject { get; set; }
    }
}
