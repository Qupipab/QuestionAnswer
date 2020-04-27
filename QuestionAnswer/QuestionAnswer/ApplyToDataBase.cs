using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionAnswer
{
    static public class ApplyToDataBase
    {

        static private string ConnectionString { get; set; }
        static private SqlConnection Connection { get; set; }

        static private SqlConnection ConnectTo(string conStr) => new SqlConnection(conStr);

        static public SqlConnection GetConnection(IConfiguration Configuration)
        {
            ConnectionString = Configuration.GetConnectionString("PollProject");
            Connection = ConnectTo(ConnectionString);
            Connection.Open();
            return Connection;
        }

    }
}
