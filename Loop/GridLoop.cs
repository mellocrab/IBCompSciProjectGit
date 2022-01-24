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

        //Gravity
        float gravity = .5f;

        

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


        // To avoid bias to one side of the grid, holds whether the process should iterate from the left of the grid, or the right
        // It is alternativly used to aid with the avoidance of double processing.
        bool _fromRight = true;

        public void IterationLoop(float x, float y, bool isMouseDown, Cell.Type toDraw, int radius)
        {
            if (isMouseDown)
            {
                drawAt(x, y, radius, toDraw);
            }
            drawToBitmap();

            _fromRight = !_fromRight;



            if (_fromRight)
            {
                for (int xpos = _width - 1; xpos >= 0; xpos--)
                {
                    for (int ypos = _height - 1; ypos >= 0; ypos--)
                    {
                        ProcessPixel(xpos, ypos);
                    }
                }

            } else
            {
                for(int xpos = 0; xpos < _width; xpos++)
                {
                    for (int ypos = _height-1; ypos >= 0 ; ypos--)		

                    {
                        ProcessPixel(xpos, ypos);
                    }
                }
            }
            processSwap = !processSwap;

        }
        //The following are used to write the grid color data to a bitmap and display it on screen. Additionally
        //They are used to aid with player input and mouse manipulation of the grid values. 
        #region Drawing


        //Set color values of bitmap to that of the grid
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


        //Used to aid with player input. Draws a circle of a specific pixel value.
        private void drawAt(double x, double y, int radius, Cell.Type type)
        {
            int centerX = (int)(x * _width);
            int centerY = (int)(y * _height);

            //Quality of the circle. Uses more iteration samples to draw cirlce
            int quality = 8 * radius;

            //Use trigonometry to draw circle. Use sin and cos to iterate over various degrees. Use radius to determine how far out to draw.
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
                        _grid[xpos, ypos] = getCellByType(type);
                    }
                }

            }

        }

        private Cell getCellByType(Cell.Type type)
        {
            switch (type)
            {
                case Cell.Type.water:
                    return new Cell(Color.AliceBlue, Cell.Type.water);

                case Cell.Type.sand:
                    return new Cell(Cell.SandColor(), Cell.Type.sand);

                case Cell.Type.empty:
                    return new Cell(Color.White, Cell.Type.empty);

                case Cell.Type.solid:
                    return new Cell(Cell.SolidColor(), Cell.Type.solid);
                default:
                    return new Cell(Color.White, Cell.Type.water);

            }
        }

        #endregion

        //For the actual processes of the games physics simulation
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
                case Cell.Type.water:
                    WaterFall();
                    break;
                default:
                    break;
            }
        }

        #region Sand
        private void SandFall()
        {
            c.velocityY += gravity;

            int moveMax = (int)Math.Round(c.velocityY);

            int amountMoved = 0;

            while (moveMax > 0)
            {
                if (SandCanMove(GetCell(place + (coord.Bottom * (amountMoved + 1))).type))
                {
                    amountMoved++;
                    moveMax--;
                }
                else
                {
                    c.velocityY = 0;
                    break;
                }

            }
            Swap(place, place + (coord.Bottom * amountMoved));

            if(amountMoved > 0)
            {
                return;
            }

           

            if (_rand.Next(0, 2) == 0)
            {
                if (SandCanMoveLiquid(GetCell(place + coord.BotRight).type))
                {
                    Swap(place, place + coord.BotRight);
                    return;
                }

                if (SandCanMoveLiquid(GetCell(place + coord.BotLeft).type))
                {
                    Swap(place, place + coord.BotLeft);
                    return;
                }
            }
            else
            {
                if (SandCanMoveLiquid(GetCell(place + coord.BotLeft).type))
                {
                    Swap(place, place + coord.BotLeft);
                    return;
                }

                if (SandCanMoveLiquid(GetCell(place + coord.BotRight).type))
                {
                    Swap(place, place + coord.BotRight);
                    return;
                }

            }
            if (SandCanMoveLiquid(GetCell(place + coord.Bottom).type))
            {
                Swap(place, place + coord.Bottom);
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
                    return false;
            }
            return can;
        }
        private bool SandCanMoveLiquid(Cell.Type type)
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
        #region Water
        private void WaterFall()
        {
            c.color = Cell.WaterColor(c.velocityX);

            c.velocityY += gravity;

            int moveMax = (int)Math.Round(c.velocityY);

            int amountMoved = 0;

            while (moveMax > 0)
            {
                if (WaterCanMove(GetCell(place + (coord.Bottom * (amountMoved + 1))).type))
                {
                    amountMoved++;
                    moveMax--;
                }
                else
                {
                    c.velocityY = 0;
                    break;
                }

            }
            Swap(place, place + (coord.Bottom * amountMoved));

            c.velocityX = Math.Min(1, c.velocityX + .05f);


            if (amountMoved > 0)
            {
                return;
            }



            if (_rand.Next(0, 2) == 0)
            {
                if (WaterCanMove(GetCell(place + coord.BotRight).type))
                {
                    Swap(place, place + coord.BotRight);
                    return;
                }

                if (WaterCanMove(GetCell(place + coord.BotLeft).type))
                {
                    Swap(place, place + coord.BotLeft);
                    return;
                }
            }
            else
            {
                if (WaterCanMove(GetCell(place + coord.BotLeft).type))
                {
                    Swap(place, place + coord.BotLeft);
                    return;
                }

                if (WaterCanMove(GetCell(place + coord.BotRight).type))
                {
                    Swap(place, place + coord.BotRight);
                    return;
                }

            }

            if (_rand.Next(0, 2) == 0)
            {
                if (WaterCanMove(GetCell(place + coord.Right).type))
                {
                    Swap(place, place + coord.Right);
                    return;
                }

                if (WaterCanMove(GetCell(place + coord.Left).type))
                {
                    Swap(place, place + coord.Left);
                    return;
                }
            }
            else
            {
                if (WaterCanMove(GetCell(place + coord.Left).type))
                {
                    Swap(place, place + coord.Left);
                    return;
                }

                if (WaterCanMove(GetCell(place + coord.Right).type))
                {
                    Swap(place, place + coord.Right);
                    return;
                }

            }
            c.velocityX = Math.Max(0, c.velocityX - .07f);

        }
        private bool WaterCanMove(Cell.Type type)
        {
            bool can = false;
            switch (type)
            {
                case Cell.Type.empty:
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
