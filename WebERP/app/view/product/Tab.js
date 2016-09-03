Ext.define('WC.view.product.Tab', {
    extend: 'Ext.container.Container',
    closable: true,
    layout: {
        type: 'hbox',
        pack: 'start',
        align: 'stretch'
    },
    bodyPadding: '0 0 0 0',
    
    gridSelectionChange: function (model, records) {
        var rec = records[0];
        if (rec) {
            this.currentRecord = rec;
            this.productForm.loadRecord(rec);

    //        var combo = this.down("form").getForm().findField('productFamily');
          //  combo.setValue(4);

        }
    },

    initComponent: function () {
        this.callParent(arguments);

        var productStore = Ext.create('WC.store.Products');
        this.productList = Ext.create('WC.view.product.List', {
            id: 'product-list',
            store: productStore,
            width:'75%',
            dockedItems: [Ext.create('Ext.toolbar.Paging', {
                dock: 'bottom',
                store: productStore,
                displayInfo: true
            })]
        });
        this.productList.parentPanel = this;
        productStore.load();

        this.productForm = Ext.create('WC.view.product.Form', {
            id: 'pte-form'
        });
        this.productForm.parentPanel = this;
        this.productForm.dataList = this.productList;
        this.productForm.modelName = 'WC.model.Product';
        this.productForm.editable = true;
   //     this.productForm.getForm().findField('productFamily').getStore().load();


        this.dataStore = productStore;
        this.currentRecord = Ext.create(this.productForm.modelName);

        this.items = [
            this.productList, this.productForm
        ];
        this.callParent(arguments);

    
    }
});
