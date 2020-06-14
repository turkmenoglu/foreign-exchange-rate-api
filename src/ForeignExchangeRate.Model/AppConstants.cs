namespace ForeignExchangeRate.Model
{
    public class AppConstants
    {
        public const string CultureInfo = "en-US";

        public const string CacheKeyForeignExchangeRates = "ForeignExchangeRates";
        public const string CacheKeyForeignExchangeRatesOfCurrency = "ForeignExchangeRatesOfCurrency";
        public const int CacheDurationInHourForeignExchangeRatesOfCurrency = 1;

        public const string DbType = "Memory";

        public const string TableNameForeignExchangeRates = "ForeignExchangeRates";
        public const int ColumnMaxLengthDate = 8;
        public const int ColumnMaxLengthBaseCurrency = 3;
        public const int ColumnMaxLengthTargetCurrency = 3;
        public const int ColumnMaxLengthPublicationDate = 50;

        public const string QueryStringKeyBase = "base";        
        public const string QueryStringKeyDate = "date";

        public const string CurrencyCodeTry = "TRY";

        public const string DateFormat = "yyyyMMdd";
    }
}
