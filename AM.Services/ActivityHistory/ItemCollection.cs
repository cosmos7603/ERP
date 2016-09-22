using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Services.ActivityHistory
{
    public class ItemCollection : ItemBase
    {
        public string CollectionIdField { get; set; }
        public string CollectionNameField { get; set; }

        public ItemCollection(string fieldName, string collectionIdField, string collectionNameField, string customFieldText = null, string customMessage = null)
        {
            FieldName = fieldName;
            CollectionIdField = collectionIdField;
            CollectionNameField = collectionNameField;
            string textFieldName = string.IsNullOrEmpty(customFieldText) ? fieldName : customFieldText;
            Message = string.IsNullOrEmpty(customMessage) ? string.Concat("The item {0} was {1} from the [" + textFieldName + "] list") : customMessage;
        }

        public override void Process(object _oldInstance, object _newInstance, List<string> changeCollection)
        {
            var oldCollection = (IEnumerable)Utils.GetPropertyValue(_oldInstance, FieldName);
            var newCollection = (IEnumerable)Utils.GetPropertyValue(_newInstance, FieldName);

            //Get Deleted
            CompareCollections(oldCollection, newCollection, "removed", changeCollection);

            //Get New 
            CompareCollections(newCollection, oldCollection, "added", changeCollection);
        }

        private void CompareCollections(IEnumerable colPrimary, IEnumerable colSecondary, string verb, List<string> changeCollection)
        {
            foreach (var itemColPrimary in colPrimary)
            {
                bool exists = false;
                foreach (var itemColSecondary in colSecondary)
                {
                    var itemIdPrimary = Utils.GetPropertyValue(itemColPrimary, CollectionIdField);
                    var itemIdSecondary = Utils.GetPropertyValue(itemColSecondary, CollectionIdField);

                    if (itemIdPrimary.ToString() == itemIdSecondary.ToString())
                        exists = true;
                }

                if (!exists)
                    changeCollection.Add(string.Format(Message, Utils.GetPropertyValue(itemColPrimary, CollectionNameField), verb));

            }
        }
    }
}