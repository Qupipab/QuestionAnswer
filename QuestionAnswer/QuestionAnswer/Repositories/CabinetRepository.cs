using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using QuestionAnswer.Models;
using QuestionAnswer.Repositories.Interfaces;

namespace QuestionAnswer.Repositories
{
    public class CabinetRepository : ICabinetRepository
    {

        private SqlConnection Connection { get; set; }

        public CabinetRepository(IConfiguration configuration) => Connection = ApplyToDataBase.GetConnection(configuration);

        public Dictionary<int, User> GetUserPolls(string id)
        {
            string query = $"SELECT Login, Title, Link, IsActive, IsPrivate FROM Polls p INNER JOIN Users u ON u.ID = p.UserID WHERE UserID = { id }";

            var lookup = new Dictionary<int, User>();

            var a = Connection.Query<User, Poll, User>(query, (u, p) =>
            {

                User user;
                if (!lookup.TryGetValue(u.Id, out user)) lookup.Add(u.Id, user = u);
                if (user.Polls == null) user.Polls = new List<Poll>();
                user.Polls.Add(p);
                return user;

            }, splitOn: "Title").AsQueryable();

            return lookup;
        }

    }
}
