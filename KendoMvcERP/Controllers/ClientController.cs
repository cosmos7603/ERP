using System.Web.Mvc;
using BLL.Abstract;
using BLL.Concrete;
using Entities.POCOEntities;
using ERP.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace ERP.Controllers
{
    public class ClientController : Controller
    {




		public ActionResult Index()
		{
			var clients = clientManager.GetAll();
			return View(clients);
		}

		public ActionResult Clients()
        {
            clientManager = GetClientManager();
	        var clients = clientManager.GetAll();
            return View(clients);
        }


        public ActionResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var result = clientManager.GetAll();
            //var result = from client in clients
            //    select new
            //           {
            //               client.Sector,
            //               client.Active,
            //               client.Address1,
            //               client.Address2,
            //               client.BirthDate,
            //               client.ChargeMethod,
            //               client.ChargeOverCost,
            //               client.ClientType,
            //               client.ClientCode,
            //               client.ComercialAgent,
            //               client.ComercialName,
            //               client.Country,
            //               client.Discount,
            //               client.Email,
            //               client.CorporateName
            //           };

            //var sectors = sectorManager.GetAll();
            //ViewData["Sectors"] = sectors;
            return Json(result.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ClientViewModel client)
        {
            if (client != null && ModelState.IsValid)
            {
                clientManager.Update(client, client.Id);
            }
            return Json(new[] { client }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, ClientViewModel client)
        {
            if (client != null)
            {
                clientManager.Delete(client.Id);
            }

            return Json(new[] { client }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add([DataSourceRequest] DataSourceRequest request, ClientViewModel client)
        {
            if (client != null && ModelState.IsValid)
            {
                clientManager.Add(client);
            }

            return Json(new[] { client }.ToDataSourceResult(request, ModelState));
        }





		protected IManager<ClientPOCO> clientManager = GetClientManager();

		protected IManager<ClientTypePOCO> clientTypeManager { get; set; }

		private static IManager<ClientPOCO> GetClientManager()
		{
			return ManagerFactory.GetInstance().GetManagerFor<ClientPOCO>();
		}

		private static IManager<ChargeMethodPOCO> GetChargeMethodManager()
		{
			return ManagerFactory.GetInstance().GetManagerFor<ChargeMethodPOCO>();
		}
		private static IManager<ClientTypePOCO> GetClientTypeManager()
		{
			return ManagerFactory.GetInstance().GetManagerFor<ClientTypePOCO>();
		}
		private static IManager<ComercialAgentPOCO> GetComercialAgentManager()
		{
			return ManagerFactory.GetInstance().GetManagerFor<ComercialAgentPOCO>();
		}
		private static IManager<TaxPOCO> GetTaxManager()
		{
			return ManagerFactory.GetInstance().GetManagerFor<TaxPOCO>();
		}

		private static IManager<PaymentDueDateTypePOCO> GetPaymentDueDateTypeManager()
		{
			return ManagerFactory.GetInstance().GetManagerFor<PaymentDueDateTypePOCO>();
		}
	}
}