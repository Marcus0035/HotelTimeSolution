using Fujtajbl;
using Fujtajbl.Interfaces;
using Fujtajbl.Models;

namespace UnitTests
{
    public class FujtajblTests
    {
        private FujtajblEngine engine = new();

        [Fact]
        public void Calculate_Addition_ReturnsCorrectValue()
        {
            var result = engine.Calculate(5, 3, '+');
            Assert.Equal(8, result);
        }

        [Fact]
        public void Calculate_Subtraction_ReturnsCorrectValue()
        {
            var result = engine.Calculate(10, 4, '-');
            Assert.Equal(6, result);
        }

        [Fact]
        public void Calculate_Multiplication_ReturnsCorrectValue()
        {
            var result = engine.Calculate(6, 7, '*');
            Assert.Equal(42, result);
        }

        [Fact]
        public void Calculate_Division_ReturnsCorrectValue()
        {
            var result = engine.Calculate(20, 4, '/');
            Assert.Equal(5, result);
        }

        [Fact]
        public void Calculate_DivisionByZero_ThrowsException()
        {
            Assert.Throws<DivideByZeroException>(() => engine.Calculate(10, 0, '/'));
        }

        [Fact]
        public void Calculate_UnknownOperation_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => engine.Calculate(5, 5, '^'));
        }

        [Fact]
        public void Calculate_Overflow_ThrowsException()
        {
            Assert.Throws<OverflowException>(() => engine.Calculate(double.MaxValue, 2, '*'));
        }
    }
}
