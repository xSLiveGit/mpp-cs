using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SellTicketsNetworking
{
    public abstract class AbstractServer
    {
        private TcpListener _server;
        private readonly String _host;
        private readonly int _port;

        protected AbstractServer(String host, int port)
        {
            this._host = host;
            this._port = port;
        }
        public void Start()
        {
            IPAddress adr = IPAddress.Parse(_host);
            IPEndPoint ep = new IPEndPoint(adr, _port);
            _server = new TcpListener(ep);
            _server.Start();
            while (true)
            {
                Console.WriteLine("Waiting for clients ...");
                TcpClient client = _server.AcceptTcpClient();
                Console.WriteLine("Client connected ...");
                ProcessRequest(client);
            }
        }

        public abstract void ProcessRequest(TcpClient client);

    }


    public abstract class ConcurrentServer : AbstractServer
    {
        protected ConcurrentServer(string host, int port) : base(host, port)
        { }

        public override void ProcessRequest(TcpClient client)
        {

            Thread t = CreateWorker(client);
            t.Start();

        }

        protected abstract Thread CreateWorker(TcpClient client);

    }
}
