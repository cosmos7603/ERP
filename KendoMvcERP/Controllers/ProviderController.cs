using System.Web.Mvc;
using BLL.Abstract;
using BLL.Concrete;
using Entities.POCOEntities;
using ERP.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace ERP.Controllers
{
    public partial class ProviderController : Controller
    {

        public ProviderController()
        {
            providerManager = GetProviderManager();
        }
        protected IManager<ProviderPOCO> providerManager = GetProviderManager();

        private static IManager<ProviderPOCO> GetProviderManager()
        {
            return ManagerFactory.GetInstance().GetManagerFor<ProviderPOCO>();
        }
  

        private static IManager<PaymentDueDateTypePOCO> GetPaymentDueDateTypeManager()
        {
            return ManagerFactory.GetInstance().GetManagerFor<PaymentDueDateTypePOCO>();
        }

        public ActionResult Providers()
        {
            return View();
        }


        public ActionResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var result = providerManager.GetAll();
            return Json(result.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ProviderViewModel provider)
        {
            if (provider != null && ModelState.IsValid)
            {
                providerManager.Update(provider, provider.Id);
            }
            return Json(new[] { provider }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, ProviderViewModel provider)
        {
            if (provider != null)
            {
                providerManager.Delete(provider.Id);
            }

            return Json(new[] { provider }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add([DataSourceRequest] DataSourceRequest request, ProviderViewModel provider)
        {
            if (provider != null && ModelState.IsValid)
            {
                providerManager.Add(provider);
            }

            return Json(new[] { provider }.ToDataSourceResult(request, ModelState));
        }
    }
}