using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabMppCsharp.utils;
using LabMPPCS.controller;
using LabMPPCS.repository;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DatabaseConnectionManager databaseConnectionManager = new DatabaseConnectionManager();
            
            MatchRepository matchRepository = new MatchRepository(databaseConnectionManager,"matches");
            TicketRepository ticketRepository = new TicketRepository(databaseConnectionManager,"tickets");

            MatchController matchController = new MatchController(matchRepository);
            TicketController ticketController = new TicketController(ticketRepository,matchRepository);
            Application.Run(new Form1(matchController,ticketController));
        }
    }
}
