
	$(document).ready(function ()
	{

		$("#btnDelete").click(function ()
		{
			removeEntity();
			return false;
		});
		$("#btnEdit").click(function ()
		{
			var grid = $("#grid").data("kendoGrid");
			var id = grid.dataItem(grid.select()).id;
			edit(id);
			return false;
		});
		$("#btnNew").click(function ()
		{
			addNew();
			return false;
		});
		$("#btnRefresh").click(function ()
		{
			enableActions(false);
			loadGrid();
			return false;
		});
	});
function onRowSelected()
{
	enableActions();
}
function onGridLoad()
{
	enableActions(false);
}
function enableActions(enable)
{
	if (enable == undefined)
		var enable = true;
	$("#btnEdit").enable(enable);
	$("#btnDelete").enable(enable);
}
function loadGrid()
{
	$('#grid').data('kendoGrid').dataSource.read();
}

function addNew()
{
	showModal({ setupMode: SETUP_MODE_NEW });
}



function triggerSearch()
{
	loadGrid();
}


function edit()
{
	showModal({ setupMode: SETUP_MODE_EDIT, entityId: id });
	return false;
}
function showModal(model)
{
	var url = $.getAbsoluteUrlFromCurrentController('EntityInfoModal');
	$.partialView("#entityModalContainer", url, model, function ()
	{
		$('#entityModal').modal('show');
		attachEntityModalHandlers();
	});
}

function attachEntityModalHandlers()
{

	$("#btnSave").click(function ()
	{
		saveEntityInfo();
		return false;
	});

	//$("#btnDelete").click(function ()
	//{
	//	removeEntity();
	//	return false;
	//});
}
