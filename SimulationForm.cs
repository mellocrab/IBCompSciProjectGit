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
using System.Windows.Input;


namespace IBCompSciProject
{
    public partial class SimulationForm : Form
    {

        public static Color backgroundColor = Color.Transparent;

        GridLoop myGrid;

        private int _width = 256;
        private int _height = 128;

        private Cell.Type _currentDrawType;

       
        private int _brushRadius;


        public SimulationForm()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.MaximizeBox = false;
            this.MinimizeBox = true;

            this.Text = "Simulation!";

            //this.BackColor = backgroundColor;
            this.BackgroundImage = Properties.Resources.pexels_pixabay_258112;
            this.Refresh();

            myGrid = new GridLoop(_width, _height);

            pbox_main.SizeMode = PictureBoxSizeMode.StretchImage;
            pbox_main.Image = myGrid._image;
            

            _brushRadius = tbar_radius.Value;
            _currentDrawType = Cell.Type.sand;

            UpdateLoop();
        }


        public async void UpdateLoop()
        {
            while (true)
            {
                bool isMouseDown = false;

                //Point worldPoint = new Point(Cursor.Position.X, Cursor.Position.Y);

                Point point = new Point();
                try
                {
                    point = PointToClient(Cursor.Position);

                } catch (Exception e)
                {

                }

                if (Control.MouseButtons == MouseButtons.Right)
                {
                    isMouseDown = true;
                }

                float valX = (float)(point.X - pbox_main.Bounds.Left) / (pbox_main.Bounds.Right - pbox_main.Bounds.Left);
                float valY = (float)(point.Y - pbox_main.Bounds.Top) / (pbox_main.Bounds.Bottom - pbox_main.Bounds.Top);


                myGrid.IterationLoop(valX, valY, isMouseDown, _currentDrawType, _brushRadius);
                pbox_main.Refresh();

                await Task.Delay(8);
                while (MainMenu.CurrentMenu != 1)
                {
                    await Task.Delay(100);
                }
            }
        }



        private void btn_sand_Click(object sender, EventArgs e)
        {
            _currentDrawType = Cell.Type.sand;
        }

        private void btn_water_Click(object sender, EventArgs e)
        {
            _currentDrawType = Cell.Type.water;
        }
        private void btn_air_Click(object sender, EventArgs e)
        {
            _currentDrawType = Cell.Type.empty;

        }

        private void btn_stone_Click(object sender, EventArgs e)
        {
            _currentDrawType = Cell.Type.solid;

        }

       

        private void tbar_radius_Scroll(object sender, EventArgs e)
        {
            _brushRadius = tbar_radius.Value;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pbox_main_Click(object sender, EventArgs e)
        {

        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            MainMenu.simulationForm.Hide();
            MainMenu.mainMenu.Show();
            MainMenu.CurrentMenu = 0;
        }
    }
}
