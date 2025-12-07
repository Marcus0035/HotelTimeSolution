using Fujtajbl.Interfaces;
using Fujtajbl.Models;
using System;
using System.Collections.Generic;

namespace Fujtajbl
{
    public static class FujtajblUtils
    {
        public static Dictionary<char, IOperationStrategy> strategies = new Dictionary<char, IOperationStrategy>
        {
            { '+', new AddStrategy() },
            { '-', new SubtractStrategy() },
            { '*', new MultiplyStrategy() },
            { '/', new DivideStrategy() }
        };

        public static double Calculate(double a, double b, char operation)
        {
            if (!strategies.ContainsKey(operation))
                throw new InvalidOperationException("Unknown operation.");

            if (operation == '/' && b == 0)
                throw new DivideByZeroException("Cannot divide by zero.");

            var result = strategies[operation].Execute(a, b);

            if (double.IsInfinity(result))
                throw new OverflowException("Result overflow.");

            return result;
        }
    }
}
