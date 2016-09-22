//using System;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using AM.Services;

//namespace AM.WebSite.Code.Authorization
//{
//	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
//	public class PrivilegeAuthorizationAttribute : AuthorizeAttribute
//	{
//		#region Properties
//		public PrivilegeCode PrivilegeCode { get; set; }
//		#endregion

//		#region Constructors
//		public PrivilegeAuthorizationAttribute(PrivilegeCode privilegeCode)
//		{
//			PrivilegeCode = privilegeCode;
//		}
//		#endregion

//		#region Methods
//		public override void OnAuthorization(AuthorizationContext filterContext)
//		{
//			if (filterContext == null)
//				throw new ArgumentNullException("No filter context.");

//			// No logged-in user trying to access a private page
//			if (!HttpContext.Current.User.Identity.IsAuthenticated)
//			{
//				filterContext.Result = UnauthorizedResults.CreateSessionExpiredResult(filterContext);
//				return;
//			}

//			// Find attribute
//			var privilegeAuthorizationAttribute = filterContext
//				.ActionDescriptor
//				.ControllerDescriptor
//				.GetCustomAttributes(typeof(PrivilegeAuthorizationAttribute), true).FirstOrDefault();

//			// Only validate if there's an attribute
//			if (privilegeAuthorizationAttribute != null)
//			{
//				var privilegeCode = ((PrivilegeAuthorizationAttribute)privilegeAuthorizationAttribute).PrivilegeCode.ToString();

//				// Resource not available for user type
//				if (AppInfo.AuhtorizedPrivilegeList.All(x => x.PrivilegeCode != privilegeCode))
//				{
//					filterContext.Result = UnauthorizedResults.CreateUnauthorizedResult(filterContext);
//				}
//			}
//		}
//		#endregion
//	}
//}