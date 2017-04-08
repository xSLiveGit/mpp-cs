using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SellTicketsModel.entity;
using SellTicketsModel.exception;
using SellTicketsServices;

namespace SellTicketsNetworking.rpcprotocol
{
    public class SellTicketsObjectWorker : ISellTicketsClient
    {
        private readonly ISellTicketsServer server;
        private readonly TcpClient connection;

        private readonly NetworkStream stream;
        private readonly IFormatter formatter;
        private volatile bool _connected = true;
        public SellTicketsObjectWorker(ISellTicketsServer server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {

                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                _connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public virtual void Run()
        {
            while (_connected)
            {
                try
                {
                    if (!stream.DataAvailable)
                        continue;
                    object request = formatter.Deserialize(stream);
                    object response = HandleRequest((Request)request);
                    if (response != null)
                    {
                        SendResponse((IResponse)response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            try
            {
                stream.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }
      

        private IResponse HandleRequest(Request request)
        {
            IResponse response = null;
            if (request is LoginRequest)
            {
                Console.WriteLine(value: "Login request ...");
                LoginRequest logReq = (LoginRequest)request;
                UserDTO udto = logReq.User;
                User user = DTOUtils.GetFromDTO(usdto: udto);
                try
                {
                    lock (server)
                    {
                        server.Login(user: user, client: this);
                    }
                    return new OkResponse();
                }
                catch (ControllerException e)
                {
                     _connected = false;
                    return new ErrorResponse(message: e.Message);
                }
            }
            if (request is LogoutRequest)
            {
                Console.WriteLine(value: "Logout request");
                LogoutRequest logReq = (LogoutRequest)request;
                UserDTO udto = logReq.User;
                User user = DTOUtils.GetFromDTO(usdto: udto);
                try
                {
                    lock (server)
                    {

                        server.Logout(user: user, client: this);
                    }
                    //_connected = false;
                    return new OkResponse();

                }
                catch (ControllerException e)
                {
                    _connected = false;
                    return new ErrorResponse(message: e.Message);
                }
            }
            if (request is GetAllRequest)
            {
                Console.WriteLine(value: "GetAllRequest");
                GetAllRequest getAllRequest = (GetAllRequest) request;
                try
                {
                    List<Match> list = null;
                    lock (server)
                    {
                        list = server.GetAllMatches();
                    }
                   // _connected = false;
                    response =  new GetAllReponse(list: list);
                }
                catch (Exception e)
                {
                    _connected = false;
                    response = new ErrorResponse(message: e.Message);
                }
            }
            if (request is GetAllFilteredAndSortedRequest)
            {
                Console.WriteLine(value: "GetAllFilteredAndSortedRequest");
                GetAllFilteredAndSortedRequest getAllRequest = (GetAllFilteredAndSortedRequest)request;
                try
                {
                    List<Match> list = null;
                    lock (server)
                    {
                        list = server.GetFilteredAndSortedMatches();
                    }
                   // _connected = false;
                    response = new GetAllFilteredAndSortedResponse(list: list);
                }
                catch (Exception e)
                {
                    _connected = false;
                    response = new ErrorResponse(message: e.Message);
                }
            }
            if (request is SellTicketsRequest)
            {
                Console.WriteLine(value: "Worker - sellTickets");
                SellTicketsRequest sellTicketsRequest = (SellTicketsRequest)request;
                try
                {
                    SalesDTO sale = sellTicketsRequest.Sale;
                    lock (server)
                    {
                        this.server.SellTickets(idMatch: sale.IdMatch, quantity: sale.Quantity, buyerPerson: sale.Person,
                            username: sale.Username);
                    }
                    response = new OkResponse();
                }
                catch (Exception e)
                {
                    _connected = false;
                    response = new ErrorResponse(e.Message);
                }
            }
            return response;
        }

        private void SendResponse(IResponse response)
        {
            Console.WriteLine("sending response " + response);
            formatter.Serialize(stream, response);
            stream.Flush();
        }

        public void ShowUpdates(Match match)
        {
            IResponse response = new ShowModifiesResponse(new MatchDTO(match));
            SendResponse(response);
        }
    }

}
