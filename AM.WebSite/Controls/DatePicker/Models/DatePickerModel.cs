using System;
using System.Collections.Generic;

namespace AM.WebSite.Controls.DatePicker.Models
{
	public class DatePickerModel
	{
		public string ID { get; set; }
		public DateTime? Value { get; set; }
		public string DisplayValue { get; set; }
		public IDictionary<string, object> HtmlAttributes { get; set; }
	}
}