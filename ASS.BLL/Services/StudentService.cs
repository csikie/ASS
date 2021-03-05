using ASS.BLL.Interfaces;
using ASS.DAL;
using ASS.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASS.BLL.Services
{
    public class StudentService : BaseService, IStudentService
    {
        public StudentService(ASSContext context, UserManager<User> userManager) : base(context, userManager) { }

        public async Task<List<Course>> GetCourses()
        {
            return await context.Courses.Include(x => x.Instructors)
                                        .ThenInclude(x => x.User)
                                        .ToListAsync();
        }

    }
}
