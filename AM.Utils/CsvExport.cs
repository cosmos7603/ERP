using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace AM.Utils
{
	public class CsvExport
	{
		#region Members
		private string m_textDelimiter = "\"";
		private string m_separator = ",";
		private bool m_excelFormat = false;
		#endregion

		#region Properties
		public string Separator
		{
			get { return m_separator; }
			set { m_separator = value; }
		}

		public string TextDelimiter
		{
			get { return m_textDelimiter; }
			set { m_textDelimiter = value; }
		}

		public bool ExcelFormat
		{
			get { return m_excelFormat; }
			set { m_excelFormat = value; }
		}
		#endregion

		public byte[] ExportToByteArray(DataTable dtSource)
		{
			return ExportToByteArray(dtSource, false);
		}
		
		public byte[] ExportToByteArray(DataTable dtSource, bool addHeaderRow)
		{
			string[] columns = new string[dtSource.Columns.Count];

			for (int i = 0; i < dtSource.Columns.Count; i++)
				columns[i] = dtSource.Columns[i].ColumnName;

			return ExportToByteArray(dtSource, columns, addHeaderRow);
		}

		public byte[] ExportToByteArray(DataTable dtSource, string[] columns, bool addHeaderRow)
		{
			StringBuilder csv = new StringBuilder();

			// Add header row
			if (addHeaderRow)
			{
				string headerRow = "";

				foreach (string columnName in columns)
				{
					if (headerRow != "")
						headerRow += this.Separator;

					headerRow += this.TextDelimiter + columnName + this.TextDelimiter;
				}

				csv.Append(headerRow + Environment.NewLine);
			}

			// Add data
			foreach (DataRow dr in dtSource.Rows)
			{
				csv.Append(ProcessRow(dr, columns));
			}

			return UTF8Encoding.UTF8.GetBytes(csv.ToString());
		}

		private string ProcessRow(DataRow dr, string[] columns)
		{
			StringBuilder row = new StringBuilder();

			foreach (string columnName in columns)
			{
				string chunk = "";

				chunk += this.TextDelimiter;

				if (dr.Table.Columns[columnName].DataType == typeof(DateTime))
				{
					if (dr[columnName] == DBNull.Value || dr[columnName].ToDateTime() == DateTime.MinValue)
						chunk += string.Empty;
					else
						chunk += Convert.ToDateTime(dr[columnName]).ToShortDateString();
				}
				else
				{
					if (this.TextDelimiter != "")
						chunk += dr[columnName].ToString().Replace(this.TextDelimiter, "");
					else
						chunk += dr[columnName].ToString();
				}

				chunk += this.TextDelimiter;

				if (this.ExcelFormat)
					chunk = "=" + chunk;

				if (row.ToString() != "")
					row.Append(this.Separator);

				row.Append(chunk);
			}

			row.Append(Environment.NewLine);
			return row.ToString();
		}
	}
}
