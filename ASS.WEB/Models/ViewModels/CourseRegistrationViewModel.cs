using System.ComponentModel.DataAnnotations;

namespace ASS.WEB.Models.ViewModels
{
    public class CourseRegistrationViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CourseRegistrationViewModel), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.CourseRegistrationViewModel), Name = "Name")]
        public int[] CourseIds { get; set; }
    }
}
