using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace AM.Utils
{
	public static class Formatting
	{
		public static string FormatDecimal(decimal? value)
		{
			return FormatDecimal(value, 2);
		}

		public static string FormatDecimal(decimal? value, int dp)
		{
			if (value.HasValue)
			{
				return value.Value.ToString("N" + dp.ToString());
			}
			else
			{
				if (dp == 0)
					return "0";
				else
					return "0." + "".PadLeft(dp, '0');
			}
		}

		public static string FormatCurrency(object value)
		{
			return FormatCurrency(value.ToDecimal());
		}

		public static string FormatCurrency(decimal value)
		{
			return value.ToString("C");
		}

		public static string FormatCurrency(decimal? value)
		{
			if (value.HasValue)
				return value.Value.ToString("C");
			else
				return "$0.00";
		}

		public static string FormatPrct(decimal value, bool isFromDecimal)
		{
            if (!isFromDecimal)
                value = value / 100;
            return String.Format("{0:P2}", value);
		}

        public static string FormatPrct(decimal value)
        {
            return FormatPrct(value, false);
        }

        public static string FormatPrct(decimal? value)
        {
            if (value.HasValue)
                return FormatPrct(value.Value);
            else
                return FormatPrct(0);
        }

        public static string FormatPrct(decimal? value, bool isFromDecimal)
        {
            if (value.HasValue)
                return FormatPrct(value.Value, isFromDecimal);
            else
                return FormatPrct(0);
        }

        public static string FormatNullDecimal(decimal? value)
		{
			if (value == null || value.Value == 0)
				return "";
			else
				return FormatDecimal(value);
		}

		public static string FormatNullCurrency(decimal? value)
		{
			if (value == null || value.Value == 0)
				return "";
			else
				return value.Value.ToString("C");
		}

		public static string FormatDateTime(DateTime value)
		{
			return value.ToShortDateString() + " " + value.ToShortTimeString();
		}

		public static string FormatDate(DateTime value)
		{
			return value.ToShortDateString();
		}

		public static string FormatDate(DateTime? value)
		{
			if (value.HasValue)
				return value.Value.ToShortDateString();
			else
				return "";
		}

		public static decimal GetAmount(string amount)
		{
			var n = amount;
			
			n = n.Replace("$", "");
			n = n.Replace("$", "(");
			n = n.Replace("$", ")");

			return n.ToDecimal();
		}

		public static string GetDate(string date)
		{
			return String.IsNullOrEmpty(date) ? String.Empty : DateTime.Parse(date).ToShortDateString();
		}

		public static string GetHours(string date)
		{
			return String.IsNullOrEmpty(date) ? String.Empty : DateTime.Parse(date).ToString("HH:mm");
		}

		public static string FormatLegacyLink(this string link)
		{
			return $"javascript:legacysso('{link}')";
		}

	}
}
