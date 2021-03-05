using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ASS.DAL.Models
{
    public class User : IdentityUser<int>
    {
        public string RealName { get; set; }
        public User() : base() { }
        public User(string neptunCode, string name) : base(neptunCode)
        {
            RealName = name;
        }

        public ICollection<UserCourse> UserCourses { get; set; }
        public ICollection<UserSubject> UserSubject { get; set; }
        public ICollection<Instructors> Instructors { get; set; }
    }
}
