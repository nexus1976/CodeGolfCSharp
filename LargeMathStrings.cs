using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSPractice
{
    public class LargeMathStrings
    {
        private ref struct LargeNumberSpans
        {
            public ReadOnlySpan<char> DigitsNbr1 { get; set; }
            public ReadOnlySpan<char> DigitsNbr2 { get; set; }
            public ReadOnlySpan<char> DecimalsNbr1 { get; set; }
            public ReadOnlySpan<char> DecimalsNbr2 { get; set; }
            public bool IsNegativeNbr1 { get; set; }
            public bool IsNegativeNbr2 { get; set; }
        }
        public static string PerformLargeAdditionString(string largeNumber1, string largeNumber2)
        {
            if (!IsNumeric(largeNumber1))
                throw new ArgumentException("The string did not contain a valid number.", nameof(largeNumber1));
            if (!IsNumeric(largeNumber2))
                throw new ArgumentException("The string did not contain a valid number.", nameof(largeNumber2));

            LargeNumberSpans largeNumberSpans = NormalizeLargeStrings(largeNumber1, largeNumber2);
            bool doSubtraction = largeNumberSpans.IsNegativeNbr1 != largeNumberSpans.IsNegativeNbr2;
            List<char> result;

            if (doSubtraction)
            {
                // todo call subtraction method
                result = new List<char>();
            }
            else
            {
                result = DoSum(largeNumberSpans.DigitsNbr1, largeNumberSpans.DecimalsNbr1, largeNumberSpans.DigitsNbr2, largeNumberSpans.DecimalsNbr2) ?? new List<char>();
                if (largeNumberSpans.IsNegativeNbr1 && largeNumberSpans.IsNegativeNbr2)
                {
                    result.Insert(0, '-');
                }
            }

            string? resultString = string.Empty;
            if (result != null && result.Any())
            {
                resultString = new string(result.ToArray());
            }
            return resultString ?? string.Empty;
        }
        private static List<char> DoSum(ReadOnlySpan<char> digitsNbr1, ReadOnlySpan<char> decimalsNbr1, ReadOnlySpan<char> digitsNbr2, ReadOnlySpan<char> decimalsNbr2)
        {
            var decimalsTuple = MainSumFx(decimalsNbr1, decimalsNbr2, true, false);
            List<char> decimals = decimalsTuple.result ?? new List<char>();
            List<char> digits = MainSumFx(digitsNbr1, digitsNbr2, false, decimalsTuple.hasOneOverflow).result ?? new List<char>();
            
            List<char> result;
            if (decimals.Any())
            {
                result = digits.Append('.').Concat(decimals).ToList();
            }
            else
            {
                result = digits;
            }
            return result;
        }
        private static (bool hasOneOverflow, List<char> result) MainSumFx(ReadOnlySpan<char> chars1, ReadOnlySpan<char> chars2, bool processingDecimals = false, bool addOneToNumber = false)
        {
            bool carryTheOne = addOneToNumber;
            List<char> digits = new();
            for (int i = chars1.Length - 1; i >= 0; i--)
            {
                double nbr1 = Char.GetNumericValue(chars1[i]);
                double nbr2 = Char.GetNumericValue(chars2[i]);
                double sum = nbr1 + nbr2;
                if (carryTheOne)
                    sum++;
                if (sum >= 10)
                {
                    sum -= 10;
                    carryTheOne = true;
                }
                else
                    carryTheOne = false;
                digits.Add(Convert.ToChar(sum.ToString()));
            }
            if (!processingDecimals && carryTheOne)
            {
                digits.Add(Convert.ToChar("1"));
                carryTheOne = false;
            }

            digits.Reverse();
            return (carryTheOne, digits);
        }
        private static LargeNumberSpans NormalizeLargeStrings(string largeNumber1, string largeNumber2)
        {
            LargeNumberSpans largeNumberSpans = new();
            ReadOnlySpan<char> spanNbr1 = largeNumber1.Trim().Replace(",", "").ToCharArray();
            ReadOnlySpan<char> spanNbr2 = largeNumber2.Trim().Replace(",", "").ToCharArray();
            largeNumberSpans.IsNegativeNbr1 = spanNbr1[0] == '-';
            largeNumberSpans.IsNegativeNbr2 = spanNbr2[0] == '-';

            Span<char> decimalsNbr1;
            Span<char> decimalsNbr2;
            Span<char> digitsNbr1;
            Span<char> digitsNbr2;
            if (spanNbr1.Contains('.'))
            {
                var splitByDecimal = spanNbr1.ToString().Split('.');
                digitsNbr1 = splitByDecimal[0].Replace("-", "").ToCharArray();
                decimalsNbr1 = splitByDecimal[1].ToCharArray();
            }
            else
            {
                digitsNbr1 = spanNbr1.ToString().Replace("-", "").ToCharArray();
                decimalsNbr1 = new Span<char>("".ToCharArray());
            }
            if (spanNbr2.Contains('.'))
            {
                var splitByDecimal = spanNbr2.ToString().Split('.');
                digitsNbr2 = splitByDecimal[0].Replace("-", "").ToCharArray();
                decimalsNbr2 = splitByDecimal[1].ToCharArray();
            }
            else
            {
                digitsNbr2 = spanNbr2.ToString().Replace("-", "").ToCharArray();
                decimalsNbr2 = new Span<char>("".ToCharArray());
            }

            int maxDigits = GetMaximumDigits(digitsNbr1, digitsNbr2);
            if (digitsNbr1.Length < maxDigits)
                digitsNbr1 = digitsNbr1.ToString().PadLeft(maxDigits, '0').ToCharArray();
            else if (digitsNbr2.Length < maxDigits)
                digitsNbr2 = digitsNbr2.ToString().PadLeft(maxDigits, '0').ToCharArray();

            int maxDecimals = GetMaximumDigits(decimalsNbr1, decimalsNbr2);
            if (decimalsNbr1.Length < maxDecimals)
                decimalsNbr1 = decimalsNbr1.ToString().PadRight(maxDecimals, '0').ToCharArray();
            else if (decimalsNbr2.Length < maxDecimals)
                decimalsNbr2 = decimalsNbr2.ToString().PadRight(maxDecimals, '0').ToCharArray();

            largeNumberSpans.DecimalsNbr1 = decimalsNbr1;
            largeNumberSpans.DecimalsNbr2 = decimalsNbr2;
            largeNumberSpans.DigitsNbr1 = digitsNbr1;
            largeNumberSpans.DigitsNbr2 = digitsNbr2;
            return largeNumberSpans;
        }
        private static bool IsNumeric(string largeNumber)
        {
            if (string.IsNullOrWhiteSpace(largeNumber)) return false;
            if (largeNumber.Count(c => c == '.') > 1) return false;
            ReadOnlySpan<char> spanNbr = largeNumber.Trim().Replace("-", "").Replace(",", "").Replace(".", "").ToCharArray();
            for (int i = 0; i < spanNbr.Length; i++)
            {
                if (!Char.IsDigit(spanNbr[i])) return false;
            }
            return true;
        }
        private static int GetMaximumDigits(ReadOnlySpan<char> spanNbr1, ReadOnlySpan<char> spanNbr2)
        {
            int digits1 = spanNbr1.Length;
            int digits2 = spanNbr2.Length;
            return digits1 < digits2 ? digits2 : digits1;
        }
    }
}
