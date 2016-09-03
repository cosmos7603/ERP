Ext.define('WC.store.BaseStore', {
    extend: 'Ext.data.Store',

    listeners: {
        write: function(store, op, eOpts) {
            store.load();
        }
    }
});
