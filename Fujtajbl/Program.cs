using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fujtajbl
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RunApplication();
            }
            catch (OperationCanceledException)
            {
                SetColor(ConsoleColor.Yellow);
                Console.WriteLine("\nApplication terminated by user.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                SetColor(ConsoleColor.Red);
                Console.WriteLine($"\nUnexpected error: {ex.Message}");
                Console.ResetColor();
            }

            void RunApplication()
            {
                SetColor(ConsoleColor.Cyan);
                Console.WriteLine("Welcome to Fujtajbl to VeryNice Calculator\n");
                Console.ResetColor();



            }

            void SetColor(ConsoleColor color)
            {
                Console.ForegroundColor = color;
            }

            string GetAnswer(string prompt)
            {
                while (true)
                {
                    Console.WriteLine(prompt);
                    var input = Console.ReadLine();

                    if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                        throw new OperationCanceledException();

                    if (!string.IsNullOrWhiteSpace(input))
                        return input;

                    SetColor(ConsoleColor.Red);
                    Console.WriteLine("Invalid Input, try again.\n");
                    Console.ResetColor();
                }
            }
        }
    }
}