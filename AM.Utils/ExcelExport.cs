using System;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using System.Xml.Xsl;
using System.Threading;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace AM.Utils
{
	public class ExcelExport
	{
		#region XLSX
		public static byte[] ExportXLSX(DataView dvExport)
		{
			return ExportXLSX(dvExport);
		}

		public static byte[] ExportXLSX(DataTable dtExport)
		{
			if (dtExport.DataSet == null)
			{
				DataSet ds = new DataSet();
				ds.Tables.Add(dtExport);
			}

			return ExportXLSX(dtExport.DataSet);
		}

		public static byte[] ExportXLSX(DataSet dsExport)
		{
			// Create XLSX using the new EPPlus library
			using (var package = new ExcelPackage())
			{
				foreach (DataTable dtExport in dsExport.Tables)
				{
					string worksheetName = dtExport.TableName;

					ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(worksheetName);

					worksheet.Cells["A1"].LoadFromDataTable(dtExport, true);
					worksheet.Cells[worksheet.Dimension.Address].AutoFilter = false;

					int rowCount = dtExport.Rows.Count;

					if (rowCount > 0)
					{
						// Format date columns (only if there are rows, otherwise an error will raise)
						var dateColumns =
							from DataColumn dc in dtExport.Columns
							where dc.DataType == typeof(DateTime)
							select dc.Ordinal + 1;

						foreach (var dc in dateColumns)
							worksheet.Cells[2, dc, rowCount + 1, dc].Style.Numberformat.Format = "dd mmm yyyy";
					}
				}

				// Save workbook
				package.Save();

				byte[] data = new byte[package.Stream.Length];
				package.Stream.Position = 0;
				package.Stream.Read(data, 0, (int)package.Stream.Length);

				return data;
			}
		}
		#endregion
	}
}