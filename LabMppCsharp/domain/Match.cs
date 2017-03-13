using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMppCsharp.domain
{
    class Match : IEntity<int>
    {

        public Match(string team1, int id, string team2, string stage, int remainingTickets)
        {
            Id = id;
            Team1 = team1;
            Team2 = team2;
            Stage = stage;
            RemainingTickets = remainingTickets;
        }

        public string Team1 { get; set; }
        public int Id { get; set; }
        public string Team2 { get; set; }
        public string Stage { get; set; }
        public int RemainingTickets { get; set; }

        protected bool Equals(Match other)
        {
            return string.Equals(Team1, other.Team1) && Id == other.Id && string.Equals(Team2, other.Team2) && string.Equals(Stage, other.Stage) && RemainingTickets == other.RemainingTickets;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Match)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Team1?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ Id;
                hashCode = (hashCode * 397) ^ (Team2?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Stage?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ RemainingTickets;
                return hashCode;
            }
        }
    }
}
