using AM.DAL;
using AM.Services.Models;
using AM.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Timers;
using System.Xml;

namespace AM.Services.Support
{
	public class EmailingService : ServiceBase
	{
		#region Enums
		public enum EmailTemplateCode
		{
			NewUserEmail,
			ResetPasswordEmail,
			DaemonNotification,
			AdminPasswordResetNotification
		}

		public enum EmailStatusCode
		{
			QUEUED,
			SENT,
			ERROR
		}
		#endregion

		#region Consts
		private const int ONE_MINUTE = 60000;
		private const int EMAIL_QUEUE_INTERVAL = ONE_MINUTE * 10;   // Ten minutes
		private const int EMAIL_REPROCESS_DAYS = 1;
		#endregion

		#region Members
		private static List<EmailTemplate> m_emailTemplateList;
		static object m_threadLock = new object();
		#endregion

		#region Delegates
		public delegate void ProcessQueueDelegate();
		#endregion

		#region Properties
		private static Timer QueueTimer { get; set; }
		public static bool ProcessingQueue { get; set; }
		public static string EmailTemplatePath { get; set; }
		public static bool EmailingEnabled { get; set; }

		public static List<EmailTemplate> EmailTemplateList
		{
			get
			{
				if (m_emailTemplateList == null)
					m_emailTemplateList = DB.EmailTemplate.ToList();

				return m_emailTemplateList;
			}

		}
		#endregion

		#region Query
		public static Email GetEmail(int emailId)
		{
			return DB
				.Email
				.Find(emailId);
		}
		#endregion

		#region Emails
		public static ServiceResponse DeliverEmail(EmailTemplateCode emailTemplateCode, string recipients, object templateData, List<int> attachmentList)
		{
			var edr = new EmailDeliveryRequest
			{
				Sender = Config.Emailing.FromAddress,
				Recipients = recipients,
				EmailTemplateCode = emailTemplateCode.ToString(),
				TemplateData = templateData,
				AttachmentList = attachmentList
			};

			return DeliverEmail(edr);
		}

		public static ServiceResponse DeliverSystemEmail(string recipients, string subject, string body, bool important)
		{
			var edr = new EmailDeliveryRequest
			{
				Sender = Config.Emailing.FromAddress,
				Recipients = recipients,
				Subject = subject,
				Body = body,
				Important = important
			};

			return DeliverEmail(edr);
		}

		public static ServiceResponse DeliverEmail(EmailDeliveryRequest edr)
		{
			ServiceResponse sr = new ServiceResponse();

			if (!EmailingEnabled)
				return sr;

		    sr = ValidateEmailDeliveryRequest(edr);
            if (!sr.Status)
                return sr;

            Email email = new Email();

			// Body
			if (edr.EmailTemplateCode != null)
			{
				ParsedEmailTemplate pet = ParseEmailTemplate(edr.EmailTemplateCode, edr.TemplateData);
				email.Body = pet.ParsedBody;
				email.Subject = pet.ParsedSubject;
			}
			else
			{
				email.Body = edr.Body;
				email.Subject = edr.Subject;
			}

			// Sender
			if (String.IsNullOrEmpty(edr.Sender))
				email.Sender = Config.Emailing.FromAddress;
			else
				email.Sender = edr.Sender;

			// Recipients
			email.Recipients = edr.Recipients;

			// Queue
			email.StatusCode = EmailStatusCode.QUEUED.ToString();

			// Attachments
			if (edr.AttachmentList != null)
			{
				foreach (int dataFileId in edr.AttachmentList)
				{
					var ea = new EmailAttach
					{
						Email = email,
						DataFileId = dataFileId
					};

					email.EmailAttachs.Add(ea);
				}
			}

			// Save email
			DB db = new DB();
			db.Email.Add(email);
			DB.SaveChanges(Identity.AuditLogin);

			// Send right away
			SendQueuedEmail(email, edr.Important);

			// Return data about email
			sr.ReturnValue = email.EmailId;

			return sr;
		}

