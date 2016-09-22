// Dropdowns
$.fillList = function (listId, jsonData, dataTextField, dataValueField, firstItemText, firstItemValue)
{
    var listItems = "";

    if (firstItemText != undefined && firstItemText != "")
        listItems += "<option value='" + firstItemValue + "'>" + firstItemText + "</option>";

    for (var i = 0; i < jsonData.length; i++)
        listItems += "<option value='" + jsonData[i][dataValueField] + "'>" + jsonData[i][dataTextField] + "</option>";

    $(listId).html(listItems);
    $(listId).enabled();
}

$.appendList = function (listId, text, value)
{
    $(listId).append('<option value="' + value + '">' + text + '</option>').val(value);
};

$.clearList = function (listId, defaultText)
{
    defaultText = defaultText || '';
    $(listId).html("");
    $(listId).html("<option value''>" + defaultText + "</option>");
    $(listId).disabled();
}

$.updateList = function (listId, text, value)
{
    $(listId).find('option[value="' + value + '"]').text(text);
};

$.buildList = function (attrs, items)
{
    var listItems = "<select ";

    for (var i = 0; i < attrs.length; i++)
        listItems += attrs[i].Name + "='" + attrs[i].Value + "'";

    listItems += ">"

    for (var i = 0; i < items.length; i++)
        listItems += "<option value='" + items[i].Value + "'>" + items[i].Text + "</option>";

    listItems += "</select>"

    return listItems;
}