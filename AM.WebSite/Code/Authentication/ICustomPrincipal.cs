using System.Security.Principal;

namespace AM.WebSite.Security
{
    interface ICustomPrincipal : IPrincipal
    {
		string Login { get; set; }
    }
}