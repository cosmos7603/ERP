using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Services.ActivityHistory
{
    public class ItemField : ItemBase
    {
        public ItemField(string fieldName,  string customFieldText = null, string customMessage = null)
        {
            FieldName = fieldName;
            string textFieldName = string.IsNullOrEmpty(customFieldText) ? fieldName : customFieldText;
            Message = string.IsNullOrEmpty(customMessage) ? string.Concat("The field [", textFieldName, "] changed from {0} to {1}") : customMessage;
        }

        public override void Process(object _oldInstance, object _newInstance, List<string> changeCollection)
        {
            var v1 = Utils.GetPropertyValue(_oldInstance, FieldName);
            var v2 = Utils.GetPropertyValue(_newInstance, FieldName);

            if (v1 == null || string.IsNullOrEmpty(v1.ToString()))
                v1 = "Empty";

            if (v2 == null || string.IsNullOrEmpty(v2.ToString()))
                v2 = "Empty";

            if (v1.ToString() != v2.ToString())
            {
                changeCollection.Add(string.Format(Message, v1, v2));
            }
        }

    }

}
