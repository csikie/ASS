using ASS.DAL;
using ASS.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace ASS.BLL.Services
{
    public abstract class BaseService : IDisposable
    {
        protected ASSContext context;
        protected UserManager<User> userManager;
        private bool disposed;


        protected BaseService(ASSContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.disposed = false;
        }

        protected string FullName(string username)
        {
            User user = context.Users.FirstOrDefault(x => x.UserName == username);
            return $"{user.RealName} / {user.UserName}";
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
