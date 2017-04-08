using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using persistence.repository;
using persistence.utils;
using persistence.utils.validator;
using SellTicketsNetworking;
using SellTicketsServices;
using SellTicketsServices.services;
using SellTicketsNetworking.rpcprotocol;
namespace Server
{
    class StartServer
    {
        static void Main(string[] args)
        {
            DatabaseConnectionManager databaseConnectionManager = new DatabaseConnectionManager();

            UserValidator userValidator = new UserValidator();
            MatchValidator matchValidator = new MatchValidator();
            TicketValidator ticketValidator = new TicketValidator();

            UserRepository userRepository = new UserRepository(databaseConnectionManager,"users",userValidator);
            TicketRepository ticketRepository = new TicketRepository(databaseConnectionManager,"tickets", ticketValidator);
            MatchRepository matchRepository = new MatchRepository(databaseConnectionManager,"matches",matchValidator);


            UserService userService = new UserService(userRepository);
            MatchService matchService = new MatchService(matchRepository);
            TicketService ticketService = new TicketService(ticketRepository,matchRepository);

            SellTicketsServer sellTicketsServer = new SellTicketsServer(ticketService,userService,matchService);
            ConcurrentServer concurrentServer = new SerialSellTicketsServer("127.0.0.1", 5555, sellTicketsServer);
            concurrentServer.Start();
        }

        public class SerialSellTicketsServer : ConcurrentServer
        {
            private ISellTicketsServer server;
            private SellTicketsObjectWorker worker;
            public SerialSellTicketsServer(string host, int port, SellTicketsServer server) : base(host, port)
            {
                this.server = server;
                Console.WriteLine("SerialChatServer...");
            }
            protected override Thread CreateWorker(TcpClient client)
            {

                worker = new SellTicketsObjectWorker(server, client);
                return new Thread(new ThreadStart(worker.Run));
            }
        }
    }
}
