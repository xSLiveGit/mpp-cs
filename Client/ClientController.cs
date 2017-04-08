using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabMPPCS;
using Microsoft.SqlServer.Server;
using SellTicketsModel.entity;
using SellTicketsServices;

namespace Client
{
    public class ClientController : ISellTicketsClient
    {
        private Form1 _parentForm = null;

        private delegate void UpdateM(Match m);
        public Form1 ParentForm
        {
            get { return _parentForm; }
            set { _parentForm = value; }
        }

        private User _user = null;
        private ISellTicketsServer _server = null;

        public ClientController(ISellTicketsServer server)
        {
            this._server = server;
        }

        public void ShowUpdates(Match match)
        {
            _parentForm.UpdateMatch(match);
        }

        public List<Match> GetAllMatches()
        {
            Console.WriteLine("Client controller - GetAll");
            return this._server.GetAllMatches();
        }

        public List<Match> GetAllMatchesFilteredAndSorted()
        {
            Console.WriteLine("Client controller - GetAllFilteredAndSorted");
            return this._server.GetFilteredAndSortedMatches();
        }

        public void SellTickets(String buyer, String id,String quantity)
        {
            this._server.SellTickets(id,quantity,buyer,this._user.Username);
        }

        public void LogOut()
        {
            this._server.Logout(this._user,this);
        }

        public void LogIn(String username,String password)
        {
            User u = new User(username, password);
            this._server.Login(u, this);
            this._user = u;
        }

    }
}
