function showClientModal(model)
{
	$.partialView("#clientModalContainer", 'Client/ClientInfoModal', model, function ()
	{
		$('#clientModal').modal('show');
		attachClientModalHandlers();
	});
}

function attachClientModalHandlers()
{

	$("#btnSaveClient").click(function ()
	{
		saveClientInfo();
		return false;
	});

	$("#btnDeleteClient").click(function ()
	{
		deleteClient();
		return false;
	});


}

function saveClientInfo()
{
	var $form = $("#frmClientInfo");
	var model = {
		Id: 0,
		FirstName: $form.find("#txtFirstName").val(),
		LastName: $form.find("#txtLastName").val(),
		ComercialName: $form.find("#txtComercialName").val(),
		CUIT: $form.find("#txtCuit").val(),
		DNI: $form.find("#txtDni").val(),
		Active: $form.find("#chkActive").checked()
}

	$.postForm($form, model, function ()
	{
		$('#clientModal').modal('hide');
		loadClients();
	});

	return false;
}

//function deleteClient()
//{
//	var clientId = $("#frmClientInfo").find("#hddAppointmentId").val();
//	$.confirmDelete(
//		"Client",
//		$("#frmClientInfo").find("#txtClient").val(),
//		clientId,
//		function (clientId)
//		{
//			$.postData(
//				'Client/Delete',
//				{ clientId: clientId },
//				function ()
//				{
//					loadClients();
//				});
//		});
//}