using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellTicketsModel.entity
{
    public class Sale
    {
        public Sale(String idMatch, String quantity, string person)
        {
            this.IdMatch = idMatch;
            this.Quantity = quantity;
            this.Person = person;
        }

        public String IdMatch { get; set; }

        public String Quantity { get; set; }

        public string Person { get; set; }
    }
}
