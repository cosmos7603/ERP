﻿using System;
using System.Web;
using Stream = System.IO.Stream;
using Encoding = System.Text.Encoding;

namespace Corpnet.Profiling.HttpModule
{
	/// <summary>
	/// Reads a resource from the assembly manifest and returns its contents as the response entity.
	/// </summary>
	internal sealed class ManifestResourceHandler : IHttpHandler
	{
		private string _resourceName;
		private string _contentType;
		private Encoding _responseEncoding;

		public ManifestResourceHandler(string resourceName, string contentType) :
			this(resourceName, contentType, null) { }

		public ManifestResourceHandler(string resourceName, string contentType, Encoding responseEncoding)
		{
			_resourceName = resourceName;
			_contentType = contentType;
			_responseEncoding = responseEncoding;
		}

		public void ProcessRequest(HttpContext context)
		{
			// Grab the resource stream from the manifest.
			Type thisType = this.GetType();

			using (Stream stream = thisType.Assembly.GetManifestResourceStream(thisType, _resourceName))
			{
				//
				// Allocate a buffer for reading the stream. The maximum size
				// of this buffer is fixed to 4 KB.
				//

				byte[] buffer = new byte[Math.Min(stream.Length, 4096)];

				//
				// Set the response headers for indicating the content type 
				// and encoding (if specified).
				//

				HttpResponse response = context.Response;
				response.ContentType = _contentType;

				if (_responseEncoding != null)
				{
					response.ContentEncoding = _responseEncoding;
				}

				//
				// Finally, write out the bytes!
				//

				int bytesWritten = 0;

				do
				{
					int readCount = stream.Read(buffer, 0, buffer.Length);
					response.OutputStream.Write(buffer, 0, readCount);
					bytesWritten += readCount;
				}
				while (bytesWritten < stream.Length);
			}
		}

		public bool IsReusable
		{
			get { return false; }
		}
	}
}
