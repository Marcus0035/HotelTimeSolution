using Fujtajbl;
using Xunit;

namespace UnitTests
{
    public class FujtajblTests
    {
        [Theory]
        [InlineData(1, 5, 6)]
        [InlineData(10, 20, 30)]
        [InlineData(-5, 5, 0)]
        [InlineData(3.5, 4.5, 8.0)]
        public void Calculate_Addition_ReturnsCorrectValue(double x, double y, double expected)
        {
            var result = FujtajblUtils.Calculate(x, y, '+');
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 4, 6)]
        [InlineData(5, 10, -5)]
        [InlineData(0, 0, 0)]
        [InlineData(3.5, 1.2, 2.3)]
        public void Calculate_Subtraction_ReturnsCorrectValue(double x, double y, double expected)
        {
            var result = FujtajblUtils.Calculate(x, y, '-');
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(6, 7, 42)]
        [InlineData(10, 0, 0)]
        [InlineData(-3, 3, -9)]
        [InlineData(1.5, 2, 3)]
        public void Calculate_Multiplication_ReturnsCorrectValue(double x, double y, double expected)
        {
            var result = FujtajblUtils.Calculate(x, y, '*');
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(20, 4, 5)]
        [InlineData(9, 3, 3)]
        [InlineData(-10, 2, -5)]
        public void Calculate_Division_ReturnsCorrectValue(double x, double y, double expected)
        {
            var result = FujtajblUtils.Calculate(x, y, '/');
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Calculate_DivisionByZero_ThrowsException()
        {
            Assert.Throws<DivideByZeroException>(() => FujtajblUtils.Calculate(10, 0, '/'));
        }

        [Fact]
        public void Calculate_UnknownOperation_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => FujtajblUtils.Calculate(5, 5, '^'));
        }

        [Fact]
        public void Calculate_Overflow_ThrowsException()
        {
            Assert.Throws<OverflowException>(() => FujtajblUtils.Calculate(double.MaxValue, 2, '*'));
        }
    }
}
