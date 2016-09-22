(function ($)
{
    $.fn.monthYearSelection = function (options)
    {
    	var ctrl = $(this);

        // Store options on the plugin element
        if (options != undefined)
            $(ctrl).data('options', options);
        else
            options = $(ctrl).data('options');

        var $monthYearSelector = $("#" + options.ID);
        var $cboMonth = $("#" + options.cboMonthID);
        var $cboYear = $("#" + options.cboYearID);

        // Add custom functions
        $.extend(this, {
            // Recreate And Get Data
            getMonth: function ()
            {
                return $cboMonth.combo().val();
            },

            getYear: function ()
            {
                return $cboYear.combo().val();
            },

            select: function (month, year)
            {
            	$cboMonth.combo().select(month);
            	$cboYear.combo().select(year);
            },

            reset: function()
            {
            	$cboMonth.combo().select(options.initialMonthValue);
            	$cboYear.combo().select(options.initialYearValue);
            }
        });

        return this;
    };
})(jQuery);