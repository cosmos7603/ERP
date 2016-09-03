Ext.require(['Ext.toolbar.Paging']);

Ext.define('WC.view.shared.BaseList', {
    extend: 'Ext.grid.Panel',

    initComponent: function () {
        this.callParent(arguments);
    },

    emptyText: 'No records found',
    loadMask: true,
    collapsible: false,
    animCollapse: false,
    listeners: {
        selectionchange: function (model, records) {
            this.parentPanel.gridSelectionChange(model, records);
        }
    }
});
