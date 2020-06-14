using ForeignExchangeRate.Model;
using System;
using System.Globalization;

namespace ForeignExchangeRate.Service.Services
{
    public interface IPublicationDateService : IService
    {
        string GetPublicationDate(string date);
    }

    public class PublicationDateService : IPublicationDateService
    {
        private readonly CultureInfo _cultureInfo;
        public PublicationDateService()
        {
            if (_cultureInfo == null)
            {
                _cultureInfo = new CultureInfo(AppConstants.CultureInfo);
            }
        }

        public string GetPublicationDate(string date)
        {
            var year = Convert.ToInt32(date.Substring(0, 4));
            var month = Convert.ToInt32(date.Substring(4, 2));
            var day = Convert.ToInt32(date.Substring(6, 2));

            var nowText = new DateTime(year, month, day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, DateTime.UtcNow.Second).ToString(_cultureInfo);
            var now = Convert.ToDateTime(nowText, _cultureInfo);
            if (nowText.Contains("PM"))
            {
                if (((now.Hour + now.Minute + now.Second) > 0) && (((now.Hour * 60 * 60) + (now.Minute * 60) + now.Second) <= ((23 * 60 * 60) + (59 * 60) + 59)))
                {
                    return $"{now.ToString("ddd")}, {now.Day.ToString("0#")} {now.ToString("MMM")} {now.Year} 12:00:01 GMT";
                }
            }
            return $"{now.ToString("ddd")}, {now.Day.ToString("0#")} {now.ToString("MMM")} {now.Year} 00:00:01 GMT";
        }
    }
}
