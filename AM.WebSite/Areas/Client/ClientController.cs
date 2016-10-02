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
		#region Main View
		public ActionResult Index()
		{
			var model = new Models.Client();
			model.ClientTypesSelectList = ClientService.GetClientTypeList().ToSelectList(c => c.Description, v => v.Id);
			
			return View(model);
		}

		//#endregion

		//#region Partial Views


		//#endregion

		//#region Actions
		//[HttpPost]
		//public JsonResult List(ClientService.ClientSearchParameters p)
		//{

		//	var sr = ClientService.GetClientList(p);

		//	if (!sr.Status)
		//		return GetJsonResponse(sr);

		//	sr.Data = ((List<DAL.Client>)sr.Data)
		//	.Select(x => new
		//	{
		//		x.Id,
		//		x.ComercialName,
		//		x.FirstName,
		//		x.LastName,
		//		x.Active,
		//		x.DNI,
		//		x.CUIT,
		//		x.Province,
		//		x.City,
		//		ClientType = new ClientType
		//		{
		//			Id = x.ClientType.Id,
		//			Description = x.ClientType.Description
		//		},
		//		Address1 = x.Address1 + " " + x.Address2,
		//		x.Observations,
		//		x.Telephone1,
		//		x.Telephone2,
		//		x.Email
		//	});
		//	return GetJsonResponse(sr);
		//}

		public PartialViewResult EntityInfoModal(string setupMode, int? entityId)
		{
			var model = new Models.Client();

			// Setup mode
			if (setupMode == SetupMode.NEW)
			{
				model.InfoTitle = "Nuevo Cliente";
				model.ClientTypesSelectList = ClientService.GetClientTypeList().ToSelectList(c => c.Description, v => v.Id);
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
				model.ClientTypesSelectList = ClientService.GetClientTypeList().ToSelectList(c => c.Description, v => v.Id).SelectItem(client.ClientTypeId);
				//model.EditRights = 
				//model.CanCompleteOrDeleteReminder = 
			}
			return PartialView("ClientInfoModal",model);
		}


		#endregion




	}
}
