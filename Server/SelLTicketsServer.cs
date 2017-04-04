using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellTicketsModel.entity;
using SellTicketsModel.exception;
using SellTicketsServices;
using SellTicketsServices.services;

namespace Server
{
    class SellTicketsServer : ISellTicketsServer
    {
        private UserService _userService = null;
        private TicketService _ticketService = null;
        private MatchService _matchService = null;
        private readonly IDictionary<String, ISellTicketsClient> _loggedClients;

        public SellTicketsServer(TicketService ticketService, UserService userService, IDictionary<string, ISellTicketsClient> loggedClients, MatchService matchService)
        {
            this._ticketService = ticketService;
            this._userService = userService;
            this._loggedClients = loggedClients;
            this._matchService = matchService;
        }

        public void Login(User user, ISellTicketsClient client)
        {
            this._userService.LogIn(user.Username, user.Password);
            this._loggedClients.Add(user.Username,client);

        }

        public void Logout(User user, ISellTicketsClient client)
        {
            this._loggedClients.Remove(key: user.Username);
        }

        public void SellTickets(string idMatch, string quantity, string buyerPerson, string username)
        {
            this._ticketService.Add(quantityS: quantity,idMatchS: idMatch,person: buyerPerson);
            Match m = this._matchService.FindById(id: idMatch);
            Notify(usernameI: username, match: m);
        }

        public IList<Match> GetAllMatches()
        {
            try
            {
                return this._matchService.GetAll();
            }
            catch (Exception e)
            {
                throw new ControllerException(e.Message);
            }
        }

        public List<Match> GetFilteredAndSortedMatches()
        {
            try
            {
                return this._matchService.GetAllMatchesWithRemainingTickets();
            }
            catch (Exception e)
            {
                throw new ControllerException(e.Message);
            }
        }

        private void Notify(String usernameI,Match match)
        {
            Console.WriteLine("Notify by:" + usernameI);
            foreach (var username in this._loggedClients.Keys)
            {
                if (!username.Equals(usernameI))
                {
                    this._loggedClients[username].ShowUpdates(match);
                }
            }
        }
 
    }
}
