﻿using System.ComponentModel.DataAnnotations;

namespace ASS.DAL.Models
{
    public class UserCourse
    {
        public UserCourse() { }
        public UserCourse(Course course, User user)
        {
            CourseId = course.Id;
            Course = course;
            UserId = user.Id;
            User = user;
        }

        [Key]
        public int Id { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool? Pending { get; set; }
    }
}
