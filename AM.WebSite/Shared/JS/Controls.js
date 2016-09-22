$.controls = function ()
{
	// PhoneBox
	$(".ps-phone-box").inputmask("(999) 999-9999");

	// MonthYearBox
	$(".ps-month-year-box").inputmask("99/99");

	// CurrencyBox
	$(".ps-currency-box").numbersOnly();
	$(".ps-currency-box").on('blur', function ()
	{
		$(this).val($.formatCurrency($.parseCurrency($(this).val())))
	});

	// IntBox
	$(".ps-int-box").numbersOnly();

	// DecimalBox
	$(".ps-decimal-box").on('blur', function ()
	{
		var dp = $(this).data("dp");

		if (dp != null)
			$(this).val($.formatDecimal($(this).val(), dp))
	});

	// DatePicker
	$(".ps-date-picker").datepicker({
		format: 'dd M yyyy',
		autoclose: true,
		orientation: 'bottom left'
	}).on('change', function (e)
	{
		$(".datepicker").hide();
	});

	//ComboBox
	$(".i-checks").iCheck({
		checkboxClass: "icheckbox_square-green",
		radioClass: "iradio_square-green"
	});

	//Automatic dropdown on hover
	// Add hover behavior to BS menu
	$(".dropdown-auto").hover(
		function ()
		{
			$(this).children('.dropdown-menu').stop(true, true).show();
			$(this).toggleClass('open');
		},
		function ()
		{
			$(this).children('.dropdown-menu').stop(true, true).hide();
			$(this).toggleClass('open');
		});
}
