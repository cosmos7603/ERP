using System;

namespace AM.WebSite
{
	public class ActiveUser
	{
		#region Properties
		public string Login { get; set; }
		public string UserLevelCode { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }

		public int StoreId { get; set; }
        public int? BranchId { get; set; }
		public int ClientMinutesOffset { get; set; }
		public string BrandCode { get; set; }
		public string HoCode { get; set; }
        public bool CantChangeBranch { get; set; }
		public bool IsEnacting { get; set; }
		public bool IsCorporate => StoreId == Services.Consts.CORPORATE_STORE_ID;
		public bool EnableBranches { get; set; }
		public bool DisableChangeCounselor { get; set; }
		public bool ChangeOnlineBookings { get; set; }

		public ActiveSettings Settings { get; set; }
		//public ActiveRights Rights { get; set; }
		public RemindersFilter RemindersFilter { get; set; }
		#endregion

		public DateTime ClientDateTime => DateTime.Now.AddMinutes(ClientMinutesOffset);
	}
}