// Global variable to exchange data between the popups and the masters
var m_popUpData;

// Extension for popupReady function
(function ($)
{
	$.fn.popUpReady = function (f)
	{
		$('#divPopUpHandler').off('shown.bs.modal');
		$("#divPopUpHandler").on('shown.bs.modal', function ()
		{
			f();
		});
	};
})(jQuery);


// Delete Dialog
$.confirmDelete = function (elementType, elementName, elementId, onDelete)
{
	// Hide Open Modal If Any and Reopen when closing delete
	var $openModal = $('.modal.fade.in');
	if ($openModal.length > 0)
	{
		$openModal.modal('hide');
		$("#divDeleteDialog").on('hidden.bs.modal', function ()
		{
			if ($openModal)
				$openModal.modal('show');
		});
	}

	$("#divDeleteDialog").find("#lblElementType").html(elementType);
	$("#divDeleteDialog").find("#lblElementName").html(elementName);
	$("#divDeleteDialog").modal("show");

	// No
	$("#btnConfirmDeleteNO").off("click");
	$("#btnConfirmDeleteNO").click(function ()
	{
		$("#divDeleteDialog").modal("hide");
	});

	// Yes
	$("#btnConfirmDeleteYES").off("click");
	$("#btnConfirmDeleteYES").click(function ()
	{
		if ($openModal.length > 0)
		{
			$openModal.remove();
			$openModal = null;
		}

		onDelete(elementId);

		$("#divDeleteDialog").modal("hide");
	});
};

// Confirm Dialog
$.confirm = function (message, onYes, onNo)
{
	$("#divConfirmDialog").find("#lblMessage").html(message);
	$("#divConfirmDialog").modal("show");

	// No
	$("#btnConfirmNO").off("click");
	$("#btnConfirmNO").click(function ()
	{
		$("#divConfirmDialog").modal("hide");
		if (onNo != null) onNo();
	});

	// Yes
	$("#btnConfirmYES").off("click");
	$("#btnConfirmYES").click(function ()
	{
		$("#divConfirmDialog").modal("hide");
		if (onYes != null) onYes();
	});
};

// Alert Dialog
$.alert = function (title, message, onOk)
{
	$("#divAlert").find("#lblAlertTitle").text(title);
	$("#divAlert").find("#lblAlertMessage").html(message);
	$("#divAlert").modal("show");

	// OK
	$("#btnAlertOK").off("click");
	$("#btnAlertOK").click(function ()
	{
		$("#divAlert").modal("hide");
		if (onOk != null) onOk();
	});
};

$.popUp = function (url, postData, onClose, onLoad)
{
	// Clear popup data
	m_popUpData = null;

	// Clear on closing handler
	$('#divPopUpHandler').off('hidden.bs.modal');

	// Add new on closing handler
	if (onClose != undefined)
	{
		$('#divPopUpHandler').on('hidden.bs.modal', function ()
		{
			onClose(m_popUpData);
		})
	}

	// Post data using AJAX
	$.ajax({
		type: "POST",
		url: url,
		traditional: true,
		data: JSON.stringify(postData),
		contentType: 'application/json; charset=utf-8',
		success: function (data)
		{
			$("#divPopUpHandlerContent").html(data);
			$("#divPopUpHandler").modal("show");

			$.unblockUI();

			// Add on load handler
			if (onLoad != undefined)
			{
				onLoad();
			}
		}
	});
};

// Email dialog
$.deliverEmail = function (model, onClose)
{
	$.popUp(
		URLs.Shared.EmailDelivery.Index,
		model,
		onClose);
}

// Resize dialog
$.resizePopUp = function (width, height)
{
	var popupWidth = width;
	var popupMargin = -(width / 2);

	// Apply dimensions
	$(".modal-dialog").css("width", popupWidth);
}

// Close dialog
$.closePopUp = function (data)
{
	m_popUpData = data;

	$("#divPopUpHandler").modal("hide");
}


