using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Models.ViewModels
{
    public class CreateCourseViewModel
    {
        public string CourseName { get; set; }
        [Required]
        public int SubjectId { get; set; }
        public string[] InstructorsNeptunCode { get; set; }
    }
}
