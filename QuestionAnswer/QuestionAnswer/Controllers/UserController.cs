using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
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

        public string GetUsers()
        {
            string query = "SELECT * FROM users";
            return JsonSerializer.Serialize(connection.Query(query).ToList());
        }

        public void NewUser(User user)
        {
            connection.Query($"INSERT INTO User VALUES ('{user.Login}'), ('{user.Password}')");
        }

    }
}