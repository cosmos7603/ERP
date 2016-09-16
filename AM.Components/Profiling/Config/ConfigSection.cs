using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Corpnet.Profiling.Config
{
	public class ProfilingConfigurationSection : ConfigurationSection
	{
		[ConfigurationProperty("connectionStringName", IsRequired = true)]
		public string ConnectionStringName
		{
			get
			{
				return this["connectionStringName"] as string;
			}
		}

		[ConfigurationProperty("logFileName", IsRequired = true)]
		public string LogFileName
		{
			get
			{
				return this["logFileName"] as string;
			}
		}
	}
}
