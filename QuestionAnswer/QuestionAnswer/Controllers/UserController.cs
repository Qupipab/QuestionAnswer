using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QuestionAnswer.Models;

namespace QuestionAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private string connectionString { get; set; }
        private SqlConnection connection;

        public UserController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("PollProject");
            connection = GetConnection(connectionString);
            connection.Open();
        }

        private SqlConnection GetConnection(string conStr) => new SqlConnection(conStr);

        [HttpGet]
        [Route("GetUserPolls")]
        [Authorize]
        public string GetUserPolls()
        {
            //var a = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //Console.WriteLine(a);
            string query = "SELECT Title, Link, IsActive, IsPrivate FROM Polls WHERE UserID = 1";
            return JsonSerializer.Serialize(connection.Query<Poll>(query).ToList());
        }

        [HttpGet]
        [Route("GetUsers")]
        public string GetUsers()
        {
            string query = "SELECT * FROM users";
            return JsonSerializer.Serialize(connection.Query<User>(query).ToList());
        }

        [HttpPost]
        public void NewUser(User user)
        {
            connection.Query($"INSERT INTO Users VALUES ('{ user.Login }', '{ user.Password }')");
        }

    }
}