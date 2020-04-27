using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using QuestionAnswer.Models;
using QuestionAnswer.Repositories.Interfaces;

namespace QuestionAnswer.Repositories
{
    public class NewPollRepository : INewPollRepository
    {

        private SqlConnection Connection { get; set; }

        public NewPollRepository(IConfiguration configuration) => Connection = ApplyToDataBase.GetConnection(configuration);

        public string GetLastPoll() => Connection.QueryFirstOrDefault<string>("SELECT MAX(ID) FROM POLLS");

        public List<dynamic> GetPollAuthor(string id) => Connection.Query("SELECT Login, ID FROM Users WHERE ID = @id", new { id }).ToList();
         
        public void AddPoll(Poll poll)
        {

            string q = $"INSERT INTO Polls VALUES" +
                $"('{ poll.UserID }', '{ poll.Title }', '{ poll.CreateDate }', '{ poll.CloseDate }'," +
                $"'{ poll.IsPrivate }', '{ poll.IsActive }', '{ poll.VotesCount }', '{ poll.CanAddAnswers }', '{ poll.Link }')";

            Connection.Query(q);

        }

        public void AddAnswers(List<Answer> answers)
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
