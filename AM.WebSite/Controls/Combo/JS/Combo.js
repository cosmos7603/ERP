(function ($)
{
    $.fn.combo = function (args, value)
    {
        var $ctrl = $(this);

        function select2Focus()
        {
        	var select2 = $(this).data("select2");

            setTimeout(function ()
            {
                if (!select2.opened())
                {
                    select2.open();
                }
            }, 0);
        }

        switch (args)
        {
            case "params":
                $ctrl.data("params", value);
                break;
            default:
                if (args != undefined)
                {
                    $ctrl.select2(args);
                    $ctrl.data("options", args);
                    $ctrl.one("select2-focus", select2Focus).on("select2-blur", function ()
                    {
                        $(this).one("select2-focus", select2Focus);
                        $(this).removeClass("error");
                    });

                    $ctrl.on("change", function ()
                    {
                        $(this).removeClass("error");
                    });
                }
        }

        var options = $ctrl.data("options");

        // Add custom functions
        $.extend(this, {
            // Recreate And Get Data
            refresh: function (callback, selectedValue)
            {
                //Make URL With Params
                var url = options.action;
                // Get Async DataSource
                $.post(
					url,
					$ctrl.combo().getParams(),
					function (data)
					{
						$ctrl.combo().clean();
						$ctrl.append("<option value=\"\"> " + options.ddlOption + "</option>");
						$(data).each(function ()
						{
							$ctrl.append("<option value=\"" + this.Key + "\"> " + this.Text + "</option>");
						});
						if (selectedValue) $ctrl.select2("val", selectedValue);
						if (callback) callback();
					});
            },

            select: function(v)
            {
            	$ctrl.select2("val", v);
            },

            clean: function ()
            {
                $ctrl.select2("val", "");
                $ctrl.empty();
            },

            reset: function ()
            {
            	$ctrl.select2("val", "");
            },

            getParams: function ()
            {
                if ($ctrl.data("params") == undefined)
                	return null;

                return $ctrl.data("params")();
            },

            text: function ()
            {
                return $ctrl.select2("data").text;
            },

        	isEmpty: function (hasDefault)
        	{
				if(hasDefault)
					return $ctrl.children().length === 1;

				return $ctrl.children().length === 0;
			}

        });

        return this;
    };
})(jQuery);