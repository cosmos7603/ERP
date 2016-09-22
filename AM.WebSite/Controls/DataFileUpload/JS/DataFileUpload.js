(function ($)
{
	$.fn.dataFileUpload = function (options)
	{
		var ctrl = $(this);

		// Store options on the plugin element
		if (options != undefined)
			$(ctrl).data('options', options);
		else
			options = $(ctrl).data('options');

		if (options.multiple === true)
			$("#hddDataFileUploadInput").attr("multiple", "multiple");
		else
			$("#hddDataFileUploadInput").removeAttr("multiple");

		// Add custom functions
		$.extend(this, {
			init: function()
			{
				//setViewLink($(this));
				setViewLink($(this).find(".row"));

				ctrl.on("click", ".btnUpload", function ()
				{
					var uploadButton = $(this);
					var item = uploadButton.closest(".row");

					$.uploadDataFile(
						function (data)
						{
							var progressBar = item.find('.divProgressBar');
							var options = $(ctrl).data('options');

							item.find(".btnUpload").disabled();
							item.find(".divProgress").show();
							item.find(".divFileName").hide();
							
							$.updateProgress(progressBar, 0);
						},
						function (data)
						{
							var progressBar = item.find('.divProgressBar');
							var options = $(ctrl).data('options');

							$.updateProgress(progressBar, data.PercentComplete);
						},
						function (data)
						{
							var progressBar = $(ctrl).find('#divProgressBar');
							var options = $(ctrl).data('options');
							var newItem = item.clone();

							for (var i = 0; i < data.length; i++)
							{
								if (options.multiple && ((item.index() - 1) === $(ctrl).find(".row").length))
									item.after(newItem);

								// Update values
								item.find(".divFileName a").text(data[i].FileName);
								item.attr("data-file-id", data[i].DataFileId);
								item.attr("data-file-key", data[i].DataFileKey);

								// Re enable current item controls
								item.find(".btnUpload").enabled();
								item.find(".divProgress").hide();
								item.find(".divFileName").show();

								// Re enable new item controls
								newItem.find(".btnUpload").enabled();
								newItem.find(".divProgress").hide();
								newItem.find(".divFileName").show();

								// Set view link
								setViewLink(item);

								// Set progress to 100
								$.updateProgress(progressBar, 100);

								$(ctrl).trigger({
									type: "uploadCompleted",
									DataFileId: data[i].DataFileId,
									Filename: data[i].FileName
								});

								item = item.next(".row");
								newItem = item.clone();
							}
						},
						options.extensions);
					return false;
				});

				ctrl.on("click", ".btnRemove", function ()
				{
					var removeButton = $(this);
					var item = removeButton.closest(".row");

					var fileId = item.attr("data-file-id");
				    var fileName = item.find(".divFileName a").text();

					// Clear values
					item.find(".divFileName a").text("");
					item.attr("data-file-id", "");
					item.attr("data-file-key", "");

					// If not is last item, then remove it
					if ((item.index() - 1) != $(ctrl).find(".row").length)
						item.remove();

					$(ctrl).trigger({type: "fileRemoved", DataFileId: fileId, Filename: fileName });

					return false;
				});
			},
			id: function()
			{
				return $(ctrl)[0].id;
			},
			// Val
			val: function () {
				var options = $(ctrl).data('options');

				if (options.multiple)
				{
					var itemValues = [];

					$(ctrl).find(".row").each(function ()
					{
						if ($(this).attr("data-file-id") != "" && $(this).attr("data-file-id") != "0")
							itemValues.push(parseInt($(this).attr("data-file-id"), "10"));
					});

					return itemValues;
				}
				
				return parseInt($(ctrl).find(".row").eq(0).attr("data-file-id"), "10");
			},
			// Key
			key: function ()
			{
				var options = $(ctrl).data('options');

				if (options.multiple)
				{
					var itemKeys = [];

					$(ctrl).find(".row").each(function ()
					{
						if ($(this).attr("data-file-key") != "")
							itemKeys.push($(this).attr("data-file-key"));
					});

					return itemKeys;
				}
				
				return $(ctrl).find(".row").eq(0).attr("data-file-key");
			},
			// Clear
			clear: function ()
			{
				var itemCount = $(ctrl).find(".row").length;

				for (var i = itemCount - 1; i > 0 ; i--)
				{
					$(ctrl).find(".row").eq(i).remove();
				}

				$(ctrl).find(".divFileName a").text("");
				$(ctrl).find(".row").attr("data-file-id", "");
				$(ctrl).find(".row").attr("data-file-key", "");
			}
		});

		function setViewLink(item)
		{
			var dataFileKey = item.attr("data-file-key");
			var divFileName = item.find(".divFileName a");

			if (dataFileKey != null && dataFileKey != "")
			{
				divFileName.click(function ()
				{
					$.downloadDataFile(dataFileKey);
					return false;
				});
			}
		}

		return this;
	};
})(jQuery);