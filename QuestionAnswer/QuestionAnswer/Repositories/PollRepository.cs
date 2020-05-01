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
    public class PollRepository : IPollRepository
    {

        private readonly SqlConnection Connection;

        public PollRepository(IConfiguration configuration) => Connection = ApplyToDataBase.GetConnection(configuration);
        
        public Dictionary<int, Poll> GetPoll(string link)
        {
            string query = @"SELECT 
                            p.ID AS PollID, u.Login AS Author, p.Title, p.IsPrivate,
                            p.IsActive, p.IsAnon, p.VotesCount AS VotesCount,

                            (SELECT Count(AnswerID)
                            FROM UserAnswer ua
                            INNER JOIN Answers a
                            ON a.ID = ua.AnswerID
                            INNER JOIN Polls p
                            ON a.PollID = p.ID
                            WHERE p.Link = @link) AS GeneralVotesCount,

                            p.CanAddAnswers, p.CloseDate, a.ID AS AnswerID, a.Title AS Title, 
                            a.CreatorID, Count(ua.AnswerID) AS VoteCount
                            FROM Answers a 
                            INNER JOIN Polls p
                            ON p.ID = a.PollID
                            LEFT JOIN UserAnswer ua
                            ON a.ID = ua.AnswerID
                            INNER JOIN Users u
                            ON u.ID = p.UserID
                            WHERE p.Link = @link
                            GROUP BY p.ID, u.Login, p.Title, p.IsPrivate, p.IsActive, p.IsAnon, p.VotesCount, p.CanAddAnswers, p.CloseDate, a.ID, ua.AnswerID, a.CreatorID, a.Title";

            var lookup = new Dictionary<int, Poll>();

            var a = Connection.Query<Poll, Answer, Poll>(query, (p, a) =>
            {

                Poll poll;
                if (!lookup.TryGetValue(p.PollID, out poll)) lookup.Add(p.PollID, poll = p);
                if (poll.Answers == null) poll.Answers = new List<Answer>();
                poll.Answers.Add(a);
                return poll;

            }, new { link }, splitOn: "AnswerID").AsQueryable();
            
            return lookup;
        }

        public void Vote(UserAnswer vote)
        {
            Review.NullReview(vote);
            string query = @"INSERT INTO UserAnswer VALUES ( @UserID, @AnswerID )";
            Connection.Query(query, new { vote.UserID, vote.AnswerID });
        }

        public bool CheckUser(int pollId, string userId)
        {
            string query = @"SELECT TOP 1 p.ID FROM UserAnswer ua INNER JOIN Answers a ON ua.AnswerID = a.ID INNER JOIN Polls p ON a.PollID = p.ID WHERE p.ID = @pollId AND ua.UserID = @userId";
            var poll = Connection.QueryFirstOrDefault<int>(query, new { pollId, userId });
            return poll == 0 ? false : true;
        }

        public void AddAnswer(Answer answer)
        {
            Review.NullReview(answer);
            string query = @"INSERT INTO Answers VALUES ( @Title, @CreatorID, @PollID )";
            Connection.Query(query, new { answer.Title, answer.CreatorID , answer.PollID });
        }
        
        public string GetLastAnswerID()
        {
            string query = @"SELECT TOP 1 ID FROM Answers ORDER BY ID DESC";
            return Connection.QueryFirstOrDefault<string>(query);
        }

    }
}
