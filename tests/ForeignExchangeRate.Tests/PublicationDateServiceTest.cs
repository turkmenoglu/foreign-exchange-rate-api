using ForeignExchangeRate.Service.Services;
using Moq;
using Xunit;

namespace ForeignExchangeRate.Tests
{
    public class PublicationDateServiceTest
    {
        private Mock<IPublicationDateService> _publicationDateService;

        public PublicationDateServiceTest()
        {
            _publicationDateService = new Mock<IPublicationDateService>();
        }

        [Fact]
        public void GetPublicationDate_Are_Equal()
        {
            _publicationDateService.Setup(x => x.GetPublicationDate("Sat, 13 Jun 2020 12:00:01 GMT")).Returns("20200613");
            var actual = _publicationDateService.Object.GetPublicationDate("Sat, 13 Jun 2020 12:00:01 GMT");
            var expected = "20200613";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetPublicationDate_Are_Not_Equal()
        {
            _publicationDateService.Setup(x => x.GetPublicationDate("Sat, 14 Jun 2020 12:00:01 GMT")).Returns("20200614");
            var actual = _publicationDateService.Object.GetPublicationDate("Sat, 14 Jun 2020 12:00:01 GMT");
            var expected = "20200613";

            Assert.Equal(expected, actual);
        }
    }
}
