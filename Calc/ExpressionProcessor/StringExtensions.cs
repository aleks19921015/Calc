using System;
using System.Collections.Generic;

namespace Calc.ExpressionProcessor
{
    public static class StringExtensions
    {
        public static IEnumerable<int> IndexesOf(this string source, string value)
        {
            if (string.IsNullOrEmpty(source))
                throw new ParseException();
            for (var index = 0;; index += value.Length) {
                index = source.IndexOf(value, index, StringComparison.Ordinal);
                if (index == -1)
                    break;
                yield return index;
            }
        }
    }
}