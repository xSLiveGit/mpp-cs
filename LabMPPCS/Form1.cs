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
        public Form1(MatchController matchController,TicketController ticketController)
        {
            InitializeComponent();
            this.matchController = matchController;
            this.ticketController = ticketController;
            Initialise();
        }

        private void Initialise()
        {
            BindMatches();
        }


        private void BindMatches()
        {
            matchList = new BindingList<Match>(matchController.GetAll());
            dataGridViewMatches.Columns.Clear();//DE CE DACA ADAUG DIN INTERFATA NU POT LEGA BUTOANELE?
            dataGridViewMatches.DataSource = matchList.Select(match =>
                    new {
                        Team1 = match.Team1,
                        Team2 = match.Team2,
                        Stage = match.Stage,
                        Tickets = match.Tickets,
                        Price = match.Price
                    }).ToList();
        }

        private void dataGridViewMatches_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_Add_Click(object sender, EventArgs e)
        {
//            try
//            {
                if (dataGridViewMatches.SelectedRows.Count != 1)
                {
                    MessageBox.Show("Select 1 row");
                    return;
                }

                int index = dataGridViewMatches.CurrentRow.Index;
                Match selectedShow = matchList[index];

                string clientName = textBox_Person.Text;
                int numberOfTickets = Int32.Parse(textBox_Tickets.Text);
                ticketController.Add(textBox_Tickets.Text, selectedShow.Id.ToString(), textBox_Person.Text);
                BindMatches();
//            }
//            catch (Exception ex) when (ex is ControllerException || ex is MySqlException)
//            {
//                MessageBox.Show(ex.Message);
//            }
//            catch (System.NullReferenceException e1)
//            {
//                MessageBox.Show("Invalid pointer." + e1.Message);
//            }
        }
    }
}
