using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Maze.Interfaces;
using Maze.Models;
using Maze.Utils;

namespace Maze
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Application entry point with exception handling
            try
            {
                RunApplication();
            }
            catch (OperationCanceledException)
            {
                PrintColoredMessage("\nApplication terminated by user.", ConsoleColor.Yellow);
            }
            catch (Exception ex)
            {
                PrintColoredMessage($"\nUnexpected error: {ex.Message}", ConsoleColor.Red);
            }

            // Main application method
            void RunApplication()
            {
                PrintColoredMessage("Welcome to Maze Solver", ConsoleColor.Cyan);
                PrintColoredMessage($"\nInfo:");
                PrintColoredMessage("For exit type 'exit' anytime\n", ConsoleColor.Yellow);

                while (true)
                {
                    //edit
                    //var path = GetFilePath("Please enter the path:");

                    var path = @"C:\Users\marek\Downloads\Maze\Maze.dat";

                    var map = EngineUtils.LoadMazeFromFile(path);

                    var startPosition = EngineUtils.GetStartPosition(map);
                    var endPositions = EngineUtils.GetAllEndPositions(map);

                    PrintUtils.PrepareConsoleForMaze(map);

                    var midgets = new List<MidgetBase>
                    {
                        new RightMidget('R', startPosition, endPositions),
                        new LeftMidget('L', startPosition, endPositions),
                        new StartrekMidget('s', startPosition, endPositions)
                    };

                    while (!midgets.TrueForAll(x => endPositions.Contains(x.Position)))
                    {
                        Task.Delay(200).Wait();
                        foreach (var midget in midgets)
                        {
                            if (!midget.IsInFinish)
                                midget.Move(map);
                        }

                        PrintUtils.PrintMap(EngineUtils.PlaceAllMidgets(midgets, map));

                    }

                    // Don't need to check the result, just continue or exit
                    GetAnswer("If you wanna continue type anything:", true, ConsoleColor.Cyan);
                }
            }

            // Input methods
            string GetFilePath(string prompt)
            {
                while (true)
                {
                    var input = GetAnswer("Enter Path:");
                    if (Directory.Exists(input) || File.Exists(input))
                        return input;
                    PrintColoredMessage("Path does not exist, try again.\n", ConsoleColor.Red);
                }
            }

            string GetAnswer(string prompt, bool acceptEnter = false, ConsoleColor color = ConsoleColor.DarkYellow)
            {
                while (true)
                {
                    PrintColoredMessage(prompt, color);
                    var input = Console.ReadLine();

                    if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                        throw new OperationCanceledException();

                    if (!string.IsNullOrWhiteSpace(input) || acceptEnter)
                        return input;

                    PrintColoredMessage("Invalid Input, try again.\n", ConsoleColor.Red);
                }
            }

            // Utility methods
            void PrintColoredMessage(string message, ConsoleColor color = ConsoleColor.White)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();
            }

        }
    }
}
