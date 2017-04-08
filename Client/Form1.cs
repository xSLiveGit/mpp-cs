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

namespace LabMPPCS
{
    public partial class Form1 : Form
    {
        private ClientController clientController;
        private BindingList<Match> matchList;
        private readonly Form logInForm;
        private bool _filtered = false;
        public Form1(Form logInForm, ClientController clientController)
        {
            InitializeComponent();

            this.logInForm = logInForm;
            this.clientController = clientController;
            Initialise();
        }

        private void Initialise()
        {
            BindMatches();
        }


        private void BindMatches()
        {
            BindTable(clientController.GetAllMatches());
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
                clientController.SellTickets(textBox_Person.Text, selectedShow.Id.ToString(), textBox_Tickets.Text);
                BindMatches();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_LogOut_Click(object sender, EventArgs e)
        {
            this.logInForm.Show();
            this.Close();
        }

        public void BindTable(List<Match> source)
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
            if (!_filtered)
            {
                BindTable(clientController.GetAllMatchesFilteredAndSorted());
                _filtered = true;
            }
            else
            {
                BindTable(clientController.GetAllMatches());
                _filtered = false;
            }
        }
       
    }
}
