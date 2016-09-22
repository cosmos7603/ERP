using System;
using System.Collections.Generic;
using System.Text;

namespace AM.DAL.QueryResults
{
	public class ReminderResult
	{
		// Widget Properties
		public int AppointmentId { get; set; }
		public DateTime AppointmentDate { get; set; }
		public string AppointmentDesc { get; set; }
		public string AppointmentTypeCode { get; set; }
		public bool PastAppointment { get; set; }
		public int? ReservationId { get; set; }
		public int? GrpId { get; set; }
		public string AppointmentCompleteDesc { get; set; }

		// Slider Properties
		public DateTime? PriActBeginDate { get; set; }
		public string ShipName { get; set; }
		public string TourPropertyName { get; set; }
		public string VendorName { get; set; }
		public string ProductTypeCode { get; set; }
		public string ProductTypeName { get; set; }
		public bool Financial { get; set; }
		public string FinVendorName { get; set; }
		public string FinProductTypeCode { get; set; }
		public string FinProductTypeName { get; set; }

		// Extra Properties
		public string GroupDateLabel { get; set; }
		public bool CanBeCompleted { get; set; }
	}
}
