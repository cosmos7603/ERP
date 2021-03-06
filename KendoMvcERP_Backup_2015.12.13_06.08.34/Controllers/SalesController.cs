﻿using System;
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
    public partial class SaleController : Controller
    {

    
        protected IManager<SalePOCO> saleManager = GetSaleManager();
        protected IManager<SaleCategoryPOCO> saleCategoryManager = GetSaleCategoryManager();
        protected IManager<ClientPOCO> clientManager = GetClientManager();
        protected IManager<BillTypePOCO> billTypeManager = GetBillTypeManager();

        private static IManager<SalePOCO> GetSaleManager()
        {
            return ManagerFactory.GetInstance().GetManagerFor<SalePOCO>();
        }

        private static IManager<SaleCategoryPOCO> GetSaleCategoryManager()
        {
            return ManagerFactory.GetInstance().GetManagerFor<SaleCategoryPOCO>();
        }

        private static IManager<ClientPOCO> GetClientManager()
        {
            return ManagerFactory.GetInstance().GetManagerFor<ClientPOCO>();
        }
        private static IManager<BillTypePOCO> GetBillTypeManager()
        {
            return ManagerFactory.GetInstance().GetManagerFor<BillTypePOCO>();
        }

        public ActionResult Sales()
        {
            var clients = clientManager.GetAll();
            ViewData["Clients"] = clients;
            var salesCategories = saleCategoryManager.GetAll();
            ViewData["SaleCategories"] = salesCategories;
            var BillTypes = billTypeManager.GetAll();
            ViewData["BillTypes"] = BillTypes;

            return View();
        }


        public ActionResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var result = saleManager.GetAll();

            return Json(result.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, SaleViewModel sale)
        {
            if (sale != null && ModelState.IsValid)
            {
                saleManager.Update(sale, sale.Id);
            }
            return Json(new[] { sale }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, SaleViewModel sale)
        {
            if (sale != null)
            {
                saleManager.Delete(sale.Id);
            }

            return Json(new[] { sale }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add([DataSourceRequest] DataSourceRequest request, SaleViewModel sale)
        {
            if (sale != null && ModelState.IsValid)
            {
                saleManager.Add(sale);
            }

            return Json(new[] { sale }.ToDataSourceResult(request, ModelState));
        }
    }
}