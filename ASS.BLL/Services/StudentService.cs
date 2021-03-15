using ASS.BLL.Interfaces;
using ASS.DAL;
using ASS.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    }
}
