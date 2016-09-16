using System;
using System.Reflection;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corpnet.SQLDeploy
{
	public static class Defaults
	{
		public const string DefaultProceduresFolder = "Stored Procedures";
		public const string DefaultFunctionsFolder = "Functions";
		public const string DefaultViewsFolder = "Views";
		public const string DefaultMigrationFolder = "Migration";
	}
}
