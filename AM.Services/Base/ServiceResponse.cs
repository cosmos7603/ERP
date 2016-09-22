using System;
using System.Collections.Generic;
using System.Linq;

namespace AM.Services
{
	public class ServiceResponse : ServiceResponse<object>
    {
    }

    public class ServiceResponse<T> : IServiceResponse<T>
    {
		#region Constructors
		public ServiceResponse()
		{
			Errors = new List<ServiceError>();
			ReturnValue = 0;
			ReturnCode = "";
			ReturnName = "";
		}

        public ServiceResponse(ServiceResponse sr)
        {
            this.Errors = sr.Errors;
            this.ReturnCode = sr.ReturnCode;
            this.ReturnName = sr.ReturnName;
            this.WarningMessage = sr.WarningMessage;
        }

        public ServiceResponse(T data) : this()
        {
            this.Data = data;
        }

		#endregion

		#region Properties
		public int ReturnValue { get; set; }
		public string ReturnCode { get; set; }
		public string ReturnName { get; set; }
		public string WarningMessage { get; set; }
		public T Data { get; set; }
		public List<ServiceError> Errors { get; set; }
		
		public bool Status
		{
			get { return (!Errors.Any(x => x.ErrorLevel == ServiceErrorLevel.VALIDATION_ERROR || x.ErrorLevel == ServiceErrorLevel.EXCEPTION)); }
		}
        
        #endregion

        #region Methods
        public void AddWarning(string warningMessage)
		{
			WarningMessage = warningMessage;
		}

		public void AddError(Exception ex)
		{
			Errors.Add(new ServiceError(ex));
		}

		public void AddError(string errorMessage)
		{
			Errors.Add(new ServiceError(errorMessage));
		}

		public void AddError(string errorCode, string errorMessage)
		{
			Errors.Add(new ServiceError(errorCode, errorMessage));
		}

		public void AddError(string errorCode, string errorMessage, ServiceErrorLevel errorLevel)
		{
			Errors.Add(new ServiceError(errorCode, errorMessage, errorLevel));
		}

		public void AddError(ServiceError serviceError)
		{
			Errors.Add(serviceError);
		}

		public void AddErrors(List<ServiceError> serviceErrorList)
		{
			foreach (ServiceError e in serviceErrorList)
				Errors.Add(e);
		}
		
		public override string ToString()
		{
			string rsp = "";

			rsp += "Status: " + Status.ToString();
			rsp += "\nData: " + this.Data?.ToString();
			rsp += "\nReturn Code: " + ReturnCode.ToString();
			rsp += "\nReturn Name: " + ReturnName.ToString();
			rsp += "\nErrors:\n";

			foreach (ServiceError error in Errors)
				rsp += "  >" + error.ErrorMessage + " / " + error.ErrorDetail;

			rsp += "\n";

			return rsp;
		}
		#endregion
	}
}
