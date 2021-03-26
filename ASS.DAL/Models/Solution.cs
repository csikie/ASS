using System;
using System.ComponentModel.DataAnnotations;

namespace ASS.DAL.Models
{
    public class Solution
    {
        [Key]
        public int Id { get; set; }
        public string SubmittedSolution { get; set; }
        public DateTime SubmissionTime { get; set; }
        public string Grade { get; set; }
        public DateTime EvaluationTime { get; set; }

        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
