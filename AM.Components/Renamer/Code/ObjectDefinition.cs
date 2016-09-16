using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Corpnet.SQLDeploy
{
	public class RenameDefinition
	{
		public string Old { get; set; }
		public string New { get; set; }
	}
}
