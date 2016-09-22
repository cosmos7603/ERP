using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AM.WebSite.Controls.Captcha.Models
{
    public class CaptchaModel
    {
        public string ID { get; set; }
        public string EncryptedValue { get; set; }
        public string Image { get; set; }

		public CaptchaModel()
		{
			ID = "captcha";
		}
    }
}