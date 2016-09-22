using System;

namespace AM.Services.Models
{
	public class ServiceUserIdentity
	{
		public string AuditLogin { get; set; }
		public string Login { get; set; }
		public string UserLevelCode { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public int StoreId { get; set; }
		public string BrandCode { get; set; }
		public string HoCode { get; set; }
		public bool IsEnacting { get; set; }
		public bool IsCorporate => StoreId == 0;
		public DateTime ClientDateTime => DateTime.Now.AddMinutes(ClientMinutesOffset);
		public int ClientMinutesOffset { get; set; }
	}
}
