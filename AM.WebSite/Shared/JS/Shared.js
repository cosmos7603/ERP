// Setup Modes
var SETUP_MODE_NEW = "NEW";
var SETUP_MODE_EDIT = "EDIT";
var SETUP_MODE_BLANK = "BLANK";

// Drop down first item options
var DROPDOWN_ALL = "-- All --";
var DROPDOWN_SELECT = "-- Select --";
var DROPDOWN_OTHER = "-- Other --";
var DROPDOWN_NONE = "-- None --";

// Forms (lost changes?)
var m_formHasChanges = false;

// Clear any validation on the screen
$.clearValidations = function ()
{
	$(".validation-msg").each(function (key, value)
	{
		$(this).children(".alert").hide();
	});
}

// Reload current page
$.reloadPage = function (block)
{
	if (block == true)
		$.blockUI();

	window.location.href = window.location.href;
}

// Redirect on client side to another page
$.redirect = function (url)
{
	window.location.href = url;
}

// Open new window to a page on client side
$.openSite = function (url)
{
	window.open(url, "_blank", "directories=no, status=yes, menubar=yes, scrollbars=yes, resizable=yes,width=1000, height=800,top=200,left=200");
}

// Download data file (with encrypted link)
$.downloadDataFile = function (p)
{
	$("#hddDownloadDataFileKey").val(p);
	$("#frmDataFileDownload").submit();
}

// Download data file (with encrypted link)
$.downloadFile = function (p)
{
	$("#hddDownloadFileKey").val(p);
	$("#frmFileDownload").submit();
}

$.viewDataFile = function (dataFileKey)
{
	window.open(URLs.DataFile.View + "?p=" + dataFileKey, "_blank", "directories=no, status=no, menubar=no, scrollbars=yes, resizable=no,width=1000, height=800,top=200,left=200");
}

$.viewReport = function (reportName, orderField, id, headerText)
{
	window.open(URLs.Reports.Viewer.Index + "?ReportName=" + reportName + "&OrderField=" + orderField + "&Id=" + id + "&HeaderText=" + headerText, "_blank", "directories=no, status=no, menubar=no, scrollbars=yes, resizable=no,width=1000, height=800,top=200,left=200");
}

$.viewReportPost = function (reportParametersDictionary, target)
{
	if (target == null)
		var map = window.open("", "Map", "directories=no, status=no, menubar=no, scrollbars=yes, resizable=no,width=1000, height=800,top=200,left=200");

	// Create a form
	var mapForm = document.createElement("form");
	mapForm.id = "frmGenerateReport";
	mapForm.name = "frmGenerateReport";
	if (target == null)
	{
		mapForm.target = "Map";
		mapForm.action = URLs.Reports.Viewer.Index;
	}

	else
	{
		mapForm.action = URLs.Reports.Viewer.GetReport;
		mapForm.target = target;
	}

	mapForm.method = "POST";

	for (var i = 0; i < reportParametersDictionary.length; i++)
	{
		// Create an input
		var mapInput = document.createElement("input");
		mapInput.type = "text";
		mapInput.name = "reportParameters[" + reportParametersDictionary[i].Key + "]";
		mapInput.value = reportParametersDictionary[i].Value;
		// Add the input to the form
		mapForm.appendChild(mapInput);
	}
	// Add the form to dom
	//document.body.appendChild(mapForm);
	mapForm.submit(function (e)
	{
		e.preventDefault();
		e.returnValue = false;
		return false;
	});
}

$.exportReport = function (reportParametersDictionary)
{
	// Create a form
	var mapForm = document.createElement("form");

	mapForm.action = URLs.Reports.Viewer.ExportReport;
	mapForm.target = "_blank";
	mapForm.method = "POST";

	for (var i = 0; i < reportParametersDictionary.length; i++)
	{
		// Create an input
		var mapInput = document.createElement("input");
		mapInput.type = "text";
		mapInput.name = "reportParameters[" + reportParametersDictionary[i].Key + "]";
		mapInput.value = reportParametersDictionary[i].Value;
		// Add the input to the form
		mapForm.appendChild(mapInput);
	}
	// Add the form to dom
	document.body.appendChild(mapForm);
	mapForm.submit();
}

$.emailReport = function (reportParametersDictionary)
{
	// Create a form
	var mapForm = document.createElement("form");

	mapForm.action = URLs.Reports.Viewer.EmailReport;
	mapForm.target = "_blank";
	mapForm.method = "POST";

	for (var i = 0; i < reportParametersDictionary.length; i++)
	{
		// Create an input
		var mapInput = document.createElement("input");
		mapInput.type = "text";
		mapInput.name = "reportParameters[" + reportParametersDictionary[i].Key + "]";
		mapInput.value = reportParametersDictionary[i].Value;
		// Add the input to the form
		mapForm.appendChild(mapInput);
	}
	// Add the form to dom
	document.body.appendChild(mapForm);
	mapForm.submit();
}

