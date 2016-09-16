using System.Text.RegularExpressions;
using System.Globalization;
using System;

namespace Corpnet.Profiling.Extensions
{
    public static class HelperExtensions
    {
		[System.Diagnostics.DebuggerStepThrough]
		public static object ToDBNull(this object value)
		{
			if (value == null)
				return DBNull.Value;
			else
				return value;
		}

		[System.Diagnostics.DebuggerStepThrough]
		public static object ToDBNull(this int value)
		{
			if (value <= 0)
				return DBNull.Value;
			else
				return value;
		}

		[System.Diagnostics.DebuggerStepThrough]
		public static object ToDBNull(this int? value)
		{
			if (!value.HasValue || value.Value <= 0)
				return DBNull.Value;
			else
				return value;
		}

		[System.Diagnostics.DebuggerStepThrough]
		public static object ToDBNull(this string value)
		{
			if (value == "" || value == null)
				return DBNull.Value;
			else
				return value;
		}

		[System.Diagnostics.DebuggerStepThrough]
		public static object ToDBNull(this DateTime value)
		{
			if (value == DateTime.MinValue)
				return DBNull.Value;
			else
				return value;
		}

		[System.Diagnostics.DebuggerStepThrough]
		public static object ToDBNull(this DateTime? value)
		{
			if (!value.HasValue)
				return DBNull.Value;
			else
				return value;
		}
		
		/// <summary>
        /// Answers true if this String is either null or empty.
        /// </summary>
        internal static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Answers true if this String is neither null or empty.
        /// </summary>
        internal static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Chops off a string at the specified length and accounts for smaller length
        /// </summary>
        /// <param name="s"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string Truncate(this string s, int maxLength)
        {
            return s != null && s.Length > maxLength ? s.Substring(0, maxLength) : s;
        }

        /// <summary>
        /// Removes trailing / characters from a path and leaves just one
        /// </summary>
        internal static string EnsureTrailingSlash(this string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            return Regex.Replace(input, "/+$", string.Empty) + "/";
        }

        /// <summary>
        /// Removes any leading / characters from a path
        /// </summary>
        internal static string RemoveLeadingSlash(this string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            return Regex.Replace(input, "^/+", string.Empty);
        }

        /// <summary>
        /// Removes any leading / characters from a path
        /// </summary>
        internal static string RemoveTrailingSlash(this string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            return Regex.Replace(input, "/+$", string.Empty);
        }

        /// <summary>
        /// Returns a lowercase string of <paramref name="b"/> suitable for use in javascript.
        /// </summary>
        internal static string ToJs(this bool b)
        {
            return b ? "true" : "false";
        }

		/// <summary>
		/// Returns an int that represents the current object
		/// </summary>
		[System.Diagnostics.DebuggerStepThrough]
		public static int ToInt(this object value)
		{
			if (value == null || value == DBNull.Value)
			{
				return 0;
			}
			else
			{
				if (value.GetType() == typeof(decimal))
				{
					return Convert.ToInt32(value);
				}
				else
				{
					return ToInt(value.ToString());
				}
			}
		}

		/// <summary>
		/// Returns an int that represents the current object
		/// </summary>
		[System.Diagnostics.DebuggerStepThrough]
		public static int ToInt(this string value)
		{
			int ret;
			int.TryParse(value, out ret);
			return ret;
		}

		[System.Diagnostics.DebuggerStepThrough]
		public static int ToInt(this bool value)
		{
			if (value)
				return 1;
			else
				return 0;
		}

		[System.Diagnostics.DebuggerStepThrough]
		public static int ToInt(this string value, int result)
		{
			int ret;
			return int.TryParse(value, out ret) ? ret : result;
		}

		/// <summary>
		/// Returns a decimal that represents the current object
		/// </summary>
		[System.Diagnostics.DebuggerStepThrough]
		public static decimal ToDecimal(this object value)
		{
			if (value == null || value == DBNull.Value)
				return 0;
			else
				return ToDecimal(value.ToString());
		}

		/// <summary>
		/// Returns a decimal that represents the current object
		/// </summary>
		public static decimal ToDecimal(this string value)
		{
			decimal ret;
			Decimal.TryParse(value, out ret);
			return ret;
		}

        /// <summary>
        /// Returns a decimal that represents the current object
        /// </summary>
        public static decimal MaxDigits(this decimal value, int digits)
		{
			double maxValue = Math.Pow(10, digits) - 1;
			decimal ret = value;

			if (Math.Abs(ret) > (decimal)maxValue)
				ret = (decimal)maxValue;

			return ret;

		}
		public static decimal ToDecimal(this string value, Decimal result)
		{
			decimal ret;
			return Decimal.TryParse(value, out ret) ? ret : result;
		}

		/// <summary>
		/// Returns a bool that represents the current object
		/// </summary>
		public static bool ToBool(this object value)
		{
			if (value == null || value == DBNull.Value)
			{
				return false;
			}
			else
			{
				if (value.GetType() == typeof(int))
					return Convert.ToBoolean(value);
				else
					return Convert.ToBoolean(value.ToString());
			}
		}

		/// <summary>
		/// Returns a DateTime that represents the current object
		/// </summary>
		[System.Diagnostics.DebuggerStepThrough]
		public static DateTime ToDateTime(this object value)
		{
			if (value == null || value == DBNull.Value || value.ToString() == "")
				return DateTime.MinValue;
			else
				return Convert.ToDateTime(value);
		}

		public static DateTime ToDateTime(this string value, string dateFormatString)
		{
			CultureInfo culture;

			if (dateFormatString.StartsWith("MM", StringComparison.OrdinalIgnoreCase))
				culture = new CultureInfo("en-US");
			else
				culture = new CultureInfo("es-AR");

			DateTime ret;
			DateTime.TryParse(value, culture, System.Globalization.DateTimeStyles.None, out ret);
			return ret;
		}
    }
}
