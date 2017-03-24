using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabMppCsharp.utils;
using LabMPPCS.domain;
using MySql.Data.MySqlClient;

namespace LabMPPCS.repository
{
    public class UserRepository : AbstractDatabaseRepository<User,String>
    {
        public UserRepository(DatabaseConnectionManager databaseConnectionManager, string tableName) : base(databaseConnectionManager, tableName)
        {
        }

        protected override string GetIdName()
        {
            return "username";
        }

        protected override Dictionary<string, string> ToMap(User item)
        {
            Dictionary<String, String> map = new Dictionary<string, string>();
            map.Add("username","'" + item.Username + "'");
            map.Add("password","'" + item.Password + "'");
            return map;
        }

        protected override User ToObject(MySqlDataReader reader)
        {
            return new User(reader.GetString(0),reader.GetString(1));
        }
    }
}
