using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;

namespace AM.Utils
{
    public static class ExtensionMethods
    {

		public static bool IsNumeric(this Type t)
		{
			var type = t.GetTypeWithoutNullability();
			return
				type == typeof(Int16) ||
				type == typeof(Int32) ||
				type == typeof(Int64) ||
				type == typeof(UInt16) ||
				type == typeof(UInt32) ||
				type == typeof(UInt64) ||
				type == typeof(decimal) ||
				type == typeof(float) ||
				type == typeof(double);
		}

		public static Type GetTypeWithoutNullability(this Type t)
		{
			return t.IsNullable() ? new NullableConverter(t).UnderlyingType : t;
		}

		public static bool IsNullable(this Type t)
		{
			return t.IsGenericType &&
				   t.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		[DebuggerStepThrough]
        public static object ToDBNull(this object value)
        {
            if (value == null)
                return DBNull.Value;
            else
                return value;
        }

        [DebuggerStepThrough]
        public static object ToDBNull(this int value)
        {
            if (value <= 0)
                return DBNull.Value;
            else
                return value;
        }

        [DebuggerStepThrough]
        public static object ToDBNull(this int? value)
        {
            if (!value.HasValue || value.Value <= 0)
                return DBNull.Value;
            else
                return value;
        }

        [DebuggerStepThrough]
        public static object ToDBNull(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return DBNull.Value;
            else
                return value;
        }

        [DebuggerStepThrough]
        public static object ToDBNull(this DateTime value)
        {
            if (value == DateTime.MinValue)
                return DBNull.Value;
            else
                return value;
        }

        [DebuggerStepThrough]
        public static object ToDBNull(this DateTime? value)
        {
            if (!value.HasValue)
                return DBNull.Value;
            else
                return value;
        }

        /// <summary>
        /// Returns an int that represents the current object
        /// </summary>
        [DebuggerStepThrough]
        public static int ToInt(this object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return 0;
            }
            else
            {
                if (value is decimal)
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
        [DebuggerStepThrough]
        public static int ToInt(this string value)
        {
            int ret;
            Int32.TryParse(value, out ret);
            return ret;
        }

        [DebuggerStepThrough]
        public static int ToInt(this string value, int result)
        {
            int ret;
            return Int32.TryParse(value, out ret) ? ret : result;
        }

        /// <summary>
        /// Returns a decimal that represents the current object
        /// </summary>
        [DebuggerStepThrough]
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

        public static decimal ToDecimal(this string value, Decimal result)
        {
            decimal ret;
            return Decimal.TryParse(value, out ret) ? ret : result;
        }

        /// <summary>
        /// Returns a currency value
        /// </summary>
        public static decimal ToCurrency(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return 0;

            decimal ret;
            value = value.Replace("$", "");
            value = value.Replace(",", "");
            Decimal.TryParse(value, out ret);
            return ret;
        }

        /// <summary>
        /// Returns a bool that represents the current object
        /// </summary>
        [DebuggerStepThrough]
        public static bool ToBool(this object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return false;
            }
            else
            {
                if (value is int)
                    return Convert.ToBoolean(value);
                else
                    return ToBool(value.ToString());
            }
        }

        /// <summary>
        /// Returns a bool that represents the current object
        /// </summary>
        public static bool ToBool(this string value)
        {
            if (TypeValidator.IsInt(value))
            {
                return Convert.ToBoolean(ToInt(value));
            }
            else
            {
                bool ret;
                Boolean.TryParse(value, out ret);
                return ret;
            }
        }

        [DebuggerStepThrough]
        public static string ToCsv(this List<int> list)
        {
            return String.Join(",", list.Select(x => x.ToString()).ToArray());
        }

        [DebuggerStepThrough]
        public static string ToCsv(this List<string> list)
        {
            return String.Join(",", list.Select(x => x).ToArray());
        }

        /// <summary>
        /// Returns a DateTime that represents the current object
        /// </summary>
        [DebuggerStepThrough]
        public static DateTime? ToDateTime(this object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == "" || value.ToString() == "From" || value.ToString() == "To")
                return null;
            else
                return Convert.ToDateTime(value);
        }

