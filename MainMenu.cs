﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IBCompSciProject
{
    public partial class MainMenu : Form
    {
        public static Form mainMenu;
        public static Form simulationForm;

        public MainMenu()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Main Menu!";

            this.CenterToScreen();
            this.MaximizeBox = false;
            this.MinimizeBox = true;


            mainMenu = this;
            simulationForm = new SimulationForm();
            simulationForm.Hide();


        }

        public static byte CurrentMenu;

        private void button1_Click(object sender, EventArgs e)
        {
            mainMenu.Hide();
            
            simulationForm.Show();

            CurrentMenu = 1;
        }

        private void lbl_title_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Exit();
        }
    }
}