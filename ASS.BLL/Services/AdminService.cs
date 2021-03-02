using ASS.Common.Enums;
using ASS.DAL;
using ASS.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ASS.BLL.Services
{
    public class AdminService : BaseService, IAdminService
    {
        public AdminService(ASSContext context, UserManager<User> userManager) : base(context, userManager) { }

        public async void AddUserToSubject(string[] neptunCodes, string subjectName)
        {
            Subject subject = new Subject(subjectName);
            foreach (string neptunCode in neptunCodes)
            {
                User user = context.Users.FirstOrDefault(x => x.UserName == neptunCode);
                if (user != null && await userManager.IsInRoleAsync(user, Role.Teacher.ToString()))
                {
                    context.UserSubjects.Add(new UserSubject(subject, user));
                    context.SaveChanges();
                    //return true;
                }
            }
            //return false;
        }

        public IEnumerable<Subject> GetSubjects()
        {
            return context.Subjects.Include(s => s.UserSubject)
                                   .ThenInclude(u => u.User)
                                   .ToList();
        }

        public async Task<IEnumerable<User>> GetUsersInRole(Role role)
        {
            return (await userManager.GetUsersInRoleAsync(role.ToString())).ToList();
        }

        public IEnumerable<User> GetUsers(Expression<Func<User,bool>> pred)
        {
            return context.Users.Where(pred);
        }

        public void UpdateSubject(int id, string subjectName, string[] teacherNeptunCodes)
        {
            Subject subject = context.Subjects.Include(s => s.UserSubject)
                                              .ThenInclude(u => u.User)
                                              .FirstOrDefault(x => x.Id == id);

            List<UserSubject> deletedTeachers = new List<UserSubject>();
            foreach (UserSubject teacher in subject.UserSubject)
            {
                if (!teacherNeptunCodes.Contains(teacher.User.UserName))
                {
                    deletedTeachers.Add(teacher);
                }
            }

            foreach (UserSubject deletedTeacher in deletedTeachers)
            {
                subject.UserSubject.Remove(deletedTeacher);
            }

            foreach (string teacherNeptunCode in teacherNeptunCodes)
            {
                if (!subject.UserSubject.Any(x => x.User.UserName == teacherNeptunCode))
                {
                    subject.UserSubject.Add(new UserSubject(subject, context.Users.FirstOrDefault(x => x.UserName == teacherNeptunCode)));
                }
            }

            subject.Name = subjectName;

            context.Subjects.Update(subject);
            context.SaveChanges();
        }

        public void DeleteSubject(int id)
        {
            Subject subject = context.Subjects.Include(s => s.UserSubject)
                                              .ThenInclude(u => u.User)
                                              .FirstOrDefault(x => x.Id == id);

            context.Subjects.Remove(subject);

            context.SaveChanges();
        }
    }
}
