using AM.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace AM.WebSite.MVC
{
	public static class ControlHelper
	{
		#region Custom Helpers
		public static IDictionary<string, object> AddClass(this IDictionary<string, object> htmlAttributes, string name, string value)
		{
			// Find attribute name, and if it doesn't exists, add it
			if (!htmlAttributes.ContainsKey(name))
				htmlAttributes.Add(new KeyValuePair<string, object>(name, value));
			else
				htmlAttributes[name] += " " + value;

			return htmlAttributes;
		}

		public static IDictionary<string, object> GetHtmlAttributes(object htmlAttributes)
		{
			// Tales object attributes converting to a dictionary, and performing some replacements
			IDictionary<string, object> newAttributes = new RouteValueDictionary(htmlAttributes);

			foreach (string k in newAttributes.Keys.ToList())
				newAttributes.UpdateKey(k, k.Replace("data_", "data-"));

			return newAttributes;
		}
		#endregion
	}
}