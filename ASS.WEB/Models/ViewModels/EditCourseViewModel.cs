using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASS.WEB.Models.ViewModels
{
    public class EditCourseViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateCourseViewModel), ErrorMessageResourceName = "InstructorUserNamesRequired")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.EditCourseViewModel), Name = "CourseName")]
        [StringLength(maximumLength: 30, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.Models.ViewModels.EditCourseViewModel), ErrorMessageResourceName = "CourseNameLengthMessage")]
        public string CourseName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateCourseViewModel), ErrorMessageResourceName = "InstructorUserNamesRequired")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.EditCourseViewModel), Name = "InstructorUserNames")]
        public string[] InstructorUserNames { get; set; }

        [NotMapped]
        public string CurrentInstructorsJSON { get; set; }

        public EditCourseViewModel() { }

        public EditCourseViewModel(int id, string courseName)
        {
            Id = id;
            CourseName = courseName;
        }
    }
}
