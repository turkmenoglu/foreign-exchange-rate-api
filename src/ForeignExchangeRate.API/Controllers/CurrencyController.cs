using ForeignExchangeRate.API.ModelBinders;
using ForeignExchangeRate.Contract;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace ForeignExchangeRate.API.Controllers
{
    [SwaggerTag("Controller containing the currency related operations.")]
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurrencyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets the inverse rate of the given currency in that date.")]
        [SwaggerResponse(200, null, typeof(GetForeignExchangeRateResponse))]
        public async Task<IActionResult> GetForeignExchangeRates
        (
            [ModelBinder(BinderType=typeof(GetForeignExchangeRateRequestBinder))]
            GetForeignExchangeRateRequest request
        )
        {
            var response = await _mediator.Send(request);

            if (response.HasError)
            {
                return BadRequest(response);
            }

            if (response.Data == null || response.Data.Rates == null || response.Data.Rates.Count == 0)
            {
                return NotFound(response);
            }

            return Ok(response.Data);
        }
    }
}
