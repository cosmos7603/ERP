// Default Setup
$.setup = {
	mainView: null,
	tabView: null
};


// Main View Default Functions
$.setup.attachMainHandlers = function ()
{
	var $btnReset = $($.setup.mainView.btnResetId);
	var $btnSearch = $($.setup.mainView.btnSearchId);

	// Reset Search
	if ($btnReset.length > 0)
	{
		$btnReset.click(function ()
		{
			$.setup.mainView.resetSearch();
			$.setup.mainView.reloadGrid();
			return false;
		});
	}

	// Search on grid
	if ($btnSearch.length > 0)
	{
		$btnSearch.click(function ()
		{
			$.setup.mainView.reloadGrid();
			return false;
		});
	}

	// Add On Enter Key Search
	$($.setup.mainView.searchPanelId + ' input[type="text"]').onEnterKey(function ()
	{
		$.setup.mainView.reloadGrid();
		return false;
	});

	// Add autopostback for checkboxes
	$($.setup.mainView.searchPanelId + ' input[type="checkbox"]').click(function ()
	{
		$.setup.mainView.reloadGrid();
	});

	// Disable Tab Clicks when new or blank
	$('.tabs-container [data-toggle="tab"]').on('click', function (e)
	{
		if ($(this).parent().hasClass('disabled'))
			e.stopImmediatePropagation();
	});

	if ($.setup.mainView.afterAttachMainHandlers)
		$.setup.mainView.afterAttachMainHandlers();
}

$.setup.resetSearch = function ()
{
	$($.setup.mainView.searchPanelId + ' input[type="text"]').val('');
	$($.setup.mainView.searchPanelId + ' select').val('');
}

$.setup.reloadGrid = function ()
{
	$($.setup.mainView.gridId).DataTable().reload();
}

$.setup.enableDisableTabs = function (setupInfo)
{
	if (setupInfo == SETUP_MODE_EDIT)
		$('.tabs-container .nav-tabs > li:gt(0)').removeClass('disabled');
	else
		$('.tabs-container .nav-tabs > li:gt(0)').addClass('disabled');
};

// Default Main View Config
$.setup.defaultMainViewConfig = {

	currentEntityId: null,

	// Controls
	btnResetId: '#btnReset',
	btnSearchId: '#btnSearch',

	// Page Elements
	gridId: null,
	searchPanelId: '#searchPanel',

	//Functions
	attachMainHandlers: $.setup.attachMainHandlers,
	afterAttachMainHandlers: null,
	reloadGrid: $.setup.reloadGrid,
	setupGrid: null,
	resetSearch: $.setup.resetSearch,
	enableDisableTabs: $.setup.enableDisableTabs
}

// Main View Init
$.setup.initMainView = function (config)
{
	$.setup.mainView = $.extend({}, $.setup.defaultMainViewConfig);
	$.extend($.setup.mainView, config);
	$.setup.mainView.attachMainHandlers();
	$.setup.mainView.setupGrid($.setup.mainView.gridId);
}

$.setup.createTab = function (setupMode)
{
	// Clear Tabs
	$('.tabs-container .panel-body').empty();

	// Load
	$($.setup.tabView.containerDivId).load(
		$.setup.tabView.loadUrl, { SetupMode: setupMode, EntityId: $.setup.mainView.currentEntityId }, function ()
		{
			// Setup Mode
			$.setup.tabView.verifySetupMode(setupMode);

			// Attach Handlers
			$.setup.tabView.attachTabHandlers();

			// Enable or Disable Tabs
			$.setup.mainView.enableDisableTabs(setupMode);

			// Permissions
			$.setup.tabView.verifyPermissions();

			// Custom Callback
			if ($.setup.tabView.afterCreateTab)
				$.setup.tabView.afterCreateTab();
		}
	);
}

// Tab View Default Functions
$.setup.verifySetupMode = function (setupMode)
{
	if (setupMode == SETUP_MODE_BLANK)
	{
		$('html, body').animate({ scrollTop: 0 }, 500);
		$.setup.tabView.disableForm();
		$($.setup.tabView.btnNewId).enabled();
		$.formReset();
	}

	if (setupMode == SETUP_MODE_NEW || setupMode == SETUP_MODE_EDIT)
	{
		$($.setup.tabView.btnNewId).disabled();
		$.formReady($.setup.tabView.formId);
		$($.setup.tabView.formId + 'input:text').first().focus();
		$('html, body').animate({ scrollTop: $($.setup.tabView.formId).offset().top }, 500);
	}

	if (setupMode == SETUP_MODE_NEW || setupMode == SETUP_MODE_BLANK)
		$($.setup.mainView.gridId).DataTable().unselect();
}

