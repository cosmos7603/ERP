


function removeEntity()
{
	var grid = $("#grid").data("kendoGrid");
	var clientId = grid.dataItem(grid.select()).id;
	var comercialName = grid.dataItem(grid.select()).ComercialName;
	$.confirmDelete(
		"Cliente",
		comercialName,
		clientId,
		function (clientId)
		{
			$.postData(
				'Delete',
				{ clientId: clientId },
				function ()
				{
					loadGrid();
				});
		});
}

function saveEntityInfo(validationSuccess)
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
	$.postForm($form, model, function (response,data)
	{
		$('#entityModal').modal('hide');
		validationSuccess(response,data);
	});

	return false;
}

function edit(id,validationSuccess)
{
	showModal({ setupMode: SETUP_MODE_EDIT, entityId: id },validationSuccess);
	return false;
}
function showModal(model,validationSuccess)
{
	var url = $.getAbsoluteUrl('Client/EntityInfoModal');
	$.partialView("#entityModalContainer", url, model, function ()
	{
		$('#entityModal').modal('show');
		attachEntityModalHandlers(validationSuccess);
	});
}

function attachEntityModalHandlers(validationSuccess)
{

	$("#btnSave").click(function ()
	{

		saveEntityInfo(validationSuccess);
		return false;
	});

	//$("#btnDelete").click(function ()
	//{
	//	removeEntity();
	//	return false;
	//});
}
