(function ($)
{
	$.fn.captcha = function (options)
	{
		var ctrl = $(this);

		// Store options on the plugin element
		if (options != undefined)
			$(ctrl).data('options', options);
		else
			options = $(ctrl).data('options');

		// Add custom functions
		$.extend(this, {
			init: function()
			{

			},
			id: function()
			{
				return $(ctrl)[0].id;
			},
			// Refresh
			refresh: function ()
			{
				$(ctrl).load(
					options.url,
					{
						ID: $(ctrl).captcha().id()
					});
			},
			// Val
			val: function ()
			{
				return $(ctrl).find("#txtCaptchaValue").val();
			},
			// Encrypted value
			encrypted: function ()
			{
				return $(ctrl).find("#hddCaptchaEncrypted").val();
			}
		});

		return this;
	};
})(jQuery);