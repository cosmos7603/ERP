/**
 * This class is the view model for the Main view of the application.
 */
Ext.define('ERP.view.client.ClientFormViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.clientformviewmodel',
    stores: {
        Clients: {
            extend: 'ERP.store.BaseStore',
            alias: 'store.clients',
            model: 'ERP.model.Client',
            autoLoad: true,
            pageSize: 10,
            remoteFilter: true,
            remoteSort: true
        },
        ClientTypes: {
            extend: 'ERP.store.BaseStore',
            alias: 'store.clienttypes',
            model: 'ERP.model.ClientType',
            autoLoad: true,
            pageSize: 10,
            remoteFilter: true,
            remoteSort: true
        },
        Sectors: {
            extend: 'ERP.store.BaseStore',
            alias: 'store.sectors',
            model: 'ERP.model.Sector',
            autoLoad: true,
            pageSize: 10,
            remoteFilter: true,
            remoteSort: true
        },
        ComercialAgents: {
            extend: 'ERP.store.BaseStore',
            alias: 'store.comercialagents',
            model: 'ERP.model.ComercialAgent',
            autoLoad: true,
            pageSize: 10,
            remoteFilter: true,
            remoteSort: true
        },
        ChargeMethods: {
            extend: 'ERP.store.BaseStore',
            alias: 'store.chargemethods',
            model: 'ERP.model.ChargeMethod',
            autoLoad: true,
            pageSize: 10,
            remoteFilter: true,
            remoteSort: true
        },
        PaymentDueDateTypes: {
            extend: 'ERP.store.BaseStore',
            alias: 'store.paymentduedatetypes',
            model: 'ERP.model.PaymentDueDateType',
            autoLoad: true,
            pageSize: 10,
            remoteFilter: true,
            remoteSort: true
        },
        Taxes: {
            extend: 'ERP.store.BaseStore',
            alias: 'store.taxes',
            model: 'ERP.model.Tax',
            autoLoad: true,
            pageSize: 10,
            remoteFilter: true,
            remoteSort: true
        }
    }



    //TODO - add data, formulas and/or methods to support your view
});