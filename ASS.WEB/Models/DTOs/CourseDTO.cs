using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Models.DTOs
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public InstructorDTO[] Instructors { get; set; }

        public CourseDTO(int id, string courseName, InstructorDTO[] instructors)
        {
            Id = id;
            CourseName = courseName;
            Instructors = instructors;
        }
    }
}
