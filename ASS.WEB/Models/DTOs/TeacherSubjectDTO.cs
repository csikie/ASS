using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Models.DTOs
{
    public class TeacherSubjectDTO : SubjectDTO
    {
        public CourseDTO[] Courses { get; set; }

        public TeacherSubjectDTO(int id, string subjectName, CourseDTO[] courses) : base(id,subjectName)
        {
            Courses = courses;
        }
    }
}
