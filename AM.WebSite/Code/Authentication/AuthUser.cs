using System.Security.Principal;

namespace AM.WebSite.Security
{
	public class AuthUser : ICustomPrincipal
	{
		public bool IsInRole(string userTypeCode)
		{
			return true;
		}

		public IIdentity Identity { get; }

		#region Properties
		public string Login { get; set; }
        #endregion

        #region Constructors
        public AuthUser(string login)
		{
			Identity = new GenericIdentity(login);
		}
		#endregion
	}
}