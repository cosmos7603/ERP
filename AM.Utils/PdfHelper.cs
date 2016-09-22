using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AM.Utils
{
	public class PdfHelper
	{
		public static byte[] ExportPDF(DataTable dt, PdfHelperOptions options)
		{
			// Preview
			DataView dv = dt.DefaultView;

			// ITextSharp document
			iTextSharp.text.Document doc;
			float totalWidth = 0;
			float pageWidth;

			// Calculate total width, and see if we should show portrait or landscape
			for (int j = 0; j < options.Columns.Count; j++)
				totalWidth = (float)(options.Columns[j].Width.ToDecimal());

			if (totalWidth > PageSize.LETTER.Width)
			{
				doc = new iTextSharp.text.Document(PageSize.LETTER.Rotate(), 18, 18, 24, 36); // Landscape
				pageWidth = PageSize.LETTER.Height;
			}
			else
			{
				doc = new iTextSharp.text.Document(PageSize.LETTER, 18, 18, 24, 36); // Portrait
				pageWidth = PageSize.LETTER.Width;
			}

			MemoryStream stmPDF = new MemoryStream();
			PdfWriter writer = PdfWriter.GetInstance(doc, stmPDF);
			writer.CloseStream = false;

			// Open document
			doc.Open();

			// Create table
			PdfPTable itable = new PdfPTable(options.Columns.Count);
			itable.WidthPercentage = 100f;	// Use 100% of the page space (minus margin defined)
			itable.TotalWidth = pageWidth;

			// Set column widths
			float[] widths = new float[options.Columns.Count];

			for (int j = 0; j < widths.Length; j++)
				widths[j] = (float)(options.Columns[j].Width.ToDecimal());

			itable.SetWidths(widths);

			// # of header rows repeated on every page (set only if there's data, or empty doc exception will raise)
			if (dv.Count > 0)
				itable.HeaderRows = 2;

			// Create Report Title row
			PdfPCell tdTitle = new PdfPCell();

			tdTitle.Colspan = options.Columns.Count;
			tdTitle.Padding = 6f;
			tdTitle.BorderWidth = 0f;
			tdTitle.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
			tdTitle.HorizontalAlignment = Element.ALIGN_CENTER;
			tdTitle.VerticalAlignment = Element.ALIGN_MIDDLE;

			iTextSharp.text.Font fontReport = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 18f, iTextSharp.text.Font.BOLD);
			tdTitle.Phrase = new Phrase(options.ReportTitle, fontReport);

			itable.AddCell(tdTitle);

			// Create Header row (from query columns info)
			if (options.ShowHeader)
			{
				iTextSharp.text.Font fontHeader = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 9f, iTextSharp.text.Font.BOLD);

				foreach (PdfHelperColumn dc in options.Columns)
				{
					PdfPCell tdHeader = new PdfPCell();

					tdHeader.Padding = 2f;
					tdHeader.BorderWidth = 0f;
					tdHeader.BorderWidthBottom = 1f;
					tdHeader.HorizontalAlignment = Element.ALIGN_CENTER;
					tdHeader.VerticalAlignment = Element.ALIGN_BOTTOM;

					tdHeader.Phrase = new Phrase(dc.Title, fontHeader);

					itable.AddCell(tdHeader);
				}
			}

			// Create data rows on the table
			iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 8f);

			foreach (DataRowView drv in dv)
			{
				// Get the datatable row
				DataRow dr = drv.Row;

				foreach (PdfHelperColumn dc in options.Columns)
				{
					PdfPCell td = new PdfPCell();

					td.Padding = 2f;
					td.BorderWidth = 0f;
					td.HorizontalAlignment = Element.ALIGN_LEFT;

					object cellValue = dr[dc.DataFieldName];

					switch (dc.Format.ToLower())
					{
						case "d":	// Date
							cellValue = cellValue.ToDateTime().Value.ToShortDateString();
							td.HorizontalAlignment = Element.ALIGN_CENTER;
							break;
						case "dt":	// Date-Time
							cellValue = cellValue.ToDateTime().Value.ToShortDateString() + " " + cellValue.ToDateTime().Value.ToShortTimeString();
							td.HorizontalAlignment = Element.ALIGN_CENTER;
							break;
						case "b":	// Bool
							cellValue = (cellValue == DBNull.Value ? "" : (cellValue.ToBool()) ? "YES" : "NO");
							td.HorizontalAlignment = Element.ALIGN_CENTER;
							break;
						case "c":	// Currency
							cellValue = cellValue.ToDecimal().ToString("c");
							td.HorizontalAlignment = Element.ALIGN_RIGHT;
							break;
						case "%":	// Percentage
							cellValue = cellValue.ToDecimal().ToString("N2") + "%";
							td.HorizontalAlignment = Element.ALIGN_RIGHT;
							break;
						case "n":	// Number
							cellValue = cellValue.ToInt().ToString("###,###,###");
							td.HorizontalAlignment = Element.ALIGN_RIGHT;
							break;
						default:
							if (dc.Alignment.ToLower() == "left")
								td.HorizontalAlignment = Element.ALIGN_LEFT;
							else if (dc.Alignment.ToLower() == "right")
								td.HorizontalAlignment = Element.ALIGN_RIGHT;
							else if (dc.Alignment.ToLower() == "center")
								td.HorizontalAlignment = Element.ALIGN_CENTER;

							break;
					}

					td.Phrase = new Phrase(cellValue.ToString(), font);

					itable.AddCell(td);
				}
			}

			doc.Add(itable);

			// Finish document
			doc.Close();
			stmPDF.Seek(0, System.IO.SeekOrigin.Begin);

			// Save in memory
			byte[] data = stmPDF.ToArray();

			stmPDF.Dispose();
			stmPDF.Close();

			return data;
		}
	}

	public class PdfHelperOptions
	{
		public string ReportTitle { get; set; }
		public bool ShowHeader { get; set; }
		public List<PdfHelperColumn> Columns { get; set; }

		public PdfHelperOptions()
		{
			Columns = new List<PdfHelperColumn>();
		}
	}

	public class PdfHelperColumn
	{
		public string Title { get; set; }
		public string DataFieldName { get; set; }
		public string Format { get; set; }
		public string Width { get; set; }
		public string Alignment { get; set; }

		public PdfHelperColumn()
		{
			Alignment = "Left";
			Width = "100";
			Format = "";
		}
	}
}
