using ForeignExchangeRate.Contract;
using ForeignExchangeRate.Infrastructure;
using ForeignExchangeRate.Model;
using ForeignExchangeRate.Service.Services;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ForeignExchangeRate.Tests
{
    public class GetForeignExchangeRateHandlerTest
    {
        public GetForeignExchangeRateHandlerTest()
        {

        }

        [Fact]
        public async Task GetForeignExchangeRateHandler_Returns_Not_Null()
        {
            var dbContextMock = new Mock<AppDbContext>();
            var unitOfWork = new UnitOfWork(dbContextMock.Object);

            var publicationDateServiceMock = new Mock<IPublicationDateService>();
            var foreignExchangeRatesServiceMock = new Mock<IForeignExchangeRatesService>();
            var cachingServiceMock = new Mock<ICachingService<IList<ForeignExchangeRateModel>>>(); 

            var getForeignExchangeRateHandler = new GetForeignExchangeRateHandler(unitOfWork, publicationDateServiceMock.Object, foreignExchangeRatesServiceMock.Object, cachingServiceMock.Object);
            var result = await getForeignExchangeRateHandler.Handle(new GetForeignExchangeRateRequest { Base = "TRY", Date = "20200613" }, CancellationToken.None);

            Assert.NotNull(result);
        }
    }
}
