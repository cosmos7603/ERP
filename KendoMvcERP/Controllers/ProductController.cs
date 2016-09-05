using System.Web.Mvc;
using BLL.Abstract;
using BLL.Concrete;
using Entities.POCOEntities;
using ERP.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace ERP.Controllers
{
    public partial class ProductController : Controller
    {


        protected IManager<ProductPOCO> productManager { get; set; }
        protected IManager<ProviderPOCO> providerManager { get; set; }
        protected IManager<ProductFamilyPOCO> productFamilyManager { get; set; }



        public ProductController()
        {
            productManager = GetProductManager();
        }

        //protected IManager<Clien> ProductDueDateTypesManager { get; set; }

        private static IManager<ProductPOCO> GetProductManager()
        {
            return ManagerFactory.GetInstance().GetManagerFor<ProductPOCO>();
        }
        private static IManager<ProductFamilyPOCO> GetProductFamilyManager()
        {
            return ManagerFactory.GetInstance().GetManagerFor<ProductFamilyPOCO>();
        }
        private static IManager<ProviderPOCO> GetProviderManager()
        {
            return ManagerFactory.GetInstance().GetManagerFor<ProviderPOCO>();
        }
        public ActionResult Products()
        {
            providerManager = GetProviderManager();
            var providers = providerManager.GetAll();
            ViewData["Providers"] = providers;
            productFamilyManager = GetProductFamilyManager();
            var productFamilies = productFamilyManager.GetAll();
            ViewData["ProductFamilies"] = productFamilies;
            return View();
        }

        public ActionResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var result = productManager.GetAll();
            //var result = from product in products
            //    select new
            //           {
            //               product.Sector,
            //               product.Active,
            //               product.Address1,
            //               product.Address2,
            //               product.BirthDate,
            //               product.ChargeMethod,
            //               product.ChargeOverCost,
            //               product.ProductFamily,
            //               product.ProductCode,
            //               product.ComercialAgent,
            //               product.ComercialName,
            //               product.Country,
            //               product.Discount,
            //               product.Email,
            //               product.CorporateName
            //           };

    
            return Json(result.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ProductViewModel productViewModel)
        {
            if (productViewModel != null && ModelState.IsValid)
            {
                productManager.Update(productViewModel, productViewModel.Id);
            }
            return Json(new[] { productViewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, ProductViewModel productViewModel)
        {
            if (productViewModel != null)
            {
                productManager.Delete(productViewModel.Id);
            }

            return Json(new[] { productViewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add([DataSourceRequest] DataSourceRequest request, ProductViewModel productViewModel)
        {
            if (productViewModel != null && ModelState.IsValid)
            {
                productManager.Add(productViewModel);
            }

            return Json(new[] { productViewModel }.ToDataSourceResult(request, ModelState));
        }
    }
}