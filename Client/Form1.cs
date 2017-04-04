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
using LabMPPCS.domain;
using MySql.Data.MySqlClient;

namespace LabMPPCS
{
    public partial class Form1 : Form
    {
        private readonly MatchController matchController;
        private readonly TicketController ticketController;
        private BindingList<Match> matchList;
        private Form logInForm;
        private bool filtered = false;
        public Form1(MatchController matchController,TicketController ticketController,Form logInForm)
        {
            InitializeComponent();
            this.matchController = matchController;
            this.ticketController = ticketController;
            this.logInForm = logInForm;
            Initialise();
        }

        private void Initialise()
        {
            BindMatches();
        }


        private void BindMatches()
        {
            BindTable(matchController.GetAll());
        }

        private void dataGridViewMatches_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewMatches.SelectedRows.Count != 1)
                {
                    MessageBox.Show("Select 1 row");
                    return;
                }

                int index = dataGridViewMatches.CurrentRow.Index;
                Match selectedShow = matchList[index];

                string clientName = textBox_Person.Text;
                ticketController.Add(textBox_Tickets.Text, selectedShow.Id.ToString(), textBox_Person.Text);
                BindMatches();
            }
            catch (Exception ex) when (ex is ControllerException || ex is MySqlException)
            {
                MessageBox.Show(ex.Message);
            }
            catch (System.NullReferenceException e1)
            {
                MessageBox.Show("Invalid pointer." + e1.Message);
            }
        }

        private void button_LogOut_Click(object sender, EventArgs e)
        {
            this.logInForm.Show();
            this.Close();
        }

        public void BindTable(IList<Match> source)
        {
            matchList = new BindingList<Match>(source);
            dataGridViewMatches.Columns.Clear();//DE CE DACA ADAUG DIN INTERFATA NU POT LEGA BUTOANELE?
            dataGridViewMatches.DataSource = matchList.Select(match =>
                    new {
                        Team1 = match.Team1,
                        Team2 = match.Team2,
                        Stage = match.Stage,
                        Tickets = match.Tickets,
                        Price = match.Price
                    }).ToList();
            foreach (DataGridViewRow Myrow in dataGridViewMatches.Rows)
            {            //Here 2 cell is target value and 1 cell is Volume
                if (Convert.ToInt32(Myrow.Cells[3].Value).Equals(0))// Or your condition 
                {
                    Myrow.DefaultCellStyle.ForeColor = Color.Red;
                }
                else
                {
                    Myrow.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void button_FilterSort_Click(object sender, EventArgs e)
        {
            if (!filtered)
            {
                BindTable(matchController.GetAllMatchesWithRemainingTickets());
                filtered = true;
            }
            else
            {
                BindTable(matchController.GetAll());
                filtered = false;
            }
        }
       
    }
}
