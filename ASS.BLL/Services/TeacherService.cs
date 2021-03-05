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
    public class TeacherService : BaseService, ITeacherService
    {
        public TeacherService(ASSContext context, UserManager<User> userManager) : base(context, userManager) { }

        public void AddUserToCourse(string[] neptunCodes, int subjectId, string courseName)
        {
            Subject subject = context.Subjects.FirstOrDefault(x => x.Id == subjectId);
            Course course = new Course(courseName, subject);
            foreach (string instructor in neptunCodes)
            {
                User user = context.Users.FirstOrDefault(x => x.UserName == instructor);
                context.Instructors.Add(new Instructors(course, user));
            }
            context.SaveChanges();
        }

        public async Task<IEnumerable<Subject>> GetSubjects(ClaimsPrincipal principal)
        {
            User teacher = await userManager.GetUserAsync(principal);
            return context.Subjects.Include(s => s.UserSubject)
                                   .ThenInclude(u => u.User)
                                   .Where(x => x.UserSubject.Any(y => y.User.Id == teacher.Id))
                                   .ToList();
        }
    }
}
