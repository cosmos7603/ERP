using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using NPOI.OpenXml4Net.OPC;
using NPOI.XWPF.Extractor;
using AM.DAL;

namespace AM.Services.Support
{
	public class DataFileService : ServiceBase
	{
		#region Lists & Fetchs
		public static DataFile Fetch(int dataFileId)
		{
			return DB
				.DataFile
				.Where(x => x.DataFileId == dataFileId)
				.FirstOrDefault();
		}

		public static DataFile LoadData(int dataFileId)
		{
			return DB
				.DataFile
				.Include(x => x.DataContent)
				.AsNoTracking()
				.Where(x => x.DataFileId == dataFileId)
				.FirstOrDefault();
		}
		#endregion

		#region Setup
		public static ServiceResponse AddDataFile(DataFile dataFile, string auditLogin)
		{
			ServiceResponse sr = ValidateDataFileSetup(dataFile);

			if (!sr.Status)
				return sr;

			DB.DataFile.Add(dataFile);
			DB.SaveChanges(auditLogin);

			sr.ReturnValue = dataFile.DataFileId;

			return sr;
		}

		public static ServiceResponse Import(string fileName, string auditLogin)
		{
			var sr = new ServiceResponse();

			DataContent dataContent = new DataContent();
			DataFile dataFile = new DataFile();

			try
			{
				// Add a file to the database from disk
				dataContent.RawData = File.ReadAllBytes(fileName);

				dataFile.FileName = System.IO.Path.GetFileName(fileName);
				dataFile.Extension = System.IO.Path.GetExtension(fileName).ToUpper().Replace(".", "");
				dataFile.SourceCode = "DSK";
				dataFile.DataContent = dataContent;

				DB.DataFile.Add(dataFile);
				DB.SaveChanges(auditLogin);

				sr.ReturnValue = dataFile.DataFileId;
				sr.ReturnName = dataFile.FileName;
			}
			catch (Exception ex)
			{
				LogService.Error(EventCode.IMPORT, ex);

				sr.AddError("File not found.");
			}

			return sr;
		}
		#endregion

		#region Text Extraction
		public static string ExtractText(int dataFieldId)
		{
			var dataFile = LoadData(dataFieldId);

			switch (dataFile.Extension)
			{
				case "PDF":
					return ExtractTextFromPdf(dataFile.DataContent.RawData);
				case "DOCX":
					return ExtractTextFromDocx(dataFile.DataContent.RawData);
			}

			return string.Empty;
		}

		public static string ExtractTextFromPdf(byte[] file)
		{
			using (var reader = new PdfReader(file))
			{
				var text = new StringBuilder();
				for (var i = 1; i <= reader.NumberOfPages; i++)
				{
					text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
				}
				return text.ToString();
			}
		}

		public static string ExtractTextFromDocx(byte[] file)
		{
			return new XWPFWordExtractor(OPCPackage.Open(new MemoryStream(file))).Text;
		}
		#endregion

		#region Validations
		public static ServiceResponse ValidateDataFileSetup(DataFile dataFile)
		{
			ServiceResponse serviceResponse = new ServiceResponse();

			if (String.IsNullOrEmpty(dataFile.FileName))
				serviceResponse.AddError("The [File Name] field cannot be empty.");

			if (String.IsNullOrEmpty(dataFile.Extension))
				serviceResponse.AddError("The [Extension] field cannot be empty.");

			if (String.IsNullOrEmpty(dataFile.SourceCode))
				serviceResponse.AddError("The [Source Code] field cannot be empty.");

			return serviceResponse;
		}
		#endregion
	}
}
