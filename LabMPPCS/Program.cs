using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabMppCsharp.utils;
using LabMPPCS.controller;
using LabMPPCS.repository;
using LabMPPCS.Validator;

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
            
            MatchValidator matchValidator = new MatchValidator();
            TicketValidator ticketValidator = new TicketValidator();
            UserValidator userValidator = new UserValidator();

            MatchRepository matchRepository = new MatchRepository(databaseConnectionManager,"matches",matchValidator);
            TicketRepository ticketRepository = new TicketRepository(databaseConnectionManager,"tickets",ticketValidator);
            UserRepository userRepository = new UserRepository(databaseConnectionManager,"users",userValidator);

            MatchController matchController = new MatchController(matchRepository);
            TicketController ticketController = new TicketController(ticketRepository,matchRepository);
            UserController userController = new UserController(userRepository);

            Application.Run(new LogInForm(userController, matchController,ticketController));
        }
    }
}
