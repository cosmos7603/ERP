Ext.define('WC.controller.Main', {
    extend: 'Ext.app.Controller',

    views: [
        'shared.MainMenu'
    ],
    init: function () {
        this.control({
            'mainmenu': {
                select: this.onSelectItem
            }
        });
    },

    onSelectItem: function (selModel, record) {
        if (record.get('id')) {
            var nodeId = record.get('id');

            var titleText = '';
            if (record.parentNode)
                if (record.parentNode.get('id'))
                    titleText = record.parentNode.get('text') + ' : ';
            titleText += record.get('text');

            var tabContainer = Ext.getCmp('tab-container');
            var tabId = 'tab-' + nodeId;
            var tabContent = null;

            if (!tabContainer.items.get(tabId)) {
                tabContent = Ext.create('WC.view.' + nodeId, {
                    id: tabId,
                    iconCls: record.get('iconCls'),
                    title: titleText
                });

                tabContainer.add(tabContent).show();
            } else {
                tabContainer.setActiveTab(tabId);
            }
        }
    }
});
