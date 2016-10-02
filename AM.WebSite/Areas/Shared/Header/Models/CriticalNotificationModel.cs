using System.Collections.Generic;
using AM.DAL.QueryResults;

namespace AM.WebSite.Areas.Shared.Header.Models
{
	public class CriticalNotificationModel 
	{
		public List<DashboardGetCriticalNotificationsResult> CriticalNotifications { get; set; }
		
	}
}