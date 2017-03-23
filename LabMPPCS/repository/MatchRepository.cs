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
    class MatchRepository : IdAbstractDatabaseRepository<Match>
    {
        public MatchRepository(DatabaseConnectionManager databaseConnectionManager, string tableName) : base(databaseConnectionManager, tableName)
        {
        }

        protected override string GetIdName()
        {
            return "id";
        }

        protected override Dictionary<string, string> ToMap(Match item)
        {
            Dictionary<String, String> map = new Dictionary<string, string>();
            map.Add("id", item.Id.ToString());
            map.Add("team1", item.Team1);
            map.Add("team2", item.Team2);
            map.Add("stage", item.Stage);
            map.Add("ticketRemainig", item.Tickets.ToString());
            map.Add("price", item.ToString());
            return map;
        }

        protected override Match ToObject(MySqlDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
