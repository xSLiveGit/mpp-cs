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
using SellTicketsModel.entity;
using SellTicketsModel.exception;
using SellTicketsServices;

namespace LabMPPCS
{
    public partial class LogInForm : Form
    {
        private ISellTicketsServer server;
        public LogInForm(ISellTicketsServer server)
        {
            InitializeComponent();
            this.server = server;
        }

        private void button_LogIn_Click(object sender, EventArgs e)
        {
            String username = textBox_Password.Text;
            String password = textBox_Username.Text;
            try
            {
                this.server.Login(new User(username, password),this);
                Form1 form = new Form1(server,this);
                form.Show();
                this.Hide();
                
            }
            catch (ControllerException e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
    }
}
