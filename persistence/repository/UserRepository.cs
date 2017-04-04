using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using persistence.utils;
using persistence.utils.validator;
using SellTicketsModel.entity;


namespace persistence.repository
{
    public class UserRepository : AbstractDatabaseRepository<User, String>
    {
        public UserRepository(DatabaseConnectionManager databaseConnectionManager, string tableName, UserValidator validator) : base(databaseConnectionManager, tableName, validator)
        {
        }

        protected override string GetIdName()
        {
            return "username";
        }

        protected override Dictionary<string, string> ToMap(User item)
        {
            Dictionary<String, String> map = new Dictionary<string, string>();
            map.Add("username", "'" + item.Username + "'");
            map.Add("password", "'" + item.Password + "'");
            return map;
        }

        protected override User ToObject(MySqlDataReader reader)
        {
            return new User(reader.GetString(0), reader.GetString(1));
        }
    }
}
