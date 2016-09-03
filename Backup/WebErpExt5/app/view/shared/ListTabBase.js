

Ext.define('ERP.view.shared.ListTabBase', {
    extend: 'Ext.container.Container',
    alias: 'widget.listtabbase',
    closable: true,
    layout: {
        type: 'hbox',
        pack: 'start',
        align: 'stretch'
    },
    bodyPadding: '0 0 0 0',
    autoDestroy: false,
    session: true,
    initComponent: function ()
    {
        Ext.apply(this, {

            items: [
                  {
                      xtype: this.className + 'list',
                      width: '100%'
                  }]
        });
        this.callParent(arguments);
    }
});
