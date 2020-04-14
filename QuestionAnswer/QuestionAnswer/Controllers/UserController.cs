using Dapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;

namespace QuestionAnswer.Controllers
{
    public class UserController : Controller
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