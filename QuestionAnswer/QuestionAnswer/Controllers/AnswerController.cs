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

namespace QuestionAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private string ConnectionString { get; set; }
        private readonly SqlConnection Connection;

        public AnswerController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("PollProject");
            Connection = GetConnection(ConnectionString);
            Connection.Open();
        }

        private SqlConnection GetConnection(string conStr) => new SqlConnection(conStr);

        [HttpGet]
        [Route("GetVotes")]
        public string GetVotes()
        {
            string query = @"SELECT a.ID, a.Title, COUNT(ua.AnswerID) AS VoteCount FROM UserAnswer ua INNER JOIN Answers a ON a.ID = ua.AnswerID WHERE a.PollID = 1 GROUP BY a.ID, a.Title";

            return JsonSerializer.Serialize(Connection.Query(query).ToList());
        }

    }
}