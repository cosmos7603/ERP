using System.IO;
using System.Text;
using AM.Services.Models;
using AM.Services.Support;

namespace AM.Services
{
	public class FileService
	{
		public static void InitializeTempFolder()
		{
			var tempPath = Config.Paths.Temp;

			// Create if not exists
			if (!Directory.Exists(tempPath))
				Directory.CreateDirectory(tempPath);

			// Delete all contents
			var di = new DirectoryInfo(tempPath);

			var rgFiles = di.GetFiles("*.*");

			foreach (FileInfo fi in rgFiles)
			{
				File.Delete(fi.FullName);
			}
		}
		
		public static string SaveTempFile(string fileName, byte[] fileData, bool encrypt)
		{
			var fullName = Config.Paths.Temp + "\\" + fileName;

			// Encrypt
			if (encrypt)
				fileData = Encoding.Default.GetBytes(EncryptionService.Encrypt(fileData));

			// Write file
			File.WriteAllBytes(fullName, fileData);

			return fullName;
        }

		public static void SaveTempFile(string fileName, string contents, bool encrypt)
		{
			// Encrypt
			if (encrypt)
			{
				byte[] fileData = Encoding.Default.GetBytes(contents);
				contents = EncryptionService.Encrypt(fileData);
			}

			// Write file
			File.WriteAllText(Config.Paths.Temp + "\\" + fileName, contents);
		}

		public static byte[] GetTempFile(string fileName, bool encrypted)
		{
			string contents = "";

			// Decrypt
			if (encrypted)
			{
				byte[] fileData = File.ReadAllBytes(Config.Paths.Temp + "\\" + fileName);
				contents = EncryptionService.Decrypt(fileData);
			}
			else
			{
				contents = File.ReadAllText(Config.Paths.Temp + "\\" + fileName);
			}

			// Return byte[]
			return Encoding.Default.GetBytes(contents);
		}

		public static void DeleteTempFile(string fileName)
		{
			if (File.Exists(fileName))
				File.Delete(fileName);
		}
	}
}
