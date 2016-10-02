using System;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Security;
using AM.Services.Models;
using AM.WebSite.Security;
using Newtonsoft.Json;

namespace AM.WebSite
{
	public static class AppInfo
	{
		#region Members
		private static WebConfigSettings m_webConfigSettings;
		#endregion

		#region Properties
	

	

		private static string AuthLogin
		{
			get
			{
				string authLogin = "NO_LOGIN";

				if (HttpContext.Current == null)
					return authLogin;

				var au = HttpContext.Current.User as AuthUser;

				if (au == null)
					return authLogin;

				return au.Login;
			}
		}





		public static SessionInfo SessionInfo
		{
			get { return SessionInfo.GetSessionInfo(); }
		}

		public static string Version
		{
			get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
		}

		public static string SupportInfo
		{
			get { return HttpContext.Current.Server.MachineName + " / " + Config.Application.EnvironmentCode; }
		}

	

	

		public static WebConfigSettings WebConfig
		{
			get
			{
				if (m_webConfigSettings == null)
					m_webConfigSettings = new WebConfigSettings();

				return m_webConfigSettings;
			}
		}

		public static string LoginFooter
		{
			get { return string.Concat("© ", DateTime.Today.Year, " - ", AppName); }
		}

		public static string AppFooter
		{
			get { return string.Concat("© ", DateTime.Today.Year, " - ", AppName, " - v", Version, " - ", SupportInfo); }
		}

		public static string AppTitle
		{
			get { return AppName; }
		}

		public static string AppName
		{
			get { return "ERP"; }
		}
		#endregion

		#region Methods
		public static void Refresh()
		{
			m_webConfigSettings = new WebConfigSettings();
		}

	
		//public static ActiveUser LoadUserInfo(string login)
		//{
		//	var key = login;

		//	// Load user info from database
		//	var user = UserService.GetUserIdentity(login);

		//	// Settings
		//	var activeSettings = new ActiveSettings
		//	{
		//		SessionTimeout = Config.Security.UserSessionTimeout,
		//		SessionWarning = Config.Security.UserSessionWarning,
		//	};

		//	// User
		//	var activeUser = new ActiveUser()
		//	{
		//		// User properties
		//		Login = user.Login,
		//		UserLevelCode = user.UserLevelCode,
		//		UserName = user.FirstName + " " + user.LastName,
		//		Email = user.Email,
		//		StoreId = user.StoreId,
		//		BranchId = user.BranchId,
		//		CantChangeBranch = user.CantChangeBranch,
		//		BrandCode = user.Store.BrandCode,
		//		HoCode = user.Store.Brand.HoCode,
		//		Settings = activeSettings,
		//		DisableChangeCounselor = user.DisableCounselor,
		//		ChangeOnlineBookings = user.ChangeOnlineBookings ?? false
		//	};

		//	// Store
		//	var activeStore = new ActiveStore
		//	{
		//		StoreId = user.StoreId,
		//		StoreName = user.Store.StoreName,
		//		BrandCode = user.Store.BrandCode,
		//		FullVersion = user.Store.FullVersion,
		//		EnableBranches = user.Store.EnableBranches,
		//		InvoiceStoreName = user.Store.InvoiceStoreName,
		//		GstPricings = user.Store.GstPricings,
		//		Virtuoso = user.Store.Virtuoso
		//	};

		//	// Brand
		//	var activeBrand = new ActiveBrand
		//	{
		//		BrandCode = user.Store.BrandCode,
		//		BrandName = user.Store.Brand.BrandName,
		//		HeadOfficeCode = user.Store.Brand.HoCode,
		//		EnableCraGst = user.Store.Brand.EnableCraGst,
  //              Berthing = user.Store.Brand.Berth,
		//		EnableSGA = user.Store.Brand.EnableSga
		//	};

		//	// Head Office
		//	var activeHeadOffice = new ActiveHeadOffice
		//	{
		//		HeadOfficeCode = user.Store.Brand.HoCode,
		//		HeadOfficeName = user.Store.Brand.HeadOffice.HoName
		//	};

		//	// Head office and brand settings
		//	if (activeUser.IsCorporate)
		//	{
		//		// Corporate users
		//		if (user.UserLevelCode == Services.UserLevelCode.B)
		//		{
		//			activeUser.BrandCode = user.CorpBrandCode;
		//			activeUser.HoCode = user.CorpBrand.HoCode;

		//			// Brand
		//			activeBrand = new ActiveBrand
		//			{
		//				BrandCode = user.CorpBrandCode,
		//				BrandName = user.CorpBrand.BrandName,
		//				HeadOfficeCode = user.CorpBrand.HoCode
		//			};

