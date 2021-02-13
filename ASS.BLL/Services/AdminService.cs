using ASS.Common.Enums;
using ASS.DAL;
using ASS.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.BLL.Services
{
    public class AdminService : BaseService, IAdminService
    {
        public AdminService(ASSContext context, UserManager<User> userManager) : base(context, userManager) { }

        public async Task<bool> AddUserToSubject(string neptunCode, Subject subject)
        {
            User user = context.Users.FirstOrDefault(x => x.UserName == neptunCode);
            if (user != null && await userManager.IsInRoleAsync(user, Role.Teacher.ToString()))
            {
                context.UserSubjects.Add(new UserSubject(subject, user));
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
