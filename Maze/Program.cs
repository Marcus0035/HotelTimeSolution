using Maze.Models;
using Maze.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Maze.Models.Abstract;
using Maze.Models.Midgets;

namespace Maze
{
    internal class Program
    {
        // Configuration
        private static Dictionary<MapTile, char> TileSymbols = new Dictionary<MapTile, char>
        {
            { MapTile.Start, 'S' },
            { MapTile.End, 'F' },
            { MapTile.Path, ' '},
            { MapTile.Wall, '#' }
        };

        private static int Delay = 50;


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
                PrintColoredMessage("\nInfo:");
                PrintColoredMessage("For exit type 'exit' anytime\n", ConsoleColor.Yellow);

                while (true)
                {
                    var path = GetFilePath("Please enter the path:");

                    // Initialize midgets
                    var map = EngineUtils.LoadMazeFromFile(path);
                    var startSymbol = TileSymbols[MapTile.Start];
                    var endSymbol = TileSymbols[MapTile.End];
                    var pathSymbol = TileSymbols[MapTile.Path];

                    var startPosition = EngineUtils.GetStartPosition(map, startSymbol);
                    var endPositions = EngineUtils.GetAllEndPositions(map, endSymbol, pathSymbol);

                    var midgets = new List<Midget>
                    {
                        new RightMidget('R', startPosition, endPositions, map, TileSymbols, ConsoleColor.Yellow),
                        new LeftMidget('L', startPosition, endPositions, map, TileSymbols, ConsoleColor.Green),
                        new StartrekMidget('s', startPosition, endPositions, map, TileSymbols, ConsoleColor.Blue),
                        new GuidedMidget('G', startPosition, endPositions, map, TileSymbols, ConsoleColor.DarkMagenta)
                    };

                    PrintUtils.PrepareConsoleBeforeStart(map);

                    while (!midgets.TrueForAll(x => x.HasReachedEnd))
                    {
                        Task.Delay(Delay).Wait();

                        foreach (var midget in midgets.Where(x => !x.HasReachedEnd))
                            midget.Move();

                        PrintUtils.PrintMap(EngineUtils.PlaceAllMidgets(midgets, map));
                    }

                    PrintUtils.PrepareConsoleAfterEnd();

                    // Don't need to check the result, just continue or exit
                    GetAnswer("If you wanna continue type anything:", true, ConsoleColor.Cyan);
                }
            }

            // Input methods
            string GetFilePath(string prompt)
            {
                while (true)
                {
                    var input = GetAnswer(prompt);
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
