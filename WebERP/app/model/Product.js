Ext.define('WC.model.Product', {
    extend: 'WC.model.BaseModel',

    fields: [
        { name: 'id', mapping: 'Id', type: 'int' },
        { name: 'productCode', mapping: 'ProductCode', type: 'string' },
        { name: 'shortDescription', mapping: 'ShortDescription', type: 'string' },
        { name: 'longDescription', mapping: 'LongDescription', type: 'string' },
        { name: 'productFamily_id', mapping: 'ProductFamily.Id' /*,reference:'ProductFamily'*/ },
        { name: 'productFamily_description', mapping: 'ProductFamily.Description' },
        { name: 'active', mapping: 'Active', type: 'bool' },
        { name: 'availableForSale', mapping: 'AvailableForSale', type: 'bool' },
        { name: 'brand', mapping: 'Brand', type: 'string' },
        { name: 'measureUnity', mapping: 'MeasureUnity', type: 'string' },
        { name: 'weight', mapping: 'Weight', type: 'string' },
        { name: 'height', mapping: 'Height', type: 'string' },
        { name: 'width', mapping: 'Width', type: 'string' },
        { name: 'length', mapping: 'Length', type: 'string' }
    ]
    //hasOne: [
    //    {
    //        name: 'productFamily',
    //        model: 'ProductFamily',
    //        primaryKey: 'id',
    //    }
   // ],
   , idProperty: 'id'

});
