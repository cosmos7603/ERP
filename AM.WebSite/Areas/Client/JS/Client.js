$(document).ready(function ()
{
	setupGrid();
	attachEvents();
	//showBlankDestination();
});




function attachEvents()
{
	$("#btnDelete").click(function ()
	{
		deleteClient();
		return false;
	});
	$("#btnEdit").click(function ()
	{
		editClient();
		return false;
	});
	$("#btnNew").click(function ()
	{
		addNewClient();
		return false;
	});

}


function addNewClient()
{
	showClientModal({ setupMode: SETUP_MODE_NEW });
}

function deleteClient()
{
	var selectedRow = $('#dtgClient tbody tr.selected')[0];

	var clientId = selectedRow.id;

	var data = $('#dtgClient').DataTable().row(selectedRow.rowIndex - 1).data();
	var comercialName = data.ComercialName;


	$.confirmDelete(
		"Cliente",
		comercialName,
		clientId,
		function (clientId)
		{
			$.postData(
				'Client/Delete',
				{ clientId: clientId },
				function ()
				{
					loadClients();
				});
		});
}

function editClient()
{
	var selectedRow = $('#dtgClient tbody tr.selected')[0];
	var clientId = selectedRow.id;
	showClientModal({ setupMode: SETUP_MODE_EDIT, entityId: clientId });
	return false;
}





function triggerSearch()
{
	loadClients();
}

//function resetSearch()
//{
//	$("#ddlSearchDestArea").val("");
//	$("#chkSearchSuppressInactive").checked(true);
//	$("#ddlSearchDestArea").focus();
//}

function setupGrid()
{
	var simple_checkbox = function (data, type, full, meta)
	{
		var is_checked = data == true ? "checked" : "";
		return '<input type="checkbox" class="checkbox" disabled ' +
			is_checked + ' />';
	};
	$("#dtgClient").DataTable({
		url: 'Client/List',
		params: function ()
		{
		},
		rowId: "Id",
		columns: [
			{ title: "Id", data: "Id", width: "5%" },
			{ title: "Cliente", data: "ComercialName", width: '5%' },
			{ title: "Nombre", data: "FirstName", width: '10%' },
			{ title: "Apellido", data: "LastName", width: '10%' },
			{ title: "Mail", data: "Email", width: '10%' },
			{ title: "Teléfono", data: "Telephone1", width: '10%' },
			{ title: "Teléfono 2", data: "Telephone2", width: '10%' },
			{ title: "Observaciones", data: "Observations", width: '10%' },
			{ title: "Domicilio", data: "Address1", width: '10%' },
			{ title: "Localidad", data: "City", width: '10%' },
			{ title: "Provincia", data: "Province", width: '10%' },
			{ title: "DNI", data: "DNI", width: '10%' },
			{ title: "CUIT", data: "CUIT", width: '10%' },
			{ title: "Condición de IVA", data: "ClientType", width: '10%' },
			{ title: "Activo", data: "Active", width: '10%', render: simple_checkbox }
		],
		paging: true,
		order: [[0, "asc"]],
		onSelect: function (item)
		{
			if ($('#dtgClient tbody tr.selected').length > 0)
				enableActions(true);
			else
				enableActions(false);
		}
		////onDeselect: function (items)
		////{
		////	if ($('#dtgClient tbody tr.selected').length > 0)
		////		enableActions(true);
		////	else
		////		enableActions(false);

		////	return true;
		////}


	});
	$('#dtgClient tbody').off('click', 'tr').on('click', 'tr', function ()
	{
		if ($(this).hasClass('selected'))
		{
			$(this).removeClass('selected');
		}
		else
		{
			$('#dtgClient tbody tr.selected').removeClass('selected');
			$(this).addClass('selected');
		}
	});

}


function enableActions(enable)
{
	if (enable == undefined)
		var enable = true;
	$("#btnEdit").enable(enable);
	$("#btnDelete").enable(enable);
}


function loadClients()
{
	$("#dtgClient").DataTable().reload();
}

//function showBlankDestination()
//{
//	$("#divDestinationInfo").load(
//        URLs.Utilities.DestinationSetup.DestinationInfo,
//        {
//        	SetupMode: SETUP_MODE_BLANK
//        },
//        function ()
//        {
//        	attachDestinationInfoEvents();
//        	$.formReset();
//        });
//}

