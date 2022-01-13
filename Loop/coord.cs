using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBCompSciProject.Loop
{
    public struct coord
    {
        public int x;
        public int y;

        public coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


        public static coord operator +(coord a) => a;
        public static coord operator -(coord a) => new coord(-a.x, -a.y);


        public static coord operator +(coord a, coord b) => new coord(a.x + b.x, a.y + b.y);
        public static coord operator -(coord a, coord b) => new coord(a.x - b.x, a.y - b.y);


        public override String ToString()
        {
            return "(" + x + ", " + y + ")";
        }

        #region StaticCoords
        public static coord Bottom
        {
            get { return new coord(0, -1);  }
        }
        public static coord Top
        {
            get { return new coord(0, 1); }
        }
        public static coord Right
        {
            get { return new coord(1, 0); }
        }
        public static coord Left
        {
            get { return new coord(-1, 0); }
        }
        public static coord BotLeft
        {
            get { return new coord(-1, -1); }
        }
        public static coord BotRight
        {
            get { return new coord(1, -1); }
        }
        public static coord TopLeft
        {
            get { return new coord(-1, 1); }
        }
        public static coord TopRight
        {
            get { return new coord(1, 1); }
        }
        public static coord Zero
        {
            get { return new coord(0, 0); }
        }
        #endregion
    }
}
