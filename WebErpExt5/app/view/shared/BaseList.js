Ext.require(['Ext.toolbar.Paging']);

Ext.define('ERP.view.shared.BaseList', {
    extend: 'Ext.grid.Panel',

    initComponent: function ()
    {
        this.callParent(arguments);
    },
    emptyText: 'No records found',
    loadMask: true,
    collapsible: false,
    animCollapse: false,
    autoDestroy: true,
    header: {
       // title: Resources.products,
        padding: '4 9 5 9',
        items: [
        {
            xtype: 'button',
         //   text: Resources["New"],//Access with index style, otherwise the cmd sencha compiler throws issues
            itemId: 'buttonNew',
            region: 'west',
            handler: 'onCreate'
        }, {
            xtype: 'button',
         //   text: Resources["Edit"],
            region: 'west',
            itemId: 'buttonEdit',
            handler: 'onEdit',
            disabled: 'true',
            bind: {
                disabled: '{!isRecordSelected}'
            }
        },
        {
            xtype: 'button',
    //        text: Resources["Delete"],
            region: 'west',
            itemId: 'buttonDelete',
            handler: 'onDelete',
            disabled: 'true',
            bind: {
                disabled: '{!isRecordSelected}'
            }
        }
        ]
    }
});
