using AM.WebSite.Controls.Captcha;
using AM.WebSite.Controls.Captcha.Models;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region Captcha
		public static MvcHtmlString PSCaptcha(this HtmlHelper htmlHelper)
		{
			return htmlHelper.PSCaptcha("");
		}

		public static MvcHtmlString PSCaptcha(this HtmlHelper htmlHelper, string name)
		{
			var model = CaptchaController.GetCaptchaModel(name);

			return htmlHelper.Partial("~/Controls/Captcha/Views/Captcha.cshtml", model);
		}
		#endregion
	}
}