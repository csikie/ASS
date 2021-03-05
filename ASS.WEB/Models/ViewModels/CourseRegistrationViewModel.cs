using System.ComponentModel.DataAnnotations;

namespace ASS.WEB.Models.ViewModels
{
    public class CourseRegistrationViewModel
    {
        [Required]
        public int[] CourseIds { get; set; }
    }
}
