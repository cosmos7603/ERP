using System;
using System.IO;
using System.Collections;
using System.Text;

namespace AM.Utils
{
	#region ZipOutputStream
	//
	// ZipOutputStream
	//
	public class ZipOutputStream : DeflaterOutputStream
	{
		private ArrayList entries  = new ArrayList();
		private Crc32     crc      = new Crc32();
		private ZipEntry  curEntry = null;
		
		int defaultCompressionLevel = Deflater.DEFAULT_COMPRESSION;
		CompressionMethod curMethod = CompressionMethod.Deflated;

		
		private long size;
		private long offset = 0;
		
		private byte[] zipComment = new byte[0];
		
		public ZipOutputStream(Stream baseOutputStream) : base(baseOutputStream, new Deflater(Deflater.DEFAULT_COMPRESSION, true))
		{
		}

		public void SetLevel(int level)
		{
			defaultCompressionLevel = level;
			def.SetLevel(level);
		}

		private  void WriteLeShort(int value)
		{
			baseOutputStream.WriteByte((byte)(value & 0xff));
			baseOutputStream.WriteByte((byte)((value >> 8) & 0xff));
		}
		
		private void WriteLeInt(int value)
		{
			WriteLeShort(value);
			WriteLeShort(value >> 16);
		}
		
		private void WriteLeLong(long value)
		{
			WriteLeInt((int)value);
			WriteLeInt((int)(value >> 32));
		}
		
		bool patchEntryHeader = false;
		
		long headerPatchPos   = -1;

		public void PutNextEntry(ZipEntry entry)
		{
			if (entries == null) {
				throw new InvalidOperationException("ZipOutputStream was finished");
			}
			
			if (curEntry != null) {
				CloseEntry();
			}

			if (entries.Count >= 0xffff) {
				throw new ZipException("Too many entries for Zip file");
			}
			
			CompressionMethod method = entry.CompressionMethod;
			int compressionLevel = defaultCompressionLevel;
			
			entry.Flags = 0;
			patchEntryHeader = false;
			bool headerInfoAvailable = true;
			
			if (method == CompressionMethod.Stored) {
				if (entry.CompressedSize >= 0) {
					if (entry.Size < 0) {
						entry.Size = entry.CompressedSize;
					} else if (entry.Size != entry.CompressedSize) {
						throw new ZipException("Method STORED, but compressed size != size");
					}
				} else {
					if (entry.Size >= 0) {
						entry.CompressedSize = entry.Size;
					}
				}
					
				if (entry.Size < 0 || entry.Crc < 0) {
					if (CanPatchEntries == true) {
						headerInfoAvailable = false;
					}
					else {
                  // Cant patch entries so storing is not possible.
						method = CompressionMethod.Deflated;
						compressionLevel = 0;
					}
				}
			}
				
			if (method == CompressionMethod.Deflated) {
				if (entry.Size == 0) {
               // No need to compress - no data.
					entry.CompressedSize = entry.Size;
					entry.Crc = 0;
					method = CompressionMethod.Stored;
				} else if (entry.CompressedSize < 0 || entry.Size < 0 || entry.Crc < 0) {
					headerInfoAvailable = false;
				}
			}
			
			if (headerInfoAvailable == false) {
				if (CanPatchEntries == false) {
					entry.Flags |= 8;
				} else {
					patchEntryHeader = true;
				}
			}
			
			if (Password != null) {
				entry.IsCrypted = true;
				if (entry.Crc < 0) {
               // Need to append data descriptor as crc is used for encryption and its not known.
					entry.Flags |= 8;
				}
			}
			entry.Offset = (int)offset;
			entry.CompressionMethod = (CompressionMethod)method;
			
			curMethod    = method;
			
			// Write the local file header
			WriteLeInt(ZipConstants.LOCSIG);
			
			WriteLeShort(entry.Version);
			WriteLeShort(entry.Flags);
			WriteLeShort((byte)method);
			WriteLeInt((int)entry.DosTime);
			if (headerInfoAvailable == true) {
				WriteLeInt((int)entry.Crc);
				WriteLeInt(entry.IsCrypted ? (int)entry.CompressedSize + ZipConstants.CRYPTO_HEADER_SIZE : (int)entry.CompressedSize);
				WriteLeInt((int)entry.Size);
			} else {
				if (patchEntryHeader == true) {
					headerPatchPos = baseOutputStream.Position;
				}
				WriteLeInt(0);	// Crc
				WriteLeInt(0);	// Compressed size
				WriteLeInt(0);	// Uncompressed size
			}
			
			byte[] name = ZipConstants.ConvertToArray(entry.Name);
			
			if (name.Length > 0xFFFF) {
				throw new ZipException("Entry name too long.");
			}

			byte[] extra = entry.ExtraData;
			if (extra == null) {
				extra = new byte[0];
			}

			if (extra.Length > 0xFFFF) {
				throw new ZipException("Extra data too long.");
			}
			
			WriteLeShort(name.Length);
			WriteLeShort(extra.Length);
			baseOutputStream.Write(name, 0, name.Length);
			baseOutputStream.Write(extra, 0, extra.Length);
			
			offset += ZipConstants.LOCHDR + name.Length + extra.Length;
			
			// Activate the entry.
			curEntry = entry;
			crc.Reset();
			if (method == CompressionMethod.Deflated) {
				def.Reset();
				def.SetLevel(compressionLevel);
			}
			size = 0;
			
			if (entry.IsCrypted == true) {
				if (entry.Crc < 0) {			// so testing Zip will says its ok
					WriteEncryptionHeader(entry.DosTime << 16);
				} else {
					WriteEncryptionHeader(entry.Crc);
				}
			}
		}
		
		public void CloseEntry()
		{
			if (curEntry == null) {
				throw new InvalidOperationException("No open entry");
			}
			
			// First finish the deflater, if appropriate
			if (curMethod == CompressionMethod.Deflated) {
				base.Finish();
			}
			
			long csize = curMethod == CompressionMethod.Deflated ? def.TotalOut : size;
			
			if (curEntry.Size < 0) {
				curEntry.Size = size;
			} else if (curEntry.Size != size) {
				throw new ZipException("size was " + size + ", but I expected " + curEntry.Size);
			}
			
			if (curEntry.CompressedSize < 0) {
				curEntry.CompressedSize = csize;
			} else if (curEntry.CompressedSize != csize) {
				throw new ZipException("compressed size was " + csize + ", but I expected " + curEntry.CompressedSize);
			}
			
			if (curEntry.Crc < 0) {
				curEntry.Crc = crc.Value;
			} else if (curEntry.Crc != crc.Value) {
				throw new ZipException("crc was " + crc.Value +	", but I expected " + curEntry.Crc);
			}
			
			offset += csize;

			if (offset > 0xffffffff) {
				throw new ZipException("Maximum Zip file size exceeded");
			}
				
			if (curEntry.IsCrypted == true) {
				curEntry.CompressedSize += ZipConstants.CRYPTO_HEADER_SIZE;
			}
				
			// Patch the header if possible
			if (patchEntryHeader == true) {
				long curPos = baseOutputStream.Position;
				baseOutputStream.Seek(headerPatchPos, SeekOrigin.Begin);
				WriteLeInt((int)curEntry.Crc);
				WriteLeInt((int)curEntry.CompressedSize);
				WriteLeInt((int)curEntry.Size);
				baseOutputStream.Seek(curPos, SeekOrigin.Begin);
				patchEntryHeader = false;
			}

			// Add data descriptor if flagged as required
			if ((curEntry.Flags & 8) != 0) {
				WriteLeInt(ZipConstants.EXTSIG);
				WriteLeInt((int)curEntry.Crc);
				WriteLeInt((int)curEntry.CompressedSize);
				WriteLeInt((int)curEntry.Size);
				offset += ZipConstants.EXTHDR;
			}
			
			entries.Add(curEntry);
			curEntry = null;
		}
		
		void WriteEncryptionHeader(long crcValue)
		{
			offset += ZipConstants.CRYPTO_HEADER_SIZE;
			
			InitializePassword(Password);
			
			byte[] cryptBuffer = new byte[ZipConstants.CRYPTO_HEADER_SIZE];
			Random rnd = new Random();
			rnd.NextBytes(cryptBuffer);
			cryptBuffer[11] = (byte)(crcValue >> 24);
			
			EncryptBlock(cryptBuffer, 0, cryptBuffer.Length);
			baseOutputStream.Write(cryptBuffer, 0, cryptBuffer.Length);
		}
		
		public override void Write(byte[] b, int off, int len)
		{
			if (curEntry == null) {
				throw new InvalidOperationException("No open entry.");
			}
			
			if (len <= 0)
				return;
			
			crc.Update(b, off, len);
			size += len;
			
			if (size > 0xffffffff || size < 0) {
				throw new ZipException("Maximum entry size exceeded");
			}
				

			switch (curMethod) {
				case CompressionMethod.Deflated:
					base.Write(b, off, len);
					break;
				
				case CompressionMethod.Stored:
					if (Password != null) {
						byte[] buf = new byte[len];
						Array.Copy(b, off, buf, 0, len);
						EncryptBlock(buf, 0, len);
						baseOutputStream.Write(buf, off, len);
					} else {
						baseOutputStream.Write(b, off, len);
					}
					break;
			}
		}
		
