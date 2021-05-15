using ASS.DAL.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASS.BLL.Interfaces
{
    public interface ITeacherService
    {
        void CreateCourse(string[] neptunCodes, int subjectId, string courseName);
        Task<IEnumerable<Subject>> GetSubjects(ClaimsPrincipal principal);
        Course GetCourse(int courseId);
        void EditCourse(int courseId, string courseName, string[] instructors);
    }
}
