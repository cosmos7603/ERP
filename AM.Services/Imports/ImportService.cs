//using AM.DAL;
//using AM.Services.Models;
//using AM.Services.Support;
//using AM.Utils;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;

//namespace AM.Services.Imports
//{
//	public class ImportService : ServiceBase
//    {
//		#region Import
//		public static Import GetImport(int importId)
//		{
//			return DB
//				.Import
//				.AsNoTracking()
//				.Include(x => x.Errors)
//				.Include(x => x.ImportUser)
//				.Include(x => x.ImportDataFile)
//				.Where(x => x.ImportId == importId)
//				.FirstOrDefault();
//		}

//		public static List<Import> GetImports(string importTypeCode, PagerParameters p)
//		{
//			var query = DB
//				.Import.AsNoTracking()
//				.Include(x => x.Errors)
//				.Include(x => x.ImportUser)
//				.Include(x => x.ImportDataFile)
//				.Where(x => x.ImportTypeCode == importTypeCode.ToString());

//			p.RowCount = query.Count();

//			return query.OrderBy(p.SortField + " " + p.SortDirection)
//				.Skip((p.PageIndex - 1) * p.PageSize)
//				.Take(p.PageSize)
//				.ToList();
//		}

//		public static ServiceResponse Save(Import import, string auditLogin)
//        {
//            var sr = new ServiceResponse();

//            // New Part
//            if (import.ImportId == 0)
//            {
//                DB.Import.Add(import);
//                DB.SaveChanges(auditLogin);

//                sr.ReturnId = import.ImportId;

//                return sr;
//            }

//            // Existing Part
//            var dbImport = DB.Import.Find(import.ImportId);

//            dbImport.FailedRows = import.FailedRows;
//            dbImport.ImportDataFileId = import.ImportDataFileId;
//            dbImport.ImportDate = import.ImportDate;
//            dbImport.ImportTypeCode = import.ImportTypeCode;
//            dbImport.ImportUserId = import.ImportUserId;
//            dbImport.ImportedRows = import.ImportedRows;
//            dbImport.TotalRows = import.TotalRows;

//            sr.ReturnId = import.ImportId;

//            // Save in DB
//            DB.SaveChanges(auditLogin);

//            return sr;
//        }

//        public static void SaveImportError(int importId, string errorDescription, string auditLogin)
//        {
//            DB.ImportError.Add(new ImportError()
//            {
//                ImportId = importId,
//                ErrorDescription = errorDescription
//            });

//            DB.SaveChanges(auditLogin);
//        }
//		#endregion Import

//		#region Emailing
//		public static void DeliverImportEmail(int importId, string importName)
//		{
//			var import = GetImport(importId);

//			// Deliver email
//			LogService.Info(EventCode.IMPORT, importName + ". Emailing.");

//			var ok = (import.FailedRows == 0 && string.IsNullOrEmpty(import.ErrorText));

//			EmailingService.DeliverSystemEmail(
//				Config.Support.SupportEmail,
//				"Import " + importName + " - " + (ok ? "OK" : "ERRORS"),
//				(ok ?
//					"File was succesfully procesed, and " + import.ImportedRows.ToString() + " were imported" :
//					 "File was processed, but " + import.FailedRows.ToString() + " errors were found. Please review errors below:<br /><br />" + import.ErrorText),
//				false,
//				"system"
//				);
//		}
//		#endregion
//	}
//}