$.setup.attachTabHandlers = function ()
{
	$($.setup.tabView.btnNewId).click(function ()
	{
		$.formCheckChanges(function ()
		{
			$.setup.mainView.currentEntityId = null;
			$.setup.createTab(SETUP_MODE_NEW);
		});

		return false;
	});

	$($.setup.tabView.btnSaveId).click(function ()
	{
		var $form = $($.setup.tabView.formId);
		var saveModel = $.setup.tabView.getSaveModel();

		$.postForm($form, saveModel, function ()
		{
			$.setup.mainView.currentEntityId = null;
			$.setup.mainView.reloadGrid();
			$.setup.tabView.showMainTab();
		});

		return false;
	});

	$($.setup.tabView.btnCancelId).click(function ()
	{
		$.formCheckChanges(function ()
		{
			$('html, body').animate({ scrollTop: 0 }, 'slow');
			$.setup.mainView.currentEntityId = null;
			$.setup.tabView.showMainTab();
		});

		return false;
	});

	$($.setup.tabView.btnDeleteId).click(function ()
	{
		var model = $.setup.tabView.getDeleteModel();

		$.confirmDelete(model.entityType, model.entityName, model.entityId, function (entityId)
		{
			$.postData(model.url, { entityId: entityId }, function ()
			{
				$.setup.mainView.currentEntityId = null;
				$.setup.mainView.reloadGrid();
				$.setup.tabView.showMainTab();
			});
		});

		return false;
	});

	// Callback
	if ($.setup.tabView.afterAttachTabHandlers)
		$.setup.tabView.afterAttachTabHandlers();
}

$.setup.disableForm = function ()
{
	$($.setup.tabView.formId).find('input, select, textarea, button').disabled();

	if ($($.setup.tabView.formId).find('.jsRichEditor').length > 0)
		$($.setup.tabView.formId).find('.jsRichEditor').richEditor().disable();
}

$.setup.showMainTab = function (entityId)
{
	$.setup.mainView.currentEntityId = entityId;
	$('.tabs-container .nav-tabs li:first > a').click();
}

$.setup.verifyPermissions = function ()
{
	// SAVE / NEW First Check if hasEditRights function is provided, if not try to find SetupRights
	if ($.setup.tabView.verifyEditRights)
	{
		if (!$.setup.tabView.verifyEditRights())
		{
			$.setup.tabView.disableForm();
			$.setup.tabView.removeUnauthorizedElements();
		}
	}
	else if (SetupInfo && SetupInfo.EditRights != undefined && !SetupInfo.EditRights)
	{
		$.setup.tabView.disableForm();
		$.setup.tabView.removeUnauthorizedElements();
	}
}

$.setup.removeUnauthorizedElements = function ()
{
	$($.setup.tabView.btnNewId).remove();
	$($.setup.tabView.btnSaveId).remove();
	$($.setup.tabView.btnCancelId).remove();
	$($.setup.tabView.btnDeleteId).remove();
}

// Default Tab View Config
$.setup.defaultTabViewConfig = {

	// Controls
	btnSaveId: '#btnSave',
	btnDeleteId: '#btnDelete',
	btnNewId: '#btnNew',
	btnCancelId: '#btnCancel',

	// Page Elements
	formId: null,
	containerDivId: null,

	//Functions
	afterCreateTab: null,
	verifySetupMode: $.setup.verifySetupMode,
	attachTabHandlers: $.setup.attachTabHandlers,
	afterAttachTabHandlers: null,
	disableForm: $.setup.disableForm,
	showMainTab: $.setup.showMainTab,
	verifyPermissions: $.setup.verifyPermissions,
	verifyEditRights: null,
	removeUnauthorizedElements: $.setup.removeUnauthorizedElements,
	getSaveModel: null,
	getDeleteModel: null,

	//Urls
	loadUrl: null
}

// Tab Initialization
$.setup.initTab = function (config)
{
	$.setup.tabView = $.extend({}, $.setup.defaultTabViewConfig);
	$.extend($.setup.tabView, config);
	$.setup.tabView.attachTabHandlers();

	if ($.setup.mainView.currentEntityId)
		$.setup.createTab(SETUP_MODE_EDIT);
	else
		$.setup.createTab(SETUP_MODE_BLANK);
}
