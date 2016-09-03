Ext.define('ERP.view.menu.MenuController', {
    extend: 'Ext.app.ViewController',
    views: [
        'ERP.view.menu.Menu'
    ],
    requires: ['ERP.view.shared.*'],
    alias: 'controller.menu',
    onSelectItem: function (selModel, record)
    {
        if (record.get('id'))
        {
            var nodeId = record.get('id');

            var titleText = '';
            if (record.parentNode)
                if (record.parentNode.get('id'))
                    titleText = record.parentNode.get('text') + ' - ';
            titleText += record.get('text');

            var tabContainer = Ext.ComponentQuery.query("tabcontainer")[0];
            var tabId = 'tab-' + nodeId;
            var tabContent = null;
            //var list = Ext.create('ERP.view.'+nodeId + '.' + nodeId.ucfirst() + 'List', {
            //    width: '60%',
            //    //bind: '{Products}',
            //    //reference: 'productsGrid',
            //    //dockedItems: [Ext.create('Ext.toolbar.Paging', {
            //    //    dock: 'bottom',
            //    //    displayInfo: true,
            //    //    bind: { store: '{Products}' }
            //    //})],
            //});

            if (!tabContainer.items.get(tabId))
            {
                tabContent = Ext.create('ERP.view.' + nodeId + '.' + nodeId.ucfirst() + 'ListTab',{
                    id: tabId,
                    iconCls: record.get('iconCls'),
                    title: titleText,
                    className:nodeId
                });
                tabContainer.add(tabContent).show();
            } else
            {
                tabContainer.setActiveTab(tabId);
            }
        }

    }
});
