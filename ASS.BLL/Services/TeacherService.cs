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
    public class TeacherService : BaseService, ITeacherService
    {
        public TeacherService(ASSContext context, UserManager<User> userManager) : base(context, userManager) { }

        public void AddUserToCourse(string[] neptunCodes, int subjectId, string courseName)
        {
            Subject subject = context.Subjects.Where(x => x.Id == subjectId)
                                              .Include(x => x.Courses)
                                              .FirstOrDefault();
            if (subject.Courses.Any(x => x.Name == courseName))
            {
                throw new ArgumentException("Ez a kurzusnév már foglalt!");
            }
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
            var a = context.Subjects.Include(s => s.UserSubject)
                                   .ThenInclude(u => u.User)
                                   .Where(x => x.UserSubject.Any(y => y.User.Id == teacher.Id))
                                   .Include(x => x.Courses).ThenInclude(x => x.Instructors).ThenInclude(x => x.User)
                                   .ToList();
            return a;
        }
    }
}
