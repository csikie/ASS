using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ASS.DAL.Models
{
    public class User : IdentityUser<int>
    {
        public string RealName { get; set; }
        public User() : base() { }
        public User(string neptunCode, string name, string email) : base(neptunCode)
        {
            RealName = name;
            Email = email;
        }

        public ICollection<UserCourse> UserCourses { get; set; }
        public ICollection<UserSubject> UserSubject { get; set; }
        public ICollection<Instructors> Instructors { get; set; }
        public ICollection<Solution> Solutions { get; set; }
    }
}
