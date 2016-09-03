Ext.define('ERP.store.Clients', {
    extend: 'ERP.store.BaseStore',
    alias:'store.clients',
    model: 'ERP.model.Client',
    pageSize: 10,
    id: 'Id',

    remoteFilter: true,
    remoteSort: true
});
