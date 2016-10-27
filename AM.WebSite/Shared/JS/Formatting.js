// Formatting
$.formatDecimal = function (value, dp)
{
	if (dp == undefined)
		dp = 2;
	else
		dp = Number(dp);

	var padding = "00";
	if (dp == 0) padding = "";
	if (dp == 1) padding = "0";
	if (dp == 2) padding = "00";
	if (dp == 3) padding = "000";
	if (dp == 4) padding = "0000";
	if (dp == 8) padding = "00000000";

	var result = "0." + padding;

	if (value != "")
	{
		value = $.parseDecimal(value);

		if (padding != "")
			result = $.format.number(value, "###,###,##0." + padding).toString();
		else
			result = $.format.number(value, "###,###,###").toString();
	}
	
	return result;
}

$.formatCurrency = function(value, dp)
{
	var result = $.formatDecimal(value, dp);

	if (result.substring(0, 1) == "-")
		result = "($" + result.substring(1, result.length) + ")";
	else
		result = "$" + result;

	return result;
}

$.formatPrct = function(value, dp)
{
	if (value != "")
		return $.formatDecimal(value, dp) + "%";
	else
		return "0%";
}

$.formatJsonDateTime = function (value)
{
	datetime = new Date(parseInt(value.substr(6)));

	var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
	var day = datetime.getDate();
	var monthIndex = datetime.getMonth();
	var year = datetime.getFullYear();
	var hour = datetime.getHours();
	var minutes = datetime.getMinutes();

	var sDatetime = "";

	sDatetime += day < 10 ? "0" + day : day;
	sDatetime += " " + months[monthIndex] + " " + year + " ";
	sDatetime += hour < 10 ? "0" + hour : hour;
	sDatetime += ":";
	sDatetime += minutes < 10 ? "0" + minutes : minutes;

	return sDatetime;
}

$.formatJsonDate = function (value) {
    datetime = new Date(parseInt(value.substr(6)));

    var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var day = datetime.getDate();
    var monthIndex = datetime.getMonth();
    var year = datetime.getFullYear();

    var sDatetime = "";

    sDatetime += day < 10 ? "0" + day : day;
    sDatetime += " " + months[monthIndex] + " " + year;

    return sDatetime;
}

// Parsing
$.parseDecimal = function (value)
{
	var result = 0;
	value = value.toString();

	if (value != "")
	{
		value = value.toString().replace(',', '');
		var number = Number(value);

		if (isNaN(number))
			return 0;
		else
			return number;
	}

	return result;
}

$.parseCurrency = function (value)
{
	var result = 0;

	value = value.toString();
	value = value.replace("$", '');
	value = value.replace("(", '-');
	value = value.replace(")", '');

	if (value != "")
	{
		return $.parseDecimal(value);
	}

	return result;
}

$.parsePercentage = function (value)
{
	var result = 0;

	value = value.toString();
	value = value.replace("%", '');
	value = value.replace("(", '-');
	value = value.replace(")", '');

	if (value != "")
	{
		return $.parseDecimal(value);
	}

	return result;
}

$.round = function(number)
{
	return Math.round(number * 100) / 100;
}