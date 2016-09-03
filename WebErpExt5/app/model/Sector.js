Ext.define('ERP.model.Sector', {
    extend: 'ERP.model.BaseModel',
        
    proxy: {
        type: 'rest',
        url: 'rest/Sector',
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
    hasMany: 'Client'

});
