Ext.require([
    'Ext.tab.*',
  //  'Ext.ux.TabCloseMenu'
]);

Ext.define('WC.view.shared.TabContainer', {
    extend: 'Ext.tab.Panel',
    alias: 'widget.tabcontainer',
    resizeTabs: true,
    enableTabScroll: true,
    defaults: {
        autoScroll: true,
        bodyPadding: 10
    },
  //  plugins: Ext.create('Ext.ux.TabCloseMenu'),
    bodyCls: 'site-tabcontainer'
});
