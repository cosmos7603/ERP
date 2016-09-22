using System.Collections.Generic;

namespace AM.Services
{
	public interface IServiceResponse<T> : IServiceResponse
    {
        T Data { get; set; }
    }

    public interface IServiceResponse 
    {
        List<ServiceError> Errors { get; set; }
		int ReturnValue { get; set; }
		string ReturnCode { get; set; }
        string ReturnName { get; set; }
        bool Status { get; }
        string WarningMessage { get; set; }
        
    }
}
