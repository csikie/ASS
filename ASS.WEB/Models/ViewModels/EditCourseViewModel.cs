using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Models.ViewModels
{
    public class EditCourseViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string CourseName { get; set; }

        [Required]
        public string[] InstructorUserNames { get; set; }
        
        public string CurrentInstructorsJSON { get; set; }

        public EditCourseViewModel() { }

        public EditCourseViewModel(int id, string courseName)
        {
            Id = id;
            CourseName = courseName;
        }
    }
}
