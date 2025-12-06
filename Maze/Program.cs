using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Maze.Core;
using Maze.Core.Utils;

namespace Maze
{
    internal class Program
    {
        // Configuration
        private const int Delay = 50;

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

                    MazeConfiguration.SetUpConfiguration(path);

                    var midgets = MazeConfiguration.GetMidgets();

                    PrintUtils.PrepareConsoleBeforeStart(MazeConfiguration.MazeContext.Map);

                    while (!midgets.TrueForAll(x => x.HasReachedEnd))
                    {
                        foreach (var midget in midgets.Where(x => !x.HasReachedEnd))
                            midget.Move();

                        PrintUtils.PrintMap(midgets, MazeConfiguration.MazeContext.Map);
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
