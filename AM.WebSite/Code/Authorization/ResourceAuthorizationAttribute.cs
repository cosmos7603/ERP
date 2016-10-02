using System.Web.Mvc;
using AM.Services;

namespace AM.WebSite.Code.Authorization
{
	public class ResourceAuthorizationAttribute : AuthorizeAttribute
	{
		#region Properties
		public ResourceCode? ResourceCode { get; set; }
		#endregion

		#region Constructors
		public ResourceAuthorizationAttribute(ResourceCode resourceCode)
		{
			ResourceCode = resourceCode;
		}
		#endregion

		#region Methods
	
		#endregion
	}
}