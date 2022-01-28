
namespace IBCompSciProject
{
    partial class MainMenu
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
            this.lbl_title = new System.Windows.Forms.Label();
            this.btn_sim = new System.Windows.Forms.Button();
            this.lbl_header = new System.Windows.Forms.Label();
            this.btn_exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.BackColor = System.Drawing.Color.Black;
            this.lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.ForeColor = System.Drawing.Color.PapayaWhip;
            this.lbl_title.Location = new System.Drawing.Point(544, 35);
            this.lbl_title.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lbl_title.MaximumSize = new System.Drawing.Size(467, 400);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(216, 46);
            this.lbl_title.TabIndex = 0;
            this.lbl_title.Text = "Main Menu";
            this.lbl_title.Click += new System.EventHandler(this.lbl_title_Click);
            // 
            // btn_sim
            // 
            this.btn_sim.Location = new System.Drawing.Point(552, 108);
            this.btn_sim.Name = "btn_sim";
            this.btn_sim.Size = new System.Drawing.Size(206, 54);
            this.btn_sim.TabIndex = 1;
            this.btn_sim.Text = "Go To Simulation";
            this.btn_sim.UseVisualStyleBackColor = true;
            this.btn_sim.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl_header
            // 
            this.lbl_header.AutoSize = true;
            this.lbl_header.BackColor = System.Drawing.Color.Transparent;
            this.lbl_header.Font = new System.Drawing.Font("MS Reference Specialty", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_header.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_header.Location = new System.Drawing.Point(46, 32);
            this.lbl_header.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lbl_header.MaximumSize = new System.Drawing.Size(467, 400);
            this.lbl_header.Name = "lbl_header";
            this.lbl_header.Size = new System.Drawing.Size(263, 92);
            this.lbl_header.TabIndex = 2;
            this.lbl_header.Text = "PARTICLE SIMULATION";
            this.lbl_header.Click += new System.EventHandler(this.label1_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(554, 194);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(206, 54);
            this.btn_exit.TabIndex = 3;
            this.btn_exit.Text = "Close Program";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 450);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.lbl_header);
            this.Controls.Add(this.btn_sim);
            this.Controls.Add(this.lbl_title);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainMenu";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Button btn_sim;
        private System.Windows.Forms.Label lbl_header;
        private System.Windows.Forms.Button btn_exit;
    }
}