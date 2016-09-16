using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using Microsoft.SqlServer.Server;

namespace SQLUtilities
{
	[Serializable]
	[SqlUserDefinedAggregate(
		Format.UserDefined, //use clr serialization to serialize the intermediate result
		IsInvariantToNulls = true, //optimizer property
		IsInvariantToDuplicates = false, //optimizer property
		MaxByteSize = -1)] //maximum size in bytes of persisted value (2 GB)
	public class SqlConcat : IBinarySerialize
	{
		private StringBuilder m_concatenated;

		public void Init()
		{
			// Create new string
			m_concatenated = new StringBuilder();
		}

		public void Accumulate(SqlString newValue)
		{
			// Check null value (IsInvariantToNulls does not remove nulls)
			if (newValue.IsNull)
				return;

			// Add comma
			if (m_concatenated.Length > 0)
				m_concatenated.Append(", ");

			// Add value
			m_concatenated.Append(newValue);
		}

		public void Merge(SqlConcat concatenatedValues)
		{
			// Check null value
			if (concatenatedValues.m_concatenated == null)
				return;
				
			// Add comma
			if (m_concatenated.Length > 0)
				m_concatenated.Append(", ");

			// Add value
			m_concatenated.Append(concatenatedValues.m_concatenated.ToString());
		}

		public SqlString Terminate()
		{
			return new SqlString(m_concatenated.ToString());
		}

		// Serialization functions (required)
		public void Read(BinaryReader r)
		{
			m_concatenated = new StringBuilder(r.ReadString());
		}

		public void Write(BinaryWriter w)
		{
			w.Write(m_concatenated.ToString());
		}
	}
}
