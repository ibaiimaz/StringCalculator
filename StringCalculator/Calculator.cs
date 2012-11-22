using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        public int Add(string numbersString)
        {
            if (numbersString == string.Empty) return 0;

            return GetNumbers(numbersString).Sum();
        }

        public List<int> GetNumbers(string numbersString)
        {
            string commaDelimitedNumbers = GetCommaDelimitedNumbers(numbersString);

            var numbers = commaDelimitedNumbers.Split(',').Select(int.Parse);

            ValidateNegativeNumbers(numbers);

            return IgnoreNumbersBiggerThan1000(numbers);
        }

        private string GetCommaDelimitedNumbers(string numbers)
        {
            if (HasCustomDelimiters(numbers))
            {
                var delimiters = GetDelimiters(numbers);
                numbers = GetNumbersStringWithoutCustomDelimiters(numbers);

                numbers = ReplaceAllCustomDelimitersWithComma(numbers, delimiters);
            }

            return numbers.Replace(@"\n", ",");
        }

        private string ReplaceAllCustomDelimitersWithComma(string numbers, List<string> delimiters)
        {
            return delimiters.Aggregate(numbers, (current, delimiter) => current.Replace(delimiter, ","));
        }

        private string GetNumbersStringWithoutCustomDelimiters(string numbers)
        {
            return numbers.Substring(numbers.IndexOf(@"\n") + 2);
        }

        private bool HasCustomDelimiters(string numbers)
        {
            return numbers.StartsWith("//");
        }

        private List<string> GetDelimiters(string numbers)
        {
            string delimitersString = GetCustomDelimitersString(numbers);

            if (ContainsDelimitersWithBrackets(delimitersString))
            {
                var delimiters = GetDelimitersListFromDelimitersString(delimitersString);

                return DiscardBlankItems(delimiters);
            }
            return new List<string> {delimitersString};
        }

        private List<string> DiscardBlankItems(List<string> delimiters)
        {
            return delimiters.Where(c => c != "").ToList();
        }

        private string GetCustomDelimitersString(string numbers)
        {
            return numbers.Substring(2, numbers.IndexOf(@"\n") - 2);
        }

        private bool ContainsDelimitersWithBrackets(string delimitersString)
        {
            return delimitersString.Contains("]");
        }

        private List<string> GetDelimitersListFromDelimitersString(string delimitersString)
        {
            return delimitersString.Split(']').Select(c => c.Replace("[", "")).ToList();
        }

        private void ValidateNegativeNumbers(IEnumerable<int> numbers)
        {
            var negativeNumbers = numbers.Where(number => number < 0);

            if (negativeNumbers.Any())
                throw new ArgumentException(string.Format(
                    "No se admiten los siguientes números por ser negativos: {0}",
                    String.Join(",", negativeNumbers.ToList())));
        }

        private List<int> IgnoreNumbersBiggerThan1000(IEnumerable<int> numbers)
        {
            return numbers.Where(number => number <= 1000).ToList();
        }
    }
}
