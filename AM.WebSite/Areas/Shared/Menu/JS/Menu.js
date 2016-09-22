$(function ()
{
	$(".navbar-inverse .navbar-nav li a, .menu-item a").click(function ()
	{
		// Validate unsaved changed before leaving
		var href = $(this).attr("href");

		$.formCheckChanges(function ()
		{
			$.redirect(href);
		});

		return false;
	});
});