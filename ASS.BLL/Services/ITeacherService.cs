using System.Threading.Tasks;

namespace ASS.BLL.Services
{
    public interface ITeacherService
    {
        Task<bool> AddUserToCourse(string neptunCode, int subjectId, string courseName);
    }
}
