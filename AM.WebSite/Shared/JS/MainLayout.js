var sessionTimeoutTimer = null;
var sessionWarningTimer = null;

$(document).ready(function ()
{
	attachMainLayoutEvents();
	setupTimeoutWarning();
	//setupTooltips();
});

function attachMainLayoutEvents()
{
	$(".timeout-link").click(function ()
	{
		resetSession();
		return false;
	});
}

function setupTooltips()
{
	$("[title!=\"\"]").qtip();
}

function setupTimeoutWarning()
{
	if (window.sessionTimeout > 0 && window.sessionWarning > 0)
	{
		$(document).ready(function ()
		{
			attachMainLayoutEvents();
			scheduleSessionStart();
		});
	}
}

function openSessionWarning()
{
	$.blockUI({
		message: $("#sessionWarningMessage"),
		fadeIn: 1000,
		css: { top: "0px", width: "100%", left: "0px", cursor: "default" },
		overlayCSS: {
			cursor: "default"
		}
	});

	$("#sessionWarningMessage").show();
	$("#navigationbar").hide();

	sessionTimeoutTimer = setTimeout(scheduleSessionLogoff, window.sessionTimeout - window.sessionWarning);
}

function resetSession()
{
	$.unblockUI();
	$("#sessionWarningMessage").hide(1000);
	$("#navigationbar").show();

	scheduleSessionStart();
}

function scheduleSessionStart()
{
	if (sessionTimeoutTimer)
		clearTimeout(sessionTimeoutTimer);

	if (sessionWarningTimer)
		clearTimeout(sessionWarningTimer);

	sessionWarningTimer = setTimeout(openSessionWarning, window.sessionWarning);
}

function scheduleSessionLogoff()
{
	window.location.href = URLs.Accounts.Login.Logout;
}
