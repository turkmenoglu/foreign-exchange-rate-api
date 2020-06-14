namespace ForeignExchangeRate.Library.Extensions
{
    public static class TextExtensions
    {
        public static string ToEnglishUpper(this string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return data;
            }
            return data.ToUpperInvariant().Replace("Ç", "C").Replace("Ğ", "G").Replace("İ", "I").Replace("Ö", "O").Replace("Ş", "S").Replace("Ü", "U");
        }
    }
}