		public override void Finish()
		{
			if (entries == null)  {
				return;
			}
			
			if (curEntry != null) {
				CloseEntry();
			}
			
			int numEntries = 0;
			int sizeEntries = 0;
			
			foreach (ZipEntry entry in entries) {
				CompressionMethod method = entry.CompressionMethod;
				WriteLeInt(ZipConstants.CENSIG); 
				WriteLeShort(ZipConstants.VERSION_MADE_BY);
				WriteLeShort(entry.Version);
				WriteLeShort(entry.Flags);
				WriteLeShort((short)method);
				WriteLeInt((int)entry.DosTime);
				WriteLeInt((int)entry.Crc);
				WriteLeInt((int)entry.CompressedSize);
				WriteLeInt((int)entry.Size);
				
				byte[] name = ZipConstants.ConvertToArray(entry.Name);
				
				if (name.Length > 0xffff) {
					throw new ZipException("Name too long.");
				}
				
				byte[] extra = entry.ExtraData;
				if (extra == null) {
					extra = new byte[0];
				}
				
				byte[] entryComment = entry.Comment != null ? ZipConstants.ConvertToArray(entry.Comment) : new byte[0];
				if (entryComment.Length > 0xffff) {
					throw new ZipException("Comment too long.");
				}
				
				WriteLeShort(name.Length);
				WriteLeShort(extra.Length);
				WriteLeShort(entryComment.Length);
				WriteLeShort(0);	// disk number
				WriteLeShort(0);	// internal file attr
									// external file attribute

				if (entry.ExternalFileAttributes != -1) {
					WriteLeInt(entry.ExternalFileAttributes);
				} else {
					if (entry.IsDirectory) {                         // mark entry as directory (from nikolam.AT.perfectinfo.com)
						WriteLeInt(16);
					} else {
						WriteLeInt(0);
					}
				}

				WriteLeInt(entry.Offset);
				
				baseOutputStream.Write(name,    0, name.Length);
				baseOutputStream.Write(extra,   0, extra.Length);
				baseOutputStream.Write(entryComment, 0, entryComment.Length);
				++numEntries;
				sizeEntries += ZipConstants.CENHDR + name.Length + extra.Length + entryComment.Length;
			}
			
			WriteLeInt(ZipConstants.ENDSIG);
			WriteLeShort(0);                    // number of this disk
			WriteLeShort(0);                    // no of disk with start of central dir
			WriteLeShort(numEntries);           // entries in central dir for this disk
			WriteLeShort(numEntries);           // total entries in central directory
			WriteLeInt(sizeEntries);            // size of the central directory
			WriteLeInt((int)offset);            // offset of start of central dir
			WriteLeShort(zipComment.Length);
			baseOutputStream.Write(zipComment, 0, zipComment.Length);
			baseOutputStream.Flush();
			entries = null;
		}
	}
	#endregion

	#region ZipEntry
	//
	// ZipEntry
	//
	public class ZipEntry : ICloneable
	{
		static int KNOWN_SIZE               = 1;
		static int KNOWN_CSIZE              = 2;
		static int KNOWN_CRC                = 4;
		static int KNOWN_TIME               = 8;
		static int KNOWN_EXTERN_ATTRIBUTES 	= 16;
	
		ushort known = 0;                       // Bit flags made up of above bits
		int    externalFileAttributes = -1;     // contains external attributes (os dependant)
	
		ushort versionMadeBy;                   // Contains host system and version information
		// only relevant for central header entries
	
		string name;
		ulong  size;
		ulong  compressedSize;
		ushort versionToExtract;                // Version required to extract (library handles <= 2.0)
		uint   crc;
		uint   dosTime;
	
		CompressionMethod  method = CompressionMethod.Deflated;
		byte[] extra = null;
		string comment = null;
	
		int flags;                             // general purpose bit flags

		int zipFileIndex = -1;                 // used by ZipFile
		int offset;                            // used by ZipFile and ZipOutputStream
	
		public bool IsCrypted 
		{
			get 
			{
				return (flags & 1) != 0; 
			}
			set 
			{
				if (value) 
				{
					flags |= 1;
				} 
				else 
				{
					flags &= ~1;
				}
			}
		}
	
		public int Flags 
		{
			get 
			{ 
				return flags; 
			}
			set 
			{
				flags = value; 
			}
		}

		public int ZipFileIndex 
		{
			get 
			{
				return zipFileIndex;
			}
			set 
			{
				zipFileIndex = value;
			}
		}
	
		public int Offset 
		{
			get 
			{
				return offset;
			}
			set 
			{
				if (((ulong)value & 0xFFFFFFFF00000000L) != 0) 
				{
					throw new ArgumentOutOfRangeException("Offset");
				}
				offset = value;
			}
		}

		public int ExternalFileAttributes 
		{
			get 
			{
				if ((known & KNOWN_EXTERN_ATTRIBUTES) == 0) 
				{
					return -1;
				} 
				else 
				{
					return externalFileAttributes;
				}
			}
		
			set 
			{
				externalFileAttributes = value;
				known |= (ushort)KNOWN_EXTERN_ATTRIBUTES;
			}
		}

		public int HostSystem 
		{
			get { return (versionMadeBy >> 8) & 0xff; }
		}

		public ZipEntry(string name) : this(name, 0, ZipConstants.VERSION_MADE_BY)
		{
		}

		internal ZipEntry(string name, int versionRequiredToExtract, int madeByInfo)
		{
			if (name == null)  
			{
				throw new System.ArgumentNullException("ZipEntry name");
			}

			if ( name.Length == 0 ) 
			{
				throw new ArgumentException("ZipEntry name is empty");
			}

			if (versionRequiredToExtract != 0 && versionRequiredToExtract < 10) 
			{
				throw new ArgumentOutOfRangeException("versionRequiredToExtract");
			}
		
			this.DateTime         = System.DateTime.Now;
			this.name             = name;
			this.versionMadeBy    = (ushort)madeByInfo;
			this.versionToExtract = (ushort)versionRequiredToExtract;
		}
	
		public int Version 
		{
			get 
			{
				if (versionToExtract != 0) 
				{
					return versionToExtract;
				} 
				else 
				{
					int result = 10;
					if (CompressionMethod.Deflated == method) 
					{
						result = 20;
					} 
					else if (IsDirectory == true) 
					{
						result = 20;
					} 
					else if (IsCrypted == true) 
					{
						result = 20;
					} 
					else if ((known & KNOWN_EXTERN_ATTRIBUTES) != 0 && (externalFileAttributes & 0x08) != 0) 
					{
						result = 11;
					}
					return result;
				}
			}
		}
	
		public long DosTime 
		{
			get 
			{
				if ((known & KNOWN_TIME) == 0) 
				{
					return 0;
				} 
				else 
				{
					return dosTime;
				}
			}
			set 
			{
				this.dosTime = (uint)value;
				known |= (ushort)KNOWN_TIME;
			}
		}
	
		public DateTime DateTime 
		{
			get 
			{
				// Although technically not valid some archives have dates set to zero.
				// This mimics some archivers handling and is a good a cludge as any probably.
				if ( dosTime == 0 ) 
				{
					return DateTime.Now;
				}
				else 
				{
					uint sec  = 2 * (dosTime & 0x1f);
					uint min  = (dosTime >> 5) & 0x3f;
					uint hrs  = (dosTime >> 11) & 0x1f;
					uint day  = (dosTime >> 16) & 0x1f;
					uint mon  = ((dosTime >> 21) & 0xf);
					uint year = ((dosTime >> 25) & 0x7f) + 1980;
					return new System.DateTime((int)year, (int)mon, (int)day, (int)hrs, (int)min, (int)sec);
				}
			}
			set 
			{
				DosTime = ((uint)value.Year - 1980 & 0x7f) << 25 | 
					((uint)value.Month) << 21 |
					((uint)value.Day) << 16 |
					((uint)value.Hour) << 11 |
					((uint)value.Minute) << 5 |
					((uint)value.Second) >> 1;
			}
		}
	
		public string Name 
		{
			get 
			{
				return name;
			}
		}
	
		static public string CleanName(string name, bool relativePath)
		{
			if (name == null) 
			{
				return "";
			}
		
			if (Path.IsPathRooted(name) == true) 
			{
				// NOTE:
				// for UNC names...  \\machine\share\zoom\beet.txt gives \zoom\beet.txt
				name = name.Substring(Path.GetPathRoot(name).Length);
			}

			name = name.Replace(@"\", "/");
		
			if (relativePath == true) 
			{
				if (name.Length > 0 && (name[0] == Path.AltDirectorySeparatorChar || name[0] == Path.DirectorySeparatorChar)) 
				{
					name = name.Remove(0, 1);
				}
			} 
			else 
			{
				if (name.Length > 0 && name[0] != Path.AltDirectorySeparatorChar && name[0] != Path.DirectorySeparatorChar) 
				{
					name = name.Insert(0, "/");
				}
			}
			return name;
		}
	
		public long Size 
		{
			get 
			{
				return (known & KNOWN_SIZE) != 0 ? (long)size : -1L;
			}
			set 
			{
				if (((ulong)value & 0xFFFFFFFF00000000L) != 0) 
				{
					throw new ArgumentOutOfRangeException("size");
				}
				this.size  = (ulong)value;
				this.known |= (ushort)KNOWN_SIZE;
			}
		}
	
		public long CompressedSize 
		{
			get 
			{
				return (known & KNOWN_CSIZE) != 0 ? (long)compressedSize : -1L;
			}
			set 
			{
				if (((ulong)value & 0xffffffff00000000L) != 0) 
				{
					throw new ArgumentOutOfRangeException();
				}
				this.compressedSize = (ulong)value;
				this.known |= (ushort)KNOWN_CSIZE;
			}
		}
	
		public long Crc 
		{
			get 
			{
				return (known & KNOWN_CRC) != 0 ? crc & 0xffffffffL : -1L;
			}
			set 
			{
				if (((ulong)crc & 0xffffffff00000000L) != 0) 
				{
					throw new ArgumentOutOfRangeException();
				}
				this.crc = (uint)value;
				this.known |= (ushort)KNOWN_CRC;
			}
		}
	
		public CompressionMethod CompressionMethod 
		{
			get 
			{
				return method;
			}
			set 
			{
				this.method = value;
			}
		}
	
		public byte[] ExtraData 
		{
			get 
			{
				return extra;
			}
			set 
			{
				if (value == null) 
				{
					this.extra = null;
					return;
				}
			
				if (value.Length > 0xffff) 
				{
					throw new System.ArgumentOutOfRangeException();
				}
			
				this.extra = new byte[value.Length];
				Array.Copy(value, 0, this.extra, 0, value.Length);
			
				try 
				{
					int pos = 0;
					while (pos < extra.Length) 
					{
						int sig = (extra[pos++] & 0xff) | (extra[pos++] & 0xff) << 8;
						int len = (extra[pos++] & 0xff) | (extra[pos++] & 0xff) << 8;
					
						if (len < 0 || pos + len > extra.Length) 
						{
							// This is still lenient but the extra data is corrupt
							// TODO: drop the extra data? or somehow indicate to user 
							// there is a problem...
							break;
						}
					
						if (sig == 0x5455) 
						{
							// extended time stamp, unix format by Rainer Prem <Rainer@Prem.de>
							int flags = extra[pos];
							// Can include other times but these are ignored.  Length of data should
							// actually be 1 + 4 * no of bits in flags.
							if ((flags & 1) != 0 && len >= 5) 
							{
								int iTime = ((extra[pos+1] & 0xff) |
									(extra[pos + 2] & 0xff) << 8 |
									(extra[pos + 3] & 0xff) << 16 |
									(extra[pos + 4] & 0xff) << 24);
							
								DateTime = (new DateTime ( 1970, 1, 1, 0, 0, 0 ) + new TimeSpan ( 0, 0, 0, iTime, 0 )).ToLocalTime ();
								known |= (ushort)KNOWN_TIME;
							}
						} 
						else if (sig == 0x0001) 
						{ 
							// ZIP64 extended information extra field
							// Of variable size depending on which fields in header are too small
							// fields appear here if the corresponding local or central directory record field
							// is set to 0xFFFF or 0xFFFFFFFF and the entry is in Zip64 format.
							//
							// Original Size          8 bytes
							// Compressed size        8 bytes
							// Relative header offset 8 bytes
							// Disk start number      4 bytes
						}
						pos += len;
					}
				} 
				catch (Exception) 
				{
					/* be lenient */
					return;
				}
			}
		}
	
		public string Comment 
		{
			get 
			{
				return comment;
			}
			set 
			{
				// TODO: this test is strictly incorrect as the length is in characters
				// While the test is correct in that a comment of this length or greater 
				// is definitely invalid, shorter comments may also have an invalid length.
				if (value != null && value.Length > 0xffff) 
				{
					throw new ArgumentOutOfRangeException();
				}
				this.comment = value;
			}
		}
	
		public bool IsDirectory 
		{
			get 
			{
				int nlen = name.Length;
				bool result = nlen > 0 && name[nlen - 1] == '/';
			
				if (result == false && (known & KNOWN_EXTERN_ATTRIBUTES) != 0) 
				{
					if (HostSystem == 0 && (ExternalFileAttributes & 16) != 0) 
					{
						result = true;
					}
				}
				return result;
			}
		}
	
		public bool IsFile 
		{
			get 
			{
				bool result = !IsDirectory;

				// Exclude volume labels
				if ( result && (known & KNOWN_EXTERN_ATTRIBUTES) != 0) 
				{
					if (HostSystem == 0 && (ExternalFileAttributes & 8) != 0) 
					{
						result = false;
					}
				}
				return result;
			}
		}
	
		public object Clone()
		{
			return this.MemberwiseClone();
		}
	}

