using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionAnswer
{
    public static class ApplyToDataBase
    {

        private static string ConnectionString { get; set; }
        private static SqlConnection Connection { get; set; }

        private static SqlConnection ConnectTo(string conStr) => new SqlConnection(conStr);

        public static SqlConnection GetConnection(IConfiguration Configuration)
        {
            ConnectionString = Configuration.GetConnectionString("PollProject");
            Connection = ConnectTo(ConnectionString);
            Connection.Open();
            return Connection;
        }

    }
}
