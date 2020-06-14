using ForeignExchangeRate.Contract;
using ForeignExchangeRate.Library.Extensions;
using ForeignExchangeRate.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ForeignExchangeRate.API.ModelBinders
{
    public class GetForeignExchangeRateRequestBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string baseValue = string.Empty;
            StringValues baseValueQuery;
            if (bindingContext.HttpContext.Request.Query.TryGetValue(AppConstants.QueryStringKeyBase, out baseValueQuery) && baseValueQuery.Any())
            {
                baseValue = baseValueQuery.FirstOrDefault();
            }

            if (string.IsNullOrWhiteSpace(baseValue))
            {
                baseValue = AppConstants.CurrencyCodeTry;
            }

            baseValue = baseValue.ToEnglishUpper();

            string dateValue = string.Empty;
            StringValues dateValueQuery;
            if (bindingContext.HttpContext.Request.Query.TryGetValue(AppConstants.QueryStringKeyDate, out dateValueQuery) && dateValueQuery.Any())
            {
                dateValue = dateValueQuery.FirstOrDefault();
            }

            if (string.IsNullOrWhiteSpace(dateValue))
            {
                dateValue = DateTime.Now.ToString(AppConstants.DateFormat);
            }

            var result = new GetForeignExchangeRateRequest
            {
                Base = baseValue,
                Date = dateValue,
            };

            bindingContext.Result = ModelBindingResult.Success(result);

            return Task.CompletedTask;
        }
    }    
}
