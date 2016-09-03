Ext.define('WC.view.Main', {
    extend: 'Ext.container.Container',
    requires:[
        'WC.view.shared.MainMenu',
        'WC.view.shared.TabContainer'
    ],
    
    xtype: 'app-main',

    layout: {
        type: 'border'
    },

    items: [{
        xtype: 'mainmenu',
        region: 'west',
        width: 230
    }, {
        id: 'tab-container',
        region: 'center',
        xtype: 'tabcontainer'
    }]
});
