using System;
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
        public async Task<string> SignInAsync(User user) => await AuthDomainModel.SignInAsync(user, HttpContext);

        [HttpGet]
        [Route("SignOut")]
        public async Task SignOutAsync() {
            await AuthDomainModel.SignOutAsync(HttpContext);
        }

        [HttpPost]
        [Route("NewUser")]
        public string NewUser(User user) => AuthDomainModel.NewUser(user);

    }
}