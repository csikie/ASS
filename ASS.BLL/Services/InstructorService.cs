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
                                     .ToList();
        }

        public int ProcessPendingStatus(int id, bool isApprove)
        {
            context.UserCourse.Where(x => x.Id == id).FirstOrDefault().Pending = isApprove;
            context.SaveChanges();

            return id;
        }
    }
}
