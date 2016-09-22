$(document).ready(function ()
{
	$("#btnChangeClient").click(function ()
	{
		$.popUp(window.SHARED.ClientSelection);
		return false;
	});
});