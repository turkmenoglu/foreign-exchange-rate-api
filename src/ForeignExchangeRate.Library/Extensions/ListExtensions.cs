using System.Collections.Generic;
using System.Linq;

namespace ForeignExchangeRate.Library.Extensions
{
    public static class ListExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
        {
            return items == null || !items.Any();
        }
    }
}