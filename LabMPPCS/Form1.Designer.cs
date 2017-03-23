namespace LabMPPCS
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewMatches = new System.Windows.Forms.DataGridView();
            this.Team1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Team2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tickets = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox_Person = new System.Windows.Forms.TextBox();
            this.textBox_Tickets = new System.Windows.Forms.TextBox();
            this.button_Add = new System.Windows.Forms.Button();
            this.button_FilterSort = new System.Windows.Forms.Button();
            this.button_LogOut = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMatches)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewMatches
            // 
            this.dataGridViewMatches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMatches.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Team1,
            this.Team2,
            this.Stage,
            this.Price,
            this.Tickets});
            this.dataGridViewMatches.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewMatches.Name = "dataGridViewMatches";
            this.dataGridViewMatches.Size = new System.Drawing.Size(544, 293);
            this.dataGridViewMatches.TabIndex = 0;
            this.dataGridViewMatches.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMatches_CellContentClick);
            // 
            // Team1
            // 
            this.Team1.HeaderText = "Team1";
            this.Team1.Name = "Team1";
            // 
            // Team2
            // 
            this.Team2.HeaderText = "Team2";
            this.Team2.Name = "Team2";
            // 
            // Stage
            // 
            this.Stage.HeaderText = "Stage";
            this.Stage.Name = "Stage";
            // 
            // Price
            // 
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            // 
            // Tickets
            // 
            this.Tickets.HeaderText = "Tickets";
            this.Tickets.Name = "Tickets";
            // 
            // textBox_Person
            // 
            this.textBox_Person.Location = new System.Drawing.Point(12, 328);
            this.textBox_Person.Name = "textBox_Person";
            this.textBox_Person.Size = new System.Drawing.Size(220, 20);
            this.textBox_Person.TabIndex = 1;
            // 
            // textBox_Tickets
            // 
            this.textBox_Tickets.Location = new System.Drawing.Point(12, 354);
            this.textBox_Tickets.Name = "textBox_Tickets";
            this.textBox_Tickets.Size = new System.Drawing.Size(220, 20);
            this.textBox_Tickets.TabIndex = 2;
            // 
            // button_Add
            // 
            this.button_Add.Location = new System.Drawing.Point(238, 328);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(98, 46);
            this.button_Add.TabIndex = 3;
            this.button_Add.Text = "Add";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // button_FilterSort
            // 
            this.button_FilterSort.Location = new System.Drawing.Point(342, 328);
            this.button_FilterSort.Name = "button_FilterSort";
            this.button_FilterSort.Size = new System.Drawing.Size(98, 46);
            this.button_FilterSort.TabIndex = 4;
            this.button_FilterSort.Text = "Filter and Sort";
            this.button_FilterSort.UseVisualStyleBackColor = true;
            // 
            // button_LogOut
            // 
            this.button_LogOut.Location = new System.Drawing.Point(446, 328);
            this.button_LogOut.Name = "button_LogOut";
            this.button_LogOut.Size = new System.Drawing.Size(110, 46);
            this.button_LogOut.TabIndex = 5;
            this.button_LogOut.Text = "LogOut";
            this.button_LogOut.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 386);
            this.Controls.Add(this.button_LogOut);
            this.Controls.Add(this.button_FilterSort);
            this.Controls.Add(this.button_Add);
            this.Controls.Add(this.textBox_Tickets);
            this.Controls.Add(this.textBox_Person);
            this.Controls.Add(this.dataGridViewMatches);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMatches)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewMatches;
        private System.Windows.Forms.DataGridViewTextBoxColumn Team1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Team2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tickets;
        private System.Windows.Forms.TextBox textBox_Person;
        private System.Windows.Forms.TextBox textBox_Tickets;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.Button button_FilterSort;
        private System.Windows.Forms.Button button_LogOut;
    }
}

