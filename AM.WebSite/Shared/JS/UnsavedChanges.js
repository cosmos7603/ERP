// Prepare form to handle unsaved changes
$.formReady = function (formSelector)
{
	$.formReset();

	$(formSelector).find("input, select, textarea").change(function ()
	{
		$.formDirty();
	});
}

// Clear dirty flag
$.formReset = function ()
{
	m_formHasChanges = false;
}

// Set dirty flag
$.formDirty = function ()
{
	m_formHasChanges = true;
}

// Has changes?
$.formChanged = function ()
{
	return m_formHasChanges;
}

// Present dialog if there's changes
$.formCheckChanges = function (onContinue)
{
	var hasChanges = $.formChanged();

	if (hasChanges)
	{
		$.confirm("You have unsaved changes. If you continue without saving, those changes will be lost. Are you sure?",
			function ()
			{
				$.formReset();
				onContinue();
			},
			function ()
			{
				// Do nothing
			});
	}
	else
	{
		// No changes? Then continue right ahead...
		onContinue();
		return true;
	}

	return !hasChanges;
}