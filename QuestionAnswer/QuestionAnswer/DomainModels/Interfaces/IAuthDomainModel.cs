using Microsoft.AspNetCore.Http;
using QuestionAnswer.Models;
using System.Threading.Tasks;

namespace QuestionAnswer.DomainModels.Interfaces
{
    public interface IAuthDomainModel
    {
        public Task<string> SignInAsync(User user, HttpContext httpContext);
        public string NewUser(User user);
    }
}
