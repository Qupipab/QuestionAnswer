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

        private readonly SqlConnection Connection;

        public NewPollRepository(IConfiguration configuration) => Connection = ApplyToDataBase.GetConnection(configuration);

        public int GetLastPoll() => Connection.QueryFirstOrDefault<int>("SELECT MAX(ID) FROM POLLS");

        public List<dynamic> GetPollAuthor(string id) => Connection.Query("SELECT Login, ID FROM Users WHERE ID = @id", new { id }).ToList();
         
        public void AddPoll(Poll p)
        {
            Review.NullReview(p);
            p.IsClosed = false;
            string q = $"INSERT INTO Polls VALUES( @UserID, @Title, @VotesCount, @CanAddAnswers, @IsPrivate, @IsDraft, @IsClosed, @IsActive, @IsAnon, @Link, @CloseDate)";
            Connection.Query(q, new { p.UserID, p.Title, p.VotesCount, p.CanAddAnswers, p.IsPrivate, p.IsDraft, p.IsClosed, p.IsActive, p.IsAnon, p.Link, p.CloseDate });
        }

        public void AddAnswers(List<Answer> answers, int userId, int lastPoll)
        {
            Review.NullReview(answers);
            foreach (var a in answers)
            {
                string q = $"INSERT INTO Answers VALUES ( @Title, @userId, @lastPoll )";
                Connection.Query(q, new { a.Title, userId, lastPoll });
            }
        }

    }
}
