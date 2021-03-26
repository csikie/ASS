using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Models.ViewModels
{
    public class SolutionSubmissionViewModel
    {
        [Required]
        public string SubmittedSolution { get; set; }
        [Required]
        public DateTime SubmissionTime { get; set; }
    }
}
