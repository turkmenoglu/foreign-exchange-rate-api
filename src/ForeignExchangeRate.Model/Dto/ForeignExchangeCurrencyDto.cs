using System.Xml;
using System.Xml.Serialization;

namespace ForeignExchangeRate.Model
{
    public sealed class ForeignExchangeCurrencyDto
    {
        [XmlElement("targetCurrency")]
        public string TargetCurrency { get; set; }

        [XmlElement("inverseRate")]
        public string InverseRate { get; set; }
    }
}
