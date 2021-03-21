using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ASS.DAL.Models
{
    public class Solution
    {
        [Key]
        public int Id { get; set; }
        public string SubmittedSolution { get; set; }

        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
