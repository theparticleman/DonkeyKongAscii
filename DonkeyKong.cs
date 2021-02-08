using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace DonkeyKong
{
    /// <summary>
    /// Contains position and control logic for Donkey Kong.  Creates barrels to be
    /// introduced into the environment
    /// </summary>
    class DonkeyKong
    {
        private Point location;
        private int moveCounter;
        private bool previousBarrelWasRight = false;
        private bool shouldCreateBarrel = false;

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

        public DonkeyKong(int x, int y)
        {
            location.X = x;
            location.Y = y;
        }

        public void Move()
        {
            moveCounter++;
            if (!shouldCreateBarrel && moveCounter % 50 == 0)
            {
                shouldCreateBarrel = true;
            }
        }

        public Barrel CreateBarrel()
        {
            shouldCreateBarrel = false;
            previousBarrelWasRight = !previousBarrelWasRight;
            if (previousBarrelWasRight)
            {
                return new Barrel(location.X - 1, location.Y, BarrelDirection.Left);
            }
            else
            {
                return new Barrel(location.X + 1, location.Y, BarrelDirection.Right);
            }
        }

        public bool ShouldCreateBarrel
        {
            get
            {
                return shouldCreateBarrel;
            }
        }
    }
}
