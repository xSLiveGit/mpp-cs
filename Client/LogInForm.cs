using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client;
using SellTicketsModel.entity;
using SellTicketsModel.exception;
using SellTicketsServices;

namespace LabMPPCS
{
    public partial class LogInForm : Form
    {
        private ClientController _client;
        public LogInForm(ClientController client)
        {
            InitializeComponent();
            this._client = client;
        }

        private void button_LogIn_Click(object sender, EventArgs e)
        {
            String username = textBox_Password.Text;
            String password = textBox_Username.Text;
            try
            {
                this._client.LogIn(username, password);
                Form1 form = new Form1(this,_client);
                this._client.ParentForm = form;
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
