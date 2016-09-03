/// <reference path="../../../ext/ext-all-debug-w-comments.js" />
Ext.require([
    'Ext.grid.filters.Filters'
]);

Ext.define('ERP.view.client.ClientList', {
    alias: 'widget.clientlist',
    extend: 'ERP.view.shared.BaseList',
    title: Resources.listOfClients,
    bind: '{Clients}',
    width: '100%',
    reference: 'clientsGrid',
    initComponent: function ()
    {
        Ext.apply(this, {
            dockedItems: [
                Ext.create('Ext.toolbar.Paging', {
                    dock: 'bottom',
                    displayInfo: true,
                    bind: {
                        store: '{Clients}'
                    }
                })
            ],
            columns: [{
                dataIndex: 'clientCode',
                text: Resources.ClientCode,
                flex: 1,
                sortable: true
            }, {
                dataIndex: 'corporateName',
                text: Resources.CorporateName,
                flex: 1,
                sortable: true
            }, {
                dataIndex: 'comercialName',
                text: Resources.ComercialName,
                flex: 1,
                sortable: true
            }, {
                dataIndex: 'email',
                text: Resources.Email,
                flex: 1,
                sortable: true
            }, {
                dataIndex: 'language',
                text: Resources.Language,
                flex: 1,
                sortable: true
            }, {
                dataIndex: 'website',
                text: Resources.Website,
                flex: 1,
                sortable: true
            }, {
                dataIndex: 'observations',
                text: Resources.Observations,
                flex: 1,
                sortable: true
            }, {
                xtype: 'datecolumn',
                dataIndex: 'birthday',
                text: Resources.Birthday,
                flex: 1,
                sortable: true
            }, {
                dataIndex: 'Sector.Description',
                text: Resources.Sector,
                flex: 1,
                sortable: true
            }, {
                dataIndex: 'Zone.Description',
                text: Resources.Zone,
                flex: 1,
                sortable: true
            }, {
                dataIndex: 'ChargeMethod.Description',
                text: Resources.ChargeMethod,
                flex: 1,
                sortable: true
            }, {
                dataIndex: 'ClientType.Description',
                text: Resources.ClientType,
                flex: 1,
                sortable: true
            }, {
                dataIndex: 'active',
                text: Resources.Active,
                xtype: 'checkcolumn',
                flex: 1,
                processEvent: function ()
                {
                    return false;
                }
            }]
        });
        this.callParent(arguments);
    }

    //features: [{
    //    ftype: 'filters',

    //}]
});