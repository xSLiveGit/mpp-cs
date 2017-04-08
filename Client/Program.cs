﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client;
using SellTicketsServices;
using SellTicketsNetworking.rpcprotocol;

namespace LabMPPCS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            const string host = "127.0.0.1";
            const int port = 5555; 
            ISellTicketsServer server = new SellTicketsServerProxy(host,port);
            ClientController clientController = new ClientController(server);
            Application.Run(new LogInForm(clientController));
        }
    }
}