	#endregion

	#region ZipConstants
	//
	// ZipConstants
	//
	public enum CompressionMethod
	{
		Stored     = 0,
		Deflated   = 8,
		Deflate64  = 9,
		BZip2      = 11,
		WinZipAES  = 99,
	}
	
	[Flags]
	enum GeneralBitFlags : int
	{
		Encrypted         = 0x0001,
		Method            = 0x0006,
		Descriptor        = 0x0008,
		Reserved          = 0x0010,
		Patched           = 0x0020,
		StrongEncryption  = 0x0040,
		EnhancedCompress  = 0x1000,
		HeaderMasked      = 0x2000
	}
	
	public sealed class ZipConstants
	{
		public const int VERSION_MADE_BY = 20;
		public const int VERSION_STRONG_ENCRYPTION = 50;
		
		public const int LOCHDR = 30;
		public const int LOCSIG = 'P' | ('K' << 8) | (3 << 16) | (4 << 24);
		public const int LOCVER =  4;
		public const int LOCFLG =  6;
		public const int LOCHOW =  8;
		public const int LOCTIM = 10;
		public const int LOCCRC = 14;
		public const int LOCSIZ = 18;
		public const int LOCLEN = 22;
		public const int LOCNAM = 26;
		public const int LOCEXT = 28;

		public const int SPANNINGSIG = 'P' | ('K' << 8) | (7 << 16) | (8 << 24);
		public const int SPANTEMPSIG = 'P' | ('K' << 8) | ('0' << 16) | ('0' << 24);
		public const int EXTSIG = 'P' | ('K' << 8) | (7 << 16) | (8 << 24);
		public const int EXTHDR = 16;
		public const int EXTCRC =  4;
		public const int EXTSIZ =  8;
		public const int EXTLEN = 12;
		
		public const int CENSIG = 'P' | ('K' << 8) | (1 << 16) | (2 << 24);
		public const int CENHDR = 46;
		public const int CENVEM =  4;
		public const int CENVER =  6;
		public const int CENFLG =  8;
		public const int CENHOW = 10;
		public const int CENTIM = 12;
		public const int CENCRC = 16;
		public const int CENSIZ = 20;
		public const int CENLEN = 24;
		public const int CENNAM = 28;
		public const int CENEXT = 30;
		public const int CENCOM = 32;
		public const int CENDSK = 34;
		public const int CENATT = 36;
		public const int CENATX = 38;
		public const int CENOFF = 42;
		
		public const int CENSIG64 = 'P' | ('K' << 8) | (6 << 16) | (6 << 24);
		
		public const int CENDIGITALSIG = 'P' | ('K' << 8) | (5 << 16) | (5 << 24);
		
		public const int ENDSIG = 'P' | ('K' << 8) | (5 << 16) | (6 << 24);
		public const int ENDHDR = 22;
		
		public const int ENDNRD =  4;
		public const int ENDDCD =  6;
		public const int ENDSUB =  8;
		public const int ENDTOT = 10;
		public const int ENDSIZ = 12;
		public const int ENDOFF = 16;
		public const int ENDCOM = 20;
		public const int CRYPTO_HEADER_SIZE = 12;
		
#if !COMPACT_FRAMEWORK

		static int defaultCodePage = 0;
		
		public static int DefaultCodePage 
		{
			get 
			{
				return defaultCodePage; 
			}
			set 
			{
				defaultCodePage = value; 
			}
		}
#endif

		public static string ConvertToString(byte[] data, int length)
		{
#if COMPACT_FRAMEWORK
			return Encoding.ASCII.GetString(data, 0, length);
#else
			return Encoding.GetEncoding(DefaultCodePage).GetString(data, 0, length);
#endif
		}
	
		public static string ConvertToString(byte[] data)
		{
			return ConvertToString(data, data.Length);
		}

		public static byte[] ConvertToArray(string str)
		{
#if COMPACT_FRAMEWORK
			return Encoding.ASCII.GetBytes(str);
#else
			return Encoding.GetEncoding(DefaultCodePage).GetBytes(str);
#endif
		}
	}
	#endregion

	#region Streams
	//
	// Streams
	//

	#region DeflaterOutputStream
	//
	// DeflaterOutputStream
	//
	public class DeflaterOutputStream : Stream
	{
		protected byte[] buf;
		protected Deflater def;
		protected Stream baseOutputStream;

		bool isClosed = false;
		bool isStreamOwner = true;
		
		public bool CanPatchEntries 
		{
			get 
			{ 
				return baseOutputStream.CanSeek; 
			}
		}
		
		public override bool CanRead 
		{
			get 
			{
				return baseOutputStream.CanRead;
			}
		}
		
		public override bool CanSeek 
		{
			get 
			{
				return false;
			}
		}
		
		public override bool CanWrite 
		{
			get 
			{
				return baseOutputStream.CanWrite;
			}
		}
		
		public override long Length 
		{
			get 
			{
				return baseOutputStream.Length;
			}
		}
		
		public override long Position 
		{
			get 
			{
				return baseOutputStream.Position;
			}
			set 
			{
				throw new NotSupportedException("DefalterOutputStream Position not supported");
			}
		}
		
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("DeflaterOutputStream Seek not supported");
		}
		

		public override void SetLength(long val)
		{
			throw new NotSupportedException("DeflaterOutputStream SetLength not supported");
		}
		
		public override int ReadByte()
		{
			throw new NotSupportedException("DeflaterOutputStream ReadByte not supported");
		}
		
