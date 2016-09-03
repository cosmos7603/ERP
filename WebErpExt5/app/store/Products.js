Ext.define('ERP.store.Products', {
    extend: 'ERP.store.BaseStore',
    alias:'store.products',
    model: 'ERP.model.Product',
    pageSize: 10,
    id: 'Id',

    remoteFilter: true,
    remoteSort: true
});
