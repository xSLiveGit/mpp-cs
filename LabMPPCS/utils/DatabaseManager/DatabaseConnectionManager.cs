using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LabMppCsharp.utils
{
    public class DatabaseConnectionManager
    {
        private MySqlConnection _connection = null;
        private string _myConnectionString = null;
        public DatabaseConnectionManager()
        {
        }

        public MySqlConnection GetConnection()
        {
            if (!ReferenceEquals(null, _connection)) return _connection;
            _myConnectionString = "server=127.0.0.1;uid=root;" + "pwd=password;database=ticktes_mpp;";
            _connection = new MySqlConnection {ConnectionString = _myConnectionString};
            _connection.Open();
            return _connection;
        }

        public MySqlConnection GetNewConnection()
        {
            _myConnectionString = "server=127.0.0.1;uid=root;" + "pwd=password;database=ticktes_mpp;";
            _connection = new MySqlConnection { ConnectionString = _myConnectionString };
            _connection.Open();
            return _connection;
        }



    }
}
