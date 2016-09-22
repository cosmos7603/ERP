using AM.DAL;

namespace AM.Services.Extensions
{
	public static class ReservationExtensions
	{
		public static string GetStatus(this Reservation res)
		{
			if (res.StatusCode == ReservationStatusCode.QUOTED && res.InquiryBooking)
				return "Inquiry";
			if (res.StatusCode == ReservationStatusCode.QUOTED && res.ExpiredQuote)
				return "Expired";
			if (res.StatusCode == ReservationStatusCode.QUOTED && res.WaitlistedQuote)
				return $"{res.ResStatus.StatusName} /WL";

			return res.ResStatus.StatusName;
		}
	}
}
