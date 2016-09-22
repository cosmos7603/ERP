using System.Collections.Generic;
using AM.DAL.QueryResults;

namespace AM.WebSite.Areas.Shared.Menu.Models
{
	public class MenuModel
    {
        public List<ModuleItem> Modules { get; set; }

        public MenuModel()
        {
			Modules = new List<ModuleItem>();
		}

		public class ModuleItem
		{
			public string ModuleCode { get; set; }
			public string Title { get; set; }
			public string Path { get; set; }
			public string Icon { get; set; }

			public int WarningLabelCount { get; set; }

			public List<MenuItem> MenuItems { get; set; }
		}

		public class MenuItem
		{
			public string ResourceCode { get; set; }
			public string ParentResourceCode { get; set; }
			public string Title { get; set; }
			public string Path { get; set; }
			public int Position { get; set; }
			public bool Separator { get; set; }

			public List<MenuItem> Childs { get; set; }
			public int WarningLabelCount { get; set; }
		}

		public List<DashboardGetReportNotificationsResult> ReportNotifications { get; set; }
	}
}