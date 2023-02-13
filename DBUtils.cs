using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skladd
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "sklad";
            string user = "root";
            string password = "0000";
            return DBMySQLUtils.GetDBConnection(host, port, database, user, password);
        }
    }
}
