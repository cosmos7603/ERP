using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using WebErpExt5.Models;

namespace WebErpExt5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new IndexModel
            {
                CurrentEnvironment = ConfigurationManager.AppSettings["CurrentEnv"],
                Language = "es"
            };
            return View(model);
        }


        public ActionResult Clients()
        {
            //var model = new IndexModel
            //{
            //    CurrentEnvironment = ConfigurationManager.AppSettings["CurrentEnv"],
            //    Language = "es"
            //};
            return View();
        }

        //public ActionResult LogOn()
        //{
        //    var model = new LogOnModel();

        //    return View(model);
        //}

        //        [HttpPost]
        //        public ActionResult LogOn(LogOnModel model)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                //var accountService = AccountService.GetInstance();

        //                //if (accountService.ValidateUser(model.UserName, model.Password))
        //                //{
        //                    var userData = new UserData
        //                    {
        //                        UserName = model.UserName
        //                    };
        //                    var authTicket = new FormsAuthenticationTicket(
        //                        1,
        //                        Guid.NewGuid().ToString(),
        //                        DateTime.Now,
        //                        DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
        //                        model.RememberMe,
        //                        JsonConvert.SerializeObject(userData),
        //                        "/");
        //                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket))
        //                    {
        //                        Expires = authTicket.Expiration
        //                    };
        //                    Response.Cookies.Add(cookie);

        //                    string returnUrl = HttpContext.Request.QueryString["ReturnUrl"];
        //                    if (!string.IsNullOrEmpty(returnUrl))
        //                    {
        //                        return Redirect(returnUrl);
        //                    }

        //                    return RedirectToAction("Index", "Home");
        //                //}

        //                // ModelState.AddModelError(string.Empty, "User or password incorrect.");
        //            }

        //            return View(model);
        //        }
    }
}
