using System;
using System.Drawing;

namespace DonkeyKong
{
    class Display
    {
        public static readonly Size Size = new Size(80, 40);

        private GameModel model;
        private GameTiles[,] buffer;

        public Display(GameModel model)
        {
            //Initialization
            Console.Title = "Donkey Kong - ASCII";
            buffer = new GameTiles[Size.Width, Size.Height];
            ClearBuffer();
            this.model = model;
        }

        /// <summary>
        /// This will set all elements in the buffer array to GameTiles.Null
        /// </summary>
        private void ClearBuffer()
        {
            for (int y = 0; y < buffer.GetLength(1); y++)
            {
                for (int x = 0; x < buffer.GetLength(0); x++)
                {
                    buffer[x, y] = GameTiles.Null;
                }
            }
        }

        /// <summary>
        /// Redraws the model in its current state to the console.  Will only
        /// redraw a position if it has a different value than the last time
        /// that position was drawn.  This is for performance reasons (to
        /// remove flicker and increase speed).
        /// 
        /// After calling this method the cursor position will be set to
        /// (0, 0) and the console fore and background colors will be set
        /// to their defaults.
        /// </summary>
        public void Update()
        {
            for (int y = 0; y < Size.Height - 1; y++)
            {
                for (int x = 0; x < Size.Width; x++)
                {
                    if (model[x, y] != buffer[x, y])
                    {
                        buffer[x, y] = model[x, y];
                        Console.SetCursorPosition(x, y);
                        switch (buffer[x, y])
                        {
                            case GameTiles.Floor:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write('=');
                                break;
                            case GameTiles.Mario:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write((char)1);
                                break;
                            case GameTiles.Empty:
                                Console.Write(' ');
                                break;
                            case GameTiles.Ladder:
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.Write('H');
                                break;
                            case GameTiles.DK:
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.Write((char)2);
                                break;
                            case GameTiles.Barrel:
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write('@');
                                break;
                        }
                    }
                }
            }
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
        }
    }
}
