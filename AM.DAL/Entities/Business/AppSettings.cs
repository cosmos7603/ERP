using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class AppSettings
    {
        public bool? DataCache { get; set; }
        public bool? SendMails { get; set; }
        public int? SendMailsInterval { get; set; }
        public string SendMailsTempFolder { get; set; }
        public string SmtpServer { get; set; }
        public int? SmtpServerPort { get; set; }
        public short? SmtpSendUsing { get; set; }
        public short? SmtpAuthenticate { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public int? SmtpTimeout { get; set; }
        public bool? SmtpUseSsl { get; set; }
        public string SharedGroupsEula { get; set; }
        public string DocImportFolder { get; set; }
        public bool? DocImportProc { get; set; }
        public int? DocImportProcInterval { get; set; }
        public string DocStoringClass { get; set; }
        public string DocFolder { get; set; }
        public bool? DocProc { get; set; }
        public int? DocProcInterval { get; set; }
        public string CuteUploadServer { get; set; }
        public bool? CuteUploadSsl { get; set; }
        public string CuteUploadLicense { get; set; }
        public string CuteUploadCab { get; set; }
        public bool SecReportingDb { get; set; }
        public int? CuteUploadPort { get; set; }
        public string CuteUploadClass { get; set; }
        public string SmtpFromAddress { get; set; }
        public bool? SessionData { get; set; }
        public bool? ValidateCc { get; set; }
        public bool? InterfaceCe { get; set; }
        public string PpoLogEmail { get; set; }
        public bool? EnableHelp { get; set; }
        public string SabreLogin { get; set; }
        public string SabrePassword { get; set; }
        public string SabrePcc { get; set; }
        public bool? ValidateMergedRes { get; set; }
        public string SabreCentralizedPccList { get; set; }
        public bool? HttpCompression { get; set; }
        public int? SmtpMaxIdleTime { get; set; }
        public int? SmtpConnectionLimit { get; set; }
        public string SmtpClient { get; set; }
        public string SmtpLogFolder { get; set; }
        public string CeEmailExportEmails { get; set; }
        public string CeExportFtpAddress { get; set; }
        public byte? CeExportFtpPort { get; set; }
        public string CeExportFtpUser { get; set; }
        public string CeExportFtpPassword { get; set; }
        public string CeSegExportEmails { get; set; }
        public string VirtuosoFtpAddress { get; set; }
        public int? VirtuosoFtpPort { get; set; }
        public string VirtuosoFtpUsername { get; set; }
        public string VirtuosoFtpPassword { get; set; }
        public bool? LogPagesAndCommands { get; set; }
        public string VirtuosoExportEmails { get; set; }
        public string ReportServerUrl { get; set; }
        public string ReportServerPath { get; set; }
        public string ReportServerPassword { get; set; }
        public string ReportServerUsername { get; set; }
        public bool? CheckConcurrency { get; set; }
        public bool PopCheck { get; set; }
        public int? PopCheckInterval { get; set; }
        public string ServiceFeesName { get; set; }
        public string MainAlertText { get; set; }
        public DateTime? MainAlertDate { get; set; }
        public bool? Silverpop { get; set; }
        public int? SilverpopExportDays { get; set; }
        public bool? ProfilingEnabled { get; set; }
        public string ApplicationName { get; set; }
        public bool? TlsEnabled { get; set; }
        public string CeSegExportFtpAddress { get; set; }
        public int? CeSegExportFtpPort { get; set; }
        public string CeSegExportFtpUser { get; set; }
        public string CeSegExportFtpPassword { get; set; }
        public string DailyNotificationEmail { get; set; }
        public string AuUrl { get; set; }
        public string AuEncryptionKey { get; set; }
        public string TanIdCdUs { get; set; }
        public string TanIdCdCa { get; set; }
        public string CruiseproUrl { get; set; }
        public string VirtuosoFtpAddress2 { get; set; }
        public int? VirtuosoFtpPort2 { get; set; }
        public string VirtuosoFtpUsername2 { get; set; }
        public string VirtuosoFtpPassword2 { get; set; }
        public string FeeCheckCadPayeeName { get; set; }
        public string FeeCheckCadAddress { get; set; }
        public string FeeCheckCadCity { get; set; }
        public string FeeCheckCadStateName { get; set; }
        public string FeeCheckCadZip { get; set; }
        public bool? CorpnetProfilerClient { get; set; }
        public bool? TlsCruiseImportEnabled { get; set; }
        public bool? CheckSessionId { get; set; }
        public bool? CheckLockResGrp { get; set; }
        public decimal? TlsLafHour { get; set; }
        public decimal? TlsCruiseImportHour { get; set; }
        public string PpoFeedUrl { get; set; }
        public decimal? PpoImportHour { get; set; }
        public bool? PpoImportEnabled { get; set; }
        public bool? CheckSessionTimestamp { get; set; }
        public bool? EnableHelpPopup { get; set; }
        public bool? BrowserRestriction { get; set; }
        public DateTime? BrowserRestrictionDate { get; set; }
        public bool? DisableIe7Compat { get; set; }
        public string HelpPopupUrl { get; set; }
        public string GroupReconciliationEmail { get; set; }
    }
}
