using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AM.Utils
{
	public class Serialization
	{
		public static string SerializeObjectToBase64(object o)
		{
			// Serialize to a base 64 string
			byte[] bytes;
			long length = 0;
			MemoryStream ws = new MemoryStream();
			BinaryFormatter sf = new BinaryFormatter();
			sf.Serialize(ws, o);
			length = ws.Length;
			bytes = ws.GetBuffer();
			string encodedData = bytes.Length + ":" + Convert.ToBase64String(bytes, 0, bytes.Length, Base64FormattingOptions.None);
			return encodedData;
		}

		public static object DeserializeObjectFromBase64(string s)
		{
			// We need to know the exact length of the string - Base64 can sometimes pad us by a byte or two
			int p = s.IndexOf(':');
			int length = Convert.ToInt32(s.Substring(0, p));

			// Extract data from the base 64 string!
			byte[] memorydata = Convert.FromBase64String(s.Substring(p + 1));
			MemoryStream rs = new MemoryStream(memorydata, 0, length);
			BinaryFormatter sf = new BinaryFormatter();
			object o = sf.Deserialize(rs);
			return o;
		}
	}
}
