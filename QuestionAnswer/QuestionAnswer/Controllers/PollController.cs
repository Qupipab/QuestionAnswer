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
using Newtonsoft.Json;
using QuestionAnswer.Models;

namespace QuestionAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {

        private string ConnectionString { get; set; }
        private readonly SqlConnection Connection;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<int, User>.ValueCollection resList { get; set; }

        public PollController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("PollProject");
            Connection = GetConnection(ConnectionString);
            Connection.Open();
        }

        private SqlConnection GetConnection(string conStr) => new SqlConnection(conStr);

        [HttpGet]
        public string GetPolls()
        {
            
            string query = @"SELECT u.ID, u.Login, p.Title, p.Link FROM Users u INNER JOIN Polls p ON u.ID = p.UserID WHERE p.IsPrivate = 'false'";
            //return JsonSerializer.Serialize(connection.Query(query).ToList());
            var lookup = new Dictionary<int, User>();

            Connection.Query<User, Poll, User>(query, (u, p) => {

                User user;
                if (!lookup.TryGetValue(u.Id, out user)) lookup.Add(u.Id, user = u);
                if (user.Polls == null) user.Polls = new List<Poll>();
                user.Polls.Add(p);
                return user;

            }, splitOn: "Title").AsQueryable();

            resList = lookup.Values;

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;

            var myJson = JsonConvert.SerializeObject(resList, settings);

            
            return myJson;

        }

    }
}