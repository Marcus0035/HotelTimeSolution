using System;
using System.Collections.Generic;
using Fujtajbl.Core.Interfaces;
using Fujtajbl.Core.Models;

namespace Fujtajbl.Core
{
    public static class FujtajblUtils
    {
        public static readonly Dictionary<char, IOperationStrategy> Strategies = new Dictionary<char, IOperationStrategy>
        {
            { '+', new AddStrategy() },
            { '-', new SubtractStrategy() },
            { '*', new MultiplyStrategy() },
            { '/', new DivideStrategy() }
        };

        public static double Calculate(double a, double b, char operation)
        {
            if (!Strategies.ContainsKey(operation))
                throw new InvalidOperationException("Unknown operation.");

            if (operation == '/' && b == 0)
                throw new DivideByZeroException("Cannot divide by zero.");

            var result = Strategies[operation].Execute(a, b);

            if (double.IsInfinity(result))
                throw new OverflowException("Result overflow.");

            return result;
        }
    }
}
