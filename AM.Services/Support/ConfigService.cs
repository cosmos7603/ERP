using System.Collections.Generic;
using System.Linq;
using AM.DAL;
using AM.Utils;
using AM.Services.Models;

namespace AM.Services.Support
{
	public class ConfigService : ServiceBase
	{
		#region Query
		public static List<ConfigGroup> GetConfigGroupList()
		{
			return DB
				.ConfigGroup
				.AsNoTracking()
				.ToList();
		}

		public static List<ConfigSetting> GetConfigSettingList()
		{
			return DB
				.ConfigSetting
				.AsNoTracking()
				.ToList();
		}
	
		public static List<ConfigSetting> GetEditableSettingsList()
		{
			return DB
				.ConfigSetting
				.AsNoTracking()
				.Where(x => x.AllowAccessControl)
				.ToList();
		}
		#endregion

		#region Config
		public static void LoadConfig()
		{
			LogService.Info(EventCode.CONFIG, "Loading config.");

			// Load system params
			var configSettingList = GetConfigSettingList();

			// Iterate over all config settings
			foreach (ConfigSetting cs in configSettingList)
			{
				// Get static config type
				var type = typeof(Config);

				// Loop through each config group on static config type
				foreach (var groupProperty in type.GetProperties())
				{
					// If this is the group
					if (groupProperty.Name != cs.ConfigGroupCode) continue;

					// Get instance
					var groupInstance = groupProperty.GetValue(null);

					// Iterate over all config group properties
					foreach (var settingProperty in groupInstance.GetType().GetProperties().Where(settingProperty => settingProperty.Name == cs.Key))
					{
						switch (settingProperty.PropertyType.Name.ToLower())
						{
							case "string":
								settingProperty.SetValue(groupInstance, cs.Value, null);
								break;
							case "int32":
								settingProperty.SetValue(groupInstance, cs.Value.ToInt(), null);
								break;
							case "datetime":
								settingProperty.SetValue(groupInstance, cs.Value.ToDateTime(), null);
								break;
							case "boolean":
								settingProperty.SetValue(groupInstance, cs.Value.ToBool(), null);
								break;
						}
					}
				}
			}

			// Push to other layers
			PushConfig();

			LogService.Info(EventCode.CONFIG, "Loading config completed.");
		}

		public static void PushConfig()
		{
			// Pushes the required configs to other layers, libraries, etc.
		}
		public static void SetupRefreshConfigTask()
		{
			// Refresh every 5 minutes
			PeriodicTaskFactory.Start(() =>
			{
				LogService.Debug(EventCode.JOB, "Refresing configs.");
				LoadConfig();
				LogService.Debug(EventCode.JOB, "Finished refreshing configs");

			}, 1000 * 60 * 5);
		}
		#endregion

		#region Setup
		public static ServiceResponse UpdateConfigSettings(List<ConfigSetting> configSettings, string auditLogin)
		{
			var sr = new ServiceResponse();

			if (configSettings == null)
				return sr;

			foreach (var configSettingSetup in configSettings)
			{
				sr = ValidateConfigSetup(configSettingSetup);

				if (!sr.Status)
					return sr;

				// Existing SystemParam
				var dbConfigSetting = DB.ConfigSetting.FirstOrDefault(x => x.Key == configSettingSetup.Key);
				dbConfigSetting.Value = configSettingSetup.Value;
			}

			// Save in DB
			DB.SaveChanges(auditLogin);

			// Refresh Config
			LoadConfig();

			return sr;
		}
		#endregion

		#region Validations
		public static ServiceResponse ValidateConfigSetup(ConfigSetting configSetting)
		{
			ServiceResponse serviceResponse = new ServiceResponse();

			if (string.IsNullOrEmpty(configSetting.Value))
				serviceResponse.AddError("The [Parameter Value] field cannot be empty.");

			return serviceResponse;
		}
		#endregion
	}
}
