using AM.Services;
using AM.Services.Support;

namespace AM.WebSite
{
	public class EncryptionConfig
	{
		public static void RegisterEncrytion()
		{
			LogService.Info(EventCode.APP_START, "Registering encryption.");

			// TO-DO: Synch with ORG!
			//if (Config.Application.EnvironmentCode == EnvironmentCode.PRD)
			//	EncryptionService.Key = Config.Encryption.EncryptionKey;
			//else
				EncryptionService.Key = "";

			LogService.Info(EventCode.APP_START, "Registering encryption completed.");
		}
	}
}