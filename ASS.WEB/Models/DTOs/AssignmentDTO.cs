using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Models.DTOs
{
    public class AssignmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<SolutionDTO> Solutions { get; set; }

        public AssignmentDTO(int id, string name, string description, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }

        public AssignmentDTO(int id, string name, string description, DateTime startDate, DateTime endDate, List<SolutionDTO> solutions) : this(id,name,description,startDate,endDate)
        {
            Solutions = solutions;
        }
    }
}
