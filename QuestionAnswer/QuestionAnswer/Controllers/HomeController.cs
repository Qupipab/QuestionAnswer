using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using QuestionAnswer.Models;

namespace QuestionAnswer.Controllers
{

    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private string connectionString { get; set; }
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            connectionString = configuration.GetConnectionString("PollProject");
        }

        public string Index()
        {
            
            string sqlOrderDetails = $"SELECT * FROM users";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                Console.WriteLine("Connected");
                var a = JsonSerializer.Serialize(connection.Query(sqlOrderDetails).ToList());
                return a;

            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
