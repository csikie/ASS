using ASS.DAL.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASS.BLL.Interfaces
{
    public interface IInstructorService
    {
        int ProcessPendingStatus(int id, bool isApprove);
        Task<List<UserCourse>> GetPendingList(ClaimsPrincipal user);
    }
}
