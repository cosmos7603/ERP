using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using AM.DAL;
using AM.DAL.SPEntities;
using AM.Services;
using AM.Services.Business;
using AM.Services.Helpers;
using AM.WebSite.Areas.Client.Models;
using AM.WebSite.Consts;
using AM.WebSite.MVC;
using AM.WebSite.Shared.Controllers;
using Entities.POCOEntities;

namespace AM.WebSite.Areas.Client
{


	[Route("Client/{action=index}")]
	[ViewsPath("~/Areas/Client/Views")]
	public class ClientController : BaseController
	{
		#region Main View
		public ActionResult Index()
		{
			var model = new ClientModel();

			return View(model);
		}


		#endregion

		#region Partial Views


		#endregion

		#region Actions
		[HttpPost]
		public JsonResult List(ClientService.ClientSearchParameters p)
		{
			var sr = ClientService.GetClientList(p);

			if (!sr.Status)
				return GetJsonResponse(sr);

			sr.Data = ((List<DAL.Client>)sr.Data)
			.Select(x => new
			{
				x.Id,
				x.ComercialName,
				x.FirstName,
				x.LastName,
				x.Active,
				x.DNI,
				x.CUIT
			});
			return GetJsonResponse(sr);
		}

		public PartialViewResult ClientInfoModal(string setupMode, int? entityId)
		{
			var model = new ClientModel
			{
				//EditRights = AppInfo.ActiveUser.Rights.CanEnterStoreNews,
			};

			// Setup mode
			if (setupMode == SetupMode.NEW)
			{
				model.InfoTitle = "Nuevo Cliente";
			}
			else if (setupMode == SetupMode.EDIT && entityId.HasValue)
			{
				var client = ClientService.GetClient(entityId.Value);
				model.InfoTitle = "Editar Cliente";
				model.ComercialName = client.ComercialName;
				model.FirstName = client.FirstName;
				model.LastName = client.LastName;
				model.Id = client.Id;
				model.Active = client.Active;
				model.CUIT = client.CUIT;
				model.DNI = client.DNI;
				
				//model.EditRights = 
				//model.CanCompleteOrDeleteReminder = 
			}

			return PartialView(model);
		}

		[HttpPost]
		public JsonResult Save(ClientModel model)
		{
			var client = new DAL.Client()
			{
				Id = model.Id,
				FirstName = model.FirstName,
				LastName = model.LastName,
				ComercialName = model.ComercialName,
				Active = model.Active,
				ClientTypeId = 1,
				CUIT = model.CUIT,
				DNI = model.DNI
			};
			
			// Validate & Save
			var sr = ClientService.SaveClient(client);

			var result = GetJsonResponse(sr, sr.ReturnValue != model.Id
				? MsgService.Operation.SuccessfullyAdded(client.ComercialName)
				: MsgService.Operation.SuccessfullyUpdated(client.ComercialName));
			return result;
		}

		[HttpPost]
		public JsonResult Delete(int clientId)
		{

			var comercialName = ClientService.GetClient(clientId).ComercialName;
			var sr = ClientService.Delete(clientId, "login");
			return GetJsonResponse(sr, MsgService.Operation.SuccessfullyRemoved("El cliente " + comercialName));
		}

		#endregion

	}
}
