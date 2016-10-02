


function removeEntity()
{
	var grid = $("#grid").data("kendoGrid");
	var id = grid.dataItem(grid.select()).id;
	var productCode = grid.dataItem(grid.select()).ShortDescription;
	$.confirmDelete(
		"Producto",
		productCode,
		id,
		function (id)
		{
			$.postData(
				'Delete',
				{ clientId: id },
				function ()
				{
					loadGrid();
				});
		});
}

function saveEntityInfo()
{
	var $form = $("#frmProductInfo");

	var salePriceField = $form.find("#txtSalePrice");
	var salePrice = salePriceField.val().substring(1, salePriceField.val().length);
	var costField = $form.find("#txtCost");
	var cost = costField.val().substring(1, costField.val().length);

	var model = {
		Id: $form.find("#txtId").val(),
		ProductCode: $form.find("#txtProductCode").val(),
		ShortDescription: $form.find("#txtShortDescription").val(),
		LongDescription: $form.find("#txtLongDescription").val(),
		SalePrice: salePrice,
		Cost: cost,
		Stock: $form.find("#txtStock").val(),
		ProviderId: $form.find("#ddlProvider").val(),
		ProductFamilyId: $form.find("#ddlProductFamily").val(),
		Active: $form.find("#chkActive").checked(),
		AvailableForSale: $form.find("#chkAvailableForSale").checked()
	}

	$.postForm($form, model, function ()
	{
		$('#entityModal').modal('hide');
		enableActions(false);
		loadGrid();
	});

	return false;
}

