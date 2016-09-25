using System.Reflection;

namespace AM.Services.Grid
{
    public class Filter
    {
        public PropertyInfo Property { get; set; }
        public object Value { get; set; }
        public ComparisonType Comparison { get; set; }
    }
}
