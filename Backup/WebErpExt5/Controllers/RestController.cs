using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using BLL.Abstract;
using BLL.Concrete;
using Entities.POCOEntities;
using WebErpExt5.Enums;
using WebErpExt5.ExtensionMethod;
using WebErpExt5.Models;

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

        //[Authorize]
        [HttpGet]
        public ContentResult Index(int? id, int page, int limit, string sort)
        {
            return IndexGet(id, page, limit, sort);
        }

        //[Authorize]
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Put | HttpVerbs.Delete)]
        public ContentResult Index(int? id, T model)
        {
            var method = (HttpMethods)Enum.Parse(typeof(HttpMethods), HttpContext.Request.HttpMethod, true);
            switch (method)
            {
                case HttpMethods.Post:
                    return IndexPost(model);
                case HttpMethods.Put:
                    return IndexPut(model);
                case HttpMethods.Delete:
                    return IndexDelete(model);
                default:
                    return this.GetJsonResponseFromObject(new ResponseObject(false, null, "No suitable HTTP verb"));
            }
        }

        //private TIdentity GetIdFromModel(TModel model)
        //{
        //    var stringId = typeof(TModel).GetProperty("Id").GetValue(model, null).ToString();
        //    var parseMethod = typeof(TIdentity).GetMethod("Parse", new[] { typeof(string) });

        //    return (TIdentity)parseMethod.Invoke(null, new object[] { stringId });
        //}

        private ContentResult IndexGet(int? id, int page, int limit, string sort)
        {
            object response;

            if (id == null)
            {
                var qs = HttpContext.Request.QueryString;
                var filterValCol = new NameValueCollection();
                foreach (var key in qs.AllKeys.Where(k => k.StartsWith("filter")))
                {
                    filterValCol.Add(key, qs.Get(key));
                }

                var extRequestData = new ExtRequestData<T>(page, limit, sort, filterValCol);

                int totalCount;
                var results = Manager.GetPaged(extRequestData.Filter, extRequestData.SortOptions, extRequestData.Page, extRequestData.PageSize, out totalCount);

                response = new PagedResult<T>(results, totalCount);
            }
            else
            {
                response = Manager.GetById(id.Value);
            }

            return this.GetJsonResponseFromObject(response);
        }

        private ContentResult IndexPut(T model)
        {
            var response = new ResponseObject(true);

            try
            {
                Manager.Update(model, model.Id);
            }
            catch (Exception exception)
            {
                response.success = false;
                response.error.message = exception.Message;
            }

            return this.GetJsonResponseFromObject(response);
        }

        private ContentResult IndexPost(T model)
        {
            var response = new ResponseObject(true);

            try
            {
                Manager.Add(model);
            }
            catch (Exception exception)
            {
                response.success = false;
                response.error.message = exception.Message;
            }

            return this.GetJsonResponseFromObject(response);
        }

        private ContentResult IndexDelete(T model)
        {
            var response = new ResponseObject(true);

            try
            {
                Manager.Delete(model.Id);
            }
            catch (Exception exception)
            {
                response.success = false;
                response.error.message = exception.Message;
            }
            
            return this.GetJsonResponseFromObject(response);
        }
    }
}
