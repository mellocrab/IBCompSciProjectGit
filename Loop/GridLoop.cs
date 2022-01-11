using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace IBCompSciProject.Loop
{

    public class GridLoop
    {

        //Is the bitmap image
        public Bitmap _image;

        //Holds Cell Data
        private Cell[,] _grid;

        //Stores image width and height;
        int _width;
        int _height;

        //For random numbers
        Random _rand;

        public GridLoop(int width, int height)
        {
            //Initialize the grid and the bitmap image;
            _grid = new Cell[width, height];
            _image = new Bitmap(width, height);

            _width = width;
            _height = height;

            //Fill the grid with empty cells
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _grid[x, y] = new Cell();
                }
            }

            drawToBitmap();

            //Declare random object
            _rand = new Random(DateTime.Now.Second);

        }



        public void IterationLoop(float x, float y, bool isMouseDown)
        {
            if (isMouseDown)
            {
                drawAt(x, y, 14);
            }
            drawToBitmap();
        }

        private void drawToBitmap()
        {
            for(int x = 0; x < _width; x++)
            {
                for(int y = 0; y < _height; y++)
                {
                    _image.SetPixel(x, y, _grid[x, y].color);
                }
            }
        }

        private void drawAt(double x, double y, int radius)
        {
            int centerX = (int)(x * _width);
            int centerY = (int)(y * _height);

            Console.WriteLine(centerX + "-" + centerY);


            int quality = 56;

            for (int i = 0; i < quality; i++)
            {
                double radians = (Math.PI * 2) * ((float)i / quality);
                double cos = Math.Cos(radians);
                double sin = Math.Sin(radians);



                for (int r = 0; r < radius; r++)
                {
                    int xpos = (int)(centerX + cos * r);
                    int ypos = (int)(centerY + sin * r);

                    if (xpos < _width && ypos < _height && xpos >= 0 && ypos >= 0)
                    {
                        _grid[xpos, ypos].color = Color.CadetBlue;
                    }
                }

            }

        }


        //https://stackoverflow.com/questions/15696812/how-to-set-relative-path-to-images-directory-inside-c-sharp-project
    }




}
