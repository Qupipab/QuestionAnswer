using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
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
        public Dictionary<int, Poll>.ValueCollection resList2 { get; set; }

        public PollController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("PollProject");
            Connection = GetConnection(ConnectionString);
            Connection.Open();
        }

        private SqlConnection GetConnection(string conStr) => new SqlConnection(conStr);

        [HttpGet]
        [Route("GetAuthor")]
        [Authorize]
        public string GetAuthor()
        {
            var a = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string query = $"SELECT ID, Login FROM Users WHERE ID = { a }";
            return JsonConvert.SerializeObject(Connection.Query(query).ToList());
        }

        [HttpGet]
        [Route("GetPolls")]
        public string GetPolls()
        {

            string query = @"SELECT u.ID, u.Login, p.Title, p.Link FROM Users u INNER JOIN Polls p ON u.ID = p.UserID WHERE p.IsPrivate = 'false'";

            var lookup = new Dictionary<int, User>();

            Connection.Query<User, Poll, User>(query, (u, p) =>
            {

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

        [HttpGet]
        [Route("GetPoll")]
        public string GetPoll()
        {
            string query = @"SELECT u.Login, p.ID, p.VotesCount, p.Title, a.Title FROM Answers a INNER JOIN Polls p ON a.PollID = p.ID INNER JOIN Users u ON p.UserID = u.ID WHERE p.ID = 1";

            var lookup = new Dictionary<int, Poll>();

            Connection.Query<Poll, Answer, Poll>(query, (p, a) => {

                Poll poll;
                if (!lookup.TryGetValue(a.ID, out poll)) lookup.Add(a.ID, poll = p);
                if (poll.Answers == null) poll.Answers = new List<Answer>();
                poll.Answers.Add(a);
                return poll;

            }, splitOn: "Title").AsQueryable();

            resList2 = lookup.Values;

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;

            var myJson = JsonConvert.SerializeObject(resList2, settings);

            return myJson;

        }

        [HttpGet]
        [Route("GetMaxPoll")]
        public string GetLastPoll() => Connection.QueryFirstOrDefault<string>("SELECT MAX(ID) FROM POLLS");

        [HttpPost]
        [Route("AddPoll")]
        public void AddPoll(Poll poll)
        {

            string q = $"INSERT INTO Polls VALUES" +
                $"('{ poll.UserID }', '{ poll.Title }', '{ poll.CreateDate }', '{ poll.CloseDate }'," +
                $"'{ poll.IsPrivate }', '{ poll.IsActive }', '{ poll.VotesCount }', '{ poll.CanAddAnswers }', '{ poll.Link }')";

            Connection.Query(q);
        }

        [HttpPost]
        [Route("AddAnswers")]
        public void AddPoll(List<Answer> answers)
        {
            foreach (var i in answers)
            {
                string q = $"INSERT INTO Answers VALUES" +
                $"('{ i.Title }', '{ i.CreatorID }', '{ i.PollID }')";

                Connection.Query(q);
                
            }
        }

    }
}