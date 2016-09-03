Ext.define('ERP.model.ComisionType', {
    extend: 'ERP.model.BaseModel',
    proxy: {
        type: 'rest',
        //type : 'dynamikarest',
        url: 'rest/ComisionType',
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
        { name: 'Id', mapping: 'Id', type: 'int' },
        { name: 'Description', mapping: 'Description', type: 'string' }
    ],
    hasMany: 'ComercialAgent'

});
