function showProductModal(model)
{
	$.partialView("#productModalContainer", 'Product/ProductInfoModal', model, function ()
	{
		$('#productModal').modal('show');
		attachProductModalHandlers();
	});
}

function attachProductModalHandlers()
{

	$("#btnSaveProduct").click(function ()
	{
		saveProductInfo();
		return false;
	});

	$("#btnDeleteProduct").click(function ()
	{
		deleteProduct();
		return false;
	});


}

function saveProductInfo()
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
		$('#productModal').modal('hide');
		enableActions(false);
		loadGrid();
	});

	return false;
}
