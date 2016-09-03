using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using BLL.Abstract;
using BLL.Concrete;
using Entities.POCOEntities;


namespace WebErpExt5.Controllers
{
    public abstract class RestController<T> : Controller where T : EntityPOCO
    {
        protected IManager<T> Manager { get; set; }
        protected RestController()
        {
            Manager = GetManager();
        }
        private static IManager<T> GetManager()
        {
            return ManagerFactory.GetInstance().GetManagerFor<T>();
        }

        
    }
}
