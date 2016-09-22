using AM.Services;
using AM.Utils;
using AM.WebSite;
using System;
using System.Web;
using System.Linq;
using AM.Services.Support;

namespace AM.WebSite
{
	[Serializable]
	public class SessionInfo
	{
		#region Consts
		private const string COOKIE_NAME = "AM-SessionInfoCookie";
        #endregion

        #region Properties
        public int? ActiveStoreId { get; private set; }
        #endregion



        #region Methods
        public void SelectHQ()
        {
            ActiveStoreId = null;

            WriteCookie(this);

           // CacheManager.GetActiveStore(true);
        }

        public void SelectStore(int storeId)
        {
            ActiveStoreId = storeId;

            WriteCookie(this);

           // CacheManager.GetActiveStore(true);
        }

        #endregion

        #region Cookie Management
        public static void StartSession()
		{
			WriteCookie(new SessionInfo());
		}

		public static void EndSession()
		{
			DeleteCookie();
		}

		private static void WriteCookie(SessionInfo sessionInfo)
		{
			var ucCookie = new HttpCookie(SessionInfo.COOKIE_NAME, SessionInfo.Serialize(sessionInfo));
			HttpContext.Current.Response.Cookies.Add(ucCookie);
		}

		private static void DeleteCookie()
		{
			var ucCookie = new HttpCookie(SessionInfo.COOKIE_NAME, "");
			ucCookie.Expires = DateTime.Now.AddYears(-1);
			HttpContext.Current.Response.Cookies.Add(ucCookie);
		}

        /// <summary>
        /// Tries first to read a cookie from the Response collection to get changes if the cookie was updated in the current request.
        /// Otherwise it reads it from the Request collection wich has the original cookie returned by the server.
        /// </summary>
        /// <param name="name">The cookie name.</param>
        /// <returns></returns>
        private static string ReadCookie(string name)
        {
            if (HttpContext.Current.Response.Cookies.AllKeys.Contains(name))
            {
                var cookie = HttpContext.Current.Response.Cookies[name];
                return cookie?.Value;
            }

            if (HttpContext.Current.Request.Cookies.AllKeys.Contains(name))
            {
                var cookie = HttpContext.Current.Request.Cookies[name];
                return cookie?.Value;
            }

            return null;
        }

        public static SessionInfo GetSessionInfo()
		{
			var sessionInfoCookie = ReadCookie(SessionInfo.COOKIE_NAME);
			var si = new SessionInfo();

			// Cookie not exists?
            if (sessionInfoCookie != null)
			{
				si = SessionInfo.Deserialize(sessionInfoCookie);

				// Existing cookie, but corrupted
				if (si == null)
					si = new SessionInfo();
			}

			return si;
		}

		private static string Serialize(SessionInfo c)
		{
			return EncryptionService.Encrypt(Serialization.SerializeObjectToBase64(c));
		}

		private static SessionInfo Deserialize(string s)
		{
			SessionInfo uc = null;

			try
			{
				uc = (SessionInfo)(Serialization.DeserializeObjectFromBase64(EncryptionService.Decrypt(s)));
			}
			catch (Exception ex)
			{
				LogService.Error(EventCode.APP, "Error deserializing cookie.", ex, s);
			}

			return uc;
		}
		#endregion
	}
}