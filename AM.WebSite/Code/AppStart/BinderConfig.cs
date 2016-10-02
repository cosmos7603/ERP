using System.Web.Mvc;

namespace AM.WebSite
{
	public sealed class EmptyStringModelBinder : DefaultModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			bindingContext.ModelMetadata.ConvertEmptyStringToNull = false;
            return base.BindModel(controllerContext, bindingContext);
		}
	}
	
	public class BinderConfig
	{
		public static void RegisterBinders()
		{
			// Avoid converting empty strings to nulls by default
			ModelBinders.Binders.DefaultBinder = new EmptyStringModelBinder();
		}
	}
}