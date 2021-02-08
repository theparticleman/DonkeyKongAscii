using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace DonkeyKong
{
    class Mario
    {
        private Point location;

        public int X
        {
            get
            {
                return location.X;
            }
            set
            {
                location.X = value;
            }
        }

        public int Y
        {
            get
            {
                return location.Y;
            }
            set
            {
                location.Y = value;
            }
        }

        public Mario(int x, int y)
        {
            location.X = x;
            location.Y = y;
        }


    }
}
