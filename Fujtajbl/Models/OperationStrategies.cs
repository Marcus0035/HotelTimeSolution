using Fujtajbl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fujtajbl.Models
{
    public class AddStrategy : IOperationStrategy
    {
        public double Execute(double a, double b) => a + b;
    }

    public class SubtractStrategy : IOperationStrategy
    {
        public double Execute(double a, double b) => a - b;
    }

    public class MultiplyStrategy : IOperationStrategy
    {
        public double Execute(double a, double b) => a * b;
    }

    public class DivideStrategy : IOperationStrategy
    {
        public double Execute(double a, double b) => a / b;
    }

}
