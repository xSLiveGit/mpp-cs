using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellTicketsModel.entity;

namespace SellTicketsServices
{
    public interface ISellTicketsServer
    {
        void Login(User user, ISellTicketsClient client);
        void Logout(User user, ISellTicketsClient client);
        void SellTickets(String idMatch, String quantity, String buyerPerson, String username);

        IList<Match> GetAllMatches() ;
        List<Match> GetFilteredAndSortedMatches();

    }
}
