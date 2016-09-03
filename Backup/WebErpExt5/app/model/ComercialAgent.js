Ext.define('ERP.model.ComercialAgent', {

    url: 'rest/product',
    extend: 'ERP.model.BaseModel',

    proxy: {
        type: 'rest',
        enablePaging: true,
        url: 'rest/ComercialAgent',
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

        { name: 'ComercialAgentCode',type: 'string' },
        { name: 'LastName', type: 'string' },
        { name: 'FirstName',type: 'string' },
        { name: 'Address1',type: 'string' },
        { name: 'Address2', type: 'string' },
        { name: 'Country',type: 'string' },
        { name: 'Province',type: 'string' },
        { name: 'ZipCode', type: 'string' },
        { name: 'Email', type: 'date' },
        { name: 'Telephone'},
        { name: 'Cellphone'},
        { name: 'StartDate'},
        { name: 'Observations'},
        { name: 'ComisionAmount'},
        { name: 'ComisionTypeId', mapping: 'ComisionType.Id' },
        { name: 'displayComercialAgent', convert: function (v, rec) { return rec[2] + rec[1]; } }

    ]
    , hasOne: ['ComisionType']
    ,hasMany: ['Client']
   , idProperty: { name: 'Id', mapping: 'Id', type: 'int' }




});
