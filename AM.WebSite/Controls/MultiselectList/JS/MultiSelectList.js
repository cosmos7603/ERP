(function ($)
{
	$.fn.multiselectList = function (options)
	{
		var ctrl = $(this);

		// Store options on the plugin element
		if (options != undefined)
			$(ctrl).data('options', options);
		else
			options = $(ctrl).data('options');

		// Add extenion methods
		$.extend(this, {
			init: function ()
			{
				$(ctrl).find("#lnkClearSearch").click(function ()
				{
					$(ctrl).multiselectList().clearSearch();
					return false;
				});

				$(ctrl).find("#txtSearch").keyup(function (e)
				{
					if (e.keyCode == 27)
					{
						$(ctrl).multiselectList().clearSearch();
						return false;
					}

					var text = $(this).val().toLowerCase();
					var lnkClearSearch = $(ctrl).find("#lnkClearSearch");

					if (text.length > 0)
						lnkClearSearch.show();
					else
						lnkClearSearch.hide();

					$(ctrl).find("label").each(function (key, value)
					{
						var itemText = $(this).text();
						var itemDiv = $(this).parent();

						if (itemText.toLowerCase().indexOf(text) != -1)
							itemDiv.show();
						else
							itemDiv.hide();
					});
				});

				$(ctrl).find("#chkSelectAll").click(function ()
				{
					var checked = $(this).checked();

					$(ctrl).find("input[type='checkbox']").each(function (key, value)
					{
						$(this).checked(checked);

						if ($(this).checked())
							$(this).closest(".item").addClass("selected");
						else
							$(this).closest(".item").removeClass("selected");
					});
				});

				$(ctrl).find("div.item").click(function ()
				{

					if ($(this).find("input[type=checkbox]").checked())
						$(this).addClass("selected");
					else
						$(this).removeClass("selected");
				});
			},
			clearSearch: function ()
			{
				$(ctrl).find("#txtSearch").val("");
				$(ctrl).find("#txtSearch").focus();
				$(ctrl).find("#lnkClearSearch").hide();
				$(ctrl).find("label").each(function (key, value)
				{
					$(this).parent().show();
				});
			},
			disable: function ()
			{
				var items = $(ctrl).find("div.item").not(".selected");

				$(items).addClass("disabled");
				$(ctrl).addClass("disabled");
			},
			enable: function ()
			{
				var items = $(ctrl).find("div.item").not(".selected");

				$(items).removeClass("disabled");
				$(ctrl).removeClass("disabled");
			},
			val: function ()
			{
				var itemList = new Array();

				$(ctrl).find("input:checked").each(function (key, value)
				{
					var value = $(this).attr("name");
					itemList.push(value);
				});

				return itemList;
			},
			text: function ()
			{
				var s = "";

				$(ctrl).find("input:checked").each(function (key, value)
				{
					var text = $(this).parent().text();
					if (s != "") s = s + ", ";
					s = s + text;
				});

				return s;
			}
		});

		return this;
	};
})(jQuery);
