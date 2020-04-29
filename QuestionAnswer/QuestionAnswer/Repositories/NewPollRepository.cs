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
         
        public void AddPoll(Poll p)
        {
            string q = $"INSERT INTO Polls VALUES( @UserID, @Title, @CreateDate, @CloseDate, @IsPrivate, @IsActive, @VotesCount, @CanAddAnswers, @Link )";
            Connection.Query(q, new { p.UserID, p.Title, p.CreateDate, p.CloseDate, p.IsPrivate, p.IsActive, p.VotesCount, p.CanAddAnswers, p.Link });
        }

        public void AddAnswers(List<Answer> answers)
        {
            foreach (var a in answers)
            {
                string q = $"INSERT INTO Answers VALUES ( @Title, @CreatorID, @PollID )";
                Connection.Query(q, new { a.Title, a.CreatorID, a.PollID });
            }
        }

    }
}
