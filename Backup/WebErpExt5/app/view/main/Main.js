/**
 * This class is the main view for the application. It is specified in app.js as the
 * "autoCreateViewport" property. That setting automatically applies the "viewport"
 * plugin to promote that instance of this class to the body element.
 *
 * TODO - Replace this content of this view to suite the needs of your application.
 */
Ext.define('ERP.view.main.Main', {
    extend: 'Ext.container.Container',
    requires: [
        'ERP.view.main.MainController',
        'ERP.view.main.MainModel',
        'ERP.view.menu.Menu',
        'ERP.view.menu.MenuController',
        'ERP.view.tabContainer.TabContainer',
        'ERP.view.tabContainer.TabContainerController',
        'Ext.picker.Date'
        //'ERP.view.product.Form',
        //'ERP.view.product.List',
        //'ERP.view.product.Tab'
       // 'Ext.data.TreeModel'
        //'ERP.view.shared.TabContainer'
    ],

    xtype: 'app-main',

    controller: 'main',
    viewModel: {
        type: 'main'
    },

    layout: {
        type: 'border'
    },

    items: [{
        xtype: 'menu',
        region: 'west',
        width: 300,
        height: 150
    }, {
        width: '300',
        region: 'center',
        xtype: 'tabcontainer'
    }]

    //items: [{
    //    xtype: 'mainmenu',
    //    region: 'west',
    //    width: 230
    //}, {
    //    id: 'tab-container',
    //    region: 'center',
    //    xtype: 'tabcontainer'
    //}]
});
