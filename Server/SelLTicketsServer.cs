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
        private readonly UserService _userService = null;
        private readonly TicketService _ticketService = null;
        private readonly MatchService _matchService = null;
        private readonly IDictionary<String, ISellTicketsClient> _loggedClients;

        public SellTicketsServer(TicketService ticketService, UserService userService, MatchService matchService)
        {
            this._ticketService = ticketService;
            this._userService = userService;
            this._loggedClients = new Dictionary<string, ISellTicketsClient>();
            this._matchService = matchService;
        }

        public void Login(User user, ISellTicketsClient client)
        {
            Console.WriteLine("SellTicketsServer - LogIn");

            this._userService.LogIn(user.Username, user.Password);
            this._loggedClients.Add(user.Username,client);
            Console.WriteLine("SellTicketsServer - Login succesfuly");

        }

        public void Logout(User user, ISellTicketsClient client)
        {
            Console.WriteLine("SellTicketsServer - LogOut");

            this._loggedClients.Remove(key: user.Username);
            Console.WriteLine("SellTicketsServer - LogOut succesfuly");

        }

        public void SellTickets(string idMatch, string quantity, string buyerPerson, string username)
        {
            Console.WriteLine("SellTicketsServer - SellTickets");

            this._ticketService.Add(quantityS: quantity,idMatchS: idMatch,person: buyerPerson);
            Match m = this._matchService.FindById(id: idMatch);
            Notify(usernameI: username, match: m);
            Console.WriteLine("SellTicketsServer - SellTickets succesfully");

        }

        public List<Match> GetAllMatches()
        {
            Console.WriteLine("SellTicketsServer - GetAll");

            try
            {
                return this._matchService.GetAll();
            }
            catch (Exception e)
            {
                Console.WriteLine("SellTicketsServer - GetAll unsuccesfully");
                throw new ControllerException(e.Message);
            }

        }

        public List<Match> GetFilteredAndSortedMatches()
        {
            Console.WriteLine("SellTicketsServer - GetAllFilteredAndSorted");
            try
            {
                return this._matchService.GetAllMatchesWithRemainingTickets();
            }
            catch (Exception e)
            {
                Console.WriteLine("SellTicketsServer - GetAllFilteredAndSorted unsuccesfully");
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
                    Console.WriteLine("Notified : " + username);
                    this._loggedClients[username].ShowUpdates(match);
                }
            }
        }
    }
}
