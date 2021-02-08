using System;
using System.Collections.Generic;
using System.Text;

namespace DonkeyKong
{
    /// <summary>
    /// Represents a single barrel.  Contains position and directions for a single barrel.
    /// Also contains updating logic.
    /// </summary>
    class Barrel
    {
        private BarrelDirection direction;
        private int x;
        private int y;

        public BarrelDirection Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }

        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }


        public Barrel(int x, int y, BarrelDirection direction)
        {
            this.x = x;
            this.y = y;
            this.direction = direction;
        }

        public void Move(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void SwitchDirection()
        {
            if (direction == BarrelDirection.Left)
            {
                direction = BarrelDirection.Right;
            }
            else
            {
                direction = BarrelDirection.Left;
            }
        }
    }

    public enum BarrelDirection
    {
        Left,
        Right
    }
}
