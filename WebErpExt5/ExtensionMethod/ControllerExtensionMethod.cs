using System;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebErpExt5.ExtensionMethod
{
    public static class ControllerExtensionMethod
    {
        public static ContentResult GetJsonResponseFromObject(this Controller controller, object responseObject)
        {
            return GetJsonResponseFromObject(controller, responseObject, "dd/MM/yyyy");
        }

        public static ContentResult GetJsonResponseFromObject(this Controller controller, object responseObject, string dateFormat)
        {
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = dateFormat };
            var strResult = JsonConvert.SerializeObject(responseObject, Formatting.None, dateTimeConverter);

            return new ContentResult { Content = strResult, ContentType = "application/json" };
        }

        public static DateTime ParseDate(this Controller controller, string stringDate)
        {
            return DateTime.ParseExact(stringDate, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
        }

        public static string ParseTime(this Controller controller, string stringDate)
        {
            var dateTime = ParseDate(controller, stringDate);

            return dateTime.ToString("HH:mm:ss");
        }

        public static UserData GetCurrentUser(this Controller controller)
        {
            var cookie = controller.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || cookie.Value == null)
            {
                throw new Exception("No logged in user.");
            }
            var decryptedCookie = FormsAuthentication.Decrypt(cookie.Value);
            if (decryptedCookie == null)
            {
                throw new Exception("Unknown cookie.");
            }

            var userData = decryptedCookie.UserData;
            return JsonConvert.DeserializeObject<UserData>(userData);
        }
    }
}