		[DebuggerStepThrough]
		public static DateTime? ToDate(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;

            return DateTime.Parse(value);
        }

		[DebuggerStepThrough]
		public static string ToShortDateString(this object date)
		{
			if (date == DBNull.Value || date == null)
				return "";

			return ((DateTime)date).ToShortDateString();
		}

		public static string ToShortDate(this DateTime? date)
        {
            return !date.HasValue ? String.Empty : date.Value.ToShortDateString();
        }

        public static string ToYesNoLetter(this bool value)
        {
            return value ? "Y" : "N";
        }

        public static string ToNA(this int? value)
        {
            return value.HasValue ? value.ToString() : "N/A";
        }

		public static string ToJson(this object value)
		{
			return ExtensionMethods.ToJson(value, false, false);
		}

		public static string ToJson(this object value, bool formatted)
		{
			return ExtensionMethods.ToJson(value, formatted, false);
		}

		public static string ToJson(this object value, bool formatted, bool ignoreNull)
		{
			if (value == null)
				return "";

			var s = new JsonSerializerSettings();

			s.PreserveReferencesHandling = PreserveReferencesHandling.None;
			s.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

			if (formatted)
				s.Formatting = Newtonsoft.Json.Formatting.Indented;

			if (ignoreNull)
				s.NullValueHandling = NullValueHandling.Ignore;

			return JsonConvert.SerializeObject(value, s);
		}

		public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        public static bool HasChanged(this string valueA, string valueB)
        {

            if (String.IsNullOrEmpty(valueA) && String.IsNullOrEmpty(valueB))
                return false;

            return valueA != valueB;
        }

        public static string NullEmptyString(this string value)
        {
            return String.IsNullOrEmpty(value) ? null : value;
        }

		// Converts Variable to Sentence
		public static string ToSentence(this string input)
		{
			return new string(input.ToCharArray().SelectMany((c, i) => i > 0 && Char.IsUpper(c) ? new[] { ' ', c } : new[] { c }).ToArray());
		}

        /// <summary>
        /// Removes the string punctuation.
        /// </summary>
        /// <param name="s">A string</param>
        /// <returns></returns>
        public static string RemovePunctuation(this string s)
        {
            var sb = new StringBuilder();

            foreach (char c in s)
            {
                if (!char.IsPunctuation(c)) sb.Append(c);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Replaces spaces with underscores.
        /// </summary>
        /// <param name="s">A string</param>
        /// <returns></returns>
        public static string SpacesToUnderscores(this string s)
        {
            return s.Replace(' ', '_');
        }

        /// <summary>
        /// Replaces an array of characters with desired string 
        /// </summary>
        /// <param name="s">A string</param>
        /// <param name="oldValues">Array of characters to replace</param>
        /// <param name="newValue">Replacement value</param>
        /// <returns></returns>
        public static string Replace(this string s, char[] oldValues, string newValue)
        {
            return oldValues.Aggregate(s, (current, oldValue) => current.Replace(oldValue.ToString(), newValue));
        }


        [DebuggerStepThrough]
        public static string ToString(this object value, string format)
        {
            if (value is decimal)
                return ((decimal) value).ToString(format);

            return value.ToString();
        }

		/// <summary>
		/// Returns an Base64 string from a byte array
		/// </summary>
		[DebuggerStepThrough]
		public static string ToB64(this byte[] b)
		{
			return Convert.ToBase64String(b);
        }

		public static void UpdateKey<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey fromKey, TKey toKey)
		{
			TValue value = dic[fromKey];
			dic.Remove(fromKey);
			dic[toKey] = value;
		}
	}
}
