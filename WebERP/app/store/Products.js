Ext.define('WC.store.Products', {
    extend: 'WC.store.BaseStore',
    model: 'WC.model.Product',
    pageSize: 10,
    id: 'Id',

    remoteFilter: true,
    remoteSort: true
});
