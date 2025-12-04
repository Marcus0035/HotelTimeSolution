using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Fujtajbl.Interfaces;
using Fujtajbl.Models;

namespace Fujtajbl
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define valid operations and their strategies
            char[] validMathOperations = { '+', '-', '*', '/' };
            Dictionary<char, IOperationStrategy> strategies = new Dictionary<char, IOperationStrategy>
            {
                { '+', new AddStrategy() },
                { '-', new SubtractStrategy() },
                { '*', new MultiplyStrategy() },
                { '/', new DivideStrategy() }
            };

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
                PrintColoredMessage("Welcome to 'From Fujtajbl to Very Nice Calculator'", ConsoleColor.Cyan);
                PrintColoredMessage($"\nInfo:", ConsoleColor.White);
                PrintColoredMessage($"For decimal numbers please use '{GetDecimalSeparator()}'", ConsoleColor.DarkCyan);
                PrintColoredMessage("For exit type 'exit' anytime\n", ConsoleColor.Yellow);

                while (true)
                {
                    var firstNum = GetDouble("Please enter the first number:");
                    var secondNum = GetDouble("Please enter the second number:");

                    var mathOperation = GetMathOperation($"Please enter the math operation ({string.Concat(validMathOperations)}):");

                    double result;

                    try
                    {
                        var strategy = strategies[mathOperation];

                        DivideByZeroCheck(secondNum, mathOperation);

                        result = strategy.Execute(firstNum, secondNum);

                        OverflowCheck(result, mathOperation);

                    }
                    catch (Exception e)
                    {
                        PrintColoredMessage(e.Message + "\n", ConsoleColor.Red);
                        continue;
                    }

                    PrintColoredMessage($"{firstNum} {mathOperation} {secondNum} = {result} \n", ConsoleColor.Green);

                    // Don't need to check the result, just continue or exit
                    GetAnswer("If you wanna continue type anything ('exit' to quit):", true);
                }

            }

            // Validation methods
            bool ConvertibleToDouble(string input)
            {
                return double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out _);
            }

            // Exception handling methods
            void DivideByZeroCheck(double number, char operation)
            {
                if (number == 0 && operation == '/')
                    throw new DivideByZeroException("Division by zero is not allowed.");
            }
            void OverflowCheck(double result, char operation)
            {
                if (double.IsInfinity(result))
                    throw new OverflowException($"Overflow occurred during '{operation}' operation.");
            }

            // Input methods
            char GetMathOperation(string prompt)
            {
                while (true)
                {
                    var input = GetCharAnswer(prompt);
                    if (validMathOperations.Contains(input))
                        return input;

                    PrintColoredMessage("Please enter a valid character.\n", ConsoleColor.Red);
                }
            }
            char GetCharAnswer(string prompt)
            {
                while (true)
                {
                    var input = GetAnswer(prompt);
                    if (input.Length == 1)
                        return input[0];
                    PrintColoredMessage("Please enter a single character.\n", ConsoleColor.Red);
                }
            }
            char GetDecimalSeparator()
            {
                return Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            }
            double GetDouble(string prompt)
            {
                while (true)
                {
                    var input = GetAnswer(prompt);

                    if (ConvertibleToDouble(input))
                        return double.Parse(input, CultureInfo.InvariantCulture);

                    PrintColoredMessage("Input isn't a valid number, try again.\n", ConsoleColor.Red);
                }
            }
            string GetAnswer(string prompt, bool acceptEnter = false)
            {
                while (true)
                {
                    Console.WriteLine(prompt);
                    var input = Console.ReadLine();
                    // Add an empty line for better readability
                    Console.WriteLine();

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
                SetColor(color);
                Console.WriteLine(message);
                Console.ResetColor();
            }
            void SetColor(ConsoleColor color)
            {
                Console.ForegroundColor = color;
            }
        }
    }
}