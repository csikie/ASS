using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASS.DAL.Models
{
    public class Course
    {
        public Course() { }
        public Course(string name, Subject subject)
        {
            Name = name;
            Subject = subject;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public ICollection<UserCourse> UserCourses { get; set; }
        public ICollection<Instructors> Instructors { get; set; }
    }
}
