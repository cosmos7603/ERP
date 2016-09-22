// AJAX global variables
var m_ajaxLoadingStatus = false;
var m_ajaxLoadingHandler = null;

// AJAX global setup
$.ajaxSetup({
	// Otherwise, a few views gets cached in IE without reaching the server
	cache: false
});

$(document).ajaxStart(function ()
{
	m_ajaxLoadingStatus = true;
	m_ajaxLoadingHandler = setTimeout(function ()
	{
		if (m_ajaxLoadingStatus)
			$("#divAjaxLoading").show();
	}, 3000);
});

// Error handling
$(document).ajaxStop(function ()
{
	m_ajaxLoadingStatus = false;
	$("#divAjaxLoading").hide();
	clearTimeout(m_ajaxLoadingHandler);
});

$(document).ajaxError(function (event, jqxhr, settings, exception)
{
	if (jqxhr.responseText != undefined)
	{
		$.unblockUI();

		$("#divErrorHandlerBody").html(jqxhr.responseText);

		// Remove original styles to avoid breaking application styles
		$("#divErrorHandlerBody").find("style").remove();
		$("#divErrorHandlerBody").find("title").remove();

		$("#divErrorHandler").modal("show");

		// No
		$("#btnErrorHandlerClose").click(function ()
		{
			$("#divErrorHandler").modal("hide");
		});
	}
});

// Timeout handling
$(document).ajaxSuccess(function (event, jqxhr, settings, exception)
{
	// Apply global JS on return from views
	$.global();

	// Handler AJAX redirects
	var loginHeader = jqxhr.getResponseHeader("X-LOGIN-PAGE");

	if (loginHeader == null)
		return;

	window.location.href = loginHeader;
});

// Makes a call to an API
$.api = function (url, postData, onSuccess, onError)
{
	$.ajax({
		type: "POST",
		url: url,
		data: JSON.stringify(postData),
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function (data)
		{
			if (onSuccess != null)
				onSuccess(data);
		},
		error: onError
	});
}

// This function post a given data object (in JSON) to the specified URL
// It blocks & unblocks the UI.
// It processes validation messages and shows them in any div with a classs ".validation-msg"
// It processes redirects if neccesary
// Accepts options to block the UI and ScrollToTop
$.postData = function (url, postData, validationSuccess, validationError, opt)
{
	// Options Set Up
	var options = { block: true, scrollToTop: true };
	$.extend(options, opt);

	// Block only if not in popup
	if (options.block)
		$.blockUI();

	// Post data using AJAX
	$.ajax({
		type: "POST",
		url: url,
		traditional: true,
		data: JSON.stringify(postData),
		contentType: 'application/json; charset=utf-8',
		success: function (data)
		{
			// Redirect?
			if (data.Redirect != "" && data.Redirect != null)
				$.redirect(data.Redirect);

			// Block?
			if (options.block)
				$.unblockUI();

			// Status OK but Warnings
			if (data.Status && data.WarningMessage != "")
			{
				$.confirm(data.WarningMessage, function ()
				{
					postData["acceptedWarning"] = "true";
					$.postData(url, postData, validationSuccess, validationError);
				});

				return false;
			}

			// Execute user code on sucess or error
			if (data.Status)
			{
				if (data.ValidationTitle)
					$.showSuccess(data.ValidationTitle);

				if (validationSuccess != null)
					validationSuccess(data);
			}
			else
			{
				if (data.Errors.length > 0)
					$.showError(data.ValidationTitle, data.Errors);

				if (validationError != null)
					validationError(data);
			}
		}
	});
}

// Form post function
// This is for posting forms that needs to be validated with the jQuery validation.
// The form is posted tot he "action" URL. The data posted does NOT comes from
// the form fields, but instead, from a postData object built manually (flexible)
$.postForm = function (form, postData, validationSuccess, validationError, opt)
{
	// Perform client side validations with jQuery Validations
	$(form).validate({
		invalidHandler: function (form, validator)
		{
			if (!validator.numberOfInvalids())
				return;

			$('html, body').animate({
				scrollTop: $(validator.errorList[0].element).offset().top - 90
			}, 300);

		}
	});

	// Not valid? Don't post...
	if (!$(form).valid())
		return;

	// Take the URL from the action attribute
	var url = form.attr("action");

	// Use the standard postData function
	$.postData(url, postData, validationSuccess, validationError, opt);
}

// Make a post and upon returning, if there were no validation messages to be shown,
// it renders the content into a partial view. Otherwise, it displays the validation
// errors in the div with class ".validation-msg"
$.partialView = function (div, url, postData, validationSuccess, validationError, opt) {

	// Options Set Up
	var options = { block: false, scrollToTop: false, append: false, blockTargetdiv: true };
	$.extend(options, opt);

	if (options.block)
		$.blockUI();

	if (options.blockTargetdiv)
		$(div).block();

	// Post data using AJAX
	$.ajax({
		type: "POST",
		url: url,
		traditional: true,
		data: JSON.stringify(postData),
		contentType: 'application/json; charset=utf-8',
		success: function (data)
		{
			// Redirect?
			if (data.Redirect != "" && data.Redirect != null)
				$.redirect(data.Redirect);

			if (options.block)
				$.unblockUI();

			if (options.blockTargetdiv)
				$(div).unblock();

			if (data.ValidationTitle != undefined)
			{
				/*
				$(".validation-msg").each(function (key, value)
				{
					$(this).html(data.ValidationTitle);
				});
				*/
				if (validationError != null)
					$.showError(data.ValidationTitle, data.Errors); //validationError(data);
				else
					$.showSuccess(data.ValidationTitle);
			}
			else
			{
				if (options.append)
					$(div).append(data);
				else
					$(div).html(data);

				if (validationSuccess != null) validationSuccess(postData);
			}

			if (options.scrollToTop)
				$.scrollToTop();

		}
	});
}
