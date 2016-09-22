(function ($)
{
	$.fn.richEditor = function (args)
	{
		var $ctrl = $(this);
		var ctrlId = $ctrl.attr('id');

		if (args != undefined)
		{
			// Delete TinyMCE instances - Prevents AjaxLoadErrors on Firefox
			if (typeof window.tinymce != 'undefined' && $(window.tinymce.editors).length > 0)
			{
				$(window.tinymce.editors).each(function (idx)
				{
					try
					{
						if (this.id == ctrlId)
							tinymce.remove(idx);
					} catch (e){}
				});
			}

			$ctrl.data("options", args);

			tinymce.init({
				selector: '#' + ctrlId,
				height: 350,
				plugins: [
							"advlist autolink lists link image charmap print preview anchor",
							"searchreplace visualblocks code fullscreen",
							"insertdatetime media table contextmenu paste"
				],
				toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | charmap | fullscreen code",
				forced_root_block: false
			});
		}

		var options = $ctrl.data("options");

		// Add custom functions
		$.extend(this, {
			disable: function ()
			{
				tinymce.editors[ctrlId].setMode('readonly');
			},

			enable: function ()
			{
				tinymce.editors[ctrlId].setMode('design');
			},

			val: function ()
			{
				return tinymce.editors[ctrlId].getContent();
			}
		});

		return this;
	};
})(jQuery);