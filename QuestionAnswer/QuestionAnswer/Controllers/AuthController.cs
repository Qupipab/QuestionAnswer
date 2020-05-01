using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestionAnswer.DomainModels.Interfaces;
using QuestionAnswer.Models;

namespace QuestionAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthDomainModel AuthDomainModel;

        public AuthController(IAuthDomainModel authDomainModel) => AuthDomainModel = authDomainModel;

        [HttpPost]
        [Route("SignIn")]
        public async Task<bool> SignInAsync(User user) => await AuthDomainModel.SignInAsync(user, HttpContext);

        [HttpPost]
        [Route("NewUser")]
        public string NewUser(User user) => AuthDomainModel.NewUser(user);

        [HttpGet]
        [Route("SignOut")]
        public async Task SignOutAsync() => await AuthDomainModel.SignOutAsync(HttpContext);
        
        [HttpGet]
        [Route("GetLoggedId")]
        public string GetLoggedId()
        {
            if (User.Identity.IsAuthenticated) return User.FindFirst(ClaimTypes.NameIdentifier).Value;
            else return "0";
        }

    }
}