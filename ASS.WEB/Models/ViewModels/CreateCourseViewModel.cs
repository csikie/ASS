using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Models.ViewModels
{
    public class CreateCourseViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateCourseViewModel), ErrorMessageResourceName = "CourseNameRequired")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.CreateCourseViewModel), Name = "CourseName")]
        [StringLength(maximumLength: 30, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateCourseViewModel), ErrorMessageResourceName = "CourseNameLengthMessage")]
        public string CourseName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateCourseViewModel), ErrorMessageResourceName = "SubjectNameRequired")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.CreateCourseViewModel), Name = "SubjectName")]
        public int? SubjectId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateCourseViewModel), ErrorMessageResourceName = "InstructorUserNamesRequired")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.CreateCourseViewModel), Name = "InstructorUserNames")]
        public string[] InstructorsNeptunCode { get; set; }
    }
}