		//			// Head Office
		//			activeHeadOffice = new ActiveHeadOffice
		//			{
		//				HeadOfficeCode = user.CorpBrand.HeadOffice.HoCode,
		//				HeadOfficeName = user.CorpBrand.HeadOffice.HoName
		//			};
		//		}
		//		else if (user.UserLevelCode == UserLevelCode.H)
		//		{
		//			activeUser.HoCode = user.CorpHoCode;

		//			// Head Office
		//			activeHeadOffice = new ActiveHeadOffice
		//			{
		//				HeadOfficeCode = user.CorpHeadOffice.HoCode,
		//				HeadOfficeName = user.CorpHeadOffice.HoName
		//			};
		//		}
		//	}

		//	// Rights
		//	activeUser.Rights = new ActiveRights
		//	{
		//		CanEnterStoreNews = user.CanEnterStoreNews,
		//		DisableChangeCounselor = user.DisableCounselor,
		//		OwnData = user.OwnData
		//	};

		//	// Reminders Filter
		//	activeUser.RemindersFilter = new RemindersFilter
		//	{
		//		ReminderFilterCe = user.ReminderFilterCe,
		//		ReminderFilterCustomerLead = user.ReminderFilterCustomerLead,
		//		ReminderFilterDateRange = user.ReminderFilterDateRange,
		//		ReminderFilterDiscretionary = user.ReminderFilterDiscretionary,
		//		ReminderFilterFinancial = user.ReminderFilterFinancial,
		//		ReminderFilterLogin = user.ReminderFilterLogin,
		//		ReminderFilterResGrp = user.ReminderFilterResGrp,
		//		ReminderFilterSuppressOverdue = user.ReminderFilterSuppressOverdue
		//	};

		//	// Cache items
		//	CacheManager.Set($"{login}-ActiveUser", activeUser);
		//	CacheManager.Set($"{login}-ActiveStore", activeStore);
		//	CacheManager.Set($"{login}-ActiveBrand", activeBrand);
		//	CacheManager.Set($"{login}-ActiveHeadOffice", activeHeadOffice);

		//	return activeUser;
		//}

		//public static void LoadUserPermissions()
		//{
		//	var login = ActiveUser.Login;

		//	var menuModuleList = MenuService.GetMenuModuleList(login);
		//	var menuResourceList = MenuService.GetMenuResourceList(login);
		//	var authorizedPermissionList = SecurityService.GetUserAuthorizedPermissionList(login);
		//	var authorizedResourceList = SecurityService.GetUserAuhtorizedResourceList(authorizedPermissionList);

		//	// Cache items
		//	CacheManager.Set($"{login}-MenuModuleList", menuModuleList);
		//	CacheManager.Set($"{login}-MenuResourceList", menuResourceList);
		//	CacheManager.Set($"{login}-AuthorizedPermissionList", authorizedPermissionList);
		//	CacheManager.Set($"{login}-AuthorizedResourceList", authorizedResourceList);
		//}

		public static void DeserializeActiveUser()
		{
			var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

			if (authCookie == null)
				return;

			var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

			if (authTicket == null || authTicket.Expired)
			{
				Logout();
				return;
			}

			var serializeModel = JsonConvert.DeserializeObject<SerializablePrincipal>(authTicket.UserData);

			if (string.IsNullOrEmpty(authTicket.UserData))
				return;

			var authUser = new AuthUser(authTicket.Name)
			{
				Login = serializeModel.Login
			};

			HttpContext.Current.User = authUser;
		}

		public static void Logout()
		{
			// Clear authentication cookie
			HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
			authCookie.Expires = DateTime.Now.AddYears(-1);
			HttpContext.Current.Response.Cookies.Add(authCookie);

			// Session info
			SessionInfo.EndSession();
		}

		public static void SetDateFormat()
		{
			CultureInfo newCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
			newCulture.DateTimeFormat.ShortDatePattern = "dd MMM yyyy";
			newCulture.DateTimeFormat.DateSeparator = " ";
			Thread.CurrentThread.CurrentCulture = newCulture;
		}

		//public static bool UserHasPermission(string permissionName)
		//{
		//	if (ActiveUser == null)
		//		return false;

		//	return MenuResourceList.Any(r => r.Path == permissionName);
		//}

		#endregion

		#region Classes
		public class WebConfigSettings
		{
			public string ConnectionString;
			public string EncryptionKey;

			public WebConfigSettings()
			{
				ConnectionString = ConfigurationManager.ConnectionStrings["ERPEntities"].ConnectionString;
				EncryptionKey = ConfigurationManager.AppSettings["encryptionKey"];
			}
		}
		#endregion
	}
}