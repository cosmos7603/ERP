using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using BLL.Grid;
using Newtonsoft.Json;

namespace WebErpExt5
{
    public class ExtRequestData<T>
    {
        public ExtRequestData(int page, int pageSize, string sort, NameValueCollection filterCollection)
        {
            if (page <= 0)
                page = 1;
            Page = page;

            if (pageSize <= 0)
                pageSize = 10;
            PageSize = pageSize;

            SortOptions = GetSortOptionsFromJsonString(sort);

            var filters = filterCollection.AllKeys.ToDictionary(key => key, key => filterCollection[key]);
            Filter = filters.Keys.Count != 0 ? GetFilterFromQueryString(filters) : null;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public SortOptions SortOptions { get; set; }
        public List<Filter> Filter { get; set; }

        private static SortOptions GetSortOptionsFromJsonString(string sortString)
        {
            if (string.IsNullOrEmpty(sortString))
                return null;

            var jsonSorter = new[] { new { property = string.Empty, direction = string.Empty } };

            try
            {
                var sorter = JsonConvert.DeserializeAnonymousType(sortString, jsonSorter)[0];

                return new SortOptions
                {
                    Direction = String.Equals(sorter.direction, "ASC",
                            StringComparison.CurrentCultureIgnoreCase)
                            ? OrderDirection.Ascending
                            : OrderDirection.Descending,
                    PropertyChain = sorter.property.Split('.').ToList()
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static List<Filter> GetFilterFromQueryString(IDictionary<string, string> filters)
        {
            if (filters == null || filters.Count == 0)
                return null;

            var filterList = new List<Filter>();
            for (int i = 0; i < filters.Keys.Count(k => k.EndsWith("[field]")); i++)
            {
                var filter = new Filter();

                var type = filters[string.Format("filter[{0}][data][type]", i)];
                var field = filters[string.Format("filter[{0}][field]", i)];
                var value = filters[string.Format("filter[{0}][data][value]", i)];

                var camelField = string.Format("{0}{1}", field.Substring(0, 1).ToUpperInvariant(), field.Substring(1));

                filter.Property = typeof (T).GetProperty(camelField);
                switch (type)
                {
                    case "string":
                        filter.Value = value;
                        break;
                    case "date":
                        filter.Value = DateTime.Parse(value);
                        break;
                    case "numeric":
                        filter.Value = int.Parse(value);
                        break;
                }
                
                var comparison = filters[string.Format("filter[{0}][data][comparison]", i)];
                if (string.IsNullOrEmpty(comparison))
                    filter.Comparison = ComparisonType.Contains;

                if (comparison == "lt")
                    filter.Comparison = ComparisonType.Less;
                else if (comparison == "gt")
                    filter.Comparison = ComparisonType.Greater;
                else filter.Comparison = ComparisonType.Equal;
                
                filterList.Add(filter);
            }

            return filterList;
        }
    }
}
