using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AM.Services.ActivityHistory
{
    public class ActivityHistoryModel<T>
    {
        private T _oldInstance;
        private T _newInstance;
        private List<ItemBase> items;
        public List<string> ChangeCollection { get; set; }


        public ActivityHistoryModel(T OldInstance, T newInstance)
        {
            _oldInstance = OldInstance;
            _newInstance = newInstance;
            items = new List<ItemBase>();
            ChangeCollection = new List<string>();
        }

        public void ProcessActivityHistory()
        {
            foreach (var item in items)
                item.Process(_oldInstance, _newInstance, ChangeCollection);
        }

        #region Add Items

        public ActivityHistoryModel<T> AddField(Expression<Func<T, object>> field, string customFieldText = null, string customMessage = null)
        {
            items.Add(new ItemField(Utils.GetMemberName(field.Body), customFieldText, customMessage));
            return this;
        }

        public ActivityHistoryModel<T> AddCollection<Y>(Expression<Func<T, object>> field, Expression<Func<Y, object>> collectionIdField, Expression<Func<Y, object>> collectionNameField, string customFieldText = null, string customMessage = null)
        {
            items.Add(new ItemCollection(Utils.GetMemberName(field.Body), Utils.GetMemberName(collectionIdField.Body), Utils.GetMemberName(collectionNameField.Body), customFieldText, customMessage));
            return this;
        }

        public ActivityHistoryModel<T> AddDropdown<Y>(Expression<Func<T, object>> field, Expression<Func<Y, object>> dropdownIdField, Expression<Func<Y, object>> dropdownNameField, string customFieldText = null, string customMessage = null)
        {
            items.Add(new ItemDropdown(Utils.GetMemberName(field.Body), Utils.GetMemberName(dropdownIdField.Body), Utils.GetMemberName(dropdownNameField.Body), customFieldText, customMessage));
            return this;
        }

        #endregion

    }
}
