using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Models.DTOs
{
    public class AdminSubjectDTO : SubjectDTO
    {
        public TeacherDTO[] TeachersName { get; set; }

        public AdminSubjectDTO(int id, string subjectName, TeacherDTO[] teachersName) : base(id,subjectName)
        {
            TeachersName = teachersName;
        }
    }
}
