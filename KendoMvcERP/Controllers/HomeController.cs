using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Abstract;
using BLL.Concrete;
using Entities.POCOEntities;

namespace KendoMvcERP.Controllers
{
    public class HomeController : Controller
    {

        protected IManager<ChargeMethodPOCO> chargeMethodManager { get; set; }
        protected IManager<ClientTypePOCO> clientTypeManager { get; set; }
        protected IManager<ComercialAgentPOCO> comercialAgentManager { get; set; }
        protected IManager<TaxPOCO> taxManager { get; set; }
        protected IManager<PaymentDueDateTypePOCO> paymentDueDateTypeManager { get; set; }
        
        //protected IManager<Clien> ClientDueDateTypesManager { get; set; }


        
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


            chargeMethodManager = GetChargeMethodManager();
            var chargeMethods = chargeMethodManager.GetAll();
            ViewData["ChargeMethods"] = chargeMethods;

            clientTypeManager = GetClientTypeManager();
            var clientTypes = clientTypeManager.GetAll();
            ViewData["ClientTypes"] = clientTypes;


            comercialAgentManager = GetComercialAgentManager();
            var comercialAgents = comercialAgentManager.GetAll();
            ViewData["ComercialAgents"] = comercialAgents;

            taxManager = GetTaxManager();
            var taxes = taxManager.GetAll(); 
            ViewData["Taxes"] = taxes;

            paymentDueDateTypeManager = GetPaymentDueDateTypeManager();
            var paymentDueDateTypes = paymentDueDateTypeManager.GetAll();
            ViewData["PaymentDueDateTypes"] = paymentDueDateTypes;

            return View();
        }

        public ActionResult Products()
        {
        
            chargeMethodManager = GetChargeMethodManager();
            var chargeMethods = chargeMethodManager.GetAll();
            ViewData["ChargeMethods"] = chargeMethods;

            clientTypeManager = GetClientTypeManager();
            var clientTypes = clientTypeManager.GetAll();
            ViewData["ClientTypes"] = clientTypes;


            comercialAgentManager = GetComercialAgentManager();
            var comercialAgents = comercialAgentManager.GetAll();
            ViewData["ComercialAgents"] = comercialAgents;

            taxManager = GetTaxManager();
            var taxes = taxManager.GetAll();
            ViewData["Taxes"] = taxes;

            paymentDueDateTypeManager = GetPaymentDueDateTypeManager();
            var paymentDueDateTypes = paymentDueDateTypeManager.GetAll();
            ViewData["PaymentDueDateTypes"] = paymentDueDateTypes;

            return View();
        }




    }
}