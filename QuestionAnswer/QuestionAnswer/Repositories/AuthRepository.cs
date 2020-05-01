using System;
using Dapper;
using Microsoft.Extensions.Configuration;
using QuestionAnswer.Models;
using QuestionAnswer.Repositories.Interfaces;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace QuestionAnswer.Repositories
{
    public class AuthRepository : IAuthRepository
    {

        private readonly SqlConnection Connection;

        static IConfiguration Configuration;
        public AuthRepository(IConfiguration configuration)
        {
            Connection = ApplyToDataBase.GetConnection(configuration);
            Configuration = configuration;
        }
        
        public int GetUserId(User user)
        {
            Review.NullReview(user);
            user.Password = CreateHash(user.Password);
            int a = Connection.QueryFirstOrDefault<int>(
                $"SELECT ID FROM Users WHERE Login = @Login AND Password = @Password",
                new { user.Login, user.Password });
            return a;
        }

        public bool CheckUser(User user)
        {
            Review.NullReview(user);
            int id = Connection.QueryFirstOrDefault<int>(
                $"SELECT ID FROM Users WHERE Login = @Login",
                new { user.Login });
            return id == 0 ? false : true;
        }

        public void AddUser(User user)
        {
            Review.NullReview(user);
            user.Password = CreateHash(user.Password);
            Connection.Query($"INSERT INTO Users VALUES (@Login, @Password)", new { user.Login, user.Password });
        }

        private static string CreateHash(string password)
        {
            using (var mySHA256 = SHA256.Create())
            {
                string passSalt = password + Configuration.GetSection("Salt");
                byte[] hashValue = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(passSalt));
                return Convert.ToBase64String(hashValue);
            }
        }

    }
}
