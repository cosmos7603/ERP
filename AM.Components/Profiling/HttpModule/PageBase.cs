using System;
using System.Web.UI;
using CultureInfo = System.Globalization.CultureInfo;

namespace Corpnet.Profiling.HttpModule
{
	internal abstract class PageBase : Page
	{
		private string _title = "Corpnet.Profiling";

		protected string BasePageName
		{
			get { return this.Request.ServerVariables["URL"]; }
		}

		protected new virtual string Title
		{
			get { return _title; }
			set { _title = value; }
		}

		protected virtual void RenderDocumentStart(HtmlTextWriter writer)
		{
			if (writer == null)
				throw new ArgumentNullException("writer");

			writer.RenderBeginTag(HtmlTextWriterTag.Html);  // <html>

			writer.RenderBeginTag(HtmlTextWriterTag.Head);  // <head>
			RenderHead(writer);
			writer.RenderEndTag();                          // </head>
			writer.WriteLine();

			writer.RenderBeginTag(HtmlTextWriterTag.Body);  // <body>
		}

		protected virtual void RenderHead(HtmlTextWriter writer)
		{
			//
			// Write the document title.
			//

			writer.RenderBeginTag(HtmlTextWriterTag.Title);
			Server.HtmlEncode(this.Title, writer);
			writer.RenderEndTag();
			writer.WriteLine();

			//
			// Write a <link> tag to relate the style sheet.
			//

			writer.AddAttribute("rel", "stylesheet");
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
			writer.AddAttribute(HtmlTextWriterAttribute.Href, this.BasePageName + "/stylesheet");
			writer.RenderBeginTag(HtmlTextWriterTag.Link);
			writer.RenderEndTag();
			writer.WriteLine();
		}

		protected virtual void RenderDocumentEnd(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, "Footer");
			writer.RenderBeginTag(HtmlTextWriterTag.P); // <p>

			// Write out server date, time and time zone details.
			DateTime now = DateTime.Now;

			writer.Write("Server date is ");
			this.Server.HtmlEncode(now.ToString("D", CultureInfo.InvariantCulture), writer);

			writer.Write(". Server time is ");
			this.Server.HtmlEncode(now.ToString("T", CultureInfo.InvariantCulture), writer);

			writer.Write(". All dates and times displayed are in the ");
			writer.Write(TimeZone.CurrentTimeZone.IsDaylightSavingTime(now) ?
				TimeZone.CurrentTimeZone.DaylightName : TimeZone.CurrentTimeZone.StandardName);
			writer.Write(" zone. ");

			writer.RenderEndTag(); // </p>

			writer.RenderEndTag(); // </body>
			writer.WriteLine();

			writer.RenderEndTag(); // </html>
			writer.WriteLine();
		}

		protected override void Render(HtmlTextWriter writer)
		{
			RenderDocumentStart(writer);
			RenderContents(writer);
			RenderDocumentEnd(writer);
		}

		protected virtual void RenderContents(HtmlTextWriter writer)
		{
			base.Render(writer);
		}
	}
}
