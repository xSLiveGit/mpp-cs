using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using SellTicketsModel.entity;
using SellTicketsModel.exception;
using SellTicketsServices.services;
using SellTicketsServices;
namespace SellTicketsNetworking.rpcprotocol
{
    public class SellTicketsServerProxy : ISellTicketsServer
    {
        private readonly string _host;
        private readonly int _port;

        private ISellTicketsClient _client;

        private NetworkStream _stream;

        private IFormatter _formatter;
        private TcpClient _connection;

        private readonly Queue<IResponse> _responses;
        private volatile bool _finished = false;
        private EventWaitHandle _waitHandle;
        public SellTicketsServerProxy(string host, int port)
        {
            this._host = host;
            this._port = port;
            _responses = new Queue<IResponse>();
            this.InitializeConnection();
        }

        public virtual void Login(User user, ISellTicketsClient client)
        {
            InitializeConnection();
            var udto = DTOUtils.GetDTO(user);
            SendRequest(new LoginRequest(udto));
            var response = ReadResponse();
            if (response is OkResponse)
            {
                this._client = client;
                return;
            }
            else if (response is ErrorResponse)
            {
                var err = (ErrorResponse)response;
                CloseConnection();
                throw new ControllerException(err.Message);
            }
        }


        public virtual void Logout(User user, ISellTicketsClient client)
        {
            UserDTO udto = DTOUtils.GetDTO(user);
            SendRequest(new LogoutRequest(udto));
            IResponse response = ReadResponse();
            CloseConnection();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new ControllerException(err.Message);
            }
        }



        private void CloseConnection()
        {
            _finished = true;
            try
            {
                _stream.Close();
                //output.close();
                _connection.Close();
                _waitHandle.Close();
                _client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

        private void SendRequest(Request request)
        {
            try
            {
                _formatter.Serialize(_stream, request);
                _stream.Flush();
            }
            catch (Exception e)
            {
                throw new ControllerException("Error sending object " + e);
            }

        }

        private IResponse ReadResponse()
        {
            IResponse response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (_responses)
                {
                    //Monitor.Wait(responses); 
                    response = _responses.Dequeue();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }
        private void InitializeConnection()
        {
            try
            {
                _connection = new TcpClient(_host, _port);
                _stream = _connection.GetStream();
                _formatter = new BinaryFormatter();
                _finished = false;
                _waitHandle = new AutoResetEvent(false);
                StartReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        private void StartReader()
        {
            Thread tw = new Thread(Run);
            tw.Start();
        }


        private void HandleUpdate(IUpdateResponse update)
        {
            if (update is ShowModifiesResponse)
            {
                ShowModifiesResponse newMatchDto = (ShowModifiesResponse) update;
                Match newMatch = DTOUtils.GetFromDTO(newMatchDto.Match);
                _client.ShowUpdates(newMatch);
            }
        }

        public virtual void Run()
        {
            while (!_finished)
            {
                try
                {
                    object response = _formatter.Deserialize(_stream);
                    Console.WriteLine("response received " + response);
                    if (response is IUpdateResponse)
                    {
                        HandleUpdate((IUpdateResponse)response);
                    }
                    else
                    {
                        lock (_responses)
                        {
                            _responses.Enqueue((IResponse)response);
                        }
                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading error " + e);
                }
            }
        }

        public void SellTickets(string idMatch, string quantity, string buyerPerson, string username)
        {
            Console.WriteLine("Proxy - sellTicketsRequest");
            SendRequest(new SellTicketsRequest(new SalesDTO(
                idMatch: idMatch,
                quantity: quantity,
                person: buyerPerson,
                username: username)));
            var response = ReadResponse();
            
            Console.WriteLine("Proxy Sell ticket response: " + response.GetType().ToString());
            if (response is OkResponse) return;
            if (response is ErrorResponse)
            {
                Console.WriteLine("Proxy Sell ticket error response: ");

                throw new ControllerException(((ErrorResponse)response).Message);
            }
        }

        public List<Match> GetAllMatches()
        {
            List<Match> matches = new List<Match>();

            SendRequest(request: new GetAllRequest());
            var response = ReadResponse();
            if (response is ErrorResponse)
            {
                throw new ControllerException(message: ((ErrorResponse)response).Message);
            }
            if (response is GetAllReponse)
            {
                ((GetAllReponse)response).List.ForEach(action: el=>matches.Add(item: new Match(id: el.Id,team1: el.Team1,team2: el.Team2,stage: el.Stage,tickets: el.Tickets,price: el.Price)));
            }
            
            return matches;
        }

        

        public List<Match> GetFilteredAndSortedMatches()
        {
            List<Match> matches = new List<Match>();

            SendRequest(new GetAllFilteredAndSortedRequest());
            var response = ReadResponse();
            if (response is ErrorResponse)
            {
                throw new ControllerException(((ErrorResponse)response).Message);
            }
            if (response is GetAllFilteredAndSortedResponse)
            {
                ((GetAllFilteredAndSortedResponse)response).List.ForEach(el => matches.Add(new Match(el.Id, el.Team1, el.Team2, el.Stage, el.Tickets, el.Price)));
            }

            return matches;
        }
    }
}
