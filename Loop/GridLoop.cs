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

        public Bitmap _image;


        Graphics _graphics;

        //Holds Cell Data
        private Cell[,] _grid;

        //Stores image width and height;
        int _width;
        int _height;

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
        }


        public void InitiateLoop()
        {
            _image = new Bitmap(5, 5);
        }

        public void IterationLoop()
        {
            Random r = new Random(24);
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _grid[x, y] = new Cell(Color.FromArgb(r.Next()));
                }
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
        

        //https://stackoverflow.com/questions/15696812/how-to-set-relative-path-to-images-directory-inside-c-sharp-project
    }




}
