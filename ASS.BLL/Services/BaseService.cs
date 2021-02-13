using ASS.DAL;
using ASS.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace ASS.BLL.Services
{
    public abstract class BaseService : IDisposable
    {
        protected ASSContext context;
        protected UserManager<User> userManager;

        protected BaseService(ASSContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.disposed = false;
        }

        private bool disposed;
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
