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
        //Holds the forms that compose this project
        private Form _mainMenu;
        private Form _simulationForm;

        //Holds data to signify which menu is active. 0 = 
        public byte CurrentMenu { get; set; }

        //Constructor
        public MainMenu()
        {
            InitializeComponent();
        }

        //Called when the form loads
        private void Form1_Load(object sender, EventArgs e)
        {
            //Set the settings of the form
            SetFormSettings();

            //Assign properties and create the simulation form
            _mainMenu = this;
            _simulationForm = new SimulationForm(this);
            _simulationForm.Hide();
        }

        //Set the text, position, and size of the form
        public void SetFormSettings()
        {
            this.Text = "Main Menu!";
            this.CenterToScreen();
            this.MaximizeBox = false;
            this.MinimizeBox = true;
        }

        //When the simulation button is clicked, switch to the simulation form
        private void button1_Click(object sender, EventArgs e)
        {
            SwitchSimulationMenu();
        }

        //Swtich displayed form to main menu
        public void SwitchMainMenu()
        {
            _mainMenu.Show();
            _simulationForm.Hide();
            CurrentMenu = 0;
        }

        //Switch displayed form to simulation
        public void SwitchSimulationMenu()
        {
            _mainMenu.Hide();
            _simulationForm.Show();
            CurrentMenu = 1;
        }
        
        //When the exit button is clicked, close the program
        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
