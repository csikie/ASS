using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Models.DTOs
{
    public class StudentCoursesDTO : CourseDTO
    {
        public new AssignmentDTO[] Assignments { get; set; }
        public StudentCoursesDTO(int id, string courseName, AssignmentDTO[] assignments) : base(id, courseName, null)
        {
            Assignments = assignments;
        }
    }
}
