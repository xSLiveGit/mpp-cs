using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabMppCsharp.domain;
using MySql.Data.MySqlClient;

namespace LabMppCsharp.utils.mapper
{
    public class TicketMapper : IMapper<Ticket>
    {
        public string GetIdPrototype()
        {
            return "id";
        }

        public Ticket ToObject(MySqlDataReader dr)
        {
            var t = new Ticket
            {
                Id = dr.GetInt32("id"),
                Price = dr.GetDouble("price")
            };
            return t;
        }

        public Dictionary<string, string> ToMap(Ticket el)
        {
            var map = new Dictionary<string, string>();
            map.Add("id",el.Id.ToString());
            map.Add("price",el.Price.ToString());
            return map;
        }

        public string getEntityName()
        {
            return "ticket";
        }
    }
}
