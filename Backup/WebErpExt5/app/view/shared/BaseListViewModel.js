Ext.define('ERP.view.shared.BaseListViewModel', {
    extend: 'Ext.app.ViewModel',

    data: {
        name: 'ERP',
        currentModel: null
    },
    formulas: {
        isRecordSelected: {
            bind: {
                bindTo: '{currentModel}',
                deep: true
            },
            get: function (data)
            {
                return data != null ? true : false;
            }

        },
        currentModel: {
            bind: {
                record: '{grid.selection}'
            },
            get: function (data)
            {
                return data.record;
            }
        }
    }

});