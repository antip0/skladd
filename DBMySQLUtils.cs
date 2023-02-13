using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skladd
{
        class DBMySQLUtils
        {
            public static MySqlConnection GetDBConnection(string host, int port, string database, string user, string password)
            {
                string connectionString = "Server=" + host + ";database=" + database + ";port=" + port.ToString() + ";user=" + user + ";password=" + password + ";";
                MySqlConnection connection = new MySqlConnection(connectionString);
                return connection;
            }
        }
}
