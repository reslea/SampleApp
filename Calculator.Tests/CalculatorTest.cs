using Calc.Logic;
using Xunit;

namespace Calc.Tests
{
    public class CalculatorTest
    {
        [Fact]
        public void Method_Signature()
        {
            var calculator = new Calculator();

            var input = string.Empty;
            int result = calculator.Add(input);
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData("1", 1)]
        [InlineData("1,2", 3)]
        public void Sum_Up_To_Two_Parameters(string input, int expectedResult)
        {
            var calculator = new Calculator();

            Assert.Equal(expectedResult, calculator.Add(input));
        }

        [Theory]
        [InlineData("3,5,8", 16)]
        [InlineData("1,2,3,4", 10)]
        [InlineData("1,1,1,1,1,1,1,1,1", 9)]
        public void Sum_More_Than_Two_Parameters(string input, int expectedResult)
        {
            var calculator = new Calculator();

            Assert.Equal(expectedResult, calculator.Add(input));
        }

        [Theory]
        [InlineData("3\n5,8", 16)]
        [InlineData("1,2\n3,4", 10)]
        public void New_Line_Delimeter(string input, int expectedResult)
        {
            var calculator = new Calculator();

            Assert.Equal(expectedResult, calculator.Add(input));
        }

        [Theory]
        [InlineData("//;\n1;2", 3)]
        public void Changed_Delimiter(string input, int expectedResult)
        {
            var calculator = new Calculator();

            Assert.Equal(expectedResult, calculator.Add(input));
        }
    }
}
