using System.Web.Mvc;
using BLL.Abstract;
using BLL.Concrete;
using Entities.POCOEntities;

namespace InspiniaERP.Controllers
{
	public abstract class RestController<T> : Controller where T : EntityPOCO
	{
		protected RestController()
		{
			Manager = GetManager();
		}

		protected IManager<T> Manager { get; set; }

		private static IManager<T> GetManager()
		{
			return ManagerFactory.GetInstance().GetManagerFor<T>();
		}
	}
}