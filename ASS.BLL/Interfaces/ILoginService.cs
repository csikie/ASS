using System.Security.Claims;
using System.Threading.Tasks;

namespace ASS.BLL.Interfaces
{
    public interface ILoginService
    {
        Task<string> CreateFullUserName(ClaimsPrincipal principal);
        Task<string> CreateUserRolesJson(ClaimsPrincipal principal);
        Task<bool> SignIn(string username, string password, bool isPersistent = false, bool lockoutOnFailure = false);
        void SignOut();
    }
}
