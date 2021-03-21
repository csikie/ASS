using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Models.ViewModels
{
    public class CreateAssignmentViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int[] CourseIds { get; set; }

        public CreateAssignmentViewModel()
        {
            Description = Resources.Models.ViewModels.CreateAssignmentViewModel.DescriptionTemplate;
        }
    }
}
