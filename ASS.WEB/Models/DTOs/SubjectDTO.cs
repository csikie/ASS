using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Models.DTOs
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public TeacherDTO[] TeachersName { get; set; }

        public SubjectDTO(int id, string subjectName, TeacherDTO[] teachersName)
        {
            Id = id;
            SubjectName = subjectName;
            TeachersName = teachersName;
        }
    }
}
