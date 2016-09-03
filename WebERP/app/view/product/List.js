/// <reference path="../../../ext/ext-all-debug-w-comments.js" />
Ext.require([
    'Ext.toolbar.Paging',
    'Ext.ux.grid.FiltersFeature'
]);

Ext.define('WC.view.product.List', {
    extend: 'WC.view.shared.BaseList',
    title: 'Lista de productos',
    //uses: [
    //    'Ext.grid.plugin.CellEditing'
    //],
    //plugins: [{
    //    ptype: 'cellediting',
    //    clicksToEdit: 2,
    //    pluginId: 'cellediting'
    //}],
    width: 600,

    columns: [{
        xtype: 'actioncolumn',
        width: 30,
        sortable: false,
        menuDisabled: true,
        items: [{
            icon: 'DynamikaERP/resources/images/grid-icons/delete.png',
            tooltip: 'Delete product',
            scope: this,
            handler: function (grid, rowIndex)
            {
                var rec = grid.getStore().getAt(rowIndex);
                if (rec)
                {
                    grid.getStore().remove(rec);
                    grid.getStore().sync();
                }
            }
        }]
    }, {
        dataIndex: 'productCode',
        text: Resources.productCode,
        flex: 1,
        sortable: true
    }, {
        dataIndex: 'shortDescription',
        text: Resources.shortDescription,
        flex: 1,
        sortable: true,
        //editor: {
        //    xtype: 'textfield'
        //}
    }, {
        dataIndex: 'longDescription',
        text: Resources.longDescription,
        flex: 1,
        sortable: true,
        //editor: {
        //    xtype: 'textfield'
        //}
    }, {
        dataIndex: 'productFamily_description',
        text: Resources.productFamily,
        flex: 1,
        sortable: true,
        //editor: {
        //    xtype: 'combo',
        //    pageSize: 10,
        //    minChars: 2,
        //    selectOnFocus: true,
        //    store: Ext.create('WC.store.ProductFamilies', {
        //        pageSize: 10,
        //        autoLoad:true
        //    }),
        //    valueField: 'id',
        //    displayField: 'description',
        //    listConfig: { minWidth: 280 }
        //}
    }, {
        xtype: 'checkcolumn',
        dataIndex: 'active',
        text: Resources.active,
        flex: 1
    }, {
        xtype: 'checkcolumn',
        dataIndex: 'availableForSale',
        text: Resources.availableForSale,
        flex: 1
    }, {
        dataIndex: 'brand',
        text: Resources.brand,
        flex: 1
    }, {
        dataIndex: 'measureUnity',
        text: Resources.measureUnity,
        flex: 1,
        //editor: {
        //    xtype: 'numberfield'
        //}
    },

        {
            dataIndex: 'weight',
            text: Resources.weight,
            flex: 1,
            //editor: {
            //    xtype: 'numberfield'
            //}

        }, {
            dataIndex: 'height',
            text: Resources.height,
            flex: 1,
            //editor: {
            //    xtype: 'numberfield'
            //}

        }, {
            dataIndex: 'width',
            text: Resources.width,
            flex: 1,
            //editor: {
            //    xtype: 'numberfield'
            //}
        }, {
            dataIndex: 'length',
            text: Resources.length,
            flex: 1,
            editor: {
                xtype: 'numberfield'
            }
        }


    ],

    features: [{
        ftype: 'filters',
        filters: [{
            type: 'string',
            dataIndex: 'productCode'
        }, {
            type: 'string',
            dataIndex: 'shortDescription'
        }]
    }]
});