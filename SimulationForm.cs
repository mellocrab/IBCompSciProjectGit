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

        private int _width = 256;
        private int _height = 128;

        private void Form1_Load(object sender, EventArgs e)
        {
            myGrid = new GridLoop(_width, _height);

            pbox_main.SizeMode = PictureBoxSizeMode.StretchImage;
            pbox_main.Image = myGrid._image;

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
                } catch(Exception e)
                {

                }

                if (Control.MouseButtons == MouseButtons.Left)
                {
                    isMouseDown = true;
                }

                float valX = (float)(point.X - pbox_main.Bounds.Left) / (pbox_main.Bounds.Right - pbox_main.Bounds.Left);
                float valY = (float)(point.Y - pbox_main.Bounds.Top) / (pbox_main.Bounds.Bottom - pbox_main.Bounds.Top);


                myGrid.IterationLoop(valX, valY, isMouseDown);
                pbox_main.Refresh();

                await Task.Delay(8);
            }
        }
    }
}
