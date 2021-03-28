using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Models.DTOs
{
    public class SolutionDTO
    {
        public int Id { get; set; }
        public string SubmittedSolution { get; set; }
        public DateTime SubmissionTime { get; set; }
        public string Grade { get; set; }
        public DateTime EvaluationTime { get; set; }

        public SolutionDTO(int id, string submittedSolution, DateTime submissionTime, string grade, DateTime evaluationTime)
        {
            Id = id;
            SubmittedSolution = submittedSolution;
            SubmissionTime = submissionTime;
            Grade = grade;
            EvaluationTime = evaluationTime;
        }
    }
}
