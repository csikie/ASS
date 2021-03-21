using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASS.DAL.Models
{
    public class Assignment
    {
        public Assignment() { }

        public Assignment(string name, string description, DateTime startDate, DateTime endDate, Course course)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            CourseId = course.Id;
            Course = course;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<Solution> Solutions { get; set; }
    }
}
