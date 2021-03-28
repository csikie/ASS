using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Models.ViewModels
{
    public class SolutionSubmissionViewModel
    {
        public int Id { get; set; }
        public string SubmittedSolution { get; set; }

        public SolutionSubmissionViewModel() { }
        public SolutionSubmissionViewModel(int id)
        {
            Id = id;
        }
    }
}