		public static ParsedEmailTemplate ParseEmailTemplate(string emailTemplateCode, object templateData)
		{
			EmailTemplate emailTemplate = EmailTemplateList.FirstOrDefault(x => x.EmailTemplateCode == emailTemplateCode);

			StreamReader templateFile = new StreamReader(EmailTemplatePath + "\\" + emailTemplateCode + ".html");
			StreamReader layoutFile = new StreamReader(EmailTemplatePath + "\\" + emailTemplate.EmailLayoutCode + ".html");

			string templateHtml = templateFile.ReadToEnd();
			string layoutHtml = layoutFile.ReadToEnd();
			string emailHtml = layoutHtml.Replace("[EMAIL_BODY]", templateHtml);

			templateFile.Close();
			templateFile.Dispose();
			layoutFile.Close();
			layoutFile.Dispose();

			return new ParsedEmailTemplate
			{
				ParsedBody = MergeFields(emailHtml, templateData),
				ParsedSubject = MergeFields(emailTemplate.Subject, templateData),
			};
		}

		public static string MergeFields(string body, object fields)
		{
			string mergedString = body;

			var d = new Dictionary<string, string>();

			// Based on dynamic
			if (!fields.GetType().Name.StartsWith("Dictionary"))
			{
				foreach (PropertyInfo pi in fields.GetType().GetProperties())
					d[pi.Name] = pi.GetValue(fields).ToString();
			}
			else
			{
				// Based on dictionary
				d = (Dictionary<string, string>)fields;
			}

			// Replace based on dictionary
			foreach (string key in d.Keys)
			{
				string fieldValue = d[key];

				// Replace \n with BR
				fieldValue = fieldValue.Replace(Environment.NewLine, "<br />");

				mergedString = mergedString.Replace("[" + key + "]", fieldValue);
			}

			return mergedString;
		}

        private static List<EmailAttach> GetEmailAttachList(int emailId)
        {
            return DB
                    .EmailAttach
                    .Include(x => x.DataFile)
                    .Include(x => x.DataFile.DataContent)
                    .Where(x => x.EmailId == emailId)
                    .ToList();
        }
		#endregion

		#region Queue Management
		public static void StartQueueManager()
		{
            //LogService.Info(EventCode.CONFIG, "Queue Manager starting.");

            //QueueTimer = new Timer();
            //QueueTimer.Interval = EMAIL_QUEUE_INTERVAL;
            //QueueTimer.Elapsed += QueueTimer_Elapsed;
            //QueueTimer.Start();

            //LogService.Info(EventCode.CONFIG, "Queue Manager starting completed.");
		}

		static void QueueTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			TriggerProcessQueue();
		}

		public static void TriggerProcessQueue()
		{
			ProcessQueueDelegate starter = new ProcessQueueDelegate(ProcessQueue);
			starter.BeginInvoke(null, null);
		}

		public static void ProcessQueue()
		{
			lock (m_threadLock)
			{
				ProcessingQueue = true;

				// Re-process any unsuccesfull email that was created during today
				DateTime oneDay = DateTime.Now.AddDays(-EMAIL_REPROCESS_DAYS);
				string queuedStatus = EmailStatusCode.ERROR.ToString();

				List<Email> emailList = (new DB()).Email.Where(x => x.StatusCode == queuedStatus && x.CreateDate > oneDay).ToList();

				foreach (Email e in emailList)
					SendQueuedEmail(e);

				ProcessingQueue = false;
			}
		}

