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
		Id: $form.find("#txtId").val(),
		FirstName: $form.find("#txtFirstName").val(),
		LastName: $form.find("#txtLastName").val(),
		ComercialName: $form.find("#txtComercialName").val(),
		CUIT: $form.find("#txtCuit").val(),
		DNI: $form.find("#txtDni").val(),
		Telephone1: $form.find("#txtTelephone1").val(),
		Telephone2: $form.find("#txtTelephone2").val(),
		Address1: $form.find("#txtAddress1").val(),
		Address2: $form.find("#txtAddress2").val(),
		City: $form.find("#txtCity").val(),
		Province: $form.find("#txtProvince").val(),
		Email: $form.find("#txtEmail").val(),
		Observations: $form.find("#txtObservations").val(),
		Active: $form.find("#chkActive").checked(),
		ClientTypeID: $form.find("#ddlClientType").val()
	}

	$.postForm($form, model, function ()
	{
		$('#clientModal').modal('hide');
		loadClients();
	});

	return false;
}
