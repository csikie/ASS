using ASS.BLL.Interfaces;
using ASS.DAL;
using ASS.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace ASS.BLL.Services
{
    public class InstructorService : BaseService, IInstructorService
    {
        public InstructorService(ASSContext context, UserManager<User> userManager) : base(context, userManager) { }
    }
}
