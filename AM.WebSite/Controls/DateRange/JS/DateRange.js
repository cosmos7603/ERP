(function ($)
{
    $.fn.dateRange = function (options)
    {
        var ctrl = $(this);
        // Store options on the plugin element
        if (options != undefined)
            $(ctrl).data('options', options);
        else
            options = $(ctrl).data('options');

        var $dateRangeSelector = $("#" + options.ddlDateRangeId);
        var $fromDate = $("#" + options.txtFromDateId);
        var $toDate = $("#" + options.txtToDateId);

        $dateRangeSelector.change(function (e)
        {
            var $selectedOption = $dateRangeSelector.find("option:selected");
            var fromDate = $selectedOption.data("from");
            var toDate = $selectedOption.data("to");
            $fromDate.val(fromDate);
            $toDate.val(toDate);
        });

        $fromDate.datepicker().on('changeDate', function (ev)
        {
            $dateRangeSelector.val('Custom');
        });

        $toDate.datepicker().on('changeDate', function (ev)
        {
            $dateRangeSelector.val('Custom');
        });

        // Add custom functions
        $.extend(this, {
            // Recreate And Get Data
            getToDate: function ()
            {
                return $toDate.val();
            },

            getFromDate: function ()
            {
                return $fromDate.val();
            },

            reset: function ()
            {
                $dateRangeSelector.val(options.initialDateRange);
                $fromDate.val(options.initialFromValue);
                $toDate.val(options.initialToValue);
            },

            setRange: function (selectedRange)
            {
                $dateRangeSelector.val(selectedRange);
                $dateRangeSelector.trigger('change');
            },

            getRange: function ()
            {
                return $dateRangeSelector.val();
            }
        });

        return this;
    };
})(jQuery);