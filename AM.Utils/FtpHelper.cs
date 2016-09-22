using FtpLib;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AM.Utils
{
	public static class FtpHelper
    {
		#region FTP
		public static byte[] GetFile(string ftpServerName, int ftpPortNumber, string ftpServerUserName, string ftpServerPassWord, string filename, string folder)
        {
            try
            {
                if (filename != "." && filename != "..")
                {
                    WebClient request = new WebClient();
                    request.Credentials = new NetworkCredential(ftpServerUserName, ftpServerPassWord);
                    if (folder != "")
                        folder = "/" + folder;
                    byte[] filedata = request.DownloadData(ftpServerName + folder + "/" + filename);

                    return filedata;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<string> GetFileList(string ftpServerName, int ftpPortNumber, string ftpServerUserName, string ftpServerPassWord, string folder, DateTime ReadDate)
        {
			var results = new List<string>();

            // New FTp client to get modify date
            string ftpServerNameNewClient = ftpServerName.Split('/').Last();
            FtpConnection ftp = new FtpConnection(ftpServerNameNewClient, ftpPortNumber, ftpServerUserName, ftpServerPassWord);
            ftp.Open();
            ftp.Login();

			var files = new FtpFileInfo[0];

			if (folder != "")
				ftp.SetCurrentDirectory(folder);

			files = ftp.GetFiles();

			foreach (var file in files)
			{
				DateTime? lastWriteTime = file.LastWriteTime;

				if (lastWriteTime != null && lastWriteTime >= ReadDate)
				{
					results.Add(file.Name);
				}
			}

            ftp.Close();
            return results;
        }
		#endregion

		#region SFTP
		public static byte[] SGetFile(string ftpServerName, int ftpPortNumber, string ftpServerUserName, string ftpServerPassWord, string filename, string folder)
		{
			try
			{
				if (filename != "." && filename != "..")
				{
					SftpClient scp = new SftpClient(ftpServerName, ftpPortNumber, ftpServerUserName, ftpServerPassWord);
					scp.Connect();

					MemoryStream ms = new MemoryStream();
					scp.DownloadFile(folder + filename, ms);
					ms.Seek(0, SeekOrigin.Begin);

					scp.Disconnect();
					var filedata = new byte[ms.Length];
					ms.Read(filedata, 0, ms.Length.ToInt());

					return filedata;
				}
				else
					return null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static List<string> SGetFileList(string ftpServerName, int ftpPortNumber, string ftpServerUserName, string ftpServerPassWord, string folder, DateTime readDate)
		{
			var results = new List<string>();

			SftpClient sftpClient = new SftpClient(ftpServerName, ftpPortNumber, ftpServerUserName, ftpServerPassWord);
			sftpClient.Connect();

			List<SftpFile> fileList = sftpClient.ListDirectory(folder).ToList();

			if (fileList != null && fileList.Count() > 2)
			{
				for (int i = 2; i < fileList.Count(); i++)
				{
					DateTime? lastWriteTime = fileList[i].LastWriteTime;

					if (lastWriteTime != null && lastWriteTime >= readDate)
					{
						results.Add(fileList[i].Name);
					}
				}
			}

			sftpClient.Disconnect();

			return results;
		}
		#endregion
	}
}