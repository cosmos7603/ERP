using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.DAL
{
	public partial class DataContent
	{
		[Key]
		public int DataContentId { get; set; }
		public byte[] RawData { get; set; }

		public DataContent()
		{
		}

		public DataContent(byte[] rawData)
		{
			RawData = rawData;
		}
	}
}
