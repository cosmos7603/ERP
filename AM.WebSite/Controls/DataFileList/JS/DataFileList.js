(function ($)
{
	$.fn.dataFileList = function (options)
	{
		var ctrl = $(this);

		// Store options on the plugin element
		if (options != undefined)
			$(ctrl).data('options', options);
		else
			options = $(ctrl).data('options');

		// Custom functions
		$.extend(this, {
			init: function()
			{
				$(this).find("#btnFileListAdd").click(function ()
				{
					$.uploadDataFile(
						function (data)
						{
							var progressBar = $(ctrl).find('#divProgressBar');
							var options = $(ctrl).data('options');

							$(ctrl).find("#btnFileListAdd").disabled();
							$(ctrl).find("#divFiles").hide();
							$(ctrl).find("#divProgress").show();
							$(ctrl).find("#lblUploading").show();
							$(ctrl).find("#lblFileName").show();
							$(ctrl).find("#lblFileName").html(data.FileName.split("\\").pop());

							$.updateProgress(progressBar, 0);
						},
						function (data)
						{
							var progressBar = $(ctrl).find('#divProgressBar');
							var options = $(ctrl).data('options');

							$.updateProgress(progressBar, data.PercentComplete);
						},
						function (data)
						{
							var progressBar = $(ctrl).find('#divProgressBar');
							var options = $(ctrl).data('options');

							// Reenable add button
							$(ctrl).find("#btnFileListAdd").enabled();
							$(ctrl).find("#divFiles").show();
							$(ctrl).find("#divProgress").hide();
							$(ctrl).find("#lblUploading").hide();
							$(ctrl).find("#lblFileName").hide();

							// Set progress to 100
							$.updateProgress(progressBar, 100);

							// Add item
							$(ctrl).find("#divFiles").append("<div><a href='" + data.ViewLink + "' target='_blank' class='file-view-link' data-file-key='" + data.DataFileKey + "'>" + data.FileName + "</a>&nbsp;<a href='Remove' class='file-remove-link' data-file-key='" + data.DataFileKey + "' data-file-name='" + data.FileName + "'><span class='fa fa-times' style='margin-right: 3px; margin-top: 4px;'></span></a></div>");

							// Re-set handlers
							$(ctrl).dataFileList().setRemoveHandlers();
						},
						options.extensions);
					return false;
				});

				// Remove handlers
				$(ctrl).dataFileList().setRemoveHandlers();
			},
			disabled: function ()
			{
				$(ctrl).find("#divFiles").css("background-color", "whitesmoke");
				$(ctrl).find("a[href='Remove']").hide();
			},
			enabled: function ()
			{
				$(ctrl).find("#divFiles").css("background-color", "white");
				$(ctrl).find("a[href='Remove']").show();
			},
			setRemoveHandlers: function ()
			{
				$(ctrl).find(".file-remove-link").click(function ()
				{
					var fileKey = $(this).data("file-key");
					var fileName = $(this).data("file-name");

					// Are you sure?
					$.confirmDelete(
						"file",
						fileName,
						fileKey,
						function (fileKey)
						{
							// Remove node
							$(ctrl).find("[data-file-key='" + fileKey + "']").parent().remove();
						});

					return false;
				});
			},
			val: function ()
			{
				var dataFileList = new Array();

				$(ctrl).find(".file-view-link").each(function ()
				{
					var fileKey = $(this).data("file-key");

					dataFileList.push(fileKey);
				});

				return dataFileList;
			}
		});

		return this;
	};
})(jQuery);

