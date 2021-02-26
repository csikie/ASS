using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Models.DTOs
{
    public class TeacherDTO
    {
        public int Id { get; set; }
        public string TeacherName { get; set; }
        public string TeacherNeptun { get; set; }

        public TeacherDTO(int id, string teacherName, string teacherNeptun)
        {
            Id = id;
            TeacherName = teacherName;
            TeacherNeptun = teacherNeptun;
        }
    }
}
