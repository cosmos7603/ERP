(function ($)
{
	$.fn.auto = function (args, value)
	{
		var $ctrl = $(this);

		switch (args)
		{
			case 'params':
				$ctrl.data('params', value);
				break;
			default:
				if (args != undefined)
				{
					$ctrl.autocomplete({
					    minChars: 2,
					    deferRequestBy: args.delayMs,
						id: function (data)
						{
							return data.Key;
						},
						autoSelectFirst: args.autoSelectFirst,
						dataType: 'json',
						serviceUrl: args.action,
						paramName: 'q',
						transformResult: function (response)
						{
							return {
								suggestions: $.map(response, function (dataItem)
								{
									return { value: dataItem.Text, data: dataItem.Key };
								})
							};
						},
						formatResult: function (suggestion, currentValue) {
						    // Do not replace anything if there current value is empty
						    if (!currentValue)
						    {
						        return suggestion.value;
						    }
						    
						    var pattern = '(' + currentValue.replace(/[|\\{}()[\]^$+*?.]/g, "\\$&") + ')';

						    var suggestionValueHighlighted = suggestion.value
                                .replace(new RegExp(pattern, 'gi'), '<strong>$1<\/strong>')
                                .replace(/&/g, '&amp;')
                                .replace(/</g, '&lt;')
                                .replace(/>/g, '&gt;')
                                .replace(/"/g, '&quot;')
                                .replace(/&lt;(\/?strong)&gt;/g, '<$1>');

                            //If not null use custom function for formatting every row of results.
                            var formatResultFunction;
                            if (args.formatResultFunction != null)
                                formatResultFunction = args.formatResultFunction;
                            else
                                formatResultFunction = function (suggestionValueHighlighted, suggestion) { return suggestionValueHighlighted; };

                            return suggestionResult = formatResultFunction(suggestionValueHighlighted, suggestion);
						},
						containerClass: args.containerClass,
						width: function () {
						    if (args.widthPx != null && args.widthPx > 0) return args.widthPx; else return "auto";
						},
						onSelect: function (suggestion)
						{
						    if (suggestion.data !== null && suggestion.data.Link !== null && suggestion.data.Link !== "")
						    {
						        window.location.href = suggestion.data.Link;
						    }
						    else if (args.onSelectFunction != null)
						        args.onSelectFunction(suggestion);
							else
							{
							    $(this).removeClass("error");
							    $ctrl.trigger('change');
							}
						},
						onSearchStart: function (query) {
						    if (args.onSearchStart != null)
						        args.onSearchStart(query);
						},
						onSearchComplete: function (query, suggestions) {
						    if (args.onSearchComplete != null)
						        args.onSearchComplete(query, suggestions);
						}
					});

					$ctrl.autocomplete().currentValue = args.text;
					$ctrl.autocomplete().selection = { data: args.value, value: args.text };
				}
		}

		// Add custom functions
		$.extend(this, {
			reset: function ()
			{
				$ctrl.val("");
				$ctrl.autocomplete().clear();
				$ctrl.autocomplete().selection = { data: "", value: "" };
			},

			val: function ()
			{
				// Find selection
				if ($ctrl.autocomplete().selection == null)
				{
					// Sometimes, we've seen there's no selection, but there's selectedIndex and suggestions,
					// so we take the val from there
					if ($ctrl.autocomplete().suggestions != null && $ctrl.autocomplete().suggestions.length >= $ctrl.autocomplete().selectedIndex)
						return $ctrl.autocomplete().suggestions[$ctrl.autocomplete().selectedIndex].data;
				}
				else
				{
					return $ctrl.autocomplete().selection.data;
				}

				return "";
			},

			text: function ()
			{
				return $ctrl.val();
			},

			set: function (text, value)
			{
				$ctrl.val(text);
				$ctrl.autocomplete().currentValue = text;
				$ctrl.autocomplete().selection = { data: value, value: text };
			},

			enable: function ()
			{
				$ctrl.enabled();
			},

			disable: function ()
			{
				$ctrl.disabled();
			}
		});

		return this;
	};
})(jQuery);