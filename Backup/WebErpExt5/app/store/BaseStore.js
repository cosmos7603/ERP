Ext.define('ERP.store.BaseStore', {
    extend: 'Ext.data.Store',
    autoLoad: true,
    pageSize: 10,
    remoteFilter: true,
    remoteSort: true,
    listeners: {
        write: function(store, op, eOpts) {
            store.load();
        }
    }
});
