using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IBCompSciProject.Loop;
using System.Threading.Tasks;


namespace IBCompSciProject
{
    public partial class SimulationForm : Form
    {
        public SimulationForm()
        {
            InitializeComponent();
        }

        GridLoop myGrid;

        private int width = 100;
        private int height = 50;

        private void Form1_Load(object sender, EventArgs e)
        {
            myGrid = new GridLoop(width, height);

            pbox_main.SizeMode = PictureBoxSizeMode.Zoom;
            pbox_main.Image = myGrid._image;

            UpdateLoop();
        }


        public async void UpdateLoop()
        {
            while (true)
            {
                Console.WriteLine("dfdf");

                myGrid.IterationLoop();
                pbox_main.Refresh();

                await Task.Delay(8);
            }
        }
    }
}
