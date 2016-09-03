Ext.define('ERP.model.Client', {

    url: 'rest/product',
    extend: 'ERP.model.BaseModel',

    proxy: {
        type: 'rest',
        enablePaging: true,
        //type : 'dynamikarest',
        url: 'rest/Client',
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

        { name: 'Active', type: 'string' },
        { name: 'ClientCode', type: 'string' },
        { name: 'CorporateName', type: 'string' },
        { name: 'ComercialName', type: 'string' },
        { name: 'Email', type: 'string' },
        { name: 'Language', type: 'string' },
        { name: 'WebSite', type: 'string' },
        { name: 'Observations', type: 'string' },
        { name: 'BirthDate', type: 'date' },
        { name: 'SectorId', mapping: 'Sector.Id' },
        { name: 'ChargeMethodId', mapping: 'ChargeMethod.Id' },
        { name: 'ClientTypeId', mapping: 'ClientType.Id' },
        { name: 'ComercialAgentId', mapping: 'ComercialAgent.Id' },
        { name: 'PaymentDueDateTypeId', mapping: 'PaymentDueDateType.Id' },
        { name: 'PaymentDay', type: 'string' },
        { name: 'PaymentDay2', type: 'string' },
        { name: 'TaxId', mapping: 'Tax.Id' }
    ]
    , hasOne: ['Sector', 'ChargeMethod', 'ClientType', 'PaymentDueDateType', 'Tax', 'ComercialAgent']
   , idProperty: { name: 'Id', type: 'int' }

});
