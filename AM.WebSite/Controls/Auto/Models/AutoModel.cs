using System.Collections.Generic;

namespace AM.WebSite.Controls.Auto.Models
{
	public class AutoModel
	{
		public string ID { get; set; }
        public string Text { get; set; }
        public int? Value { get; set; }
        public bool Enabled { get; set; }
        public IDictionary<string, object> HtmlAttributes { get; set; }

        //Ajax Settings
        public string Action { get; set; }
		public int MinLength { get; set; }
        public int DelayMs { get; set; }

        //ConfigurationSettings
        //public string TransformResultFunction { get; set; }
        public string OnSelectFunction { get; set; }
        public string OnSearchStartFunction { get; set; }
        public string OnSearchCompleteFunction { get; set; }

        //Presentation Settings
        public string ContainerClass { get; set; }
        public string FormatResultsFunction { get; set; }
        public int WidthPx { get; set; }
        public bool AutoSelectFirst { get; set; }

        public AutoModel()
		{
			Value = null;
			Text = string.Empty;
            DelayMs = 0;
			MinLength = 2;
			Enabled = true;
            ContainerClass = "autocomplete-suggestions";
            WidthPx = 0;
            AutoSelectFirst = true;
            FormatResultsFunction = null;
            OnSelectFunction = null;
            OnSearchStartFunction = null;
            OnSearchCompleteFunction = null;
        }
	}
}