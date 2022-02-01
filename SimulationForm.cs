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

        public static Color BackgroundColor = Color.Transparent;

        GridLoop myGrid;

        //These variables hold the dimensions of the simulation grid
        private int _width = 256;
        private int _height = 128;

        private Cell.Type _currentDrawType;

        //This determines the radius of the drawing "brush"
        private int _brushRadius;


        //Constructor, intialize components
        public SimulationForm()
        {
            InitializeComponent();  
        }

        //This function is called when the form is loaded. This sets up various initial settings for the form
        private void Form1_Load(object sender, EventArgs e)
        {
            //Sets up how the form itself is to function
            this.CenterToScreen();
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.Text = "Simulation!";

            //Declares a new grid loop, which processes the simulation
            myGrid = new GridLoop(_width, _height);


            //Sets up the form's picture box settings
            pbox_main.SizeMode = PictureBoxSizeMode.StretchImage;
            pbox_main.Image = myGrid._image;
            

            //Sets up the radius of the brush
            _brushRadius = tbar_radius.Value;
            _currentDrawType = Cell.Type.sand;

            //Starts the UpdateLoop, which will start the game loop
            UpdateLoop();
        }


        public async void UpdateLoop()
        {
            while (true)
            {
                bool isMouseDown = false;

                //An error is thrown for some reason when this code is run. It is an unimportant error, so we catch it here.
                Point point = new Point();
                try
                {
                    point = PointToClient(Cursor.Position);

                } catch (Exception e)
                {

                }


                //This controls input. When the right mouse button is held down, we set a variable indicating that is true
                if (Control.MouseButtons == MouseButtons.Right)
                {
                    isMouseDown = true;
                }


                //This calculates the position of the mouse relative to the picture box. These coordinates will be used to indicate
                //the brush position
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



        // Material buttons: These will set the material of the brush
        #region Input buttons

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
        #endregion

        private void Form1_FormClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {

            Application.Exit();
        }


    }
}
