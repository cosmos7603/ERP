/// <reference path="../../../ext/ext-all-debug-w-comments.js" />
Ext.require([
    'Ext.grid.filters.Filters'
]);

Ext.define('ERP.view.product.ProductList', {
    extend: 'ERP.view.shared.BaseList',
    alias: 'widget.productlist',
    title: Resources.listOfProducts,
    editable: false,
    bind: '{Products}',
    reference: 'grid',
    width: '100%',

    initComponent: function ()
    {
        Ext.apply(this, {
            dockedItems: [Ext.create('Ext.toolbar.Paging', {
                dock: 'bottom',
                displayInfo: true,
                bind: {
                    store: '{Products}'
                }
            })],
            columns: [{
                dataIndex: 'ProductCode',
                text: Resources.ProductCode,
                flex: 1,
                sortable: true
            }, {
                dataIndex: 'ShortDescription',
                text: Resources.ShortDescription,
                flex: 1,
                sortable: true
            }, {
                dataIndex: 'LongDescription',
                text: Resources.LongDescription,
                flex: 1,
                sortable: true
            }, {
                dataIndex: 'ProductFamily',
                renderer: function (value, metaData, record, row, col, store, gridView)
                {
                    return value["Description"];
                },
                getSortParam: function ()
                {
                    return 'ProductFamily.Description';
                },
                text: Resources.ProductFamily,
                flex: 1,
                sortable: true
            }, {
                xtype: 'checkcolumn',
                dataIndex: 'Active',
                text: Resources.Active,
                flex: 1,
                processEvent: function ()
                {
                    return false;
                }
            }, {
                xtype: 'checkcolumn',
                dataIndex: 'AvailableForSale',
                text: Resources.AvailableForSale,
                flex: 1,
                processEvent: function ()
                {
                    return false;
                }
            }, {
                dataIndex: 'Brand',
                text: Resources.Brand,
                flex: 1
            }, {
                dataIndex: 'MeasureUnity',
                text: Resources.MeasureUnity,
                flex: 1
            },

                {
                    dataIndex: 'Weight',
                    text: Resources.Weight,
                    flex: 1

                }, {
                    dataIndex: 'Height',
                    text: Resources.Height,
                    flex: 1

                }, {
                    dataIndex: 'Width',
                    text: Resources.Width,
                    flex: 1
                }, {
                    dataIndex: 'Length',
                    text: Resources.Length,
                    flex: 1,
                    editor: {
                        xtype: 'numberfield'
                    }
                }


            ]
            //,
            //filters: [{
            //    type: 'string',
            //    dataIndex: 'productCode'
            //}, {
            //    type: 'string',
            //    dataIndex: 'shortDescription'
            //}]

        });
        this.callParent(arguments);

    },
    autoDestroy: false

});