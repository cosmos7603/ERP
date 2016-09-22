$(document).ready(function ()
{
	$.global();
});

$.global = function ()
{
	// Apply global control behaviors
	if ($.controls != undefined)
	{
		$.controls();
	}

	// Disable tab navigation when tab is not active
	$('a[data-toggle="tab"]').on('click', function (e)
	{
		if ($(this).parent('li').hasClass('disabled'))
		{
			e.preventDefault();
			return false;
		};
	});

	// Set validator defaults (for jQuery Validate plugin)
	if (jQuery.validator != undefined)
	{
		jQuery.validator.setDefaults({
			errorPlacement: function (error, element) { return true; }
		});

		// Select2 makes select hidden, in order to allow client side validation put this line
		$.validator.setDefaults({ ignore: "input[type='text']:hidden" });
	}

	// Pull down all controls with pull-down custom class (not existing in BS3)
	$('.pull-down').each(function ()
	{
		var $this = $(this);
		$this.css('margin-top', $this.parent().height() - $this.height())
	});

	// Clickable rows on tables
	$(".clickable-row").click(function (event)
	{
		if (!$(event.target).is('input'))
		{
			var chk = $(this).find("input:last");

			if (chk.checked())
				chk.checked(false);
			else
				chk.checked(true);
		}
	});

	// Dismiss alerts
	$(".validation-msg").click(function ()
	{
		$(".alert").alert("close");
	});
}
