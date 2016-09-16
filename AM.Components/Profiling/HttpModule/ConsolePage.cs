using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.SessionState;
using System.Configuration;
using System.Data;
using Corpnet.Profiling.Storage;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using Corpnet.Profiling.Helpers;
using Corpnet.Profiling.Log;
using Corpnet.Profiling.Schedule;

namespace Corpnet.Profiling.HttpModule
{
	internal sealed class ConsolePage : PageBase
	{
		protected override void OnLoad(EventArgs e)
		{
			this.Title = string.Format("Corpnet.Profiling Admin Console on {0}", Environment.MachineName);

			HandleActions();

			base.OnLoad(e);
		}

		protected override void RenderHead(HtmlTextWriter writer)
		{
			if (writer == null)
				throw new ArgumentNullException("writer");

			base.RenderHead(writer);
		}

		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (writer == null)
				throw new ArgumentNullException("writer");

			RenderTitle(writer);
			RenderStatus(writer);
			RenderLinks(writer);

			writer.Write("<br /><br /><table><tr><td style='width: 600px'>");

			RenderSettings(writer);

			writer.Write("</td><td style='padding-left: 20px'>");

			RenderActions(writer);

			writer.Write("</td></tr></table>");

			RenderLog(writer);

			base.RenderContents(writer);
		}

