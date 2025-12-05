using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                PrintColoredMessage($"\nInfo:", ConsoleColor.White);
                PrintColoredMessage("For exit type 'exit' anytime\n", ConsoleColor.Yellow);

                while (true)
                {
                    var path = GetFilePath("Please enter the path:");

                    var map = MazeUtils.LoadMazeFromFile(path);

                    MazeUtils.PrepareConsoleForMaze(map);

                    MazeUtils.PrintMaze(map);




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
                    if (System.IO.Directory.Exists(input) || File.Exists(input))
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
            void PrintColoredMessage(string message, ConsoleColor color)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
    }
}
