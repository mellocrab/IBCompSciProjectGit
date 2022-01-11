﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace IBCompSciProject.Loop
{
    public class Cell
    {
        private static readonly Random random = new Random();


        public Color color { get; set; } 
        public  Type type { get; set; }

        public float velocityX { get; set; }
        public float velocityY { get; set; }


        //Constructors
        #region Constructors
        public Cell(Color color)
        {
            this.color = color;
            this.type = Type.solid;
            velocityX = 0;
            velocityY = 0;
        }

        public Cell(Color color, Type type)
        {
            this.color = color;
            this.type = type;
            velocityX = 0;
            velocityY = 0;
        }

        public Cell(Type type)
        {
            this.color = Color.White;
            this.type = type;
            velocityX = 0;
            velocityY = 0;
        }

        public Cell()
        {

            color = Color.White;
            this.type = Type.empty;
            velocityX = 0;
            velocityY = 0;
        }
        #endregion

        public enum Type
        {
            empty,
            solid,
            sand,
            water,
            barrier
        }

        #region Colors

        public static Color RandomColor(float size)
        {

            return Color.FromArgb((int)(random.NextDouble() * size * 255), (int)(random.NextDouble() * size * 255), (int)(random.NextDouble() * size * 255));
        }

        public static Color AddColor(Color a, Color b)
        {
            return ColorClamp(a.R + b.R, a.G + b.G, a.B + b.B);
        }
        public static Color SandColor()
        {
            return AddColor(Color.SandyBrown, RandomColor(.1f));
        }

        public static Color ColorClamp(int r, int g, int b)
        {
            return Color.FromArgb(Math.Min((int)r, 255), Math.Min((int)g, 255), Math.Min((int)b, 255));
            
        }
        #endregion
    }
}