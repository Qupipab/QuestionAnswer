using Microsoft.AspNetCore.Http;
using QuestionAnswer.Models;
using System.Threading.Tasks;

namespace QuestionAnswer.DomainModels.Interfaces
{
    public interface IAuthDomainModel
    {
        Task<bool> SignInAsync(User user, HttpContext httpContext);
        Task SignOutAsync(HttpContext httpContext);
        string NewUser(User user);
    }
}
