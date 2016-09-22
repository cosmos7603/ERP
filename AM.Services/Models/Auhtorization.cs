namespace AM.Services
{
	public enum ResourceCode
	{
		MyProfile,
		AdminUserSetup,
		AdminRoleSetup,
		AdminGlobalAppSettings
	}

	public enum PrivilegeCode
	{
		AdminUserSetupAccess,
        AdminUserSetupAdd,
		AdminUserSetupEdit,
		AdminUserSetupDelete,
		AdminUserSetupReset,

		AdminRoleSetupAccess,
		AdminRoleSetupAdd,
		AdminRoleSetupEdit,
		AdminRoleSetupDelete,

		UtilitiesGlobalAppSettingsAccess,
		UtilitiesGlobalAppSettingsUpdate
	}
}