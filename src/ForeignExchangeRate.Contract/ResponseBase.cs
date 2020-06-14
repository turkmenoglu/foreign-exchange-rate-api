using System.Collections.Generic;
using System.Linq;

namespace ForeignExchangeRate.Contract
{
    public class ResponseBase<TData>
    {
        public ResponseBase()
        {
            Errors = new List<string>();
        }

        public bool HasError => Errors.Any();
        public List<string> Errors { get; set; }
        public TData Data { get; set; }
    }
}