//function showNewDestination()
//{
//	$("#divDestinationInfo").load(
//        URLs.Utilities.DestinationSetup.DestinationInfo,
//        {
//        	SetupMode: SETUP_MODE_NEW
//        },
//        function ()
//        {
//        	attachDestinationInfoEvents();
//        	collapseSearch();
//        	$.formReady("#frmDestinationInfo");
//        	$("#dtgDestination").DataTable().unselect();
//        	$("#txtDestinationName").focus();
//        });

//}

//function showEditDestination(destinationId)
//{
//	$("#divDestinationInfo").load(
//        URLs.Utilities.DestinationSetup.DestinationInfo,
//        {
//        	SetupMode: SETUP_MODE_EDIT,
//        	DestinationId: destinationId
//        },
//        function ()
//        {
//        	attachDestinationInfoEvents();
//        	collapseSearch();
//        	$.formReady("#frmDestinationInfo");
//        	$("#txtDestinationName").focus();
//        });
//}

//function collapseSearch()
//{
//	$("#SearchHeader").searchHeader().collapse();
//}

//function expandSearch()
//{
//	$("#SearchHeader").searchHeader().expand();
//}

//function attachDestinationInfoEvents()
//{
//	$("#btnNew").click(function ()
//	{
//		$.formCheckChanges(function ()
//		{
//			$.clearValidations();
//			showNewDestination();
//		});

//		return false;
//	});

//	$("#btnSave").click(function ()
//	{
//		save();
//		return false;
//	});

//	$("#btnCancel").click(function ()
//	{
//		$.formCheckChanges(function ()
//		{
//			$.clearValidations();
//			expandSearch();
//			showBlankDestination();
//		});

//		return false;
//	});

//	$("#btnDelete").click(function ()
//	{
//		deleteDestination();
//		return false;
//	});

//	$("#btnReactivate").click(function ()
//	{
//		reactivateDestination();
//		return false;
//	});

//	$("#btnConsolidate").click(function ()
//	{
//		consolidateDestination();
//		return false;
//	});
//}

//function save()
//{
//	var model = {
//		DestinationId: SetupInfo.DestinationId,
//		DestinationName: $("#txtDestinationName").val(),
//		DestAreaId: $("#ddlDestArea").combo().val(),
//		PpoDestinationId: $("#txtPpoDestinationId").val()
//	};

//	$.postForm(
//        $("#frmDestinationInfo"),
//        model,
//        function ()
//        {
//        	loadClients();
//        	expandSearch();
//        	showBlankDestination();
//        });
//}

//function deleteDestination()
//{
//	var destinationId = SetupInfo.DestinationId;
//	var destinationName = SetupInfo.DestinationName;

//	$.confirmDelete(
//		"Destination",
//		destinationName,
//		destinationId,
//		function (destinationId)
//		{
//			$.postData(
//				URLs.Utilities.DestinationSetup.Delete,
//				{ destinationId: destinationId },
//				function ()
//				{
//					loadClients();
//					showBlankDestination();
//					expandSearch();
//				});
//		});
//}

//function reactivateDestination()
//{
//	$.postData(
//		URLs.Utilities.DestinationSetup.Reactivate,
//		{ destinationId: SetupInfo.DestinationId },
//		function ()
//		{
//			loadClients();
//			showBlankDestination();
//			expandSearch();
//		});
//}

//function consolidateDestination()
//{
//	try
//	{
//		$.popUp(URLs.Utilities.DestinationSetup.ConsolidateView, { destinationId: SetupInfo.DestinationId });
//	}
//	catch (ex)
//	{
//		console.log(ex);
//	}

//}

//function confirmConsolidate()
//{
//	var model = {
//		SourceDestinationId: SetupInfo.DestinationId,
//		DestDestinationId: $("#frmConsolidate").find("#ddlDestination").select().val()
//	};
//	$.postForm(
//        $("#frmConsolidate"),
//        model,
//        function ()
//        {
//        	$.closePopUp();
//        	expandSearch();
//        	loadClients();
//        	showBlankDestination();
//        },
//		function ()
//		{
//			$.closePopUp();
//		}
//		);
//}

//function cancelConsolidate()
//{
//	$.closePopUp();
//}
