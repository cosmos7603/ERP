Ext.define('WC.model.ProductFamily', {
    extend: 'WC.model.BaseModel',

    fields: [
        { name: 'id', mapping: 'Id', type: 'int' },
        { name: 'description', mapping: 'Description', type: 'string' }
    ],
    idProperty: 'id'

});
