namespace ForeignExchangeRate.Model
{
    public class ForeignExchangeRateModel : ModelBase
    {
        public int Date { get; set; }
        public string BaseCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public decimal InverseRate { get; set; }
        public string PublicationDate { get; set; }
    }
}