		public static void SendQueuedEmail(Email email, bool important = false)
		{
			if (!EmailingEnabled)
				return;

			MailMessage mm = null;
			SmtpClient smtp = null;
			DB db = new DB();

			try
			{
				// Get SMTP configuration
				smtp = GetSmtpClient();

				// Setup mail message
				mm = new MailMessage();
				mm.IsBodyHtml = true;
				mm.Subject = email.Subject;
				mm.From = new MailAddress(email.Sender);
				mm.Body = email.Body;
			    mm.Priority = important ? MailPriority.High : MailPriority.Normal;

				foreach (string emailAddress in email.Recipients.Split(';'))
					mm.To.Add(emailAddress.Trim());

                // Attachments
                foreach (EmailAttach emailAttach in GetEmailAttachList(email.EmailId))
                {
                    var ms = new MemoryStream(emailAttach.DataFile.DataContent.RawData);
                    string attachName = emailAttach.DataFile.FileName;
                    mm.Attachments.Add(new Attachment(ms, attachName));
                }

				// Create alternative view for HTML
				PreProcessHtmlMail(mm);

				smtp.Send(mm);
			}
			catch (Exception ex)
			{
				// Mark message as ERROR
				db.Email.Attach(email);
				email.SentDate = DateTime.Now;
				email.Log = ex.ToString();
				email.StatusCode = EmailStatusCode.ERROR.ToString();
				db.SaveChanges();

				// Re-throw exception to be catched by ELMAH
				throw new Exception("Error sending email.", ex);
			}
			finally
			{
				mm.Dispose();
				smtp.Dispose();
			}

			// Mark message as SENT
			email.SentDate = DateTime.Now;
			email.StatusCode = EmailStatusCode.SENT.ToString();
			db.Email.Attach(email);

			db.SaveChanges();
		}

		public static void PreProcessHtmlMail(MailMessage mail)
		{
			// If the mail is not in HTML
			if (mail.Body.IndexOf("<html>") == -1)
				return;

			// Set HTML Body
			mail.IsBodyHtml = true;

			// Embedd images as Base64
			ArrayList linkedResources = new ArrayList();
			XmlDocument xml = new XmlDocument();
			StringReader s = new StringReader(mail.Body);
			xml.Load(s);
			int imageCount = 0;

			foreach (XmlNode node in xml.SelectNodes("//img"))
			{
				imageCount++;
				XmlAttribute attr = node.Attributes["src"];
				String file = Config.Paths.Application + attr.Value.Replace("/", "\\");

				// Add resource
				LinkedResource resource = new LinkedResource(file);
				resource.ContentId = "image" + imageCount.ToString();
				resource.TransferEncoding = TransferEncoding.Base64;
				linkedResources.Add(resource);

				// Change Source
				attr.Value = "cid:" + resource.ContentId;
			}

			// Update Body
			mail.Body = xml.InnerXml;

			// Add HTML view plus all linked resources
			AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mail.Body, null, "text/html");

			foreach (LinkedResource resource in linkedResources)
				htmlView.LinkedResources.Add(resource);

			mail.AlternateViews.Add(htmlView);
		}

		private static SmtpClient GetSmtpClient()
		{
			SmtpClient smtpClient = new SmtpClient();

			smtpClient.Host = Config.Emailing.SmtpHost;
			smtpClient.Port = Config.Emailing.SmtpPort;
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.UseDefaultCredentials = Config.Emailing.SmtpUseDefaultCredentials;
			smtpClient.Credentials = new NetworkCredential(Config.Emailing.SmtpUsername, Config.Emailing.SmtpPassword);
			smtpClient.EnableSsl = Config.Emailing.SmtpEnableSSL;

			// To prevent the SMTP client to leave connections open to the SMTP server
			smtpClient.ServicePoint.MaxIdleTime = 0;

			return smtpClient;
		}

        private static ServiceResponse ValidateEmailDeliveryRequest(EmailDeliveryRequest request)
        {
            var sr = new ServiceResponse();

            if (string.IsNullOrEmpty(request.Sender))
                sr.AddError("The [From] field cannot be empty.");

            if (string.IsNullOrEmpty(request.Recipients))
                sr.AddError("The [To] field cannot be empty.");

            return sr;
        }
        #endregion

        #region Classes
        public class EmailDeliveryRequest
		{
			public string Sender { get; set; }
			public string Recipients { get; set; }
			public string Subject { get; set; }
			public string Body { get; set; }
			public List<int> AttachmentList { get; set; }
			public string EmailTemplateCode { get; set; }
			public object TemplateData { get; set; }
			public bool Important { get; set; }
        }

        public class ParsedEmailTemplate
		{
			public string ParsedBody { get; set; }
			public string ParsedSubject { get; set; }
		}
		#endregion
	}
}
