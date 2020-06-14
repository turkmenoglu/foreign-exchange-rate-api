using ForeignExchangeRate.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using ForeignExchangeRate.Library.Extensions;
using System.Globalization;
using ForeignExchangeRate.Library.Web;
using Microsoft.Extensions.Options;
using ForeignExchangeRate.Service.Options;

namespace ForeignExchangeRate.Service.Services
{
    public interface IForeignExchangeRatesService : IService
    {
        Task<IList<ForeignExchangeRateModel>> GetForeignExchangeRateModels(string @base, string date);

        InverseRatesDto ToInverseRatesDto(string @base, IList<ForeignExchangeRateModel> foreignExchangeRates);

        IList<ForeignExchangeRateModel> ToForeignExchangeRateModels(ForeignExchangeRateDto foreignExchangeRateDto, string date);

        int GetDateAsInt(string dateTimeAsGmt);
        string GetCacheKey(string @base, string date, string publicationDate);
    }

    public class ForeignExchangeRatesService : IForeignExchangeRatesService
    {
        private readonly CultureInfo _cultureInfo;
        private readonly ForeignExchangeRateOption _foreignExchangeRateOptionValue;
        public ForeignExchangeRatesService(IOptions<ForeignExchangeRateOption> foreignExchangeRateOption)
        {
            if (_cultureInfo == null)
            {
                _cultureInfo = new CultureInfo(AppConstants.CultureInfo);
            }

            _foreignExchangeRateOptionValue = foreignExchangeRateOption.Value;
        }

        public async Task<IList<ForeignExchangeRateModel>> GetForeignExchangeRateModels(string @base, string date)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(string.Format(_foreignExchangeRateOptionValue.Url, @base));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypes.ApplicationXml));
    
            HttpResponseMessage response = await client.GetAsync(string.Empty);
            if (!response.IsSuccessStatusCode)
            {
                return await Task.FromResult<IList<ForeignExchangeRateModel>>(ToForeignExchangeRateModels(null, date));
            }

            var xmlString = await response.Content.ReadAsStringAsync();
            XmlSerializer serializer = new XmlSerializer(typeof(ForeignExchangeRateDto), new XmlRootAttribute("channel"));
            using (StringReader stringReader = new StringReader(xmlString))
            {
                var foreignExchangeRateDto = (ForeignExchangeRateDto)serializer.Deserialize(stringReader);
                return await Task.FromResult<IList<ForeignExchangeRateModel>>(ToForeignExchangeRateModels(foreignExchangeRateDto, date));
            }
        }
        public string GetCacheKey(string @base, string date, string publicationDate)
        {
            var cacheKey = $"{AppConstants.CacheKeyForeignExchangeRatesOfCurrency}_{@base}_{date}_{publicationDate}";
            return cacheKey;
        }

        public int GetDateAsInt(string dateTimeAsGmt)
        {
            if (string.IsNullOrWhiteSpace(dateTimeAsGmt))
            {
                return 0;
            }

            var items = dateTimeAsGmt.Split(' ');
            if (items == null || items.Length < 4)
            {
                return 0;
            }

            var year = Convert.ToInt32(items[3]);
            var month = DateTime.ParseExact(items[2], "MMM", _cultureInfo).Month.ToString("0#");
            var day = Convert.ToInt32(items[1]).ToString("0#");

            return Convert.ToInt32($"{year}{month}{day}");
        }

        public InverseRatesDto ToInverseRatesDto(string @base, IList<ForeignExchangeRateModel> foreignExchangeRates)
        {
            if (ListExtensions.IsNullOrEmpty(foreignExchangeRates))
            {
                return new InverseRatesDto { Base = @base };
            }

            return new InverseRatesDto
            {
                Base = foreignExchangeRates.FirstOrDefault()?.BaseCurrency ?? @base,
                Rates = foreignExchangeRates.ToDictionary(x => x.TargetCurrency, x => x.InverseRate.ToDecimalString(_cultureInfo))
            };
        }

        public IList<ForeignExchangeRateModel> ToForeignExchangeRateModels(ForeignExchangeRateDto foreignExchangeRateDto, string date)
        {
            if (foreignExchangeRateDto == null || ListExtensions.IsNullOrEmpty(foreignExchangeRateDto.TargetCurrencies))
            {
                return new List<ForeignExchangeRateModel>();
            }

            var dateAsInt = GetDateAsInt(foreignExchangeRateDto.PublicationDate);

            if (dateAsInt.ToString() != date)
            {
                return new List<ForeignExchangeRateModel>();
            }

            var rates = new List<ForeignExchangeRateDto> { foreignExchangeRateDto };

            return rates.SelectMany(rate => rate.TargetCurrencies, (r, c) => new ForeignExchangeRateModel { 
                Id = Guid.NewGuid(),
                Date = dateAsInt,
                BaseCurrency = TextExtensions.ToEnglishUpper(r.BaseCurrency),
                TargetCurrency = TextExtensions.ToEnglishUpper(c.TargetCurrency),
                InverseRate = ConvertExtensions.ToDecimal(c.InverseRate),
                PublicationDate = r.PublicationDate
            }).ToList();
        }
    }
}