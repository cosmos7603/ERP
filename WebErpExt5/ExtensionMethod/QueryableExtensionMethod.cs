using System.Linq;
using WebErpExt5.Models;

namespace WebErpExt5.ExtensionMethod
{
    public static class QueryableExtensionMethod
    {
        public static PagedResult<TDelegate> GetPage<TDelegate>(this IQueryable<TDelegate> collection, int page, int pageSize)
        {
            var skipvalue = (page - 1) * pageSize;
            var results = collection.Skip(skipvalue).Take(pageSize);

            return new PagedResult<TDelegate>(results.ToList(), collection.Count());
        }
    }
}
