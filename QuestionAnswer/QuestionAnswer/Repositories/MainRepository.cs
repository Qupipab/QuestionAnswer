﻿using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using QuestionAnswer.Models;
using QuestionAnswer.Repositories.Interfaces;

namespace QuestionAnswer.Repositories
{
    public class MainRepository : IMainRepository
    {

        private readonly SqlConnection Connection;

        public MainRepository(IConfiguration configuration) => Connection = ApplyToDataBase.GetConnection(configuration);

        public Dictionary<int, User> GetPolls()
        {

            string query = @"SELECT u.ID, u.Login, p.ID AS PollID, p.IsActive, p.Title, p.Link 
                             FROM Users u 
                             INNER JOIN Polls p ON u.ID = p.UserID 
                             WHERE p.IsPrivate = 'false' AND p.IsDraft = 'false' AND p.IsClosed = 'false'";

            var lookup = new Dictionary<int, User>();

            var a = Connection.Query<User, Poll, User>(query, (u, p) =>
            {

                User user;
                if (!lookup.TryGetValue(u.Id, out user)) lookup.Add(u.Id, user = u);
                if (user.Polls == null) user.Polls = new List<Poll>();
                user.Polls.Add(p);
                return user;

            }, splitOn: "PollID").AsQueryable();

            return lookup;
        }

    }
}
