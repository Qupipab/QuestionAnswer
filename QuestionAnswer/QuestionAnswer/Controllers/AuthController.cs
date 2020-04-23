using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QuestionAnswer.Models;

namespace QuestionAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private string ConnectionString { get; set; }
        private readonly SqlConnection Connection;

        public AuthController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("PollProject");
            Connection = GetConnection(ConnectionString);
            Connection.Open();
        }

        private SqlConnection GetConnection(string conStr) => new SqlConnection(conStr);

        //[HttpGet]
        //public void /* IActionResult */ Login()
        //{
            
        //}

        [HttpPost]
        [Route("CheckUser")]
        public async void LoginAsync(Login login)
        {
            Console.WriteLine("1");
            string userCheck = LoginUser(login.Username, login.Password);
            if (userCheck != "-1")
            {
                Console.WriteLine("2");
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userCheck)
                };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            }
            else Console.WriteLine("fsaasd");
        }


        private string LoginUser(string username, string password)
        {
            string a = JsonSerializer.Serialize(Connection.Query($"SELECT ID FROM Users WHERE Login = '{ username }' AND Password = '{ password }'"));
            return a != "[]" ? a : "-1";
        }

    }
}