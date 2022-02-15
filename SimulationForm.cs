using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using IBCompSciProject.Loop;


namespace IBCompSciProject
{
    public partial class SimulationForm : Form
    {

        public static Color BackgroundColor { get; } = Color.Transparent;

        //The grid loop used for the simulation
        private GridLoop _myGrid;

        //These variables hold the dimensions of the simulation grid
        private int _width = 256;
        private int _height = 128;

        //The current type of cell that should be "drawn" by the "brush" on the grid.
        private Cell.Type _currentDrawType;

        //This determines the radius of the drawing "brush"
        private int _brushRadius;
        private bool _previouslyHeldDown;

        private float _previousX;
        private float _previousY;

        //Constructor, intialize components
        public SimulationForm()
        {
            InitializeComponent();  
        }

        //This function is called when the form is loaded. This sets up various initial settings for the form
        private void Form1_Load(object sender, EventArgs e)
        {
            //Initilizes certain aspects of the simulation
            InitializeSimulation();

            //Initialize certain settings for form
            SetUpFormSettings();
            
            //Starts the UpdateLoop, which will start the game loop
            UpdateLoop();
        }

        private void SetUpFormSettings()
        {
            //Sets up how the form itself is to function
            this.CenterToScreen();
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.Text = "Simulation!";

            //Sets up the form's picture box settings
            pbox_main.SizeMode = PictureBoxSizeMode.StretchImage;
            pbox_main.Image = _myGrid.Image;
        }

        private void InitializeSimulation()
        {
            //Declares a new grid loop, which processes the simulation
            _myGrid = new GridLoop(_width, _height);

            //Sets up the radius of the brush
            _brushRadius = tbar_radius.Value;
            _currentDrawType = Cell.Type.sand;
        }

        public async void UpdateLoop()
        {
            while (true)
            {
                bool isMouseDown = false;

                //A System.ObjectDisposedException is thrown for some reason when this code is run. It is an unimportant error, so we catch it here.
                Point point = new Point();
                try
                {
                    point = PointToClient(Cursor.Position);
                }
                catch (Exception e)
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

                //The following lists store the coordinates that the simulation should draw a circle around.
                List<float> listX = new List<float>();
                List<float> listY = new List<float>();

                if (_previouslyHeldDown)
                {
                    //If the previous frame the mouse has been held down, use algorithm to fill in spots between old mouse position and new mouse position
                    listX = DrawLine(valX, valY, _previousX, _previousY, out listY);
                } else
                {
                    //Else, just input current mouse position
                    listX.Add(valX);
                    listY.Add(valY);
                }

                //Call the simulation
                _myGrid.IterationLoop(listX, listY, isMouseDown, _currentDrawType, _brushRadius);
                
                //Refresh the box so that the changes are updated on the display
                pbox_main.Refresh();

                //Delay the async so that the program runs at around 60 fps
                await Task.Delay(8);

                //If the mouse is held down, save this fact and its position. This will aid in drawing filled lines accross the screen
                if (isMouseDown)
                {
                    _previouslyHeldDown = true;
                    _previousX = valX;
                    _previousY = valY;
                } else
                {
                    _previouslyHeldDown = false;
                }

                //Do not run simulation if the user is on the main menu. Simply stall
                while (MainMenu.CurrentMenu != 1)
                {
                    await Task.Delay(100);
                }
            }
        }

        //Algorithm for filling in lines
        private List<float> DrawLine(float newX, float newY, float oldX, float oldY, out List<float> yOutput)
        {
            //The output lists
            List<float> xlist = new List<float>();
            List<float> ylist = new List<float>();

            //Add the initial position of mouse
            xlist.Add(newX);
            ylist.Add(newY);

            //The smaller, the more accurate. The more increments the lerp will take, the more positions added
            float accuracy = .05f;

            //Interpolate between old position and new position, adding each increment to the position lists
            float f = 0;
            while(f < 1)
            {
                xlist.Add(Lerp(newX, oldX, f));
                ylist.Add(Lerp(newY, oldY, f));
                f += accuracy;
            }

            //Set the y output list to the y position list
            yOutput = ylist;

            //Return the x position list
            return xlist;
        }

        //Linear interpolation function
        private float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
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
        private void btn_gas_Click(object sender, EventArgs e)
        {
            _currentDrawType = Cell.Type.gas;
        }
        //When the scroll bar is manipulated, set the brush radius to this value
        private void tbar_radius_Scroll(object sender, EventArgs e)
        {
            _brushRadius = tbar_radius.Value;
        }
        //When the back button is clicked, switch forms to main menu. Save which form is active
        private void btn_back_Click(object sender, EventArgs e)
        {
            MainMenu.simulationForm.Hide();
            MainMenu.mainMenu.Show();
            MainMenu.CurrentMenu = 0;
        }
        //Clear the entire simulation when this button is clicked(set everything to air)
        private void btn_clear_Click(object sender, EventArgs e)
        {
            _myGrid.ClearGrid();
        }
        #endregion



    }
}
