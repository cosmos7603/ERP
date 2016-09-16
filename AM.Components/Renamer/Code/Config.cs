using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Corpnet.SQLDeploy
{
	public class Config
	{
		public static string BasePath
		{
			get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); }
		}

		public static string TemplatesPath
		{
			get { return BasePath + "\\Scripts"; }
		}
	}
}
