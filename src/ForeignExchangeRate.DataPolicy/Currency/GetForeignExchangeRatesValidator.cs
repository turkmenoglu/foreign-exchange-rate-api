using FluentValidation;
using ForeignExchangeRate.Contract;

namespace ForeignExchangeRate.DataPolicy.Currency
{
    public class GetForeignExchangeRatesValidator : AbstractValidator<GetForeignExchangeRateRequest>, IDataPolicy
    {
        public GetForeignExchangeRatesValidator()
        {
            RuleFor(x => x).Custom((x, context) => {
                if (!string.IsNullOrWhiteSpace(x.Base) && x.Base.Length > DataPolicyConstants.CurrencyCodeMaxLength)
                {
                    context.AddFailure(nameof(x.Base), $"'{nameof(x.Base)}' max length should be {DataPolicyConstants.CurrencyCodeMaxLength}.");
                }

                if (!string.IsNullOrWhiteSpace(x.Date) && x.Date.Length > DataPolicyConstants.DateMaxLength)
                {
                    context.AddFailure(nameof(x.Date), $"'{nameof(x.Date)}' max length should be {DataPolicyConstants.DateMaxLength}.");
                }
            });
        }
    }
}
