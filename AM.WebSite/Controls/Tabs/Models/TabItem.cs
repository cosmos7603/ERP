﻿namespace AM.WebSite.Controls.Tabs.Models
{
	public class TabItem
	{
		public TabItem(string title)
		{
			Title = title;
		}

		public string Title { get; set; }
		public bool Active { get; set; }

		public string TabID
		{
			get
			{
				return "tab" + GetID(this.Title);
			}
		}

		public string DivID
		{
			get
			{
				return "div" + GetID(this.Title);
			}
		}

        public string TabPaneID
        {
            get
            {
                return "tabPane" + GetID(this.Title);
            }
        }

        private string GetID(string title)
		{
			string id = title;

			id = id.Replace(" ", "");
			id = id.Replace("&", "");
			id = id.Replace("+", "");
			id = id.Replace("/", "");
			id = id.Replace("-", "");
			id = id.Replace("-", "");
			id = id.Replace("(", "");
			id = id.Replace(")", "");
	        id = id.Replace(".", "");

			return id;
		}
	}
}