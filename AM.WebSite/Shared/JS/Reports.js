// Consts
var REPORT_EXECUTION_RESULT_UNDEFINED = "UNDEFINED";
var REPORT_EXECUTION_RESULT_OK = "OK";
var REPORT_EXECUTION_RESULT_ERROR = "ERROR";
var REPORT_EXECUTION_RESULT_CANCELLED = "CANCELLED";
var REPORT_EXECUTION_RESULT_NODATA = "NODATA";

$.runReport = function (url, params)
{
    // Once the starting popup has been shown, display progress
    $.postData(
        url,
        params,
        function (jr)
        {
            // Once the report has been posted, update progress
        	var reportExecutionKey = jr.Data.ReportExecutionKey;

            // Update report history
            $.showReportHistory();

            // Keep report process in window
            $.showReportProgress(reportExecutionKey);
        });
}

$.cancelReport = function (reportExecutionKey)
{
    $.api(
		API.CancelReport,
		{
		    ReportExecutionKey: reportExecutionKey
		},
		function (data)
		{
			$.showReportHistory();
		});
}

$.showReportHistory = function ()
{
	$(".report-history").each(function ()
	{
		var reportId = $(this).data("report-id");

		$(this).load(
			URLs.Shared.ReportHistory.Index,
			{
				ReportId: reportId
			});

	});
}

$.showReportProgress = function (reportExecutionKey)
{
    $.popUp(
		URLs.Shared.ReportProgress.Index,
		{
			ReportExecutionKey: reportExecutionKey
		}
	);
}