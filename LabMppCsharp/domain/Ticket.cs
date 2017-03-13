using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMppCsharp.domain
{
    public class Ticket : IEntity<Int32>
    {
        public Ticket(Int32 id, double price)
        {
            Id = id;
            Price = price;
        }

        public Ticket()
        {
            
        }

        public Int32 Id { get; set; }
        public double Price { get; set; }

        protected bool Equals(Ticket other)
        {
            return Id == other.Id && Price.Equals(other.Price);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Ticket) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id * 397) ^ Price.GetHashCode();
            }
        }
    }
}
