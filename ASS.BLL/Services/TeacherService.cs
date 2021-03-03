using ASS.Common.Enums;
using ASS.DAL;
using ASS.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.BLL.Services
{
    public class TeacherService : BaseService, ITeacherService
    {
        public TeacherService(ASSContext context, UserManager<User> userManager) : base(context, userManager) { }

        public async Task<bool> AddUserToCourse(string neptunCode, int subjectId, string courseName)
        {
            User user = context.Users.FirstOrDefault(x => x.UserName == neptunCode);
            if (user != null && await userManager.IsInRoleAsync(user, Role.Instructor.ToString()))
            {
                Subject subject = context.Subjects.FirstOrDefault(x => x.Id == subjectId);
                Course course = new Course(courseName, subject);
                context.UserCourse.Add(new UserCourse(course, user));
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
