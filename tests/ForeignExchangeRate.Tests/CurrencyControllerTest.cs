using ForeignExchangeRate.API.Controllers;
using ForeignExchangeRate.Contract;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace ForeignExchangeRate.Tests
{
    public class CurrencyControllerTest
    {
        private Mock<IMediator> _mediator;
        public CurrencyControllerTest()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public async void GetForeignExchangeRates_Success_Result()
        {
            var getForeignExchangeRateRequest = new GetForeignExchangeRateRequest { Base = "TRY", Date = "20200613" };
            
            _mediator.Setup(x => x.Send(It.IsAny<GetForeignExchangeRateRequest>(), new CancellationToken()))
                     .ReturnsAsync(new GetForeignExchangeRateResponse { Errors = new List<string> { }, 
                                                                        Data = new Model.InverseRatesDto { Base = getForeignExchangeRateRequest.Base, 
                                                                                                           Rates = new Dictionary<string, string> { { "USD", "0.14641048" } } } });

            var currencyController = new CurrencyController(_mediator.Object);

            var result = await currencyController.GetForeignExchangeRates(getForeignExchangeRateRequest);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetForeignExchangeRates_Not_Found_Result()
        {
            var getForeignExchangeRateRequest = new GetForeignExchangeRateRequest { Base = "TRY", Date = "20200615" };

            _mediator.Setup(x => x.Send(It.IsAny<GetForeignExchangeRateRequest>(), new CancellationToken()))
                     .ReturnsAsync(new GetForeignExchangeRateResponse());

            var currencyController = new CurrencyController(_mediator.Object);

            var result = await currencyController.GetForeignExchangeRates(getForeignExchangeRateRequest);

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
