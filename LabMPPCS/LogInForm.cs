using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabMppCsharp.utils.exceptions;
using LabMPPCS.controller;

namespace LabMPPCS
{
    public partial class LogInForm : Form
    {
        private UserController userController;
        private MatchController matchController;
        private TicketController ticketController;
        public LogInForm(controller.UserController userControl,MatchController matchController,TicketController ticketController)
        {
            InitializeComponent();

            this.userController = userControl;
            this.matchController = matchController;
            this.ticketController = ticketController;
        }

        private void button_LogIn_Click(object sender, EventArgs e)
        {
            String username = textBox_Password.Text;
            String password = textBox_Username.Text;
            try
            {
                var u = this.userController.LogIn(username, password);
                if (u != null)
                {
                    Form1 form = new Form1(matchController,ticketController,this);
                    form.Show();
                    this.Hide();
                }
            }
            catch (ControllerException e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
    }
}
