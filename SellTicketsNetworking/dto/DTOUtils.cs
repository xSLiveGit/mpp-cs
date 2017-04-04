using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using SellTicketsModel.entity;

namespace SellTicketsNetworking
{
    class DTOUtils
    {
        public static User GetFromDTO(UserDTO usdto) => new User(
            username: usdto.Username, 
            password: usdto.Password);

        public static UserDTO GetDTO(User user) => new UserDTO(
            username:user.Username,
            password:user.Password);

        public static Sale GetFromDTO(SalesDTO salesDto) => new Sale(
            idMatch: salesDto.IdMatch, 
            quantity: salesDto.Quantity, 
            person: salesDto.Person);

        public static SalesDTO GetDTO(Sale sale,String username) => new SalesDTO(
            idMatch: sale.IdMatch, 
            quantity: sale.Quantity, 
            username: username,
            person:sale.Person);

        public static Match GetFromDTO(MatchDTO match) => new Match(
            id:match.Id,
            team1:match.Team1,
            team2:match.Team2,
            stage:match.Stage,
            tickets:match.Tickets,
            price:match.Price);

        public static MatchDTO GetDTO(Match m) => new MatchDTO(
            id:m.Id,
            team1:m.Team1,
            team2:m.Team2,
            stage:m.Stage,
            tickets:m.Tickets,
            price:m.Tickets);

       
    }
}
