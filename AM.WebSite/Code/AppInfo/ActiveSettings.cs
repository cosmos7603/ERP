using AM.Services;
using AM.Services.Models;

namespace AM.WebSite
{
    public class ActiveSettings
    {
		#region Properties
		public int SessionTimeout { get; set; }
		public int SessionWarning { get; set; }
		#endregion
	}
}