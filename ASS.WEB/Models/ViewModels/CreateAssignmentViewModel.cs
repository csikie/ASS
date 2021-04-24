using System;
using System.ComponentModel.DataAnnotations;

namespace ASS.WEB.Models.ViewModels
{
    public class CreateAssignmentViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateAssignmentViewModel), ErrorMessageResourceName = "NameRequired")]
        [Display(ResourceType =  typeof(Resources.Models.ViewModels.CreateAssignmentViewModel), Name = "AssignmentName")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateAssignmentViewModel), ErrorMessageResourceName = "DescriptionRequired")]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.CreateAssignmentViewModel), Name = "DescriptionName")]
        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateAssignmentViewModel), ErrorMessageResourceName = "StartDateRequired")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.CreateAssignmentViewModel), Name = "StartDateName")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateAssignmentViewModel), ErrorMessageResourceName = "EndDateRequired")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.CreateAssignmentViewModel), Name = "EndDateName")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateAssignmentViewModel), ErrorMessageResourceName = "CourseIdsRequired")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.CreateAssignmentViewModel), Name = "CourseIdsName")]
        public int[] CourseIds { get; set; }

        public CreateAssignmentViewModel()
        {
            Description = Resources.Models.ViewModels.CreateAssignmentViewModel.DescriptionTemplate;
        }
    }
}
