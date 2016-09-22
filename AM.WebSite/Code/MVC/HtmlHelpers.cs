using AM.Services;
using AM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace AM.WebSite.MVC
{
	public static partial class HtmlHelperExtensions
	{
		#region Scripts
		public static MvcHtmlString Script(this HtmlHelper htmlHelper, string url)
		{
			string scriptTag = "<script type=\"text/javascript\" src=\"" + UrlHelper.GenerateContentUrl(url + "?v=" + AppInfo.Version, new HttpContextWrapper(HttpContext.Current)) + "\"></script>";

			return new MvcHtmlString(scriptTag);
		}

		private static string Script(string script)
		{
			return "<script type=\"text/javascript\">" + script + "</script>";
		}
		#endregion

		#region Style
		public static MvcHtmlString Style(this HtmlHelper htmlHelper, string url)
		{
			string styleTag = "<link href=\"" + UrlHelper.GenerateContentUrl(url + "?v=" + AppInfo.Version, new HttpContextWrapper(HttpContext.Current)) + "\" rel=\"stylesheet\" />";
		   
			return new MvcHtmlString(styleTag);
		}
		#endregion

		#region Json
		public static MvcHtmlString RenderObject(this HtmlHelper htmlHelper, string varName, object o)
		{
			string json = "null";

			if (o != null)
				json = Json.Encode(o);

			return new MvcHtmlString(Script(String.Format("var {0} = {1};", varName, json)));
		}

		
		#endregion

		#region DropDownList Extensions
		public static SelectList ToSelectList<T>(
			this IEnumerable<T> enumerable,
			Func<T, string> text,
			Func<T, int> value)
		{
			List<SelectListItem> list = enumerable
			  .Select(x => new SelectListItem { Text = text(x), Value = value(x).ToString() })
			  .ToList();

			return new SelectList(list, "Value", "Text");
		}

		public static SelectList ToSelectList<T>(
			this IEnumerable<T> enumerable,
			Func<T, string> text,
			Func<T, int> value,
			int? selectedValue)
		{
			List<SelectListItem> list = enumerable
			  .Select(x => new SelectListItem { Text = text(x), Value = value(x).ToString() })
			  .ToList();

			return new SelectList(list, "Value", "Text", selectedValue);
		}

        public static SelectList ToSelectList<T>(
            this IEnumerable<T> enumerable,
            Func<T, string> text,
            Func<T, int> value,
            SelectListItem selectedItem)
        {
            List<SelectListItem> list = enumerable
              .Select(x => new SelectListItem { Text = text(x), Value = value(x).ToString() })
              .ToList();

            list.Insert(0, selectedItem);

            return new SelectList(list, "Value", "Text", selectedItem.Value);
        }

        public static SelectList ToSelectList<T>(
            this IEnumerable<T> enumerable,
            Func<T, string> text,
            Func<T, int> value,
            SelectListItem selectedItem,
            int? selectedValue)
        {
            List<SelectListItem> list = enumerable
              .Select(x => new SelectListItem { Text = text(x), Value = value(x).ToString() })
              .ToList();

            list.Insert(0, selectedItem);

            return new SelectList(list, "Value", "Text", selectedValue);
        }

        public static SelectList ToSelectList<T>(
			this IEnumerable<T> enumerable,
			Func<T, string> text,
			Func<T, string> value)
		{

			List<SelectListItem> list = enumerable
			  .Select(x => new SelectListItem { Text = text(x), Value = value(x) })
			  .ToList();

			return new SelectList(list, "Value", "Text");
		}

		public static SelectList ToSelectList<T>(
			this IEnumerable<T> enumerable,
			Func<T, string> text,
			Func<T, string> value,
			string selectedValue)
		{

			List<SelectListItem> list = enumerable
			  .Select(x => new SelectListItem { Text = text(x), Value = value(x) })
			  .ToList();

			return new SelectList(list, "Value", "Text", selectedValue);
		}

		public static SelectList ToSelectList<T>(
			this IEnumerable<T> enumerable,
			Func<T, string> text,
			Func<T, string> value,
			Func<T, string> optGroup)
		{
			List<SelectListItem> list = enumerable
				.Select(x => new SelectListItem { Text = text(x), Value = value(x), Group = new SelectListGroup { Name = optGroup(x) } })
				.ToList();

			return new SelectList(list, "Value", "Text", "Group");
		}

		public static SelectList ToSelectList<T>(
			this IEnumerable<T> enumerable,
			Func<T, string> text,
			Func<T, string> value,
			Func<T, string> optGroup,
			string selectedValue)
		{
			List<SelectListItem> list = enumerable
				.Select(x => new SelectListItem { Text = text(x), Value = value(x), Group = new SelectListGroup { Name = optGroup(x) } })
				.ToList();

			return new SelectList(list, "Value", "Text", "Group", (object)selectedValue);
		}

		public static SelectList ToSelectList<T>(
			this IEnumerable<T> enumerable,
			Func<T, string> text,
			Func<T, int> value,
			Func<T, string> optGroup)
		{
			List<SelectListItem> list = enumerable
				.Select(x => new SelectListItem { Text = text(x), Value = value(x).ToString(), Group = new SelectListGroup { Name = optGroup(x) } })
				.ToList();

			return new SelectList(list, "Value", "Text", "Group.Name", (object)null);
		}

		public static SelectList ToSelectList<T>(
			this IEnumerable<T> enumerable,
			Func<T, string> text,
			Func<T, int> value,
			Func<T, string> optGroup,
			int? selectedValue)
		{
			List<SelectListItem> list = enumerable
				.Select(x => new SelectListItem { Text = text(x), Value = value(x).ToString(), Group = new SelectListGroup { Name = optGroup(x) } })
				.ToList();

			return new SelectList(list, "Value", "Text", "Group", selectedValue);
		}

		public static SelectList SelectItem(this SelectList selectList, int value)
		{
			return SelectItem(selectList, value.ToString());
		}

		public static SelectList SelectItem(this SelectList selectList, string value)
		{
			return SelectItem(selectList, value, "");
		}

		public static SelectList SelectItem(this SelectList selectList, int value, string text)
		{
			if (String.IsNullOrEmpty(text) && value == 0)
				return selectList;

			return SelectItem(selectList, value.ToString(), text);
		}

		public static SelectList SelectItem(this SelectList selectList, string value, string text)
		{
			SelectListItem item = selectList.Where(x => x.Value == value).FirstOrDefault();

			if (item != null)
			{
				selectList = new SelectList(selectList.Items, selectList.DataValueField, selectList.DataTextField, selectList.DataGroupField, (object)value);
			}
			else if (text != "")
			{
				List<SelectListItem> items = selectList.ToList();
				SelectListItem newItem = new SelectListItem() { Value = value, Text = text };
				items.Insert(0, newItem);
				selectList = new SelectList(items, "Value", "Text", value);
			}

			return selectList;
		}
		#endregion
	}

	#region Classes
	public class DropDownOption
	{
		public static readonly string ALL = "-- All --";
		public static readonly string SELECT = "-- Select --";
		public static readonly string OTHER = "-- Other --";
		public static readonly string NONE = "-- None --";
		public static readonly string EMPTY = "--";
        public static readonly string REMOVE = "";
	}
	#endregion
}