namespace AM.WebSite.Areas.Shared.Error.Models
{
	public class UnauthorizedModel
	{
		public string ControllerName { get; set; }
		public string ActionName { get; set; }
		public string Message { get; set; }

		public UnauthorizedModel()
		{
		}

		public UnauthorizedModel(string controllerName, string actionName, string message)
		{
			ControllerName = controllerName;
			ActionName = actionName;
			Message = message;
		}
	}
}