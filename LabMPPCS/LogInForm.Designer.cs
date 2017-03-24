namespace LabMPPCS
{
    partial class LogInForm
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
            this.textBox_Username = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_LogIn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_Username
            // 
            this.textBox_Username.Location = new System.Drawing.Point(92, 23);
            this.textBox_Username.Name = "textBox_Username";
            this.textBox_Username.Size = new System.Drawing.Size(109, 20);
            this.textBox_Username.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "User";
            // 
            // textBox_Password
            // 
            this.textBox_Password.Location = new System.Drawing.Point(92, 68);
            this.textBox_Password.Name = "textBox_Password";
            this.textBox_Password.Size = new System.Drawing.Size(109, 20);
            this.textBox_Password.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Password";
            // 
            // button_LogIn
            // 
            this.button_LogIn.Location = new System.Drawing.Point(12, 103);
            this.button_LogIn.Name = "button_LogIn";
            this.button_LogIn.Size = new System.Drawing.Size(189, 29);
            this.button_LogIn.TabIndex = 2;
            this.button_LogIn.Text = "LogIn";
            this.button_LogIn.UseVisualStyleBackColor = true;
            this.button_LogIn.Click += new System.EventHandler(this.button_LogIn_Click);
            // 
            // LogInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 146);
            this.Controls.Add(this.button_LogIn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_Password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_Username);
            this.Name = "LogInForm";
            this.Text = "LogInForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Username;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_LogIn;
    }
}