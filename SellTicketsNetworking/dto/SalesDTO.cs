using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellTicketsNetworking
{
    [Serializable]
    public class SalesDTO
    {
  

        public SalesDTO(string idMatch, string quantity, string person, string username)
        {
            this.IdMatch = idMatch;
            this.Quantity = quantity;
            this.Person = person;
            this.Username = username;
        }

        public string IdMatch { get; set; }

        public string Quantity { get; set; }

        public string Person{ get; set; }

        public string Username { get; set; }
    }
}
