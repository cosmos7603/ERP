using AM.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AM.WebSite
{
	public class JSonResponse
	{
		#region Members
		public int ReturnValue { get; set; }
		public string ReturnCode { get; set; }
		public string ReturnName { get; set; }
		public object Data { get; set; }
		public bool Status { get; set; }
		public string ValidationTitle { get; set; }
		public string Redirect { get; set; }
		public string WarningMessage { get; set; }
		public List<string> Errors { get; set; }
		#endregion

		#region Constructors
		public JSonResponse()
		{
			WarningMessage = "";
			Status = true;
			Errors = new List<string>();
		}

		public JSonResponse(ModelStateDictionary modelState)
		{
			Errors = new List<string>();

			foreach (var error in modelState.Values.Where(item => item.Errors.Count > 0).SelectMany(item => item.Errors).ToList())
				Errors.Add(error.ErrorMessage);

			WarningMessage = "";
			ValidationTitle = "Error Summary";
			Status = false;
		}

		public JSonResponse(ServiceResponse sr, string successMessage)
		{
			Errors = new List<string>();

			if (sr != null)
			{
				WarningMessage = "";
				ReturnValue = sr.ReturnValue;
				ReturnCode = sr.ReturnCode;
				ReturnName = sr.ReturnName;
				Status = sr.Status;
				Data = sr.Data;

				if (sr.Status)
				{
					ValidationTitle = successMessage;
				}
				else
				{
					ValidationTitle = "Error Summary";

					foreach (ServiceError serviceError in sr.Errors)
						Errors.Add(serviceError.ErrorMessage);
				}
			}
			else
			{
				WarningMessage = "";
				ValidationTitle = successMessage;
				Status = true;
			}
		}
		#endregion
	}
}