// Download several data files (with encrypted link)
$.downloadFiles = function (files)
{
	//Remove any existing download iframe
	$(".download-iframe").remove();

	var formActionUrl = $("#frmFileDownload").attr("action");

	//Add a download iframe and form for each file to be downloaded and submit the form
	for (var i = 0; i < files.length; ++i)
	{
		var iframeHtml = "<iframe id=\"downloadIframe" + i + "\" class=\"download-iframe\"></iframe>";
		var formHtml = "<form action=\"" + formActionUrl + "\" method=\"post\"><input type=\"hidden\" name=\"fileKey\" value=\"" + files[i] + "\"/></form>";

		$("#uploadDownloadForms").append(iframeHtml);
		$("#downloadIframe" + i).contents().find("body").html(formHtml);
		$("#downloadIframe" + i).contents().find("form")[0].submit();
	}
}

// Download data file (with encrypted link)
$.uploadDataFile = function (
	onBeforeUpload,
	onProgress,
	onComplete,
	extensions)
{
	// Prepare ajax form
	$("#frmDataFileUpload").ajaxForm({
		dataType: "json",
		beforeSend: function (xhr, opts)
		{
			var files = $("#hddDataFileUploadInput")[0].files;
			var filename = "";

			for (var i = 0; i < files.length; i++)
			{
				// Check valid extension
				filename = files[i].name;
				var ext = filename.substring(filename.lastIndexOf(".") + 1).toLowerCase();

				if (extensions != undefined && extensions && extensions.toLowerCase().lastIndexOf(ext) == -1)
				{
					$("#hddDataFileUploadInput").val("");
					$.alert("Error", "Invalid file type. Supported extensions: " + extensions);
					xhr.abort();
					return;
				}
			}

			// Trigger event
			if (onBeforeUpload != undefined)
				onBeforeUpload(
					{
						FileName: filename
					});
		},
		uploadProgress: function (event, position, total, percentComplete)
		{
			if (onProgress != undefined)
				onProgress(
					{
						Position: position,
						Total: total,
						PercentComplete: percentComplete
					});
		},
		complete: function (xhr)
		{
			$("#hddDataFileUploadInput").val("");

			if (onComplete != undefined)
				onComplete(JSON.parse(xhr.responseText));
		}
	});

	$("#hddDataFileUploadInput").off("change");
	$("#hddDataFileUploadInput").change(function ()
	{
		var form = $("#frmDataFileUpload");

		if ($(this).val() != "")
			$(form).submit();
	});

	// Open file dialog
	$("#hddDataFileUploadInput").trigger("click");
}

// Update Bootstrap progress bar
$.updateProgress = function (progressBar, percentValue)
{
	// ProgressBar should be a jQuery object
	progressBar.css("width", percentValue + "%");
	progressBar.text(percentValue + "%");
}

// Goes to the top of the page
$.scrollToTop = function ()
{
	$("html, body").animate({ scrollTop: 0 }, 300);
}

$.showValidationError = function (title, errors)
{
	$.partialView(".validation-msg", URLs.Global.ShowValidationError, { title: title, errors: errors }, null, null, { block: false, scrollToTop: true });
};

$.showValidationSuccess = function (title)
{
	$.partialView(".validation-msg", URLs.Global.ShowValidationSuccess, { title: title }, null, null, { block: false, scrollToTop: true });
};

$.beep = function ()
{
	$.playSound("../../Content/sounds/beep");
}

$.playSound = function (filename)
{
	if ($("#sound").length == 0)
		$("body").append("<div id='sound'></div>");

	$("#sound").html('<audio autoplay="autoplay"><source src="' + filename + '.mp3" type="audio/mpeg" /><source src="' + filename + '.ogg" type="audio/ogg" /><embed hidden="true" autostart="true" loop="false" src="' + filename + '.mp3" /></audio>');
}

$.urlParam = function (name)
{
	var results = new RegExp("[\?&]" + name + "=([^&#]*)").exec(window.location.href);

	if (results != null)
		return results[1];
	else
		return "";
}

$.showInfo = function (msg)
{
	toastr.info("", msg);
}

$.showError = function (msg, errors)
{
	var errorHtml = "<ul class=\"error-list\">";
	errors.forEach(function (item) { errorHtml += "<li>" + item + "</li>" });
	errorHtml += "</ul>";

	toastr.error(errorHtml, msg, { timeOut: 10000 });
}

$.showSuccess = function (msg)
{
	toastr.success("", msg);
}

$.showWarning = function (msg)
{
	toastr.warning("", msg);
}

$.clearNotifications = function ()
{
	toastr.clear();
}

$.toJSDate = function (value)
{
	var pattern = /Date\(([^)]+)\)/;
	var results = pattern.exec(value);
	var dt = new Date(parseFloat(results[1]));
	return dt;
}

$.toDictionary = function (objectToConvert)
{
	sourceMapping = objectToConvert;
	var someMapping = [];
	for (var key in sourceMapping)
	{
		if (sourceMapping.hasOwnProperty(key))
		{
			someMapping.push({ Key: key, Value: sourceMapping[key] });
		}
	}
	return someMapping;
}


$.getAbsoluteUrl = function (relativePath)
{

	var baserUrl = currentURL.replace(currentAction, "");
	var url = "";
	if (baserUrl.slice(-1) == '/')
	{
		url = baserUrl + relativePath;
	}
	else
	{
		url = baserUrl + "/" + relativePath;
	}
	return url;
}