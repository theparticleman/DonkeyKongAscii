using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace DonkeyKong
{
    class GameModel
    {
        private List<Barrel> barrelList = new List<Barrel>();
        private GameTiles[,] map;
        private GameTiles[,] originalMap;
        private Mario mario;
        private DonkeyKong dk;
        private bool isGameOver = false;
        private bool isGameWon = false;

        /// <summary>
        /// Returns true if the game has been won, false otherwise.
        /// Will be true if the player has successfully gotten
        /// Mario to Donkey Kong.  If Mario has not reached
        /// Donkey Kong this property will return false.
        /// </summary>
        public bool IsGameWon
        {
            get
            {
                return isGameWon;
            }
        }

        /// <summary>
        /// Returns true if the game has been lost, false otherwise.
        /// Will be true if the player allowed Mario to get
        /// hit by a barrel.  If Mario is still alive this property
        /// will return false.
        /// </summary>
        public bool IsGameOver
        {
            get
            {
                return isGameOver;
            }
        }

        public GameTiles this[int x, int y]
        {
            get
            {
                return map[x, y];
            }
        }

        public GameModel(string path)
        {
            map = new GameTiles[Display.Size.Width, Display.Size.Height - 1];
            originalMap = new GameTiles[Display.Size.Width, Display.Size.Height - 1];
            LoadLevelFromFile(path);
        }

        /// <summary>
        /// Loads a level from the specified file.
        /// 
        /// The file should be in plain text format and should 
        /// contain an 80x40 matrix of characters.
        /// </summary>
        /// <param name="path">The path where the file to be loaded is stored</param>
        private void LoadLevelFromFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            string line;
            for (int y = 0; y < map.GetLength(1); y++)
			{
                line = sr.ReadLine();
                if (line == null || line.Length != map.GetLength(0))
                {
                    throw new ArgumentException("Invalid map file.  Invalid size on line " + (y + 1));
                }
                for (int x = 0; x < line.Length; x++)
                {
                    switch (line[x])
                    {
                        case ' ':
                        case '.':
                            map[x, y] = GameTiles.Empty;
                            originalMap[x, y] = GameTiles.Empty;
                            break;
                        case '-':
                        case '=':
                            map[x, y] = GameTiles.Floor;
                            originalMap[x, y] = GameTiles.Floor;
                            break;
                        case 'K':
                            map[x, y] = GameTiles.DK;
                            if (dk == null)
                            {
                                dk = new DonkeyKong(x, y);
                            }
                            else
                            {
                                throw new ArgumentException("Invalid map file.  Multiple enemy starting locations");
                            }
                            break;
                        case 'M':
                            map[x, y] = GameTiles.Mario;
                            if (mario == null)
                            {
                                mario = new Mario(x, y);
                            }
                            else
                            {
                                throw new ArgumentException("Invalid map file.  Multiple player starting locations");
                            }
                            break;
                        case 'H':
                            map[x, y] = GameTiles.Ladder;
                            originalMap[x, y] = GameTiles.Ladder;
                            break;
                    }
                }
			}

            if (mario == null)
            {
                throw new ArgumentException("Invalid map file.  No player starting location");
            }
            if (dk == null)
            {
                throw new ArgumentException("Invalid map file.  No enemy starting location");
            }
            
            
        }

        /// <summary>
        /// Creates a test map (not loaded from a file).  Used for debugging purposes only.
        /// </summary>
        private void SetupMap()
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = GameTiles.Empty;
                }
            }

            for (int x = 0; x < map.GetLength(0); x++)
            {
                map[x, map.GetLength(1) - 1] = GameTiles.Floor;
            }

            for (int y = 0; y < 10; y++)
            {
                map[30, map.GetLength(1) - 2 - y] = GameTiles.Ladder;
            }

            map[0, map.GetLength(1) - 2] = GameTiles.Mario;
            mario = new Mario(0, map.GetLength(1) - 2);
        }

        /// <summary>
        /// Moves mario to the specified location.  Updates map array and mario object.
        /// </summary>
        /// <param name="x">The X position of where to move Mario.</param>
        /// <param name="y">The Y position of where to move Mario.</param>
        /// <remarks>
        /// This method contains the logic for checking the Game Won condition.
        /// </remarks>
        private void MoveMario(int x, int y)
        {
            map[mario.X, mario.Y] = originalMap[mario.X, mario.Y];
            map[x, y] = GameTiles.Mario;
            mario.X = x;
            mario.Y = y;
            if (mario.X == dk.X && mario.Y == dk.Y)
            {
                isGameWon = true;
            }
        }

        /// <summary>
        /// Move Mario left one position.  Checks to see if move is valid.  
        /// If invalid Mario will not be moved.
        /// </summary>
        public void MarioLeft()
        {
            if (mario.X > 0)
            {
                if ((map[mario.X - 1, mario.Y + 1] == GameTiles.Floor ||
                    map[mario.X - 1, mario.Y + 1] == GameTiles.Ladder) &&
                    map[mario.X - 1, mario.Y] != GameTiles.Floor)
                {
                    MoveMario(mario.X - 1, mario.Y);
                }
            }
        }

        /// <summary>
        /// Move Mario right one position.  Checks to see if move is valid.  
        /// If invalid Mario will not be moved.
        /// </summary>
        public void MarioRight()
        {
            if (mario.X < map.GetLength(0) - 2)
            {
                if ((map[mario.X + 1, mario.Y + 1] == GameTiles.Floor ||
                    map[mario.X + 1, mario.Y + 1] == GameTiles.Ladder) &&
                    map[mario.X + 1, mario.Y] != GameTiles.Floor)
                {
                    MoveMario(mario.X + 1, mario.Y);
                }
            }
        }

        /// <summary>
        /// Move Mario up one position.  Checks to see if move is valid.  
        /// If invalid Mario will not be moved.
        /// </summary>
        public void MarioUp()
        {
            if (mario.Y > 0)
            {
                if (originalMap[mario.X, mario.Y] == GameTiles.Ladder)
                {
                    MoveMario(mario.X, mario.Y - 1);
                }
            }
        }

        /// <summary>
        /// Move Mario down one position.  Checks to see if move is valid.  
        /// If invalid Mario will not be moved.
        /// </summary>
        public void MarioDown()
        {
            if (mario.Y < map.GetLength(1) - 2)
            {
                if (map[mario.X, mario.Y + 1] == GameTiles.Ladder)
                {
                    MoveMario(mario.X, mario.Y + 1);
                }
            }
        }

        /// <summary>
        /// Causes Donkey Kong to perform an action.  This action may be do nothing
        /// or throw a barrel, as determined by the logic in the Donkey Kong class
        /// </summary>
        public void MoveDonkeyKong()
        {
            dk.Move();
            if (dk.ShouldCreateBarrel)
            {
                Barrel barrel = dk.CreateBarrel(); 
                barrelList.Add(barrel);
                map[barrel.X, barrel.Y] = GameTiles.Barrel;
            }
        }

        /// <summary>
        /// Causes all barrels to move once.
        /// </summary>
        /// <remarks>
        /// Map array locations of all barrels are updated by this method.
        /// This methods contains the checking logic for the Game Over condition.
        ///</remarks>
        public void MoveBarrels()
        {
            for(int i = 0; i < barrelList.Count; i++)
            {
                Barrel b = barrelList[i];
                map[b.X, b.Y] = originalMap[b.X, b.Y];
                if (b.X <= 0 || b.X >= map.GetLength(0) - 1)
                {
                    barrelList.Remove(b);
                    continue;
                }
                if (originalMap[b.X, b.Y + 1] == GameTiles.Ladder)
                {
                    b.Move(b.X, b.Y + 1);
                }
                else
                {
                    switch (b.Direction)
                    {
                        case BarrelDirection.Left:
                            if (originalMap[b.X - 1, b.Y + 1] == GameTiles.Floor ||
                                originalMap[b.X - 1, b.Y + 1] == GameTiles.Ladder)
                            {
                                b.Move(b.X - 1, b.Y);
                            }
                            else
                            {
                                b.SwitchDirection();
                            }
                            break;
                        case BarrelDirection.Right:
                            if (originalMap[b.X + 1, b.Y + 1] == GameTiles.Floor ||
                                originalMap[b.X + 1, b.Y + 1] == GameTiles.Ladder)
                            {
                                b.Move(b.X + 1, b.Y);
                            }
                            else
                            {
                                b.SwitchDirection();
                            }
                            break;
                    }
                }
                map[b.X, b.Y] = GameTiles.Barrel;
                if (b.X == mario.X && b.Y == mario.Y)
                {
                    isGameOver = true;
                }
            }
            map[mario.X, mario.Y] = GameTiles.Mario;
            map[dk.X, dk.Y] = GameTiles.DK;
        }
    }

    public enum GameTiles
    {
        Empty,
        Floor,
        Ladder,
        Barrel,
        Mario,
        DK,
        Null
    }
}
