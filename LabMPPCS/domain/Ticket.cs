using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Types;

namespace LabMPPCS.domain
{
    public class Ticket : IHasId<Int32>
    {
        public Int32 Id { get; set; }
        public Int32 Quantity { get; set; }
        public Int32 IdMatch { get; set; }
            
        public Ticket(Int32 id, Int32 quantity,Int32 idMatch)
        {
            Id = id;
            Quantity = quantity;
            IdMatch = idMatch;
        }

        public Ticket(Int32 quantity,Int32 idMatch) : this(-1, quantity,idMatch)
        {
        }

        protected bool Equals(Ticket other)
        {
            return Id == other.Id && Quantity == other.Quantity && IdMatch == other.IdMatch;
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
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ Quantity;
                hashCode = (hashCode * 397) ^ IdMatch;
                return hashCode;
            }
        }
    }
}
