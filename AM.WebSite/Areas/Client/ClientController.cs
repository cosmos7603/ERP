using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AM.DAL;
using AM.Services.Business;
using AM.Services.Helpers;
using AM.WebSite.Areas.Client.Models;
using AM.WebSite.Consts;
using AM.WebSite.MVC;
using AM.WebSite.Shared.Controllers;
using Entities.POCOEntities;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace AM.WebSite.Areas.Client
{


	[Route("Client/{action=index}")]
	[ViewsPath("~/Areas/Client/Views")]
	public class ClientController : CrudController<DAL.Client>
	{


		protected ClientService clientService = new ClientService();

		#region Main View
		public ActionResult Index()
		{
			//var model = new Models.Client();
			//	model.ClientTypesSelectList = ClientService.GetClientTypeList().ToSelectList(c => c.Description, v => v.Id);

			return View();
		}

		//protected ClientController()
		//{
		//	ClientService = new ClientService<DataSourceResult>();
		//}


		public PartialViewResult EntityInfoModal(string setupMode, int? entityId)
		{
			var model = new Models.Client();

			// Setup mode
			if (setupMode == SetupMode.NEW)
			{
				model.InfoTitle = "Nuevo Cliente";
				model.ClientTypesSelectList = clientService.GetClientTypeList().ToSelectList(c => c.Description, v => v.Id);
			}
			else if (setupMode == SetupMode.EDIT && entityId.HasValue)
			{
				var client = Service.GetById(entityId.Value);
				model.InfoTitle = "Editar Cliente";
				model.ComercialName = client.ComercialName;
				model.FirstName = client.FirstName;
				model.LastName = client.LastName;
				model.Id = client.Id;
				model.Active = client.Active;
				model.CUIT = client.CUIT;
				model.DNI = client.DNI;
				model.Address1 = client.Address1;
				model.Address2 = client.Address2;
				model.Email = client.Email;
				model.Observations = client.Observations;
				model.Telephone1 = client.Telephone1;
				model.Telephone2 = client.Telephone2;
				model.City = client.City;
				model.Province = client.Province;
				model.ClientTypesSelectList = clientService.GetClientTypeList().ToSelectList(c => c.Description, v => v.Id).SelectItem(client.ClientTypeId);
				//model.EditRights = 
				//model.CanCompleteOrDeleteReminder = 
			}
			return PartialView("ClientInfoModal", model);
		}

		public ActionResult ClientLookup(string query)
		{

			var clientList = clientService.ClientLookup(query);

			var clients = from p in clientList
						   select new
						   {
							   Id = p.Id,
							   Name = p.ComercialName
						   };
			return Json(clients, JsonRequestBehavior.AllowGet);
		}


		#endregion




	}
}
