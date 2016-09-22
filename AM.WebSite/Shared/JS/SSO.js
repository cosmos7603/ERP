// Function for jumping from LPG app, to AM app
function legacysso(url)
{
	$.post("/SSO/GetToken",
		{
			url: url
		},
		function (data)
		{
			$('<form action="' + data.SsoUrl + '" method="post"><input type="hidden" id="AuthToken" name="AuthToken" value="' + data.AuthToken + '"></form>').appendTo('body').submit();
		});
}