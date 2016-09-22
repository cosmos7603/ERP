using AM.Utils;
using AM.WebSite.Controls.Captcha.Models;
using AM.WebSite.Shared.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AM.WebSite.Controls.Captcha
{
    [Route("Controls/Captcha/{action=index}")]
    [ViewsPath("~/Controls/Captcha/Views")]
    public class CaptchaController : BaseController
    {
        #region Partial View

        [HttpPost]
        public ActionResult Captcha(string name)
        {
            var model = GetCaptchaModel(name);

            return PartialView(model);
        }

        #endregion

        #region Models

        [HttpPost]
        public static CaptchaModel GetCaptchaModel(string name)
        {
            var model = new CaptchaModel();

			if (name != "")
				model.ID = name;
			else
				model.ID = "captcha";

            // This Captcha code was extracted from:
            // http://www.stefanprodan.eu/2012/01/user-friendly-captcha-for-asp-net-mvc/ 

            var rand = new Random((int)DateTime.Now.Ticks);

            // Generate new question 
            int a = rand.Next(0, 9);
            int b = rand.Next(0, 9);
            int c = rand.Next(0, 9);
            int d = rand.Next(0, 9);
            int e = rand.Next(0, 9);
            var captcha = string.Format("{0}  {1}  {2}  {3}  {4}", a, b, c, d, e);

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                // Add noise 
                int i, r, x, y;
                Pen pen = new Pen(Color.Yellow);
                for (i = 1; i < 10; i++)
                {
                    pen.Color = Color.FromArgb((rand.Next(0, 255)), (rand.Next(0, 255)), (rand.Next(0, 255)));

                    r = rand.Next(0, (130 / 3));
                    x = rand.Next(0, 130);
                    y = rand.Next(0, 30);

                    gfx.DrawEllipse(pen, x - r, y - r, r, r);
                }

                // Add question 
                gfx.DrawString(captcha, new Font("Tahoma", 16), Brushes.Gray, 2, 3);

                // Render as Png 
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Png);

                model.Image = System.Convert.ToBase64String(mem.GetBuffer());
                model.EncryptedValue = CustomEncrypt.Encrypt(captcha.Replace(" ", ""));
            }

            return model;
        }

        #endregion

    }
}