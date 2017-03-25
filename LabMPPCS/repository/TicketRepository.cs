using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabMppCsharp.utils;
using LabMPPCS.domain;
using LabMPPCS.Validator;
using MySql.Data.MySqlClient;

namespace LabMPPCS.repository
{
    public class TicketRepository : IdAbstractDatabaseRepository<Ticket>
    {
        public TicketRepository(DatabaseConnectionManager databaseConnectionManager, string tableName,TicketValidator validator) : base(databaseConnectionManager, tableName,validator)
        {
        }

        protected override string GetIdName()
        {
            return "id";
        }

        protected override Ticket ToObject(MySqlDataReader reader)
        {
            var t = new Ticket( reader.GetInt32(0),reader.GetInt32(3),reader.GetInt32(1),reader.GetString(2));
            return t;
        }

        protected override Dictionary<String, String> ToMap(Ticket item)
        {
            var map = new Dictionary<String, String>();
            map.Add("id", item.Id.ToString());
            map.Add("idMatch", item.IdMatch.ToString());
            map.Add("person", "'" + item.Person + "'");

            map.Add("quantity", item.Quantity.ToString());
            return map;
        }
    }
}
