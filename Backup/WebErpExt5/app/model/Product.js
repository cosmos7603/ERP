Ext.define('ERP.model.Product', {

    url : 'rest/product',
    extend: 'ERP.model.BaseModel',
        
    proxy: {
        type: 'rest',
        enablePaging:true,
        //type : 'dynamikarest',
        url: 'rest/Product',
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
        

        { name: 'ProductCode', type: 'string' },
        { name: 'ShortDescription', type: 'string'},
        { name: 'LongDescription',  type: 'string' },
        { name: 'ProductFamilyId', mapping: 'ProductFamily.Id' },
        { name: 'Active',  type: 'bool' },
        { name: 'AvailableForSale', type: 'bool' },
        { name: 'Brand',  type: 'string' },
        { name: 'MeasureUnity', type: 'string' },
        { name: 'Weight', type: 'string' },
        { name: 'Height', type: 'string' },
        { name: 'Width', type: 'string' },
        { name: 'Length', type: 'string' }
    ]
    ,hasOne: 'ProductFamily'
   , idProperty: { name: 'Id', mapping: 'Id', type: 'int' }

});
