using ForeignExchangeRate.Model;
using System.Collections.Generic;

namespace ForeignExchangeRate.Contract
{
    public class ErrorResponse
    {
        public ErrorResponse() { }

        public ErrorResponse(ErrorDto error)
        {
            Errors.Add(error);
        }

        public List<ErrorDto> Errors { get; set; } = new List<ErrorDto>();
    }
}
