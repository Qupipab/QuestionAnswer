using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using QuestionAnswer.DomainModels.Interfaces;
using QuestionAnswer.Models;
using QuestionAnswer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuestionAnswer.DomainModels
{
    public class AuthDomainModel : IAuthDomainModel
    {

        private readonly IAuthRepository AuthRepository;

        public AuthDomainModel(IAuthRepository authRepository)
        {
            AuthRepository = authRepository;
        }

        public async Task<string> SignInAsync(User user, HttpContext httpContext)
        {
            int userCheck = AuthRepository.GetUserId(user);

            if (userCheck == 0) return "0";
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userCheck.ToString())
                };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return "1";
            }
        }

        public string NewUser(User user)
        {
            if (!AuthRepository.CheckUser(user))
            {
                AuthRepository.AddUser(user);
                return "1";
            }
            else return "0";
        }

    }
}
