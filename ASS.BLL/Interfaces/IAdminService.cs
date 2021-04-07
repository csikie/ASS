using ASS.Common.Enums;
using ASS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ASS.BLL.Interfaces
{
    public interface IAdminService
    {
        void AddUserToSubject(string[] neptunCodes, string subjectName);
        IEnumerable<Subject> GetSubjects();
        IEnumerable<User> GetUsers(Expression<Func<User, bool>> pred);
        void UpdateSubject(int id, string subjectName, string[] teacherNeptunCodes);
        void DeleteSubject(int id);
        public List<User> GetAllUser();
        public string[] GetUserRoles(int userId);
    }
}
