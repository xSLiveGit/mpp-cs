using System;
using SellTicketsModel.exception;

namespace SellTicketsModel.entity
{
    public class Match : IHasId<Int32>
    {
        public Int32 Id { get; set; }
        public String Team1 { get; set; }
        public String Team2 { get; set; }
        public String Stage { get; set; }
        public Double Price { get; set; }
        private Int32 _tickets;

        /// <summary>
        /// throw new EntityArgumentException() if  value < 0
        /// </summary>
        public Int32 Tickets
        {
            get { return this._tickets; }
            set
            {
                if (value < 0)
                {
                    throw new EntityArgumentException("Numbers of tickets must be positive.");
                }
                this._tickets = value;
            }
        }

       

        /// <summary>
        /// Construct an match entity. Constraint : tickets >= 0
        /// </summary>
        /// <param name="team1"> home team</param>
        /// <param name="team2"> visitator team</param>
        /// <param name="stage"> competition stage</param>
        /// <param name="tickets"> numbers of tickets remaining at this match</param>
        /// <param name="price">price for one place at match</param>
        public Match(string team1, string team2, string stage, int tickets, double price) : this(0, team1, team2, stage, tickets, price)
        {

        }

        /// <summary>
        /// Construct an match entity. Constraint : tickets >= 0 && id >= 0
        /// </summary>
        /// <param name="id"> unique key</param>
        /// <param name="team1"> home team</param>
        /// <param name="team2"> visitator team</param>
        /// <param name="stage"> competition stage</param>
        /// <param name="tickets"> numbers of tickets remaining at this match</param>
        /// <param name="price">price for one place at match</param>
        public Match(int id, string team1, string team2, string stage, int tickets, double price)
        {
            _tickets = tickets;
            Id = id;
            Team1 = team1;
            Team2 = team2;
            Stage = stage;
            Price = price;
            if (tickets < 0)
            {
                throw new EntityArgumentException("Numbers of tickets must be positive.");
            }
        }

        protected bool Equals(Match other)
        {
            return Id == other.Id && string.Equals(Team1, other.Team1) && string.Equals(Team2, other.Team2) && string.Equals(Stage, other.Stage) && Tickets == other.Tickets;
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
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Team1 != null ? Team1.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Team2 != null ? Team2.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Stage != null ? Stage.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Tickets;
                return hashCode;
            }
        }
    }
}
