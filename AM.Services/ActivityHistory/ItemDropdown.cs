using System.Collections.Generic;

namespace AM.Services.ActivityHistory
{
    public class ItemDropdown : ItemBase
    {
        public string DropdownIdField { get; set; }
        public string DropdownNameField { get; set; }

        public ItemDropdown(string fieldName, string collectionIdField, string collectionNameField, string customFieldText = null, string customMessage = null)
        {
            FieldName = fieldName;
            DropdownIdField = collectionIdField;
            DropdownNameField = collectionNameField;
            string textFieldName = string.IsNullOrEmpty(customFieldText) ? fieldName : customFieldText;
            Message = string.IsNullOrEmpty(customMessage) ? string.Concat("The item [" + textFieldName + "] was changed from {0} to {1}") : customMessage;
        }

        public override void Process(object _oldInstance, object _newInstance, List<string> changeCollection)
        {
            var oldObjectProperty = Utils.GetPropertyValue(_oldInstance, FieldName);
            var newObjectProperty = Utils.GetPropertyValue(_newInstance, FieldName);

            var oldItemId = oldObjectProperty != null ? Utils.GetPropertyValue(oldObjectProperty, DropdownIdField) : "Empty";
            var newItemId = newObjectProperty != null ? Utils.GetPropertyValue(newObjectProperty, DropdownIdField) : "Empty";

            if (oldItemId.ToString() != newItemId.ToString())
            {
                var oldItemValue = oldObjectProperty != null ? Utils.GetPropertyValue(oldObjectProperty, DropdownNameField) : "Empty";
                var newItemValue = newObjectProperty != null ? Utils.GetPropertyValue(newObjectProperty, DropdownNameField) : "Empty";
                changeCollection.Add(string.Format(Message, oldItemValue, newItemValue));
            }

        }
    }
}
