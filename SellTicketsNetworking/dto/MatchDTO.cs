using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellTicketsModel.entity;

namespace SellTicketsNetworking
{
    [Serializable]
    public class MatchDTO 
    {
        
        public Int32 Id { get; set; }
        public String Team1 { get; set; }
        public String Team2 { get; set; }
        public String Stage { get; set; }
        public Double Price { get; set; }
        public Int32 Tickets { get; set; }

        public MatchDTO(string team1, string team2, string stage, int tickets, double price) : this(0, team1, team2, stage, tickets, price)
        {

        }

        public MatchDTO(Match m)
        {
            Id = m.Id;
            Team1 = m.Team1;
            Team2 = m.Team2;
            Stage = m.Stage;
            Price = m.Price;
            Tickets = m.Tickets;
        }

        public MatchDTO(int id, string team1, string team2, string stage, int tickets, double price)
        {
            Tickets = tickets;
            Id = id;
            Team1 = team1;
            Team2 = team2;
            Stage = stage;
            Price = price;
        }

    }
}
