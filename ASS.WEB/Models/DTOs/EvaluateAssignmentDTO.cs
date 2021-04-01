using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Models.DTOs
{
    public class EvaluateAssignmentDTO
    {
        public AssignmentDTO Assignment { get; set; }
        public string StudentName { get; set; }
    }
}
