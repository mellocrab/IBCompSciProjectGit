﻿using System;
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
        public Bitmap Image { get; set; }

        //Holds Cell Data
        private Cell[,] _grid;

        //Stores image width and height;
        private int _width;
        private int _height;

        //For random numbers
        Random _rand;

        //Gravity
        private float _gravity = .5f;

        // To avoid bias to one side of the grid, holds whether the process should iterate from the left of the grid, or the right
        // It is alternativly used to aid with the avoidance of double processing.
        private bool _fromRight = true;

        #region Constructor
        public GridLoop(int width, int height)
        {
            //Initialize the grid and the bitmap image;
            _grid = new Cell[width, height];
            Image = new Bitmap(width, height);

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

            //Declare object to handle random number generation
            _rand = new Random(DateTime.Now.Second);

        }

        #endregion

        
        public void IterationLoop(List<float> listx, List<float> listy, bool isMouseDown, Cell.Type toDraw, int radius)
        {
            //Check if the mouse is held down. If it is, then call the drawAt() method to paint a circle of cells according to
            //what the "toDraw" paramter is.
            if (isMouseDown)
            {
                for(int i = 0; i < listx.Count; i++)
                {
                    drawAt(listx[i], listy[i], radius, toDraw);
                }
            }

            //Transfer the grid color data over to the bitmap image.
            drawToBitmap();

            //The _fromRight varaible stores which direction the grid is processed from in the loop(right to left, or left to right). This prevents bias. 
            _fromRight = !_fromRight;

            if (_fromRight)
            {
                //Loop from right to left along 2D array, and call the processing method on each individual value
                for (int xpos = _width - 1; xpos >= 0; xpos--)
                {
                    for (int ypos = _height - 1; ypos >= 0; ypos--)
                    {
                        ProcessPixel(xpos, ypos);
                    }
                }

            } else
            {
                //Loop from right to left along 2D array, and call the processing method on each individual value
                for (int xpos = 0; xpos < _width; xpos++)
                {
                    for (int ypos = _height-1; ypos >= 0 ; ypos--)		
                    {
                        ProcessPixel(xpos, ypos);
                    }
                }
            }
            _processSwap = !_processSwap;

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
                    Image.SetPixel(x, y, _grid[x, y].color);
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

        private float standardGasDensity = 2000f;
        private Cell getCellByType(Cell.Type type)
        {
            switch (type)
            {
                case Cell.Type.water:
                    return new Cell(Color.AliceBlue, Cell.Type.water);

                case Cell.Type.sand:
                    return new Cell(Cell.SandColor(), Cell.Type.sand);

                case Cell.Type.empty:
                    return new Cell(Cell.AirColor(), Cell.Type.empty);

                case Cell.Type.solid:
                    return new Cell(Cell.SolidColor(), Cell.Type.solid);
                case Cell.Type.gas:
                    Cell c = new Cell(Color.DarkOliveGreen, Cell.Type.gas);
                    c.velocityX = standardGasDensity;
                    return c;
                default:
                    return new Cell(Color.White, Cell.Type.water);

            }
        }

        #endregion

        //For the actual processes of the games physics simulation
        #region Procesing
        private coord _currentPlace = new coord(0, 0);
        private Cell _currentCell;
        private bool _processSwap = true;

        public void ClearGrid()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _grid[x, y] = new Cell(Cell.AirColor(), Cell.Type.empty);
                }
            }
        }

        public void ProcessPixel(int x, int y)
        {
            _currentCell = _grid[x, y];
            _currentPlace.x = x;
            _currentPlace.y = y;
            if(_currentCell.processed == _processSwap)
            {
                return;
            }

            _currentCell.processed = _processSwap;

            switch (_currentCell.type)
            {
                case Cell.Type.sand:
                    SandFall();
                    break;
                case Cell.Type.water:
                    WaterFall();
                    break;
                case Cell.Type.gas:
                    GasProcess();
                    break;
                default:
                    break;
            }
        }

        #region Sand
        private void SandFall()
        {
            _currentCell.velocityY += _gravity;

            int moveMax = (int)Math.Round(_currentCell.velocityY);

            int amountMoved = 0;

            while (moveMax > 0)
            {
                if (SandCanMove(GetCell(_currentPlace + (coord.Bottom * (amountMoved + 1))).type))
                {
                    amountMoved++;
                    moveMax--;
                }
                else
                {
                    _currentCell.velocityY = 0;
                    break;
                }

            }
            Swap(_currentPlace, _currentPlace + (coord.Bottom * amountMoved));

            if(amountMoved > 0)
            {
                return;
            }

           

            if (_rand.Next(0, 2) == 0)
            {
                if (SandCanMoveLiquid(GetCell(_currentPlace + coord.BotRight).type))
                {
                    Swap(_currentPlace, _currentPlace + coord.BotRight);
                    return;
                }

                if (SandCanMoveLiquid(GetCell(_currentPlace + coord.BotLeft).type))
                {
                    Swap(_currentPlace, _currentPlace + coord.BotLeft);
                    return;
                }
            }
            else
            {
                if (SandCanMoveLiquid(GetCell(_currentPlace + coord.BotLeft).type))
                {
                    Swap(_currentPlace, _currentPlace + coord.BotLeft);
                    return;
                }

                if (SandCanMoveLiquid(GetCell(_currentPlace + coord.BotRight).type))
                {
                    Swap(_currentPlace, _currentPlace + coord.BotRight);
                    return;
                }

            }
            if (SandCanMoveLiquid(GetCell(_currentPlace + coord.Bottom).type))
            {
                Swap(_currentPlace, _currentPlace + coord.Bottom);
            }
        }
        private bool SandCanMove(Cell.Type type)
        {
            
            switch (type)
            {
                case Cell.Type.empty:
                    return true;
                case Cell.Type.water:
                    return false;
                case Cell.Type.gas:
                    return true;
            }
            return false;
        }
        private bool SandCanMoveLiquid(Cell.Type type)
        {
            switch (type)
            {
                case Cell.Type.empty:
                    return true;
                case Cell.Type.water:
                    return true;
                case Cell.Type.gas:
                    return true;
            }
            return false;
        }
        #endregion
        #region Water
        private void WaterFall()
        {
            _currentCell.color = Cell.WaterColor(_currentCell.velocityX);

            _currentCell.velocityY += _gravity;

            int moveMax = (int)Math.Round(_currentCell.velocityY);

            int amountMoved = 0;

            while (moveMax > 0)
            {
                if (WaterCanMove(GetCell(_currentPlace + (coord.Bottom * (amountMoved + 1))).type))
                {
                    amountMoved++;
                    moveMax--;
                }
                else
                {
                    _currentCell.velocityY = 0;
                    break;
                }

            }
            Swap(_currentPlace, _currentPlace + (coord.Bottom * amountMoved));

            _currentCell.velocityX = Math.Min(1, _currentCell.velocityX + .05f);


            if (amountMoved > 0)
            {
                return;
            }



            if (_rand.Next(0, 2) == 0)
            {
                if (WaterCanMove(GetCell(_currentPlace + coord.BotRight).type))
                {
                    Swap(_currentPlace, _currentPlace + coord.BotRight);
                    return;
                }

                if (WaterCanMove(GetCell(_currentPlace + coord.BotLeft).type))
                {
                    Swap(_currentPlace, _currentPlace + coord.BotLeft);
                    return;
                }
            }
            else
            {
                if (WaterCanMove(GetCell(_currentPlace + coord.BotLeft).type))
                {
                    Swap(_currentPlace, _currentPlace + coord.BotLeft);
                    return;
                }

                if (WaterCanMove(GetCell(_currentPlace + coord.BotRight).type))
                {
                    Swap(_currentPlace, _currentPlace + coord.BotRight);
                    return;
                }

            }

            if (_rand.Next(0, 2) == 0)
            {
                if (WaterCanMove(GetCell(_currentPlace + coord.Right).type))
                {
                    Swap(_currentPlace, _currentPlace + coord.Right);
                    return;
                }

                if (WaterCanMove(GetCell(_currentPlace + coord.Left).type))
                {
                    Swap(_currentPlace, _currentPlace + coord.Left);
                    return;
                }
            }
            else
            {
                if (WaterCanMove(GetCell(_currentPlace + coord.Left).type))
                {
                    Swap(_currentPlace, _currentPlace + coord.Left);
                    return;
                }

                if (WaterCanMove(GetCell(_currentPlace + coord.Right).type))
                {
                    Swap(_currentPlace, _currentPlace + coord.Right);
                    return;
                }

            }
            _currentCell.velocityX = Math.Max(0, _currentCell.velocityX - .07f);

        }
        private bool WaterCanMove(Cell.Type type)
        {
            bool can = false;
            switch (type)
            {
                case Cell.Type.empty:
                    return true;
                case Cell.Type.gas:
                    return true;

            }
            return can;
        }

        #endregion

        #region Gas

        private float _gasMin = 100;

        public void GasProcess()
        {

            _currentCell.velocityX -= 1;

            if (_currentCell.velocityX < 0)
            {
                _currentCell = new Cell(Cell.AirColor(), Cell.Type.empty);
                return;
            }

            if (_currentCell.velocityX < _gasMin)
            {
                return;
            }


            _currentCell.color = Cell.GasColor(_currentCell.velocityX / standardGasDensity);

            List<Cell> neighborList = new List<Cell>();
            coord[] coordList = coord.AllNeighbors;
            foreach(coord c in coordList)
            {
                if(_rand.Next(0,2) == 0)
                {
                    neighborList.Add(GetCell(_currentPlace, c));
                } else
                {
                    neighborList.Insert(0, GetCell(_currentPlace, c));
                }
            }
            int possibleMoves = 1;
            foreach(Cell n in neighborList)
            {
                if (GasCanMove(n.type))
                {
                    if(n.type == Cell.Type.gas)
                    {
                        if(n.velocityX < _currentCell.velocityX)
                        {
                            possibleMoves++;
                            continue;
                        }
                    }
                    possibleMoves++;

                }
            }

            float distribution = _currentCell.velocityX / possibleMoves;
            float leftOvers = distribution;

            foreach (Cell n in neighborList)
            {
                if(n.type == Cell.Type.gas)
                {
                    if(n.velocityX >= _currentCell.velocityX)
                    {
                        leftOvers += distribution;
                        continue;
                    }
                    n.velocityX += distribution;
                }
                if(n.type == Cell.Type.empty)
                {
                    n.type = Cell.Type.gas;
                    n.velocityX = distribution;
                }
            }
            _currentCell.velocityX = leftOvers;

        }
        public bool GasCanMove(Cell.Type type)
        {
            switch (type)
            {
                case Cell.Type.empty:
                    return true;
                case Cell.Type.gas:
                    return true;
            }
            return false;
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
