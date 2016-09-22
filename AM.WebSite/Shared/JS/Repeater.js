(function ($)
{
	$.fn.repeater = function (options)
	{
		$.defaultOptions = {
			pagerTemplate: "hbRepeaterPagerTemplate",
			emptyTemplate: "hbRepeaterEmptyTemplate",
			defaultTemplate: "hbRepeaterDefaultTemplate",
			pageSize: 50,
			sortField: "",
			sortDirection: "asc",
			block: true
		};

		$.defaultPager = {
			PageIndex: 0,
			PageSize: 0,
			SortField: "",
			SortDirection: "asc"
		};

		var templates = {};
		var repeaterData = {};
		var pagerData = {};

		var ctrl = $(this);

		// Store options on the plugin element
		if (options != undefined)
		{
			// Extend default options and save
			var o = $.defaultOptions;
			$.extend(o, options);
			options = o;

			// Load and compile templates
			templates.repeater = Handlebars.compile($("#" + options.repeaterTemplate).html());
			templates.pager = Handlebars.compile($("#" + options.pagerTemplate).html());
			templates.default = Handlebars.compile($("#" + options.defaultTemplate).html());
			templates.empty = Handlebars.compile($("#" + options.emptyTemplate).html());

			// Store data on control
			$(ctrl).data('options', options);
			$(ctrl).data('templates', templates);
			$(ctrl).data('repeaterData', {});
			$(ctrl).data('pagerdata', {});

			// Render default template
			$(getRepeaterSelector(this)).html(templates.default({}));
		}
		else
		{
			options = $(ctrl).data('options');
			repeaterData = $(ctrl).data('repeaterData');
			pagerData = $(ctrl).data('pagerData');
			templates = $(ctrl).data('templates');
		}

		// Add custom functions
		$.extend(this, {
			// Return Id
			id: function ()
			{
				return $(ctrl)[0].id;
			},
			// Reset
			reset: function()
			{
				// Get selector for repeater & pager
				var pagerSelector = getPagerSelector(ctrl);
				var repeaterSelector = getRepeaterSelector(ctrl);

				// Render default template
				$(getRepeaterSelector(this)).html(templates.default({}));

				// Hide pager
				$(pagerSelector).hide();

				// Clear data
				$(ctrl).data('repeaterData', {});
				$(ctrl).data('pagerdata', {});
			},
			// Refresh
			refresh: function ()
			{
				// Get data to be posted
				var pd;

				if (options.postData != undefined)
					pd = options.postData();

				// Extend to add current pager
				if (pagerData != undefined)
				{
					$.extend(pd, { Pager: pagerData });
				}
				else
				{
					// First time, use options passed
					pagerData = $.defaultPager;

					pagerData.PageSize = options.pageSize;
					pagerData.SortField = options.sortField;
					pagerData.SortDirection = options.sortDirection;
				}

				// Pass pager data
				$.extend(pd, { Pager: pagerData });

				// Post
				$.postData(
					options.url,
					pd,
					function (jr)
					{
						// Get selector for repeater & pager
						var pagerSelector = getPagerSelector(ctrl);
						var repeaterSelector = getRepeaterSelector(ctrl);

						// Retrieve items and data from json response
						repeaterData = jr.Data.Items;
						pagerData = jr.Data.Pager;

						// Save repeater and pager data
						$(ctrl).data('repeaterData', repeaterData);
						$(ctrl).data('pagerData', pagerData);

						// Render repeater
						if (pagerData.RowCount == 0)
							$(repeaterSelector).html(templates.empty(repeaterData));
						else
							$(repeaterSelector).html(templates.repeater(repeaterData));

						// Render pager
						if (options.pagerTemplate != undefined)
						{
							$(pagerSelector).html(templates.pager(pagerData));
							$(pagerSelector).show();
						}

						// Update pager
						$(pagerSelector).find("#btnPrev").enable(pagerData.PageIndex > 0);
						$(pagerSelector).find("#btnNext").enable(pagerData.PageIndex < pagerData.PageCount - 1);

						$(pagerSelector).find("#btnNext").click(function()
						{
							$(ctrl).repeater().nextPage();
						})

						$(pagerSelector).find("#btnPrev").click(function ()
						{
							$(ctrl).repeater().prevPage();
						});

						// Automatic sorting (only for tables headers)
						$(ctrl).find("th").each(function ()
						{
							var sortField = $(this).data("sort");

							if (sortField != undefined)
							{
								$(this).css('cursor', 'pointer');

								$(this).click(function ()
								{
									$(ctrl).repeater().sort(sortField);
								});
							}
						});

						// Trigger onLoaded
						options.onLoaded(pd, jr);
					},
					null,
					{
						block: options.block,
						scrollToTop: false
					});
			},
			dataSource: function(index)
			{
				if (index == undefined)
					return repeaterData;
				else if (index >= 0)
					return repeaterData[index];

				return null;
			},
			nextPage: function ()
			{
				pagerData.PageIndex = pagerData.PageIndex + 1;
				$(ctrl).data('pagerData', pagerData);
				$(ctrl).repeater().refresh();
			},
			prevPage: function ()
			{
				pagerData.PageIndex = pagerData.PageIndex - 1;
				$(ctrl).data('pagerData', pagerData);
				$(ctrl).repeater().refresh();
			},
			sort: function (sortField)
			{
				// If sorting by the same column, invert sort order
				if (pagerData.SortField == sortField)
				{
					if (pagerData.SortDirection.toLowerCase() == "asc")
						pagerData.SortDirection = "desc";
					else
						pagerData.SortDirection = "asc";
				}

				// Set new sort field
				pagerData.SortField = sortField;

				// Upadte pager data
				$(ctrl).data('pagerData', pagerData);

				// Refresh
				$(ctrl).repeater().refresh();
			}
		});

		function getRepeaterSelector(repeater)
		{
			var repeaterSelector = "#" + ($(repeater)[0].id);

			// if it's table, render on body
			if ($(repeater).is("table"))
				repeaterSelector = repeaterSelector + ">tbody";

			return repeaterSelector;
		}

		function getPagerSelector(repeater)
		{
			return "#" + options.pager;
			return pagerSelector;		}

		return this;
	};
})(jQuery);