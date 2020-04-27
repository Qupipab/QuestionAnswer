using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using QuestionAnswer.Repositories.Interfaces;

namespace QuestionAnswer.Repositories
{
    public class PollRepository : IPollRepository
    {

        private SqlConnection Connection { get; set; }

        public PollRepository(IConfiguration configuration) => Connection = ApplyToDataBase.GetConnection(configuration);

        public string GetPoll()
        {
            //string query = @"SELECT a.ID, a.Title, COUNT(ua.AnswerID) AS VoteCount FROM UserAnswer ua INNER JOIN Answers a ON a.ID = ua.AnswerID WHERE a.PollID = 1 GROUP BY a.ID, a.Title";
            return "str";
        }

    }
}
