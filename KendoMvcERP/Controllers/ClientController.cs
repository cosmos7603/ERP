using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web.Mvc;
using BLL.Abstract;
using BLL.Concrete;
using Entities.POCOEntities;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoMvcERP.Models;

namespace KendoMvcERP.Controllers
{
    public class ClientController : Controller
    {


        protected IManager<ClientPOCO> clientManager = GetClientManager();

        protected IManager<ClientTypePOCO> clientTypeManager { get; set; }
        //protected IManager<ChargeMethodPOCO> chargeMethodManager { get; set; }
        //protected IManager<ComercialAgentPOCO> comercialAgentManager { get; set; }
        //protected IManager<TaxPOCO> taxManager { get; set; }
        //protected IManager<PaymentDueDateTypePOCO> paymentDueDateTypeManager { get; set; }

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

        public ActionResult Clients()
        {
           // clientManager = GetClientManager();
            
            //chargeMethodManager = GetChargeMethodManager();
            clientTypeManager = GetClientTypeManager();
            //comercialAgentManager = GetComercialAgentManager();
            //taxManager = GetTaxManager();
            //paymentDueDateTypeManager = GetPaymentDueDateTypeManager();




            //var chargeMethods = chargeMethodManager.GetAll();
            //ViewData["ChargeMethods"] = chargeMethods;


            var clientTypes = clientTypeManager.GetAll();
            ViewData["ClientTypes"] = clientTypes;



            //var comercialAgents = comercialAgentManager.GetAll();
            //ViewData["ComercialAgents"] = comercialAgents;


            //var taxes = taxManager.GetAll();
            //ViewData["Taxes"] = taxes;

            //var paymentDueDateTypes = paymentDueDateTypeManager.GetAll();
            //ViewData["PaymentDueDateTypes"] = paymentDueDateTypes;

            return View();
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
    }
}