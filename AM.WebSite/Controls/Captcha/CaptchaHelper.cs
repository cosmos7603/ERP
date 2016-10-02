using System.Web.Mvc;
using System.Web.Mvc.Html;
using AM.WebSite.Controls.Captcha;

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