using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASS.DAL.Models
{
    public class Subject
    {
        public Subject() { }
        public Subject(string name)
        {
            Name = name;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Course> Courses { get; set; }
        public ICollection<UserSubject> UserSubject { get; set; }
    }
}
