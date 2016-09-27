$(document).ready(function ()
{
	setupGrid();

});







function addNew()
{
	showClientModal({ setupMode: SETUP_MODE_NEW });
}

function remove()
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

function edit()
{
	var selectedRow = $('#dtgClient tbody tr.selected')[0];
	var clientId = selectedRow.id;
	showClientModal({ setupMode: SETUP_MODE_EDIT, entityId: clientId });
	return false;
}





function triggerSearch()
{
	loadGrid();
}



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
			{ title: "Condición de IVA", data: "ClientType.Description", width: '10%' },
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




function loadGrid()
{
	$("#dtgClient").DataTable().reload();
}
