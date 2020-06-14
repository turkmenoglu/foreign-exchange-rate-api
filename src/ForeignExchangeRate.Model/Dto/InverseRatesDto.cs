using System.Collections.Generic;

namespace ForeignExchangeRate.Model
{
    public class InverseRatesDto
    {
        public string Base { get; set; }
        public IDictionary<string, string> Rates { get; set; }

        public InverseRatesDto()
        {
            Rates = new Dictionary<string, string>();
        }
    }
}
