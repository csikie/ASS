using ASS.BLL.Interfaces;
using ASS.Common.Enums;
using ASS.DAL;
using ASS.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ASS.BLL.Services
{
    public class AdminService : BaseService, IAdminService
    {
        public AdminService(ASSContext context, UserManager<User> userManager) : base(context, userManager) { }

        public void AddUserToSubject(string[] neptunCodes, string subjectName)
        {
            if (context.Subjects.Any(x => x.Name == subjectName))
            {
                throw new ArgumentException("Ez a tárgynév már foglalt!");
            }
            Subject subject = new Subject(subjectName);
            foreach (string neptunCode in neptunCodes)
            {
                User user = context.Users.FirstOrDefault(x => x.UserName == neptunCode);
                if (user != null && userManager.IsInRoleAsync(user, Role.Teacher.ToString()).Result)
                {
                    context.UserSubjects.Add(new UserSubject(subject, user));
                    context.SaveChanges();
                }
            }
        }

        public IEnumerable<Subject> GetSubjects()
        {
            return context.Subjects.Include(s => s.UserSubject)
                                   .ThenInclude(u => u.User)
                                   .ToList();
        }

        public IEnumerable<User> GetUsers(Expression<Func<User, bool>> pred)
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
