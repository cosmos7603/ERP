var trackingCookieName = "CorpNetProfilerTracking";

window.onload = function ()
{
	if (!('performance' in window) || !('timing' in window.performance) || !('navigation' in window.performance))
		return;

	setTimeout(function ()
	{
		try
		{
			trackPageRequest();
		}
		catch (e)
		{
		}
	}, 0);
}

function trackPageRequest()
{
	var p = window.performance.timing;
	var requestId = getTrackingCookie();

	// This means that the request ID was NOT written by the server
	if (requestId == null ||
		requestId.length != 36 ||
		requestId.indexOf("|") > 0)
		return;

	// Track request ID
	var t =
		{
			requestId: requestId,
			clientTotalDuration: p.loadEventEnd - p.navigationStart,
			clientRedirectDuration: p.redirectEnd - p.redirectStart,
			clientDnsDuration: p.domainLookupEnd - p.domainLookupStart,
			clientConnectionDuration: p.connectEnd - p.connectStart,
			clientRequestDuration: p.responseStart - p.requestStart,
			clientResponseDuration: p.responseEnd - p.responseStart,
			clientDomDuration: p.domComplete - p.domLoading,
			clientLoadDuration: p.loadEventEnd - p.loadEventStart
		};

	setTrackingCookie(t);
}

function setTrackingCookie(tracking)
{
	var value =
		tracking.requestId + "|" +
		tracking.clientTotalDuration + "|" +
		tracking.clientRedirectDuration + "|" +
		tracking.clientDnsDuration + "|" +
		tracking.clientConnectionDuration + "|" +
		tracking.clientRequestDuration + "|" +
		tracking.clientResponseDuration + "|" +
		tracking.clientDomDuration + "|" +
		tracking.clientLoadDuration;
	
	document.cookie = trackingCookieName + "=" + value + "; path=/";
}

function getTrackingCookie()
{
	if (document.cookie.length > 0)
	{
		cStart = document.cookie.indexOf(trackingCookieName + "=");

		if (cStart != -1)
		{
			cStart = cStart + trackingCookieName.length + 1;
			cEnd = document.cookie.indexOf(";", cStart);

			if (cEnd == -1)
				cEnd = document.cookie.length;

			return unescape(document.cookie.substring(cStart, cEnd));
		}
	}

	return "";
}

function showTracking(t)
{
	console.log("{");
	console.log("   requestId: " + t.requestId);
	console.log("   clientTotalDuration: " + t.clientTotalDuration);
	console.log("   clientRedirectDuration: " + t.clientRedirectDuration);
	console.log("   clientDnsDuration: " + t.clientDnsDuration);
	console.log("   clientConnectionDuration: " + t.clientConnectionDuration);
	console.log("   clientRequestDuration: " + t.clientRequestDuration);
	console.log("   clientResponseDuration: " + t.clientResponseDuration);
	console.log("   clientDomDuration: " + t.clientDomDuration);
	console.log("   clientLoadDuration: " + t.clientLoadDuration);
	console.log("}");
	console.log("");
}
