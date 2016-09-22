using System;
using System.Text;
using AM.Utils;

namespace AM.Services
{
	public class EncryptionService
	{
		#region Properties
		public static string Key
		{
			get
			{
				return Utils.CustomEncrypt.PassPhrase;
			}
			set
			{
				Utils.CustomEncrypt.PassPhrase = value;
			}
		}
		#endregion

		#region Methods
		public static decimal Amt(object amtKey)
		{
			if (amtKey == null || amtKey.ToString() == "" || amtKey == DBNull.Value)
				return 0;

			return Decrypt(amtKey.ToString()).ToDecimal();
		}

		public static string Amt(decimal amt)
		{
			return Encrypt(amt);
		}
		#endregion

		#region Encryption
		public static string Encrypt(decimal dec)
		{
			return Encrypt(dec.ToString("N2"));
		}

		public static string Encrypt(byte[] data)
		{
			return Encrypt(Encoding.Default.GetString(data));
		}

		public static string Encrypt(string text)
		{
			return CustomEncrypt.Encrypt(text);
		}
		#endregion

		#region Decryption
		public static string Decrypt(byte[] data)
		{
			return Decrypt(Encoding.Default.GetString(data));
		}

		public static string Decrypt(string cryptedString)
		{
			return CustomEncrypt.Decrypt(cryptedString);
		}
		#endregion
	}
}
