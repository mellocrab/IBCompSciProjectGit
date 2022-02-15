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

        private static readonly Random random = new Random();


        //The color of the cell, written to the bitmap and displayed on the form
        public Color color { get; set; } 

        //The type of the cell, such as water, or sand, which determines its behavior. Declared in an enum
        public  Type type { get; set; }

        //The variables are commonly used to represent cell velocity for certain physics calculations. However, they can be used
        //for other values as well.

        public float velocityX { get; set; }
        public float velocityY { get; set; }

        //It is possible for a cell to be processed twice when it is moved. To avoid this, this variable saves whether in the current iteration
        //The cell has been processed.
        public bool processed { get; set; } = false;


        //Various constructors for the cell. They use different parameters for ease of use. 
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
            color = AirColor();
            this.type = Type.empty;
            velocityX = 0;
            velocityY = 0;
        }
        #endregion


        //The enum which holds various cell types. Update this when adding a new type. The type determines the code to be run when processing 
        //A pixel of a certain type.
        public enum Type
        {
            empty,
            solid,
            sand,
            water,
            gas,
            barrier
        }

        //For the generation and manipulation of colors.
        #region Colors

        //Gets a random color. Size determines how strong the color will be.  
        public static Color RandomColor(float size)
        {

            return Color.FromArgb((int)(random.NextDouble() * size * 255), (int)(random.NextDouble() * size * 255), (int)(random.NextDouble() * size * 255));
        }

        //Add the values of two colors together.
        public static Color AddColor(Color a, Color b)
        {
            return ColorClamp(a.R + b.R, a.G + b.G, a.B + b.B, a.A + b.A);
        }

        //linearly interpolates between two colors.
        public static Color LerpColor(Color a, Color b, float t)
        {
            return ColorClamp( (int)((b.R - a.R) * t + a.R), (int)((b.G - a.G) * t + a.G), (int)((b.B - a.B) * t + a.B), (int)((b.A - a.A) * t + a.A));
        }

        //For cells of sand type. Get a sandy color with slight random variation.
        public static Color SandColor()
        {
            return AddColor(Color.SandyBrown, RandomColor(.1f));
        }


        //For cells of solid type. Get a neutral color with slight random variation.
        public static Color SolidColor()
        {
            return AddColor(Color.RosyBrown, RandomColor(.1f));
        }

        //Returns color of the form background, so it will blend in. 
        public static Color AirColor()
        {
            return SimulationForm.BackgroundColor;
        }

        //For cells of water type. Interpolate between shades of blue based on velocity.
        public static Color WaterColor(float velocity)
        {
            Color a = Color.RoyalBlue;
            a = ColorClamp(a.R, a.G, a.B, 170);



            return LerpColor(a, Color.AliceBlue, velocity);
        }

        //Get color for cells of gas type. Based off of paramter density, Interpolate between shades of green
        public static Color GasColor(float density)
        {
            Color a = Color.Green;
            a = ColorClamp(a.R, a.G, a.B, 0);
            Color b = Color.Olive;
            b = ColorClamp(a.R, a.G, a.B, 200);
            return LerpColor(a, b, density);
        }

        //Since going above 255 for color values throws and error, clamps it.
        public static Color ColorClamp(int r, int g, int b)
        {
            return Color.FromArgb(Math.Max(Math.Min((int)r, 255), 0), Math.Max(Math.Min((int)g, 255), 0), Math.Max(Math.Min((int)b, 255), 0));
            
        }
        //Same thing, but with the color alpha
        public static Color ColorClamp(int r, int g, int b, int a)
        {
            return Color.FromArgb(Math.Max(Math.Min((int)a, 255), 0), Math.Max(Math.Min((int)r, 255), 0), Math.Max(Math.Min((int)g, 255), 0), Math.Max(Math.Min((int)b, 255), 0));

        }
        #endregion
    }
}
