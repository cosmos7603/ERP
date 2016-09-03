Ext.define('ERP.model.ClientType', {
    extend: 'ERP.model.BaseModel',
    proxy: {
        type: 'rest',
        //type : 'dynamikarest',
        url: 'rest/ClientType',
        reader: {
            type: 'json',
            rootProperty: 'Items',
            totalProperty: 'TotalCount'
        },
        writer: {
            type: 'json',
            writeAllFields: false,
            nameProperty: 'mapping',
            root: 'model'
        }
    },
    fields: [
        { name: 'Id',type: 'int' },
        { name: 'Description', type: 'string' }
    ],
    hasMany: 'Client'

});
