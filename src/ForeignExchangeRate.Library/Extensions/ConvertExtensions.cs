using System;
using System.Globalization;

namespace ForeignExchangeRate.Library.Extensions
{
    public static class ConvertExtensions
    {
        public static decimal ToDecimal(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ?
                0m :
                Convert.ToDecimal(value.Replace(",", ""));
        }

        public static string ToDecimalString(this decimal value, CultureInfo cultureInfo)
        {
            return string.Format(cultureInfo, "{0:#,##0..#########}", value);
        }
    }
}
