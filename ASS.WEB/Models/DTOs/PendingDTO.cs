using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Models.DTOs
{
    public class PendingDTO
    {
        public int UserCourseId { get; set; }
        public string StudentName { get; set; }
        public string StudentNeptunCode { get; set; }
        public string CourseName { get; set; }
        public string SubjectName { get; set; }

        public PendingDTO(int userCourseId, string studentName, string studentNeptunCode, string courseName, string subjectName)
        {
            UserCourseId = userCourseId;
            StudentName = studentName;
            StudentNeptunCode = studentNeptunCode;
            CourseName = courseName;
            SubjectName = subjectName;
        }
    }
}