		public override int Read(byte[] b, int off, int len)
		{
			throw new NotSupportedException("DeflaterOutputStream Read not supported");
		}
		
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException("DeflaterOutputStream BeginRead not currently supported");
		}
		
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException("DeflaterOutputStream BeginWrite not currently supported");
		}
		
		protected void Deflate()
		{
			while (!def.IsNeedingInput) 
			{
				int len = def.Deflate(buf, 0, buf.Length);
				
				if (len <= 0) 
				{
					break;
				}
				
				if (this.keys != null) 
				{
					this.EncryptBlock(buf, 0, len);
				}
				
				baseOutputStream.Write(buf, 0, len);
			}
			
			if (!def.IsNeedingInput) 
			{
				throw new SharpZipBaseException("DeflaterOutputStream can't deflate all input?");
			}
		}

		public DeflaterOutputStream(Stream baseOutputStream, Deflater defl) : this(baseOutputStream, defl, 512)
		{
		}
		
		public DeflaterOutputStream(Stream baseOutputStream, Deflater deflater, int bufsize)
		{
			if (baseOutputStream.CanWrite == false) 
			{
				throw new ArgumentException("baseOutputStream", "must support writing");
			}

			if (deflater == null) 
			{
				throw new ArgumentNullException("deflater");
			}
			
			if (bufsize <= 0) 
			{
				throw new ArgumentOutOfRangeException("bufsize");
			}
			
			this.baseOutputStream = baseOutputStream;
			buf = new byte[bufsize];
			def = deflater;
		}
		

		public override void Flush()
		{
			def.Flush();
			Deflate();
			baseOutputStream.Flush();
		}
		
		public virtual void Finish()
		{
			def.Finish();
			while (!def.IsFinished)  
			{
				int len = def.Deflate(buf, 0, buf.Length);
				if (len <= 0) 
				{
					break;
				}
				
				if (this.keys != null) 
				{
					this.EncryptBlock(buf, 0, len);
				}
				
				baseOutputStream.Write(buf, 0, len);
			}
			if (!def.IsFinished) 
			{
				throw new SharpZipBaseException("Can't deflate all input?");
			}
			baseOutputStream.Flush();
			keys = null;
		}
		
		public override void Close()
		{
			if ( !isClosed ) 
			{
				isClosed = true;
				Finish();
				if ( isStreamOwner ) 
				{
					baseOutputStream.Close();
				}
			}
		}
		
		public override void WriteByte(byte bval)
		{
			byte[] b = new byte[1];
			b[0] = bval;
			Write(b, 0, 1);
		}
		
		public override void Write(byte[] buf, int off, int len)
		{
			def.SetInput(buf, off, len);
			Deflate();
		}
		
		#region Encryption
		
		// TODO:  Refactor this code.  The presence of Zip specific code in this low level class is wrong
		string password = null;
		uint[] keys     = null;
		
		public string Password 
		{
			get 
			{ 
				return password; 
			}
			set 
			{
				if ( value != null && value.Length == 0 ) 
				{
					password = null;
				} 
				else 
				{
					password = value; 
				}
			}
		}
		
		protected byte EncryptByte()
		{
			uint temp = ((keys[2] & 0xFFFF) | 2);
			return (byte)((temp * (temp ^ 1)) >> 8);
		}
		
		protected void EncryptBlock(byte[] buffer, int offset, int length)
		{
			// TODO: refactor to use crypto transform
			for (int i = offset; i < offset + length; ++i) 
			{
				byte oldbyte = buffer[i];
				buffer[i] ^= EncryptByte();
				UpdateKeys(oldbyte);
			}
		}
		
		protected void InitializePassword(string password) 
		{
			keys = new uint[] {
								  0x12345678,
								  0x23456789,
								  0x34567890
							  };
			
			for (int i = 0; i < password.Length; ++i) 
			{
				UpdateKeys((byte)password[i]);
			}
		}

		protected void UpdateKeys(byte ch)
		{
			keys[0] = Crc32.ComputeCrc32(keys[0], ch);
			keys[1] = keys[1] + (byte)keys[0];
			keys[1] = keys[1] * 134775813 + 1;
			keys[2] = Crc32.ComputeCrc32(keys[2], (byte)(keys[1] >> 24));
		}
	
		#endregion
	}
	#endregion

	#endregion

	#region Checksums
	//
	// Checksums
	//

	#region Adler32
	//
	// Adler32
	//

	public sealed class Adler32
	{
		readonly static uint BASE = 65521;
		uint checksum;
		
		public long Value 
		{
			get 
			{
				return checksum;
			}
		}
		public void Reset()
		{
			checksum = 1;
		}

		public void Update(byte[] buf, int off, int len)
		{
			if (buf == null) 
			{
				throw new ArgumentNullException("buf");
			}
			
			if (off < 0 || len < 0 || off + len > buf.Length) 
			{
				throw new ArgumentOutOfRangeException();
			}
			
			//(By Per Bothner)
			uint s1 = checksum & 0xFFFF;
			uint s2 = checksum >> 16;
			
			while (len > 0) 
			{
				// We can defer the modulo operation:
				// s1 maximally grows from 65521 to 65521 + 255 * 3800
				// s2 maximally grows by 3800 * median(s1) = 2090079800 < 2^31
				int n = 3800;
				if (n > len) 
				{
					n = len;
				}
				len -= n;
				while (--n >= 0) 
				{
					s1 = s1 + (uint)(buf[off++] & 0xFF);
					s2 = s2 + s1;
				}
				s1 %= BASE;
				s2 %= BASE;
			}
			
			checksum = (s2 << 16) | s1;
		}
	}
	#endregion

	#region CRC32
	//
	// CRC32
	//
	public sealed class Crc32
	{
		readonly static uint CrcSeed = 0xFFFFFFFF;
		
		readonly static uint[] CrcTable = new uint[] {
			0x00000000, 0x77073096, 0xEE0E612C, 0x990951BA, 0x076DC419,
			0x706AF48F, 0xE963A535, 0x9E6495A3, 0x0EDB8832, 0x79DCB8A4,
			0xE0D5E91E, 0x97D2D988, 0x09B64C2B, 0x7EB17CBD, 0xE7B82D07,
			0x90BF1D91, 0x1DB71064, 0x6AB020F2, 0xF3B97148, 0x84BE41DE,
			0x1ADAD47D, 0x6DDDE4EB, 0xF4D4B551, 0x83D385C7, 0x136C9856,
			0x646BA8C0, 0xFD62F97A, 0x8A65C9EC, 0x14015C4F, 0x63066CD9,
			0xFA0F3D63, 0x8D080DF5, 0x3B6E20C8, 0x4C69105E, 0xD56041E4,
			0xA2677172, 0x3C03E4D1, 0x4B04D447, 0xD20D85FD, 0xA50AB56B,
			0x35B5A8FA, 0x42B2986C, 0xDBBBC9D6, 0xACBCF940, 0x32D86CE3,
			0x45DF5C75, 0xDCD60DCF, 0xABD13D59, 0x26D930AC, 0x51DE003A,
			0xC8D75180, 0xBFD06116, 0x21B4F4B5, 0x56B3C423, 0xCFBA9599,
			0xB8BDA50F, 0x2802B89E, 0x5F058808, 0xC60CD9B2, 0xB10BE924,
			0x2F6F7C87, 0x58684C11, 0xC1611DAB, 0xB6662D3D, 0x76DC4190,
			0x01DB7106, 0x98D220BC, 0xEFD5102A, 0x71B18589, 0x06B6B51F,
			0x9FBFE4A5, 0xE8B8D433, 0x7807C9A2, 0x0F00F934, 0x9609A88E,
			0xE10E9818, 0x7F6A0DBB, 0x086D3D2D, 0x91646C97, 0xE6635C01,
			0x6B6B51F4, 0x1C6C6162, 0x856530D8, 0xF262004E, 0x6C0695ED,
			0x1B01A57B, 0x8208F4C1, 0xF50FC457, 0x65B0D9C6, 0x12B7E950,
			0x8BBEB8EA, 0xFCB9887C, 0x62DD1DDF, 0x15DA2D49, 0x8CD37CF3,
			0xFBD44C65, 0x4DB26158, 0x3AB551CE, 0xA3BC0074, 0xD4BB30E2,
			0x4ADFA541, 0x3DD895D7, 0xA4D1C46D, 0xD3D6F4FB, 0x4369E96A,
			0x346ED9FC, 0xAD678846, 0xDA60B8D0, 0x44042D73, 0x33031DE5,
			0xAA0A4C5F, 0xDD0D7CC9, 0x5005713C, 0x270241AA, 0xBE0B1010,
			0xC90C2086, 0x5768B525, 0x206F85B3, 0xB966D409, 0xCE61E49F,
			0x5EDEF90E, 0x29D9C998, 0xB0D09822, 0xC7D7A8B4, 0x59B33D17,
			0x2EB40D81, 0xB7BD5C3B, 0xC0BA6CAD, 0xEDB88320, 0x9ABFB3B6,
			0x03B6E20C, 0x74B1D29A, 0xEAD54739, 0x9DD277AF, 0x04DB2615,
			0x73DC1683, 0xE3630B12, 0x94643B84, 0x0D6D6A3E, 0x7A6A5AA8,
			0xE40ECF0B, 0x9309FF9D, 0x0A00AE27, 0x7D079EB1, 0xF00F9344,
			0x8708A3D2, 0x1E01F268, 0x6906C2FE, 0xF762575D, 0x806567CB,
			0x196C3671, 0x6E6B06E7, 0xFED41B76, 0x89D32BE0, 0x10DA7A5A,
			0x67DD4ACC, 0xF9B9DF6F, 0x8EBEEFF9, 0x17B7BE43, 0x60B08ED5,
			0xD6D6A3E8, 0xA1D1937E, 0x38D8C2C4, 0x4FDFF252, 0xD1BB67F1,
			0xA6BC5767, 0x3FB506DD, 0x48B2364B, 0xD80D2BDA, 0xAF0A1B4C,
			0x36034AF6, 0x41047A60, 0xDF60EFC3, 0xA867DF55, 0x316E8EEF,
			0x4669BE79, 0xCB61B38C, 0xBC66831A, 0x256FD2A0, 0x5268E236,
			0xCC0C7795, 0xBB0B4703, 0x220216B9, 0x5505262F, 0xC5BA3BBE,
			0xB2BD0B28, 0x2BB45A92, 0x5CB36A04, 0xC2D7FFA7, 0xB5D0CF31,
			0x2CD99E8B, 0x5BDEAE1D, 0x9B64C2B0, 0xEC63F226, 0x756AA39C,
			0x026D930A, 0x9C0906A9, 0xEB0E363F, 0x72076785, 0x05005713,
			0x95BF4A82, 0xE2B87A14, 0x7BB12BAE, 0x0CB61B38, 0x92D28E9B,
			0xE5D5BE0D, 0x7CDCEFB7, 0x0BDBDF21, 0x86D3D2D4, 0xF1D4E242,
			0x68DDB3F8, 0x1FDA836E, 0x81BE16CD, 0xF6B9265B, 0x6FB077E1,
			0x18B74777, 0x88085AE6, 0xFF0F6A70, 0x66063BCA, 0x11010B5C,
			0x8F659EFF, 0xF862AE69, 0x616BFFD3, 0x166CCF45, 0xA00AE278,
			0xD70DD2EE, 0x4E048354, 0x3903B3C2, 0xA7672661, 0xD06016F7,
			0x4969474D, 0x3E6E77DB, 0xAED16A4A, 0xD9D65ADC, 0x40DF0B66,
			0x37D83BF0, 0xA9BCAE53, 0xDEBB9EC5, 0x47B2CF7F, 0x30B5FFE9,
			0xBDBDF21C, 0xCABAC28A, 0x53B39330, 0x24B4A3A6, 0xBAD03605,
			0xCDD70693, 0x54DE5729, 0x23D967BF, 0xB3667A2E, 0xC4614AB8,
			0x5D681B02, 0x2A6F2B94, 0xB40BBE37, 0xC30C8EA1, 0x5A05DF1B,
			0x2D02EF8D
		};
		
		internal static uint ComputeCrc32(uint oldCrc, byte bval)
		{
			return (uint)(Crc32.CrcTable[(oldCrc ^ bval) & 0xFF] ^ (oldCrc >> 8));
		}
		
		uint crc = 0;
		
		public long Value 
		{
			get 
			{
				return (long)crc;
			}
			set 
			{
				crc = (uint)value;
			}
		}
		
		public void Reset() 
		{ 
			crc = 0; 
		}

		public void Update(byte[] buf, int off, int len)
		{
			if (buf == null) 
			{
				throw new ArgumentNullException("buf");
			}
			
			if (off < 0 || len < 0 || off + len > buf.Length) 
			{
				throw new ArgumentOutOfRangeException();
			}
			
			crc ^= CrcSeed;
			
			while (--len >= 0) 
			{
				crc = CrcTable[(crc ^ buf[off++]) & 0xFF] ^ (crc >> 8);
			}
			
			crc ^= CrcSeed;
		}
	}

	#endregion

	#endregion

	#region Exception
	//
	// Exception
	//

	#region SharpZipBaseException
	//
	// SharpZipBaseException
	//
	public class SharpZipBaseException : ApplicationException
	{

		public SharpZipBaseException()
		{
		}

		public SharpZipBaseException(string msg) : base(msg)
		{
		}
	}

	#endregion

	#region ZipException
	//
	// ZipException
	//
	public class ZipException : SharpZipBaseException
	{
		public ZipException(string msg) : base(msg)
		{
		}
	}

	#endregion

	#endregion

	#region Compression
	//
	// Compression
	//

	#region DeflaterConstants
	//
	// DeflaterConstants
	//
	public class DeflaterConstants 
	{
		public const bool DEBUGGING = false;
		public const int STORED_BLOCK = 0;
		public const int STATIC_TREES = 1;
		public const int DYN_TREES    = 2;
		public const int PRESET_DICT  = 0x20;
		public const int DEFAULT_MEM_LEVEL = 8;
		public const int MAX_MATCH = 258;
		public const int MIN_MATCH = 3;
		public const int MAX_WBITS = 15;
		public const int WSIZE = 1 << MAX_WBITS;
		public const int WMASK = WSIZE - 1;
		public const int HASH_BITS = DEFAULT_MEM_LEVEL + 7;
		public const int HASH_SIZE = 1 << HASH_BITS;
		public const int HASH_MASK = HASH_SIZE - 1;
		public const int HASH_SHIFT = (HASH_BITS + MIN_MATCH - 1) / MIN_MATCH;
		public const int MIN_LOOKAHEAD = MAX_MATCH + MIN_MATCH + 1;
		public const int MAX_DIST = WSIZE - MIN_LOOKAHEAD;
		public const int PENDING_BUF_SIZE = 1 << (DEFAULT_MEM_LEVEL + 8);
		public static int MAX_BLOCK_SIZE = Math.Min(65535, PENDING_BUF_SIZE - 5);
		public const int DEFLATE_STORED = 0;
		public const int DEFLATE_FAST   = 1;
		public const int DEFLATE_SLOW   = 2;
		public static int[] GOOD_LENGTH = { 0, 4,  4,  4,  4,  8,   8,   8,   32,   32 };
		public static int[] MAX_LAZY    = { 0, 4,  5,  6,  4, 16,  16,  32,  128,  258 };
		public static int[] NICE_LENGTH = { 0, 8, 16, 32, 16, 32, 128, 128,  258,  258 };
		public static int[] MAX_CHAIN   = { 0, 4,  8, 32, 16, 32, 128, 256, 1024, 4096 };
		public static int[] COMPR_FUNC  = { 0, 1,  1,  1,  1,  2,   2,   2,    2,    2 };
	}
	#endregion

	#region Deflater
	//
	// Deflater
	//
	public class Deflater
	{
		public static  int BEST_COMPRESSION = 9;
		public static  int BEST_SPEED = 1;
		public static  int DEFAULT_COMPRESSION = -1;
		public static  int NO_COMPRESSION = 0;
		public static  int DEFLATED = 8;
	
		private static  int IS_SETDICT              = 0x01;
		private static  int IS_FLUSHING             = 0x04;
		private static  int IS_FINISHING            = 0x08;
		
		private static  int INIT_STATE              = 0x00;
		//private static  int SETDICT_STATE           = 0x01;
		private static  int BUSY_STATE              = 0x10;
		private static  int FLUSHING_STATE          = 0x14;
		private static  int FINISHING_STATE         = 0x1c;
		private static  int FINISHED_STATE          = 0x1e;
		private static  int CLOSED_STATE            = 0x7f;
		
		private int level;
		private bool noZlibHeaderOrFooter;
		private int state;
		private long totalOut;
		private DeflaterPending pending;
		private DeflaterEngine engine;

		public Deflater(int level, bool noZlibHeaderOrFooter)
		{
			if (level == DEFAULT_COMPRESSION) 
			{
				level = 6;
			} 
			else if (level < NO_COMPRESSION || level > BEST_COMPRESSION) 
			{
				throw new ArgumentOutOfRangeException("level");
			}
			
			pending = new DeflaterPending();
			engine = new DeflaterEngine(pending);
			this.noZlibHeaderOrFooter = noZlibHeaderOrFooter;
			SetStrategy(DeflateStrategy.Default);
			SetLevel(level);
			Reset();
		}
		
		public void Reset()
		{
			state = (noZlibHeaderOrFooter ? BUSY_STATE : INIT_STATE);
			totalOut = 0;
			pending.Reset();
			engine.Reset();
		}

		public long TotalOut 
		{
			get 
			{
				return totalOut;
			}
		}
		
		public void Flush() 
		{
			state |= IS_FLUSHING;
		}
		
		public void Finish() 
		{
			state |= IS_FLUSHING | IS_FINISHING;
		}
		
		public bool IsFinished 
		{
			get 
			{
				return state == FINISHED_STATE && pending.IsFlushed;
			}
		}
		
		public bool IsNeedingInput 
		{
			get 
			{
				return engine.NeedsInput();
			}
		}
		
		public void SetInput(byte[] input, int off, int len)
		{
			if ((state & IS_FINISHING) != 0) 
			{
				throw new InvalidOperationException("finish()/end() already called");
			}
			engine.SetInput(input, off, len);
		}
		
		public void SetLevel(int lvl)
		{
			if (lvl == DEFAULT_COMPRESSION) 
			{
				lvl = 6;
			} 
			else if (lvl < NO_COMPRESSION || lvl > BEST_COMPRESSION) 
			{
				throw new ArgumentOutOfRangeException("lvl");
			}
			
			if (level != lvl) 
			{
				level = lvl;
				engine.SetLevel(lvl);
			}
		}

		public void SetStrategy(DeflateStrategy strategy)
		{
			engine.Strategy = strategy;
		}

		public int Deflate(byte[] output, int offset, int length)
		{
			int origLength = length;
			
			if (state == CLOSED_STATE) 
			{
				throw new InvalidOperationException("Deflater closed");
			}
			
			if (state < BUSY_STATE) 
			{
				int header = (DEFLATED +
					((DeflaterConstants.MAX_WBITS - 8) << 4)) << 8;
				int level_flags = (level - 1) >> 1;
				if (level_flags < 0 || level_flags > 3) 
				{
					level_flags = 3;
				}
				header |= level_flags << 6;
				if ((state & IS_SETDICT) != 0) 
				{
					header |= DeflaterConstants.PRESET_DICT;
				}
				header += 31 - (header % 31);
				
				
				pending.WriteShortMSB(header);
				if ((state & IS_SETDICT) != 0) 
				{
					int chksum = engine.Adler;
					engine.ResetAdler();
					pending.WriteShortMSB(chksum >> 16);
					pending.WriteShortMSB(chksum & 0xffff);
				}
				
				state = BUSY_STATE | (state & (IS_FLUSHING | IS_FINISHING));
			}
			
			for (;;) 
			{
				int count = pending.Flush(output, offset, length);
				offset   += count;
				totalOut += count;
				length   -= count;
				
				if (length == 0 || state == FINISHED_STATE) 
				{
					break;
				}
				
				if (!engine.Deflate((state & IS_FLUSHING) != 0, (state & IS_FINISHING) != 0)) 
				{
					if (state == BUSY_STATE) 
					{
						return origLength - length;
					} 
					else if (state == FLUSHING_STATE) 
					{
						if (level != NO_COMPRESSION) 
						{
							int neededbits = 8 + ((-pending.BitCount) & 7);
							while (neededbits > 0) 
							{
								pending.WriteBits(2, 10);
								neededbits -= 10;
							}
						}
						state = BUSY_STATE;
					} 
					else if (state == FINISHING_STATE) 
					{
						pending.AlignToByte();

						// Compressed data is complete.  Write footer information if required.
						if (!noZlibHeaderOrFooter) 
						{
							int adler = engine.Adler;
							pending.WriteShortMSB(adler >> 16);
							pending.WriteShortMSB(adler & 0xffff);
						}
						state = FINISHED_STATE;
					}
				}
			}
			return origLength - length;
		}
	}

	#endregion

	#region DeflaterEngine
	//
	// DeflaterEngine
	//
	public enum DeflateStrategy 
	{
		Default  = 0,
		Filtered = 1,
		HuffmanOnly = 2
	}

	public class DeflaterEngine : DeflaterConstants 
	{
		static int TOO_FAR = 4096;
		int ins_h;
		short[] head;
		short[] prev;
		
		int    matchStart;
		int    matchLen;
		bool   prevAvailable;
		int    blockStart;

		int    strstart;
		int    lookahead;
		byte[] window;
		
		DeflateStrategy strategy;
		int max_chain, max_lazy, niceLength, goodLength;
		int comprFunc;
		byte[] inputBuf;
		int totalIn;
		int inputOff;
		int inputEnd;
		
		DeflaterPending pending;
		DeflaterHuffman huffman;
		
		Adler32 adler;
		
		public DeflaterEngine(DeflaterPending pending) 
		{
			this.pending = pending;
			huffman = new DeflaterHuffman(pending);
			adler = new Adler32();
			
			window = new byte[2 * WSIZE];
			head   = new short[HASH_SIZE];
			prev   = new short[WSIZE];
			
			blockStart = strstart = 1;
		}

		public void Reset()
		{
			huffman.Reset();
			adler.Reset();
			blockStart = strstart = 1;
			lookahead = 0;
			totalIn   = 0;
			prevAvailable = false;
			matchLen = MIN_MATCH - 1;
			
			for (int i = 0; i < HASH_SIZE; i++) 
			{
				head[i] = 0;
			}
			
			for (int i = 0; i < WSIZE; i++) 
			{
				prev[i] = 0;
			}
		}

		public void ResetAdler()
		{
			adler.Reset();
		}

		public int Adler 
		{
			get 
			{
				return (int)adler.Value;
			}
		}

		public DeflateStrategy Strategy 
		{
			get 
			{
				return strategy;
			}
			set 
			{
				strategy = value;
			}
		}
		
		public void SetLevel(int lvl)
		{
			goodLength = DeflaterConstants.GOOD_LENGTH[lvl];
			max_lazy   = DeflaterConstants.MAX_LAZY[lvl];
			niceLength = DeflaterConstants.NICE_LENGTH[lvl];
			max_chain  = DeflaterConstants.MAX_CHAIN[lvl];
			
			if (DeflaterConstants.COMPR_FUNC[lvl] != comprFunc) 
			{
				switch (comprFunc) 
				{
					case DEFLATE_STORED:
						if (strstart > blockStart) 
						{
							huffman.FlushStoredBlock(window, blockStart,
								strstart - blockStart, false);
							blockStart = strstart;
						}
						UpdateHash();
						break;
					case DEFLATE_FAST:
						if (strstart > blockStart) 
						{
							huffman.FlushBlock(window, blockStart, strstart - blockStart,
								false);
							blockStart = strstart;
						}
						break;
					case DEFLATE_SLOW:
						if (prevAvailable) 
						{
							huffman.TallyLit(window[strstart-1] & 0xff);
						}
						if (strstart > blockStart) 
						{
							huffman.FlushBlock(window, blockStart, strstart - blockStart, false);
							blockStart = strstart;
						}
						prevAvailable = false;
						matchLen = MIN_MATCH - 1;
						break;
				}
				comprFunc = COMPR_FUNC[lvl];
			}
		}
		
		void UpdateHash() 
		{
			ins_h = (window[strstart] << HASH_SHIFT) ^ window[strstart + 1];
		}
		
		int InsertString() 
		{
			short match;
			int hash = ((ins_h << HASH_SHIFT) ^ window[strstart + (MIN_MATCH -1)]) & HASH_MASK;
			prev[strstart & WMASK] = match = head[hash];
			head[hash] = (short)strstart;
			ins_h = hash;
			return match & 0xffff;
		}
		
		void SlideWindow()
		{
			Array.Copy(window, WSIZE, window, 0, WSIZE);
			matchStart -= WSIZE;
			strstart   -= WSIZE;
			blockStart -= WSIZE;
			
			for (int i = 0; i < HASH_SIZE; ++i) 
			{
				int m = head[i] & 0xffff;
				head[i] = (short)(m >= WSIZE ? (m - WSIZE) : 0);
			}
			
			for (int i = 0; i < WSIZE; i++) 
			{
				int m = prev[i] & 0xffff;
				prev[i] = (short)(m >= WSIZE ? (m - WSIZE) : 0);
			}
		}
		
		public void FillWindow()
		{
			if (strstart >= WSIZE + MAX_DIST) 
			{
				SlideWindow();
			}
			
			while (lookahead < DeflaterConstants.MIN_LOOKAHEAD && inputOff < inputEnd) 
			{
				int more = 2 * WSIZE - lookahead - strstart;
				
				if (more > inputEnd - inputOff) 
				{
					more = inputEnd - inputOff;
				}
				
				System.Array.Copy(inputBuf, inputOff, window, strstart + lookahead, more);
				adler.Update(inputBuf, inputOff, more);
				
				inputOff += more;
				totalIn  += more;
				lookahead += more;
			}
			
			if (lookahead >= MIN_MATCH) 
			{
				UpdateHash();
			}
		}
		
		bool FindLongestMatch(int curMatch) 
		{
			int chainLength = this.max_chain;
			int niceLength  = this.niceLength;
			short[] prev    = this.prev;
			int scan        = this.strstart;
			int match;
			int best_end = this.strstart + matchLen;
			int best_len = Math.Max(matchLen, MIN_MATCH - 1);
			
			int limit = Math.Max(strstart - MAX_DIST, 0);
			
			int strend = strstart + MAX_MATCH - 1;
			byte scan_end1 = window[best_end - 1];
			byte scan_end  = window[best_end];
			
			if (best_len >= this.goodLength) 
			{
				chainLength >>= 2;
			}
			
			if (niceLength > lookahead) 
			{
				niceLength = lookahead;
			}
			
			do 
			{
				if (window[curMatch + best_len] != scan_end      || 
					window[curMatch + best_len - 1] != scan_end1 || 
					window[curMatch] != window[scan]             || 
					window[curMatch + 1] != window[scan + 1]) 
				{
					continue;
				}
				
				match = curMatch + 2;
				scan += 2;
				
			while (window[++scan] == window[++match] && 
				window[++scan] == window[++match] && 
				window[++scan] == window[++match] && 
				window[++scan] == window[++match] && 
				window[++scan] == window[++match] && 
				window[++scan] == window[++match] && 
				window[++scan] == window[++match] && 
				window[++scan] == window[++match] && scan < strend) ;
				
				if (scan > best_end) 
				{
					matchStart = curMatch;
					best_end = scan;
					best_len = scan - strstart;
					
					if (best_len >= niceLength) 
					{
						break;
					}
					
					scan_end1  = window[best_end - 1];
					scan_end   = window[best_end];
				}
				scan = strstart;
			} while ((curMatch = (prev[curMatch & WMASK] & 0xffff)) > limit && --chainLength != 0);
			
			matchLen = Math.Min(best_len, lookahead);
			return matchLen >= MIN_MATCH;
		}

		bool DeflateStored(bool flush, bool finish)
		{
			if (!flush && lookahead == 0) 
			{
				return false;
			}
			
			strstart += lookahead;
			lookahead = 0;
			
			int storedLen = strstart - blockStart;
			
			if ((storedLen >= DeflaterConstants.MAX_BLOCK_SIZE) || /* Block is full */
				(blockStart < WSIZE && storedLen >= MAX_DIST) ||   /* Block may move out of window */
				flush) 
			{
				bool lastBlock = finish;
				if (storedLen > DeflaterConstants.MAX_BLOCK_SIZE) 
				{
					storedLen = DeflaterConstants.MAX_BLOCK_SIZE;
					lastBlock = false;
				}
				
				huffman.FlushStoredBlock(window, blockStart, storedLen, lastBlock);
				blockStart += storedLen;
				return !lastBlock;
			}
			return true;
		}
		
		private bool DeflateFast(bool flush, bool finish)
		{
			if (lookahead < MIN_LOOKAHEAD && !flush) 
			{
				return false;
			}
			
			while (lookahead >= MIN_LOOKAHEAD || flush) 
			{
				if (lookahead == 0) 
				{
					huffman.FlushBlock(window, blockStart, strstart - blockStart, finish);
					blockStart = strstart;
					return false;
				}
				
				if (strstart > 2 * WSIZE - MIN_LOOKAHEAD) 
				{
					SlideWindow();
				}
				
				int hashHead;
				if (lookahead >= MIN_MATCH && 
					(hashHead = InsertString()) != 0 && 
					strategy != DeflateStrategy.HuffmanOnly &&
					strstart - hashHead <= MAX_DIST && 
					FindLongestMatch(hashHead)) 
				{
					if (huffman.TallyDist(strstart - matchStart, matchLen)) 
					{
						bool lastBlock = finish && lookahead == 0;
						huffman.FlushBlock(window, blockStart, strstart - blockStart, lastBlock);
						blockStart = strstart;
					}
					
					lookahead -= matchLen;
					if (matchLen <= max_lazy && lookahead >= MIN_MATCH) 
					{
						while (--matchLen > 0) 
						{
							++strstart;
							InsertString();
						}
						++strstart;
					} 
					else 
					{
						strstart += matchLen;
						if (lookahead >= MIN_MATCH - 1) 
						{
							UpdateHash();
						}
					}
					matchLen = MIN_MATCH - 1;
					continue;
				} 
				else 
				{
					huffman.TallyLit(window[strstart] & 0xff);
					++strstart;
					--lookahead;
				}
				
				if (huffman.IsFull()) 
				{
					bool lastBlock = finish && lookahead == 0;
					huffman.FlushBlock(window, blockStart, strstart - blockStart, lastBlock);
					blockStart = strstart;
					return !lastBlock;
				}
			}
			return true;
		}
		
		bool DeflateSlow(bool flush, bool finish)
		{
			if (lookahead < MIN_LOOKAHEAD && !flush) 
			{
				return false;
			}
			
			while (lookahead >= MIN_LOOKAHEAD || flush) 
			{
				if (lookahead == 0) 
				{
					if (prevAvailable) 
					{
						huffman.TallyLit(window[strstart-1] & 0xff);
					}
					prevAvailable = false;
					
					huffman.FlushBlock(window, blockStart, strstart - blockStart,
						finish);
					blockStart = strstart;
					return false;
				}
				
				if (strstart >= 2 * WSIZE - MIN_LOOKAHEAD) 
				{
					SlideWindow();
				}
				
				int prevMatch = matchStart;
				int prevLen = matchLen;
				if (lookahead >= MIN_MATCH) 
				{
					int hashHead = InsertString();
					if (strategy != DeflateStrategy.HuffmanOnly && hashHead != 0 && strstart - hashHead <= MAX_DIST && FindLongestMatch(hashHead)) 
					{
						if (matchLen <= 5 && (strategy == DeflateStrategy.Filtered || (matchLen == MIN_MATCH && strstart - matchStart > TOO_FAR))) 
						{
							matchLen = MIN_MATCH - 1;
						}
					}
				}
				
				if (prevLen >= MIN_MATCH && matchLen <= prevLen) 
				{
					huffman.TallyDist(strstart - 1 - prevMatch, prevLen);
					prevLen -= 2;
					do 
					{
						strstart++;
						lookahead--;
						if (lookahead >= MIN_MATCH) 
						{
							InsertString();
						}
					} while (--prevLen > 0);
					strstart ++;
					lookahead--;
					prevAvailable = false;
					matchLen = MIN_MATCH - 1;
				} 
				else 
				{
					if (prevAvailable) 
					{
						huffman.TallyLit(window[strstart-1] & 0xff);
					}
					prevAvailable = true;
					strstart++;
					lookahead--;
				}
				
				if (huffman.IsFull()) 
				{
					int len = strstart - blockStart;
					if (prevAvailable) 
					{
						len--;
					}
					bool lastBlock = (finish && lookahead == 0 && !prevAvailable);
					huffman.FlushBlock(window, blockStart, len, lastBlock);
					blockStart += len;
					return !lastBlock;
				}
			}
			return true;
		}
		
		public bool Deflate(bool flush, bool finish)
		{
			bool progress;
			do 
			{
				FillWindow();
				bool canFlush = flush && inputOff == inputEnd;
				switch (comprFunc) 
				{
					case DEFLATE_STORED:
						progress = DeflateStored(canFlush, finish);
						break;
					case DEFLATE_FAST:
						progress = DeflateFast(canFlush, finish);
						break;
					case DEFLATE_SLOW:
						progress = DeflateSlow(canFlush, finish);
						break;
					default:
						throw new InvalidOperationException("unknown comprFunc");
				}
			} while (pending.IsFlushed && progress); /* repeat while we have no pending output and progress was made */
			return progress;
		}

		public void SetInput(byte[] buf, int off, int len)
		{
			if (inputOff < inputEnd) 
			{
				throw new InvalidOperationException("Old input was not completely processed");
			}
			
			int end = off + len;
			
			if (0 > off || off > end || end > buf.Length) 
			{
				throw new ArgumentOutOfRangeException();
			}
			
			inputBuf = buf;
			inputOff = off;
			inputEnd = end;
		}

		public bool NeedsInput()
		{
			return inputEnd == inputOff;
		}
	}
	#endregion

	#region DeflaterPending
	//
	// DeflaterPending
	//
	public class DeflaterPending : PendingBuffer
	{
		public DeflaterPending() : base(DeflaterConstants.PENDING_BUF_SIZE)
		{
		}
	}
	#endregion

	#region DeflaterHuffman
	//
	// DeflaterHuffman
	//
	public class DeflaterHuffman
	{
		static  int BUFSIZE = 1 << (DeflaterConstants.DEFAULT_MEM_LEVEL + 6);
		static  int LITERAL_NUM = 286;
		static  int DIST_NUM = 30;
		static  int BITLEN_NUM = 19;
		static  int REP_3_6    = 16;
		static  int REP_3_10   = 17;
		static  int REP_11_138 = 18;
		static  int EOF_SYMBOL = 256;
		static  int[] BL_ORDER = { 16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15 };
		
		static byte[] bit4Reverse = {
										0,
										8,
										4,
										12,
										2,
										10,
										6,
										14,
										1,
										9,
										5,
										13,
										3,
										11,
										7,
										15
									};
		
		public class Tree 
		{
			public short[] freqs;
			public byte[]  length;
			public int     minNumCodes;
			public int     numCodes;
			
			short[] codes;
			int[]   bl_counts;
			int     maxLength;
			DeflaterHuffman dh;
			
			public Tree(DeflaterHuffman dh, int elems, int minCodes, int maxLength) 
			{
				this.dh =  dh;
				this.minNumCodes = minCodes;
				this.maxLength  = maxLength;
				freqs  = new short[elems];
				bl_counts = new int[maxLength];
			}
			
			public void Reset() 
			{
				for (int i = 0; i < freqs.Length; i++) 
				{
					freqs[i] = 0;
				}
				codes = null;
				length = null;
			}
			
			public void WriteSymbol(int code)
			{
				dh.pending.WriteBits(codes[code] & 0xffff, length[code]);
			}

			public void SetStaticCodes(short[] stCodes, byte[] stLength)
			{
				codes = stCodes;
				length = stLength;
			}
			
			public void BuildCodes() 
			{
				int numSymbols = freqs.Length;
				int[] nextCode = new int[maxLength];
				int code = 0;
				codes = new short[freqs.Length];
				
				for (int bits = 0; bits < maxLength; bits++) 
				{
					nextCode[bits] = code;
					code += bl_counts[bits] << (15 - bits);
				}
				if (code != 65536) 
				{
					throw new SharpZipBaseException("Inconsistent bl_counts!");
				}
				
				for (int i=0; i < numCodes; i++) 
				{
					int bits = length[i];
					if (bits > 0) 
					{
						codes[i] = BitReverse(nextCode[bits-1]);
						nextCode[bits-1] += 1 << (16 - bits);
					}
				}
			}
			
			void BuildLength(int[] childs)
			{
				this.length = new byte [freqs.Length];
				int numNodes = childs.Length / 2;
				int numLeafs = (numNodes + 1) / 2;
				int overflow = 0;
				
				for (int i = 0; i < maxLength; i++) 
				{
					bl_counts[i] = 0;
				}
				
				int[] lengths = new int[numNodes];
				lengths[numNodes-1] = 0;
				
				for (int i = numNodes - 1; i >= 0; i--) 
				{
					if (childs[2*i+1] != -1) 
					{
						int bitLength = lengths[i] + 1;
						if (bitLength > maxLength) 
						{
							bitLength = maxLength;
							overflow++;
						}
						lengths[childs[2*i]] = lengths[childs[2*i+1]] = bitLength;
					} 
					else 
					{
						int bitLength = lengths[i];
						bl_counts[bitLength - 1]++;
						this.length[childs[2*i]] = (byte) lengths[i];
					}
				}
				
				if (overflow == 0) 
				{
					return;
				}
				
				int incrBitLen = maxLength - 1;
				do 
				{
				while (bl_counts[--incrBitLen] == 0)
					;
					
					do 
					{
						bl_counts[incrBitLen]--;
						bl_counts[++incrBitLen]++;
						overflow -= 1 << (maxLength - 1 - incrBitLen);
					} while (overflow > 0 && incrBitLen < maxLength - 1);
				} while (overflow > 0);
				
				bl_counts[maxLength-1] += overflow;
				bl_counts[maxLength-2] -= overflow;
				
				int nodePtr = 2 * numLeafs;
				for (int bits = maxLength; bits != 0; bits--) 
				{
					int n = bl_counts[bits-1];
					while (n > 0) 
					{
						int childPtr = 2*childs[nodePtr++];
						if (childs[childPtr + 1] == -1) 
						{
							length[childs[childPtr]] = (byte) bits;
							n--;
						}
					}
				}
			}
			
			public void BuildTree()
			{
				int numSymbols = freqs.Length;
				
				int[] heap = new int[numSymbols];
				int heapLen = 0;
				int maxCode = 0;
				for (int n = 0; n < numSymbols; n++) 
				{
					int freq = freqs[n];
					if (freq != 0) 
					{
						int pos = heapLen++;
						int ppos;
						while (pos > 0 && freqs[heap[ppos = (pos - 1) / 2]] > freq) 
						{
							heap[pos] = heap[ppos];
							pos = ppos;
						}
						heap[pos] = n;
						
						maxCode = n;
					}
				}
				
				while (heapLen < 2) 
				{
					int node = maxCode < 2 ? ++maxCode : 0;
					heap[heapLen++] = node;
				}
				
				numCodes = Math.Max(maxCode + 1, minNumCodes);
				
				int numLeafs = heapLen;
				int[] childs = new int[4*heapLen - 2];
				int[] values = new int[2*heapLen - 1];
				int numNodes = numLeafs;
				for (int i = 0; i < heapLen; i++) 
				{
					int node = heap[i];
					childs[2*i]   = node;
					childs[2*i+1] = -1;
					values[i] = freqs[node] << 8;
					heap[i] = i;
				}
				
				do 
				{
					int first = heap[0];
					int last  = heap[--heapLen];
					
					int ppos = 0;
					int path = 1;
					
				while (path < heapLen) 
				{
					if (path + 1 < heapLen && values[heap[path]] > values[heap[path+1]]) 
					{
						path++;
					}
							
					heap[ppos] = heap[path];
					ppos = path;
					path = path * 2 + 1;
				}
						
					int lastVal = values[last];
				while ((path = ppos) > 0 && values[heap[ppos = (path - 1)/2]] > lastVal) 
				{
					heap[path] = heap[ppos];
				}
					heap[path] = last;
					
					
					int second = heap[0];
					
					last = numNodes++;
					childs[2*last] = first;
					childs[2*last+1] = second;
					int mindepth = Math.Min(values[first] & 0xff, values[second] & 0xff);
					values[last] = lastVal = values[first] + values[second] - mindepth + 1;
					
					ppos = 0;
					path = 1;
					
				while (path < heapLen) 
				{
					if (path + 1 < heapLen && values[heap[path]] > values[heap[path+1]]) 
					{
						path++;
					}
							
					heap[ppos] = heap[path];
					ppos = path;
					path = ppos * 2 + 1;
				}
						
				while ((path = ppos) > 0 && values[heap[ppos = (path - 1)/2]] > lastVal) 
				{
					heap[path] = heap[ppos];
				}
					heap[path] = last;
				} while (heapLen > 1);
				
				if (heap[0] != childs.Length / 2 - 1) 
				{
					throw new SharpZipBaseException("Heap invariant violated");
				}
				
				BuildLength(childs);
			}
			
			public int GetEncodedLength()
			{
				int len = 0;
				for (int i = 0; i < freqs.Length; i++) 
				{
					len += freqs[i] * length[i];
				}
				return len;
			}
			
			public void CalcBLFreq(Tree blTree) 
			{
				int max_count;
				int min_count;
				int count;
				int curlen = -1;
				
				int i = 0;
				while (i < numCodes) 
				{
					count = 1;
					int nextlen = length[i];
					if (nextlen == 0) 
					{
						max_count = 138;
						min_count = 3;
					} 
					else 
					{
						max_count = 6;
						min_count = 3;
						if (curlen != nextlen) 
						{
							blTree.freqs[nextlen]++;
							count = 0;
						}
					}
					curlen = nextlen;
					i++;
					
					while (i < numCodes && curlen == length[i]) 
					{
						i++;
						if (++count >= max_count) 
						{
							break;
						}
					}
					
					if (count < min_count) 
					{
						blTree.freqs[curlen] += (short)count;
					} 
					else if (curlen != 0) 
					{
						blTree.freqs[REP_3_6]++;
					} 
					else if (count <= 10) 
					{
						blTree.freqs[REP_3_10]++;
					} 
					else 
					{
						blTree.freqs[REP_11_138]++;
					}
				}
			}
		
			public void WriteTree(Tree blTree)
			{
				int max_count;
				int min_count;
				int count;
				int curlen = -1;
				
				int i = 0;
				while (i < numCodes) 
				{
					count = 1;
					int nextlen = length[i];
					if (nextlen == 0) 
					{
						max_count = 138;
						min_count = 3;
					} 
					else 
					{
						max_count = 6;
						min_count = 3;
						if (curlen != nextlen) 
						{
							blTree.WriteSymbol(nextlen);
							count = 0;
						}
					}
					curlen = nextlen;
					i++;
					
					while (i < numCodes && curlen == length[i]) 
					{
						i++;
						if (++count >= max_count) 
						{
							break;
						}
					}
					
					if (count < min_count) 
					{
						while (count-- > 0) 
						{
							blTree.WriteSymbol(curlen);
						}
					} 
					else if (curlen != 0) 
					{
						blTree.WriteSymbol(REP_3_6);
						dh.pending.WriteBits(count - 3, 2);
					} 
					else if (count <= 10) 
					{
						blTree.WriteSymbol(REP_3_10);
						dh.pending.WriteBits(count - 3, 3);
					} 
					else 
					{
						blTree.WriteSymbol(REP_11_138);
						dh.pending.WriteBits(count - 11, 7);
					}
				}
			}
		}
		
		public DeflaterPending pending;
		
		Tree literalTree, distTree, blTree;
		
		short[] d_buf;
		byte[]  l_buf;
		int last_lit;
		int extra_bits;
		
		static short[] staticLCodes;
		static byte[]  staticLLength;
		static short[] staticDCodes;
		static byte[]  staticDLength;
		
		public static short BitReverse(int toReverse) 
		{
			return (short) (bit4Reverse[toReverse & 0xF] << 12 | 
				bit4Reverse[(toReverse >> 4) & 0xF] << 8 | 
				bit4Reverse[(toReverse >> 8) & 0xF] << 4 |
				bit4Reverse[toReverse >> 12]);
		}
		
		
		static DeflaterHuffman() 
		{
			staticLCodes = new short[LITERAL_NUM];
			staticLLength = new byte[LITERAL_NUM];
			int i = 0;
			while (i < 144) 
			{
				staticLCodes[i] = BitReverse((0x030 + i) << 8);
				staticLLength[i++] = 8;
			}
			while (i < 256) 
			{
				staticLCodes[i] = BitReverse((0x190 - 144 + i) << 7);
				staticLLength[i++] = 9;
			}
			while (i < 280) 
			{
				staticLCodes[i] = BitReverse((0x000 - 256 + i) << 9);
				staticLLength[i++] = 7;
			}
			while (i < LITERAL_NUM) 
			{
				staticLCodes[i] = BitReverse((0x0c0 - 280 + i)  << 8);
				staticLLength[i++] = 8;
			}
			
			staticDCodes = new short[DIST_NUM];
			staticDLength = new byte[DIST_NUM];
			for (i = 0; i < DIST_NUM; i++) 
			{
				staticDCodes[i] = BitReverse(i << 11);
				staticDLength[i] = 5;
			}
		}
		
		public DeflaterHuffman(DeflaterPending pending)
		{
			this.pending = pending;
			
			literalTree = new Tree(this, LITERAL_NUM, 257, 15);
			distTree    = new Tree(this, DIST_NUM, 1, 15);
			blTree      = new Tree(this, BITLEN_NUM, 4, 7);
			
			d_buf = new short[BUFSIZE];
			l_buf = new byte [BUFSIZE];
		}

		public void Reset() 
		{
			last_lit = 0;
			extra_bits = 0;
			literalTree.Reset();
			distTree.Reset();
			blTree.Reset();
		}
		
		int Lcode(int len) 
		{
			if (len == 255) 
			{
				return 285;
			}
			
			int code = 257;
			while (len >= 8) 
			{
				code += 4;
				len >>= 1;
			}
			return code + len;
		}
		
		int Dcode(int distance) 
		{
			int code = 0;
			while (distance >= 4) 
			{
				code += 2;
				distance >>= 1;
			}
			return code + distance;
		}

		public void SendAllTrees(int blTreeCodes)
		{
			blTree.BuildCodes();
			literalTree.BuildCodes();
			distTree.BuildCodes();
			pending.WriteBits(literalTree.numCodes - 257, 5);
			pending.WriteBits(distTree.numCodes - 1, 5);
			pending.WriteBits(blTreeCodes - 4, 4);
			for (int rank = 0; rank < blTreeCodes; rank++) 
			{
				pending.WriteBits(blTree.length[BL_ORDER[rank]], 3);
			}
			literalTree.WriteTree(blTree);
			distTree.WriteTree(blTree);
		}

		public void CompressBlock()
		{
			for (int i = 0; i < last_lit; i++) 
			{
				int litlen = l_buf[i] & 0xff;
				int dist = d_buf[i];
				if (dist-- != 0) 
				{
					int lc = Lcode(litlen);
					literalTree.WriteSymbol(lc);
					
					int bits = (lc - 261) / 4;
					if (bits > 0 && bits <= 5) 
					{
						pending.WriteBits(litlen & ((1 << bits) - 1), bits);
					}
					
					int dc = Dcode(dist);
					distTree.WriteSymbol(dc);
					
					bits = dc / 2 - 1;
					if (bits > 0) 
					{
						pending.WriteBits(dist & ((1 << bits) - 1), bits);
					}
				} 
				else 
				{
					literalTree.WriteSymbol(litlen);
				}
			}
			literalTree.WriteSymbol(EOF_SYMBOL);
		}
		
		public void FlushStoredBlock(byte[] stored, int storedOffset, int storedLength, bool lastBlock)
		{
			pending.WriteBits((DeflaterConstants.STORED_BLOCK << 1) + (lastBlock ? 1 : 0), 3);
			pending.AlignToByte();
			pending.WriteShort(storedLength);
			pending.WriteShort(~storedLength);
			pending.WriteBlock(stored, storedOffset, storedLength);
			Reset();
		}

		public void FlushBlock(byte[] stored, int storedOffset, int storedLength, bool lastBlock)
		{
			literalTree.freqs[EOF_SYMBOL]++;
			
			literalTree.BuildTree();
			distTree.BuildTree();
			
			literalTree.CalcBLFreq(blTree);
			distTree.CalcBLFreq(blTree);
			
			blTree.BuildTree();
			
			int blTreeCodes = 4;
			for (int i = 18; i > blTreeCodes; i--) 
			{
				if (blTree.length[BL_ORDER[i]] > 0) 
				{
					blTreeCodes = i+1;
				}
			}
			int opt_len = 14 + blTreeCodes * 3 + blTree.GetEncodedLength() + 
				literalTree.GetEncodedLength() + distTree.GetEncodedLength() + 
				extra_bits;
			
			int static_len = extra_bits;
			for (int i = 0; i < LITERAL_NUM; i++) 
			{
				static_len += literalTree.freqs[i] * staticLLength[i];
			}
			for (int i = 0; i < DIST_NUM; i++) 
			{
				static_len += distTree.freqs[i] * staticDLength[i];
			}
			if (opt_len >= static_len) 
			{
				opt_len = static_len;
			}
			
			if (storedOffset >= 0 && storedLength + 4 < opt_len >> 3) 
			{
				FlushStoredBlock(stored, storedOffset, storedLength, lastBlock);
			} 
			else if (opt_len == static_len) 
			{
				pending.WriteBits((DeflaterConstants.STATIC_TREES << 1) + (lastBlock ? 1 : 0), 3);
				literalTree.SetStaticCodes(staticLCodes, staticLLength);
				distTree.SetStaticCodes(staticDCodes, staticDLength);
				CompressBlock();
				Reset();
			} 
			else 
			{
				pending.WriteBits((DeflaterConstants.DYN_TREES << 1) + (lastBlock ? 1 : 0), 3);
				SendAllTrees(blTreeCodes);
				CompressBlock();
				Reset();
			}
		}
		
		public bool IsFull()
		{
			return last_lit >= BUFSIZE;
		}
		
		public bool TallyLit(int lit)
		{
			d_buf[last_lit] = 0;
			l_buf[last_lit++] = (byte)lit;
			literalTree.freqs[lit]++;
			return IsFull();
		}
		
		public bool TallyDist(int dist, int len)
		{
			d_buf[last_lit]   = (short)dist;
			l_buf[last_lit++] = (byte)(len - 3);
			
			int lc = Lcode(len - 3);
			literalTree.freqs[lc]++;
			if (lc >= 265 && lc < 285) 
			{
				extra_bits += (lc - 261) / 4;
			}
			
			int dc = Dcode(dist - 1);
			distTree.freqs[dc]++;
			if (dc >= 4) 
			{
				extra_bits += dc / 2 - 1;
			}
			return IsFull();
		}
	}
	#endregion

	#region PendingBuffer
	//
	// PendingBuffer
	//
	public class PendingBuffer
	{
		protected byte[] buf;
		
		int    start;
		int    end;
		
		uint    bits;
		int    bitCount;

		public PendingBuffer(int bufsize)
		{
			buf = new byte[bufsize];
		}

		public void Reset() 
		{
			start = end = bitCount = 0;
		}

		public void WriteByte(int b)
		{
			if (start != 0) 
			{
				throw new SharpZipBaseException();
			}
			buf[end++] = (byte) b;
		}

		public void WriteShort(int s)
		{
			if (start != 0) 
			{
				throw new SharpZipBaseException();
			}
			buf[end++] = (byte) s;
			buf[end++] = (byte) (s >> 8);
		}

		public void WriteInt(int s)
		{
			if (start != 0) 
			{
				throw new SharpZipBaseException();
			}
			buf[end++] = (byte) s;
			buf[end++] = (byte) (s >> 8);
			buf[end++] = (byte) (s >> 16);
			buf[end++] = (byte) (s >> 24);
		}
		
		public void WriteBlock(byte[] block, int offset, int len)
		{
			if (start != 0) 
			{
				throw new SharpZipBaseException();
			}
			System.Array.Copy(block, offset, buf, end, len);
			end += len;
		}

		public int BitCount 
		{
			get 
			{
				return bitCount;
			}
		}
		
		public void AlignToByte() 
		{
			if (start != 0) 
			{
				throw new SharpZipBaseException();
			}
			if (bitCount > 0) 
			{
				buf[end++] = (byte) bits;
				if (bitCount > 8) 
				{
					buf[end++] = (byte) (bits >> 8);
				}
			}
			bits = 0;
			bitCount = 0;
		}

		public void WriteBits(int b, int count)
		{
			if (start != 0) 
			{
				throw new SharpZipBaseException();
			}
			bits |= (uint)(b << bitCount);
			bitCount += count;
			if (bitCount >= 16) 
			{
				buf[end++] = (byte) bits;
				buf[end++] = (byte) (bits >> 8);
				bits >>= 16;
				bitCount -= 16;
			}
		}

		public void WriteShortMSB(int s) 
		{
			if (start != 0) 
			{
				throw new SharpZipBaseException();
			}
			buf[end++] = (byte) (s >> 8);
			buf[end++] = (byte) s;
		}
		
		public bool IsFlushed 
		{
			get 
			{
				return end == 0;
			}
		}
		
		public int Flush(byte[] output, int offset, int length) 
		{
			if (bitCount >= 8) 
			{
				buf[end++] = (byte) bits;
				bits >>= 8;
				bitCount -= 8;
			}
			if (length > end - start) 
			{
				length = end - start;
				System.Array.Copy(buf, start, output, offset, length);
				start = 0;
				end = 0;
			} 
			else 
			{
				System.Array.Copy(buf, start, output, offset, length);
				start += length;
			}
			return length;
		}

		public byte[] ToByteArray()
		{
			byte[] ret = new byte[end - start];
			System.Array.Copy(buf, start, ret, 0, ret.Length);
			start = 0;
			end = 0;
			return ret;
		}
	}
	#endregion

	#endregion
}
