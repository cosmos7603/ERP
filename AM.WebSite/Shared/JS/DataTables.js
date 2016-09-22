if ($.fn.dataTable != undefined)
{
	$.extend(true, $.fn.dataTable.defaults, {
		lengthChange: false,
		responsive: true,
		processing: true,
		serverSide: true,
		searching: false,
		paging: false,
		ordering: true,
		info: true,
		scrollCollapse: false,
		autoWidth: true,
		pageLength: 50,
		scrollY: 420,
		language:
			{
				zeroRecords: '<div id="divNoRecordsFoundOnGrid" style="padding: 75px; text-align: center; color: red; margin-left: auto;">The search criteria resulted in no matches. Please try again.</div>'
			},
		ajax: function (data, callback, settings)
		{
			// Default pager options
			var pager = {
				PageIndex: 1,
				PageSize: 0,
				SortField: "",
				SortDirection: "ASC"
			};

			// Is sorting?
			if (data.order && data.order.length > 0)
			{
				var orderColumn = data.columns[data.order[0].column];

				if (orderColumn.name != "")
					pager.SortField = orderColumn.name;
				else
					pager.SortField = orderColumn.data;

				pager.SortDirection = data.order[0].dir.toUpperCase();
			}

			// Has rows?
			if (data.length > 0)
			{
				pager.PageIndex = (data.start / data.length) + 1;
				pager.PageSize = data.length;
			};

			// Prepare post data with params and pager info
			var postData = $.extend(settings.oInit.params(), pager);

			// Do custom ajax post
			$.ajax({
				type: "POST",
				url: settings.oInit.url,
				data: JSON.stringify(postData),
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function (jr)
				{
					// Default data for datatables.net (empty)
					var d = {
						recordsTotal: 0,
						recordsFiltered: 0,
						data: new Array()
					}

					// If coming with status?
					if (jr.Status != undefined)
					{
						// If valid, show data..
						if (jr.Status)
						{
							d = {
								recordsTotal: jr.ReturnValue,
								recordsFiltered: jr.ReturnValue,
								data: jr.Data
							}
						}
						else
						{
							// If not, show validation errors.
							$(".validation-msg").each(function (key, value)
							{
								$(this).html(jr.ValidationHTML);
							});
						}
					}
					else
					{
						// Data comes directly as an array (no paging, no validation, etc)
						d = {
							recordsTotal: jr.length,
							recordsFiltered: jr.length,
							data: jr
						}
					}

					// Call datatables
					callback(d);
				},
				error: function (data)
				{
					alert("Error on AJAX call for DataTables.net");
				}
			});
		},
		initComplete: function (settings)
		{
			var table = new $.fn.dataTable.Api(settings);

			// Add tooltips
			$(".glyphicon-comment").qtip({ content: { text: $(this).attr("title") } });

			// Custom select events
			table.on('click', 'tr', function ()
			{
				if (settings.oInit.onSelect == undefined)
					return;

				var tr = this;
				var row = table.row(this);
				var data = row.data();

				return $.formCheckChanges(function ()
				{
					$.clearValidations();
					table.$('tr.selected').removeClass('selected');
					$(tr).addClass('selected');

					settings.oInit.onSelect(data);
				});
			});
		}
	});

	// Extensions for DataTables
	$.fn.dataTable.Api.register('reload()', function ()
	{
		var table = new $.fn.dataTable.Api(this);
		table.ajax.reload();
	});

	$.fn.dataTable.Api.register('unselect()', function ()
	{
		var table = new $.fn.dataTable.Api(this);
		table.$('tr.selected').removeClass('selected');
	});

	//$.fn.dataTable.Api.register('select()', function ()
	//{
	//	var table = new $.fn.dataTable.Api(this);
	//	table.$('tr.selected').removeClass('selected');
	//});

	// Adjust column widths when a table is made visible in a Bootstrap tab
	$(document).ready(function ()
	{
		// TODO: REVIEW THIS ISSUE THAT STILL HAPPENS!
		$("li").on("shown.bs.tab", function ()
		{
			setTimeout(function ()
			{
				var t = $().DataTable().tables();

				if (t != null && t.columns != undefined)
					t.columns.adjust().draw();
			}, 250);
		});

		$(".search-header").on("change", function ()
		{
			setTimeout(function ()
			{
				var t = $().DataTable().tables();

				if (t != null && t.columns != undefined)
					t.columns.adjust().draw();
			}, 250);
		});
	});

	// Renders
	$.fn.dataTable.render.check = function (data, type, full, meta)
	{
		if (data)
			return "<span class=\"fa fa-check\"></span>";
		else
			return "";
	};

	$.fn.dataTable.render.comments = function (data, type, full, meta)
	{
		if (data != "")
			return '<i class="fa fa-comment" title="' + data + '"></i>';
		else
			return "";
	}

	$.fn.dataTable.render.dataFileList = function (data, type, full, meta)
	{
		if (data != "")
		{
			var attachs = data.split(",");
			var html = "";

			for (i = 0; i < attachs.length; i++)
			{
				if (html != "")
					html += ", ";

				var key = attachs[i].split("@@")[0];
				var fileName = attachs[i].split("@@")[1];

				html += "<a href=\"javascript:$.downloadDataFile('" + key + "')\">" + fileName + "</a>";
			}

			return html;
		}
		else
			return "";
	}

	$.fn.dataTable.render.hour = function (data, type, full, meta)
	{
		var value = "'" + data + "'";
		return data == null ? "" : data + " hs";
	}

	$.fn.dataTable.render.currency = function (data, type, full, meta)
	{
		if (isNaN(data))
			return data;

		return $.formatCurrency(data)
	}

	$.fn.dataTable.render.prct = function (data, type, full, meta)
	{
		return $.formatPrct(data)
	}

	$.fn.dataTable.render.decimal = function (data, type, full, meta)
	{
		if (isNaN(data))
			return data;

		return $.formatDecimal(data)
	}

	$.fn.dataTable.render.decimal8 = function (data, type, full, meta)
	{
		if (isNaN(data))
			return data;

		return $.formatDecimal(data, 8)
	}

	$.fn.dataTable.render.NA = function (data, type, full, meta)
	{
		if (data == "N/A")
			return "<div style=\"color: gray\">N/A</div>";

		return data;
	}

	$.fn.dataTable.render.button = function (data, type, full, meta)
	{
		return "<button class='btn btn-default btn-xs' type='button'><i class='fa fa-" + data + "'></i></button>";
	}

	$.fn.dataTable.render.play = function (data, type, full, meta)
	{
		return $.fn.dataTable.render.button("play");
	}

	$.fn.dataTable.render.edit = function (data, type, full, meta)
	{
		return $.fn.dataTable.render.button("edit");
	}

	$.fn.dataTable.render.plus = function (data, type, full, meta)
	{
		return $.fn.dataTable.render.button("plus");
	}

	$.fn.dataTable.render.trash = function (data, type, full, meta)
	{
		return $.fn.dataTable.render.button("trash");
	}

	$.fn.dataTable.render.remove = function (data, type, full, meta)
	{
		return $.fn.dataTable.render.button("remove");
	}
}
