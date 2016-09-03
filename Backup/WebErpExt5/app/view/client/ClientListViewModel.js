/**
 * This class is the view model for the Main view of the application.
 */
Ext.define('ERP.view.client.ClientListViewModel', {
    extend: 'ERP.view.shared.BaseListViewModel',
    alias: 'viewmodel.clientlistviewmodel',
    stores: {
        Clients: {
            extend: 'ERP.store.BaseStore',
            alias: 'store.clients',
            model: 'ERP.model.Client',
            autoLoad: true,
            pageSize: 10,
            remoteFilter: true,
            remoteSort: true
        }
    }
});