using ASS.DAL.Models;
using System.Threading.Tasks;

namespace ASS.BLL.Services
{
    public interface IAdminService
    {
        Task<bool> AddUserToSubject(string neptunCode, Subject subject);
    }
}
