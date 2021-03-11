using System.Linq;

namespace Calc.Logic
{
    public class Calculator
    {
        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input)) return 0;

            var delimeter = ',';
            var delimeterIdentifier = "//";

            if (input.Length > 2 && input.Substring(0, delimeterIdentifier.Length) == delimeterIdentifier)
            {
                const int delimeterIndex = 4;
                delimeter = input[2];
                input = input.Substring(delimeterIndex, input.Length - delimeterIndex);
            }

            var splitted = input.Split(delimeter, '\n');

            if (splitted.Length == 1) return GetNumber(input);

            return splitted.Select(GetNumber).Sum();
        }

        private int GetNumber(string number)
        {
            return int.Parse(number);
        }
    }
}
