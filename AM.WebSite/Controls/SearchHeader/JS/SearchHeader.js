(function ($)
{
	$.fn.searchHeader = function (options)
	{
		var ctrl = $(this);
		var id = ctrl[0].id;

		// Store options on the plugin element
		if (options != undefined)
			$(ctrl).data('options', options);
		else
			options = $(ctrl).data('options');

		// Add custom functions
		$.extend(this, {
			init: function()
			{
				ctrl.css('cursor', 'pointer');

				ctrl.click(function ()
				{
					$.toggleBox(id, null, function () { ctrl.trigger("change"); });
				});
			},
			collapse: function ()
			{
				$.collapseBox(id, function () { ctrl.trigger("change"); });
			},
			expand: function ()
			{
				$.expandBox(id, function () { ctrl.trigger("change"); });
			}
		});

		return this;
	};
})(jQuery);
