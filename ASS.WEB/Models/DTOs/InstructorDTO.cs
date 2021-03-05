using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Models.DTOs
{
    public class InstructorDTO
    {
        public int Id { get; set; }
        public string InstructorName { get; set; }
        public string InstructorNeptun { get; set; }

        public InstructorDTO(int id, string instructorName, string instructorNeptun)
        {
            Id = id;
            InstructorName = instructorName;
            InstructorNeptun = instructorNeptun;
        }
    }
}
