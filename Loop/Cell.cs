using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace IBCompSciProject.Loop
{
    public class Cell
    {
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
            water
        }
    }
}
