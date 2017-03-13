using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMppCsharp.domain
{
    class Sale : IEntity<int>
    {
        public Sale(int id, int idTicket, int idMatch, string person)
        {
            Id = id;
            IdTicket = idTicket;
            IdMatch = idMatch;
            Person = person;
        }

        public int Id { get; set; }
        public int IdTicket { get; set; }
        public int IdMatch { get; set; }
        public string Person { get; set; }

        protected bool Equals(Sale other)
        {
            return Id == other.Id && IdTicket == other.IdTicket && IdMatch == other.IdMatch && string.Equals(Person, other.Person);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Sale) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ IdTicket;
                hashCode = (hashCode * 397) ^ IdMatch;
                hashCode = (hashCode * 397) ^ (Person?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}
