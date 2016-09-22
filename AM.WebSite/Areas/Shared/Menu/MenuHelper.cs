using AM.DAL;
using AM.Utils;
using AM.WebSite.Areas.Shared.Menu.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc.Html;
using System.Web.Routing;
using AM.DAL.QueryResults;
using AM.Services.Business;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region Menu
		public static MvcHtmlString AMMenu(this HtmlHelper htmlHelper)
		{
			//var model = new MenuModel();

			//// Don't render any menu for public users
			//if (AppInfo.PublicMode)
			//	return null;
			//model.ReportNotifications = CriticalNotificationService.GetReportNotifications(AppInfo.ActiveUser.Login);
			//// Render menu
			//foreach (var m in AppInfo.MenuModuleList)
			//{
			//	model.Modules.Add(new MenuModel.ModuleItem
			//	{
			//		Title = m.ModuleName,
			//		ModuleCode = m.ModuleCode,
			//		Path = "",
			//		Icon = m.Icon,
			//		MenuItems = GetModuleMenuItems(m.ModuleCode, m.ModuleName, model),
			//		WarningLabelCount = (m.ModuleName == "Reports" && model.ReportNotifications != null) ? model.ReportNotifications.Count : 0
			//	});
			//}

			

			return htmlHelper.Partial("~/Areas/Shared/Menu/Views/_Navigation.cshtml");
		}

		//private static List<MenuModel.MenuItem> GetModuleMenuItems(string moduleCode, string moduleName, MenuModel menuModel)
		//{
		//	var menuItems = new List<MenuModel.MenuItem>();

		//	foreach (var r in AppInfo.MenuResourceList.Where(x => x.ModuleCode == moduleCode && x.ParentResourceName == null))
		//	{
		//		var menuItem = BuildMenuItem(r);

		//		menuItem.Childs = GetChildMenuItems(r.ResourceName);

		//		menuItem.WarningLabelCount = (menuItem.Title == "Memorized Reports" && menuModel.ReportNotifications != null)
		//			? menuModel.ReportNotifications.Count
		//			: 0;
		//		menuItems.Add(menuItem);
		//	};

		//	return menuItems;
		//}

		//private static List<MenuModel.MenuItem> GetChildMenuItems(string resourceName)
		//{
		//	var menuItems = new List<MenuModel.MenuItem>();

		//	foreach (var r in AppInfo.MenuResourceList.Where(x => x.ParentResourceName == resourceName))
		//	{
		//		var menuItem = BuildMenuItem(r);

		//		menuItem.Childs = GetChildMenuItems(r.ResourceName);

		//		menuItems.Add(menuItem);
		//	};

		//	return menuItems;
		//}

		//private static MenuModel.MenuItem BuildMenuItem(Resource r)
		//{
		//	var menuItem = new MenuModel.MenuItem();

		//	menuItem.Title = r.MenuText;
		//	menuItem.Position = r.MenuSeqNum.GetValueOrDefault();
		//	menuItem.Separator = r.MenuSeparator.GetValueOrDefault();
		//	menuItem.ResourceCode = r.ResourceName;
		//	menuItem.ParentResourceCode = r.ParentResourceName;
		//	menuItem.Path = r.Path;

		//	if (r.Legacy)
		//		menuItem.Path = "javascript:legacysso(\"" + r.Path + "\");'";

		//	return menuItem;
		//}
		#endregion
	}
}