using ForeignExchangeRate.Infrastructure;
using ForeignExchangeRate.Library.Extensions;
using ForeignExchangeRate.Model;
using ForeignExchangeRate.Service.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ForeignExchangeRate.Contract
{
    public class GetForeignExchangeRateHandler : IRequestHandler, MediatR.IRequestHandler<GetForeignExchangeRateRequest, GetForeignExchangeRateResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublicationDateService _publicationDateService;
        private readonly IForeignExchangeRatesService _foreignExchangeRatesService;
        private readonly ICachingService<IList<ForeignExchangeRateModel>> _cachingService;

        public GetForeignExchangeRateHandler(
            IUnitOfWork unitOfWork, 
            IPublicationDateService publicationDateService,
            IForeignExchangeRatesService foreignExchangeRatesService,
            ICachingService<IList<ForeignExchangeRateModel>> cachingService
        )
        {
            _unitOfWork = unitOfWork;
            _publicationDateService = publicationDateService;
            _foreignExchangeRatesService = foreignExchangeRatesService;
            _cachingService = cachingService;
        }

        public async Task<GetForeignExchangeRateResponse> Handle(GetForeignExchangeRateRequest request, CancellationToken cancellationToken)
        {
            var response = new GetForeignExchangeRateResponse();

            var publicationDate = _publicationDateService.GetPublicationDate(request.Date);

            string cacheKey = _foreignExchangeRatesService.GetCacheKey(request.Base, request.Date, publicationDate);

            var dateAsInt = Convert.ToInt32(request.Date);

            var foreignExchangeRates = await _cachingService.GetOrCreateAsync
            (
                cacheKey, 
                () => {
                    return _unitOfWork.ForeignExchangeRate
                        .GetWhereAsync(x => x.BaseCurrency == request.Base && x.Date == dateAsInt && x.PublicationDate == publicationDate);
                },
                AppConstants.CacheDurationInHourForeignExchangeRatesOfCurrency
            );

            if (ListExtensions.IsNullOrEmpty(foreignExchangeRates))
            {
                foreignExchangeRates = await _foreignExchangeRatesService.GetForeignExchangeRateModels(request.Base, request.Date);

                var foreignExchangeRatesToDelete = await _unitOfWork.ForeignExchangeRate.GetWhereAsync(x => x.BaseCurrency == request.Base && x.Date == dateAsInt && x.PublicationDate == publicationDate);
                await _unitOfWork.ForeignExchangeRate.BulkSaveAsync(foreignExchangeRates, foreignExchangeRatesToDelete);
                if (!ListExtensions.IsNullOrEmpty(foreignExchangeRates) || !ListExtensions.IsNullOrEmpty(foreignExchangeRatesToDelete))
                {
                    await _unitOfWork.CompleteAsync();
                }

                await _cachingService.SetAsync(cacheKey, foreignExchangeRates);
            }

            if (ListExtensions.IsNullOrEmpty(foreignExchangeRates))
            {
                foreignExchangeRates = new List<ForeignExchangeRateModel>();
            }

            response.Data = _foreignExchangeRatesService.ToInverseRatesDto(request.Base, foreignExchangeRates);

            return response;
        }
    }
}
