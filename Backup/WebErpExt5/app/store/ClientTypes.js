Ext.define('ERP.store.ClientTypes', {
    extend: 'ERP.store.BaseStore',
    alias:'store.clienttypes',
    model: 'ERP.model.ClientType',
    pageSize: 10,
    id: 'Id',

    remoteFilter: true,
    remoteSort: true
});
