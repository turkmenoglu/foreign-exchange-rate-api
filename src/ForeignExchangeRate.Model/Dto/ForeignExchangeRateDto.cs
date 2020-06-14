using System.Xml;
using System.Xml.Serialization;

namespace ForeignExchangeRate.Model
{
    [XmlRoot("channel")]
    public sealed class ForeignExchangeRateDto
    {
        [XmlElement("pubDate")]
        public string PublicationDate { get; set; }

        [XmlElement("baseCurrency")]
        public string BaseCurrency { get; set; }

        [XmlElement("item")]
        public ForeignExchangeCurrencyDto[] TargetCurrencies { get; set; }
    }
}
