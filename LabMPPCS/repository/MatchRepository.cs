using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabMppCsharp.utils;
using LabMPPCS.domain;
using LabMPPCS.Validator;
using MySql.Data.MySqlClient;

namespace LabMPPCS.repository
{
    public class MatchRepository : IdAbstractDatabaseRepository<Match>
    {
        public MatchRepository(DatabaseConnectionManager databaseConnectionManager, string tableName,MatchValidator validator) : base(databaseConnectionManager, tableName,validator)
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
            map.Add("team1", "'" + item.Team1 + "'");
            map.Add("team2", "'" + item.Team2 + "'");
            map.Add("stage", "'" + item.Stage + "'");
            map.Add("tickets", item.Tickets.ToString());
            map.Add("price", item.Price.ToString());
            return map;
        }

        protected override Match ToObject(MySqlDataReader reader)
        {
            Match m = new Match(reader.GetInt32(0),reader.GetString(1),reader.GetString(2),reader.GetString(3),reader.GetInt32(4),reader.GetDouble(5));
            return m;
        }

        public void SellTickets(Int32 id, Int32 quantity)
        {
            String querry = $"UPDATE {this._tableName} SET tickets = (tickets - @tick) WHERE id = @searchedId";
            try {
                using (var cmd = _databaseConnectionManager.GetConnection().CreateCommand())
                {
                    cmd.CommandText = querry;
                    cmd.Parameters.AddWithValue("@tick",quantity);
                    cmd.Parameters.AddWithValue("@searchedId", id);
                    cmd.ExecuteNonQuery();
                }
            } catch (MySqlException e) {
                CodeThrowExceptionStatement(e);
            }
        }
    }
}
