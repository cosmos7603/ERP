Ext.require([
    'Ext.tab.*',
    'Ext.form.Checkbox',
    'Ext.ux.CheckColumn',
    'Ext.form.ComboBox'
    //'Ext.form.RadioButton'
  //  'Ext.ux.TabCloseMenu'
]);

Ext.define('ERP.view.tabContainer.TabContainer', {
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
