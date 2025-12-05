using Fujtajbl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fujtajbl
{
    public class FujtajblEngine
    {
        private readonly Dictionary<char, IOperationStrategy> _strategies;

        public FujtajblEngine(Dictionary<char, IOperationStrategy> strategies)
        {
            _strategies = strategies;
        }

        public double Calculate(double a, double b, char operation)
        {
            if (!_strategies.ContainsKey(operation))
                throw new InvalidOperationException("Unknown operation.");

            if (operation == '/' && b == 0)
                throw new DivideByZeroException("Cannot divide by zero.");

            var result = _strategies[operation].Execute(a, b);

            if (double.IsInfinity(result))
                throw new OverflowException("Result overflow.");

            return result;
        }
    }
}
