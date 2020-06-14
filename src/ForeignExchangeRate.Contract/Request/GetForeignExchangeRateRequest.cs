namespace ForeignExchangeRate.Contract
{
    public class GetForeignExchangeRateRequest : IRequest<GetForeignExchangeRateResponse>
    {
        public string Base { get; set; }

        public string Date { get; set; }
    }
}