		private void RenderTitle(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, "page-title");
			writer.RenderBeginTag(HtmlTextWriterTag.P);
			writer.Write("Corpnet.Profiling Console on ");
			Server.HtmlEncode(Environment.MachineName, writer);
			writer.RenderEndTag();
			writer.WriteLine();
		}

		private void RenderStatus(HtmlTextWriter writer)
		{
			writer.Write("<span class='status-title'>Status:</span>&nbsp;");

			if (!Settings.Enabled)
			{
				writer.Write("<span class='status-disabled'>DISABLED</span>");
			}
			else
			{
				if (Settings.Running)
				{
					writer.Write("<span class='status-running'>RUNNING</span>");
				}
				else
				{
					writer.Write("<span class='status-stopped'>STOPPED</span>");
				}
			}

			writer.Write("&nbsp;&nbsp;&nbsp;&nbsp;");
		}

		private void RenderSettings(HtmlTextWriter writer)
		{
			// Console table
			Table table = new Table();

			if (Settings.Enabled)
				table.CssClass = "settings-table";
			else
				table.CssClass = "settings-table-disabled";

			// Fixed layout
			table.Style.Add("table-layout", "fixed");

			// Get storage queue data
			long storageLength = 0;
			long storageSize = 0;

			if (Settings.Storage != null)
			{
				storageLength = ((SqlServerStorage)Settings.Storage).StorageQueueLength;
				storageSize = ((SqlServerStorage)Settings.Storage).StorageQueueSize / 1024;
			}

			// Render settings
			RenderSetting(table, "Version", "<b>" + Settings.Version + "</b>");
			RenderSetting(table, "Enabled", Settings.Enabled.ToString());
			RenderSetting(table, "Connection String", Settings.ProfilingConfigurationSection.ConnectionStringName);
			RenderSetting(table, "Log File Name", !Logger.ValidateLog() ? "<span class='red'>LOG ERROR. Please review.</span>" : Settings.LogFileName);
			RenderSetting(table, "Active User Locator", Utils.IsNull(Settings.ActiveUserLocator, "No Active User Locator"));
			RenderSetting(table, "Run On Startup", Settings.RunOnStartup.ToString());
			RenderSetting(table, "Run Schedule (cron)", Utils.IsNull(Settings.RunScheduleCron, "No schedule cron defined"));
			RenderSetting(table, "Run Duration (mins)", Utils.IsNull(Settings.RunDurationMins.ToString(), "No run duration set. Schedule is off."));
			RenderSetting(table, "Next Run Date", Settings.InvalidSchedule ? "<span class='red'>Schedule setup error. Please review.</span>" : (Scheduler.NextRunDate.HasValue ? Scheduler.NextRunDate.ToString() : "No schedule defined"));
			RenderSetting(table, "Next Stop Date", Scheduler.NextStopDate.HasValue ? Scheduler.NextStopDate.ToString() : "-");
			RenderSetting(table, "Storage Queue Interval (ms)", Settings.StorageQueueInterval.ToString());
			RenderSetting(table, "Storage Queue Length", storageLength.ToString() + " / " + storageSize.ToString("###,###,### KB"));
			RenderSetting(table, "Storage Status", (MiniProfiler.StorageSuspended ? "<b><span class='red'>SUSPENDED</span></b>" : "Active"));
			RenderSetting(table, "Storage Resume (mins)", Utils.IsNull(Settings.StorageResumeMins.ToString(), "No set."));
			RenderSetting(table, "Profiling Filters", Utils.IsNull(GetProfilingFilters(), "No fitlers"));

			table.RenderControl(writer);
		}

		private void RenderActions(HtmlTextWriter writer)
		{
			writer.Write("<b>Actions:</b><br /><br />");
			writer.Write("<ul>");
			writer.Write("<li><a href='Profiling.axd?refresh-settings'>Reload settings from database</a></li><br />");
			writer.Write("<li><a href='Profiling.axd?enable'>Enable</a>&nbsp;/&nbsp;<a href='Profiling.axd?disable'>Disable</a></li><br />");
			writer.Write("<li><a href='Profiling.axd?clear-log'>Clear Log</a></li><br />");
			writer.Write("</ul>");
		}

		private string GetProfilingFilters()
		{
			string result = "";

			foreach (DataRow dr in Settings.ProfilingFilter.Rows)
			{
				result += dr["UrlString"].ToString() + "<br />";
			}

			if (result != "")
				result += "<br />";

			return result;
		}

		private void RenderLinks(HtmlTextWriter writer)
		{
			if (!Settings.Enabled)
				return;

			writer.Write("<span class='status-title'>(</span>");

			// Console table
			if (Settings.Running)
				RenderLink(writer, "STOP", "stop");
			else
				RenderLink(writer, "START", "start");

			writer.Write("<span class='status-title'>)</span>");
		}

		private void RenderLog(HtmlTextWriter writer)
		{
			if (String.IsNullOrEmpty(Settings.LogFileName))
				return;

			writer.Write("<br /><br /><b>Log Tail:</b><br />");

			int tailCount = 50;
			string[] tail = Logger.Tail(tailCount);

			tailCount = tail.Length;

			writer.Write("<div style='width: 100%; height: 250px; overflow-y: auto; border: solid 1px silver'>[...]<br />");

			for (int index = 0; index < tail.Length; index++)
				writer.Write(tail[index] + "<br />");

			writer.Write("</div>");
		}

		private void RenderLink(HtmlTextWriter writer, string title, string queryString)
		{
			writer.Write(String.Format("<a class='action-link' href='Profiling.axd?{0}'>{1}</a>", queryString, title));
		}

		private void RenderSetting(Table table, string settingName, string settingValue)
		{
			TableRow row = new TableRow();
			TableCell nameCell = new TableCell();
			TableCell valueCell = new TableCell();

			nameCell.Style.Add("width", "200px");
			nameCell.Text = settingName;
			nameCell.CssClass = "settings-table field";

			if (!Settings.Enabled)
				nameCell.CssClass += " disabled";

			valueCell.Text = settingValue;
			valueCell.CssClass = "settings-table";
			valueCell.Style.Add("word-wrap", "break-word");

			if (!Settings.Enabled)
				valueCell.CssClass += " disabled";

			row.CssClass = "settings-table";
			row.Cells.Add(nameCell);
			row.Cells.Add(valueCell);
				
			table.Rows.Add(row);
		}

		private void HandleActions()
		{
			string actionName = Request.QueryString.ToString().ToLower();

			if (actionName == "start")
			{
				Settings.Running = true;
				MiniProfiler.ResumeStorage();
			}
			else if (actionName == "stop")
			{
				Settings.Running = false;
			}
			else if (actionName == "refresh-settings")
			{
				Settings.LoadProfilingConfig();
				MiniProfiler.Prepare();
			}
			else if (actionName == "disable")
			{
				Settings.UpdateEnableStatus(false);
			}
			else if (actionName == "enable")
			{
				Settings.UpdateEnableStatus(true);
			}
			else if (actionName == "clear-log")
			{
				Logger.ClearLog();
			}

			// Refresh and remove query string
			if (actionName != "")
				Response.Redirect(Request.Path);
		}
	}
}
