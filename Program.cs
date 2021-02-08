using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace DonkeyKong
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckConsoleSize();

            bool playAgain = true;

            while (playAgain)
            {
                bool isGameWon = false;
                string levelName = string.Empty;
                bool hasMoreLevels = true;

                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                List<string> levelList = new List<string>(Directory.GetFiles(path, "level*.map"));

                if (levelList.Count < 1)
                {
                    Console.WriteLine("No level files found");
                    Environment.Exit(0);
                }
                else
                {
                    levelList.Sort();
                    levelName = levelList[0];
                    levelList.RemoveAt(0);
                }

                while (hasMoreLevels)
                {
                    bool skipLevel = false;
                    Console.Clear();
                    GameModel model = new GameModel(levelName);
                    Display display = new Display(model);
                    while (!model.IsGameOver && !model.IsGameWon)
                    {
                        if (Console.KeyAvailable)
                        {
                            switch (Console.ReadKey().Key)
                            {
                                case ConsoleKey.LeftArrow:
                                    model.MarioLeft();
                                    break;
                                case ConsoleKey.RightArrow:
                                    model.MarioRight();
                                    break;
                                case ConsoleKey.UpArrow:
                                    model.MarioUp();
                                    break;
                                case ConsoleKey.DownArrow:
                                    model.MarioDown();
                                    break;
                                case ConsoleKey.PageDown:
                                    skipLevel = true;
                                    break;
                            }
                            while (Console.KeyAvailable)
                            {
                                Console.ReadKey();
                            }
                        }
                        if (skipLevel)
                        {
                            break;
                        }
                        model.MoveDonkeyKong();
                        model.MoveBarrels();
                        display.Update();
                        Thread.Sleep(50);
                    }

                    Console.SetCursorPosition(0, 0);
                    if (model.IsGameOver)
                    {
                        Console.WriteLine("You have lost!  Please try again");
                        hasMoreLevels = false;
                    }
                    else if (model.IsGameWon || skipLevel)
                    {
                        //Console.WriteLine("You win!  Good job!");
                        if (levelList.Count > 0)
                        {
                            levelName = levelList[0];
                            levelList.RemoveAt(0);
                        }
                        else
                        {
                            isGameWon = true;
                            hasMoreLevels = false;
                        }
                    }
                }

                if (isGameWon)
                {
                    Console.WriteLine("You have won!  Thanks for playing!");
                }

                Console.WriteLine("Play again? (Y/N): ");
                bool validInput = false;
                while (!validInput)
                {
                    ConsoleKey key = Console.ReadKey().Key;
                    if (key == ConsoleKey.N)
                    {
                        playAgain = false;
                        validInput = true;
                    }
                    else if (key == ConsoleKey.Y)
                    {
                        validInput = true;
                    }
                }
            }
        }

        private static void CheckConsoleSize()
        {
            if (Console.WindowHeight < Display.Size.Height ||
            Console.WindowWidth < Display.Size.Width)
            {
                throw new ApplicationException("The current console size is too small. Try increasing the console size or reducing your console font size.");
            }
        }
    }
}
