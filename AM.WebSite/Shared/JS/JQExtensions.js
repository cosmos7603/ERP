// JQuery Extensions Only
(function ($)
{
	$.fn.readonly = function (text)
	{
		this.attr("readonly", "readonly");
		this.attr("class", "readonly");

		if (text != undefined)
			this.val(text);
	};
})(jQuery);

(function ($)
{
	$.fn.checked = function (value)
	{
		if (value != undefined)
			this.prop("checked", value);

		return this.is(":checked");
	};
})(jQuery);

(function ($)
{
	$.fn.disabled = function ()
	{
		this.addClass("disabled");
		this.prop("disabled", true);
		return this.is(":disabled");
	};
})(jQuery);

(function ($)
{
	$.fn.enabled = function ()
	{
		this.removeClass("disabled");
		this.prop("disabled", false);
		return this.is(":disabled");
	};
})(jQuery);

(function ($)
{
	$.fn.enable = function (status)
	{
		if (status)
			$(this).enabled();
		else
			$(this).disabled();
	};

})(jQuery);

// Evaluate boolean hidden fields
(function ($)
{
	$.fn.true = function ()
	{
		return this.val().toLowerCase() == "true";
	};
})(jQuery);

(function ($)
{
	$.fn.false = function ()
	{
		return this.val().toLowerCase() == "false";
	};
})(jQuery);

// Defines an event for when the user clicks ENTER key o a textbox
(function ($)
{
	$.fn.onEnterKey =
        function (closure)
        {
        	$(this).keydown(
                function (event)
                {
                	code = event.keyCode ? event.keyCode : event.which;

                	if (code == 13)
                	{
                		closure();

                		return false;
                	}
                });
        }
})(jQuery);

// Selects the next option on the given dropdown
(function ($)
{
	$.fn.nextOption = function ()
	{
		var next = $(this).find("option:selected", "select").next("option");

		if (next.length > 0)
			$(this).find("option:selected", "select").removeAttr("selected").next("option").attr("selected", "selected");

	};

})(jQuery);

// Selects the previous option on the given dropdown
(function ($)
{
	$.fn.prevOption = function ()
	{
		$(this).find("option:selected", "select").removeAttr("selected").prev("option").attr("selected", "selected");
		return false;
	};

})(jQuery);

// Selects the first option on the given dropdown
(function ($)
{
	$.fn.reset = function ()
	{
		$(this).find("option:first-child").attr("selected", "selected");
		return false;
	};

})(jQuery);

// Only allows for numbers on textboxes
(function ($)
{
	$.fn.numbersOnly = function ()
	{
		$(this).keypress(function (e)
		{
			if (e.which == 44 || e.which == 46 || e.which == 45)
				return true;

			if (e.which != 13 && e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57))
				return false;
		});
	};

})(jQuery);

// Add SortableList behavior to a control
(function ($)
{
	$.fn.sortableList = function ()
	{
		$(this).children("div.item").click(function ()
		{
			var selectedItem = $(this).parent().children("div.item.selected");

			if (selectedItem.length > 0)
				$(selectedItem).removeClass("selected");

			$(this).addClass("selected");
		});
	};

})(jQuery);

// Get sorted IDs from Sortable List
(function ($)
{
	$.fn.sortedValues = function ()
	{
		var itemList = new Array();

		$(this).find("div.item").each(function (key, value)
		{
			var value = $(this).data("id");
			itemList.push(value);
		});

		return itemList;
	};

})(jQuery);

// Move items up & down
(function ($)
{
	$.fn.moveSortableListItemUp = function ()
	{
		var selectedItem = $(this).children("div.item.selected");
		var previousItem = selectedItem.prev();

		selectedItem.insertBefore(previousItem);
	}
})(jQuery);

(function ($)
{
	$.fn.moveSortableListItemDown = function ()
	{
		var selectedItem = $(this).children("div.item.selected");
		var nextItem = selectedItem.next();

		selectedItem.insertAfter(nextItem);
	}
})(jQuery);

// Amounts
(function ($)
{
	$.fn.amount = function ()
	{
		return $.parseCurrency($(this).val().replace('$', '').replace(',', '').replace('(', '-').replace(')', ''));
	};
})(jQuery);
