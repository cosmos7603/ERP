using AM.DAL;

namespace AM.Services.Models
{
	public static class Config
	{
		public static ApplicationConfig Application { get; set; }
		public static BusinessConfig Business { get; set; }
		public static EncryptionConfig Encryption { get; set; }
		public static DefaultsConfig Defaults { get; set; }
		public static DatabaseConfig Database { get; set; }
		public static EmailingConfig Emailing { get; set; }
		public static SecurityConfig Security { get; set; }
		public static SupportConfig Support { get; set; }
		public static PathsConfig Paths { get; set; }
		public static SSOConfig SSO { get; set; }

        static Config()
		{
			Application = new ApplicationConfig();
			Business = new BusinessConfig();
			Encryption = new EncryptionConfig();
			Defaults = new DefaultsConfig();
			Database = new DatabaseConfig();
			Emailing = new EmailingConfig();
			Security = new SecurityConfig();
			Support = new SupportConfig();
			Paths = new PathsConfig();
            SSO = new SSOConfig();
		}
	}

	public class BusinessConfig
	{
	}

	public class DefaultsConfig
	{
		public string CountryCode { get; set; }
		public string TimeZoneName { get; set; }
		public string TimeZoneCode { get; set; }
	}

	public class EncryptionConfig
	{
		public string EncryptionKey { get { return "TODO"; } }
	}

	public class PathsConfig
	{
		public string EmailTemplates { get; set; }
		public string Application { get; set; }
		public string Temp { get; set; }
	}

    public class SSOConfig
    {
        public string LegacySsoUrl { get; set; }
        public string CurrentSsoUrl { get; set; }
    }

    public class DatabaseConfig
	{
		public string ConnectionString
		{
			get { return DB.DatabaseConnectionString; }
			set { DB.DatabaseConnectionString = value; }
		}
	}

	public class ApplicationConfig
	{
		public string AppUrl { get; set; }
		public string AppName { get; set; }
		public string EnvironmentCode { get; set; }
		public string ProjectCode { get; set; }
		public string ThemeSkin { get; set; }
		public string PrimeroSkin { get; set; }
		public int MaxSearchResults { get; set; }
	}

	public class SupportConfig
	{
		public string LogLevel { get; set; }
		public bool EnableLogging { get; set; }
		public bool EnableProfiling { get; set; }
		public string DaemonMailList { get; set; }
		public string SupportEmail { get; set; }
	}

	public class EmailingConfig
	{
		public string FromAddress { get; set; }
		public int QueueInterval { get; set; }
		public string SmtpHost { get; set; }
		public int SmtpPort { get; set; }
		public string SmtpUsername { get; set; }
		public string SmtpPassword { get; set; }
		public bool SmtpUseDefaultCredentials { get; set; }
		public bool SmtpEnableSSL { get; set; }
		public string EmailTestAddress { get; set; }
	}

	public class SecurityConfig
	{
		public int UserRegistrationLinkExpirationHs { get; set; }
		public int PasswordLinkExpirationHs { get; set; }
		public string UserRegistrationLink { get; set; }
		public string PasswordResetLink { get; set; }
		public string LoginUrl { get; set; }
		public string LogoutUrl { get; set; }
		public int MaxLoginAttempts { get; set; }
		public int UserLockingTimeout { get; set; }
		public int UserSessionTimeout { get; set; }
		public int UserSessionWarning { get; set; }
		public int PasswordRecoveryQuestions { get; set; }
		public int PasswordExpirationDays { get; set; }
		public bool EnablePasswordStrength { get; set; }
		public bool EnablePasswrodTracking { get; set; }
		public bool EnableAccountLocking { get; set; }
		public bool EnablePasswordExpiration { get; set; }
	}
}
