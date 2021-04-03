using ASS.BLL.Interfaces;
using ASS.DAL;
using ASS.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASS.BLL.Services
{
    public class StudentService : BaseService, IStudentService
    {
        public StudentService(ASSContext context, UserManager<User> userManager) : base(context, userManager) { }

        public async Task<List<Course>> GetCourses(ClaimsPrincipal user)
        {
            User student = await userManager.GetUserAsync(user);
            return await context.Courses.Include(x => x.UserCourses)
                                        .Where(x => !x.UserCourses.Where(y => y.UserId == student.Id && (y.Pending == null || !y.Pending.Value)).Any())
                                        .Include(x => x.Instructors)
                                        .ThenInclude(x => x.User)
                                        .ToListAsync();
        }

        public async void CourseRegistration(int[] courseIds, ClaimsPrincipal user)
        {
            User student = await userManager.GetUserAsync(user);
            foreach (int courseId in courseIds)
            {
                Course course = context.Courses.FirstOrDefault(x => x.Id == courseId);
                context.UserCourse.Add(new UserCourse(course, student));
                context.SaveChanges();
            }
        }

        public async Task<List<UserCourse>> Read_AssignmentGrid(ClaimsPrincipal user)
        {
            int studentId = (await userManager.GetUserAsync(user)).Id;

            return context.UserCourse.Where(x => x.UserId == studentId && x.Pending.HasValue && x.Pending.Value)
                                      .Include(x => x.Course)
                                      .ThenInclude(x => x.Assignments)
                                      .ThenInclude(x => x.Solutions)
                                      .ToList();
        }

        public async Task<Assignment> GetAssignment(int assignmentId, ClaimsPrincipal user)
        {
            int userId = (await userManager.GetUserAsync(user)).Id;
            Assignment assignment = context.Assignments.Include(x => x.Solutions).FirstOrDefault(x => x.Id == assignmentId);
            int courseId = assignment.CourseId;
            if (!context.UserCourse.Any(x => x.CourseId == assignment.CourseId && x.UserId == userId))
            {
                throw new ArgumentException(); // TODO
            }
            return assignment;
        }

        public async void SubmitSolution(int assignmentId, ClaimsPrincipal user, string submittedSolution, DateTime submissionTime)
        {
            User student = await userManager.GetUserAsync(user);
            int userId = student.Id;
            Assignment assignment = context.Assignments.FirstOrDefault(x => x.Id == assignmentId);
            int courseId = assignment.CourseId;
            if (assignment.EndDate <= submissionTime && !context.UserCourse.Any(x => x.CourseId == assignment.CourseId && x.UserId == userId))
            {
                throw new Exception();
            }
            context.Solutions.Add(new Solution(submittedSolution,submissionTime,assignment,student));
            context.SaveChanges();
        }
    }
}
