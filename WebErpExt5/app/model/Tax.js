Ext.define('ERP.model.Tax', {
    extend: 'ERP.model.BaseModel',
        
    proxy: {
        type: 'rest',
        //type : 'dynamikarest',
        url: 'rest/Tax',
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
        { name: 'Id',  type: 'int' },
        { name: 'Description', type: 'string' }
    ],
    hasMany: 'Tax'
    //,
//    idProperty: 'id'

});
