namespace AM.WebSite
{
	public class RemindersFilter
	{
		public bool ReminderFilterFinancial { get; set; }
		public bool ReminderFilterCe { get; set; }
		public bool ReminderFilterResGrp { get; set; }
		public bool ReminderFilterCustomerLead { get; set; }
		public bool ReminderFilterDiscretionary { get; set; }
		public string ReminderFilterDateRange { get; set; }
		public bool ReminderFilterSuppressOverdue { get; set; }
		public string ReminderFilterLogin { get; set; }
	}
}