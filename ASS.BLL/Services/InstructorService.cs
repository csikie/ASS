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
    public class InstructorService : BaseService, IInstructorService
    {
        public InstructorService(ASSContext context, UserManager<User> userManager) : base(context, userManager) { }

        public async Task<List<UserCourse>> GetPendingList(ClaimsPrincipal user)
        {
            User instructor = await userManager.GetUserAsync(user);

            return context.UserCourse.Include(x => x.Course)
                                     .ThenInclude(x => x.Instructors)
                                     .Where(x => x.Course.Instructors.Any(y => y.UserId == instructor.Id))
                                     .Where(x => x.Pending == null)
                                     .Include(x => x.User)
                                     .Include(x => x.Course.Subject)
                                     .ToList();
        }

        public int ProcessPendingStatus(int id, bool isApprove)
        {
            context.UserCourse.Where(x => x.Id == id).FirstOrDefault().Pending = isApprove;
            context.SaveChanges();

            return id;
        }

        public async Task<List<Course>> GetCourses(ClaimsPrincipal user)
        {
            User instructor = await userManager.GetUserAsync(user);

            return context.Courses.Include(x => x.Instructors)
                                  .Where(x => x.Instructors.Any(y => y.UserId == instructor.Id))
                                  .Include(x => x.Subject)
                                  .Include(x => x.Assignments)
                                  .ThenInclude(x => x.Solutions)
                                  .Include(x => x.UserCourses)
                                  .ThenInclude(x => x.User)
                                  .ToList();
        }

        public void CreateAssignment(string name, string desc, DateTime startDate, DateTime endDate, int[] courseIds)
        {
            foreach (int courseId in courseIds)
            {
                Course course = context.Courses.FirstOrDefault(x => x.Id == courseId);
                context.Assignments.Add(new Assignment(name,desc,startDate,endDate,course));
                context.SaveChanges();
            }
        }
    }
}
