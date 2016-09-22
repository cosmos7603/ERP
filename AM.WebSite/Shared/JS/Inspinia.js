$.toggleBox = function (boxId, direction, callback)
{
	var delay = 200;
	var ibox = $("#" + boxId).closest("div.ibox");
	var button = ibox.find("i");
	var content = ibox.find("div.ibox-content");
	var visible = $(content).is(":visible");

	if (direction == undefined || direction == null)
	{
		if (!visible)
			direction = "expand";
		else
			direction = "collapse";
	}

	if (direction === "collapse")
		content.slideUp(delay, callback);
	else
		content.slideDown(delay, callback);

	button.toggleClass("fa-chevron-up").toggleClass("fa-chevron-down");
	ibox.toggleClass("").toggleClass("border-bottom");

	setTimeout(function ()
	{
		ibox.resize();
		ibox.find("[id^=map-]").resize();
	}, 50);
}

$.collapseBox = function (boxId, callback)
{
	$.toggleBox(boxId, "collapse", callback);
}

$.expandBox = function (boxId, callback)
{
	$.toggleBox(boxId, "expand", callback);
}