Ext.define('WC.view.shared.MainMenu', {
    extend: 'Ext.tree.Panel',
    alias: 'widget.mainmenu',

    title: 'Menu',
    split: true,
    width: 200,
    minWidth: 200,
    rootVisible: false,
    autoScroll: true,
    collapsible: true,
    floatable: false,

    initComponent: function () {
        this.store = Ext.create('WC.store.MainMenu', { id: 'mainMenuStore' });
        this.callParent(arguments);
    }
});
