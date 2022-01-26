using System;
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

        private void button1_Click(object sender, EventArgs e)
        {
            mainMenu.Hide();
            
            simulationForm.Show();

        }
    }
}
