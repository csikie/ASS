using ASS.BLL.Interfaces;
using ASS.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ASS.BLL.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public LoginService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<string> CreateFullUserName(ClaimsPrincipal principal)
        {
            User user = await userManager.GetUserAsync(principal);
            return $"{user.RealName} / {user.UserName}";
        }

        public async Task<string> CreateUserRolesJson(ClaimsPrincipal principal)
        {
            return JsonConvert.SerializeObject(await userManager.GetRolesAsync(await userManager.GetUserAsync(principal)));
        }

        public async Task<bool> SignIn(string username, string password, bool isPersistent = false, bool lockoutOnFailure = false)
        {
            SignInResult result = await signInManager.PasswordSignInAsync(username, password, isPersistent, lockoutOnFailure);
            return result.Succeeded;
        }

        public async void SignOut()
        {
            await signInManager.SignOutAsync();
        }
    }
}
