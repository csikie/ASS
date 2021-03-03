using ASS.DAL;
using ASS.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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

        public string FullName(string username)
        {
            User user = context.Users.FirstOrDefault(x => x.UserName == username);
            return $"{user.RealName} / {user.UserName}";
        }

        public async Task<User> GetUserData(ClaimsPrincipal principal)
        {
            return await userManager.GetUserAsync(principal);
        }

        public async Task<string[]> GetUserRoles(ClaimsPrincipal principal)
        {
            return (await userManager.GetRolesAsync(await GetUserData(principal))).ToArray();
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
