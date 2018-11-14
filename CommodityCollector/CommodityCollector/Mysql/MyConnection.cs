using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CommodityCollector.Mysql
{
    public class DapperService
    {
        public static MySqlConnection MySqlConnection()
        {
            var connection = new MySqlConnection(DbConnection.ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
