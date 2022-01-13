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

        #region Constructor
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

        #endregion


        bool _fromRight = true;

        public void IterationLoop(float x, float y, bool isMouseDown)
        {
            if (isMouseDown)
            {
                drawAt(x, y, 14);
            }
            drawToBitmap();

            _fromRight = !_fromRight;



            if (_fromRight)
            {
                for (int xpos = _width - 1; xpos >= 0; xpos--)
                {
                    for (int ypos = 0; ypos < _height; ypos++)
                    {
                        ProcessPixel(xpos, ypos);
                    }
                }

            } else
            {
                for(int xpos = 0; xpos < _width; xpos++)
                {
                    for (int ypos = 0; ypos < _height; ypos++)
                    {
                        ProcessPixel(xpos, ypos);
                    }
                }
            }
            processSwap = !processSwap;

        }
        #region Drawing

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
                        _grid[xpos, ypos] = new Cell(Cell.SandColor(), Cell.Type.sand);
                    }
                }

            }

        }
        #endregion

        #region Procesing
        coord place = new coord(0, 0);
        Cell c;
        bool processSwap = true;


        public void ProcessPixel(int x, int y)
        {
            c = _grid[x, y];
            place.x = x;
            place.y = y;
            if(c.processed == processSwap)
            {
                return;
            }
            
            c.processed = processSwap;

            switch (c.type)
            {
                case Cell.Type.sand:
                    SandFall();
                    break;
                default:
                    break;
            }
        }

        #region Sand
        private void SandFall()
        {
            if(SandCanMove(GetCell(place + coord.Top).type))
            {
                Swap(place, place + coord.Top);
                return;
            }

            if(_rand.Next(0, 2) == 0)
            {
                if (SandCanMove(GetCell(place + coord.TopRight).type))
                {
                    Swap(place, place + coord.TopRight);
                    return;
                }

                if (SandCanMove(GetCell(place + coord.TopLeft).type))
                {
                    Swap(place, place + coord.TopLeft);
                    return;
                }
            }
            else
            {
                if (SandCanMove(GetCell(place + coord.TopLeft).type))
                {
                    Swap(place, place + coord.TopLeft);
                    return;
                }

                if (SandCanMove(GetCell(place + coord.TopRight).type))
                {
                    Swap(place, place + coord.TopRight);
                    return;
                }

            }

        }
        private bool SandCanMove(Cell.Type type)
        {
            bool can = false;
            switch (type)
            {
                case Cell.Type.empty:
                    return true;
                case Cell.Type.water:
                    return true;
            }
            return can;
        }

        #endregion

        #region Tools

        private void Swap(coord a, coord b)
        {
            if (IsSafe(a) == false || IsSafe(b) == false)
            {
                return;
            }
            Cell temp = GetCell(a);
            SetCell(a, GetCell(b));
            SetCell(b, temp);
        }

        private Cell GetCell(coord a)
        {
            if(IsSafe(a) == false)
            {
                return new Cell(Cell.Type.barrier);
            }
            return _grid[a.x, a.y];
        }
        private Cell GetCell(coord a, coord direction)
        {
            if (IsSafe(a + direction) == false)
            {
                return new Cell(Cell.Type.barrier);
            }
            coord temp = a + direction;
            return _grid[temp.x, temp.y];
        }

        private void SetCell(coord a, Cell c)
        {
            if (IsSafe(a) == false)
            {
                return;
            }
            _grid[a.x, a.y] = c;
        }

        private bool IsSafe(coord c)
        {
            if (c.x >= _width || c.x < 0 || c.y >= _height || c.y < 0)
            {
                return false;
            }
            return true;
        }

        #endregion


        #endregion
        //https://stackoverflow.com/questions/15696812/how-to-set-relative-path-to-images-directory-inside-c-sharp-project
    }




}
