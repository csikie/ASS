using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Models.ViewModels
{
    public class CreateSubjectViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Models.ViewModels.CreateSubjectViewModel), ErrorMessageResourceName = "SubjectNameRequired")]
        [Display(ResourceType = typeof(Resources.Models.ViewModels.CreateSubjectViewModel), Name = "SubjectName")]
        public string SubjectName { get; set; }

        [Display(ResourceType = typeof(Resources.Models.ViewModels.CreateSubjectViewModel), Name = "TeachersNeptunCode")]
        public string[] TeachersNeptunCode { get; set; }
    }
}
