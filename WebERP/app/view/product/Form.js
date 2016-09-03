
Ext.define('WC.view.product.Form', {
    extend: 'WC.view.shared.BaseFormList',
    title: 'Formulario product',
    require: 'WC.store.Products',
    initComponent: function () {
        this.callParent(arguments);
        this.getForm().findField('productFamily_id').getStore().load();
    },

    items: [
    {
        xtype: 'fieldset',
        border: 0,
        padding: '10 0 0 15',

        defaultType: 'textfield',
        items: [
        {
            xtype: 'hiddenfield',
            name: 'id'
        }, {
            fieldLabel: Resources.productCode,
            name: 'productCode',
            allowBlank: false,
            enforceMaxLength: true,
            maxLength: 100,
            width: 300
        }, {
            fieldLabel: Resources.shortDescription,
            name: 'shortDescription',
            allowBlank: false,
            enforceMaxLength: true,
            maxLength: 100,
            width: 300
        }, {
            fieldLabel: Resources.longDescription,
            name: 'longDescription',
            allowBlank: false,
            enforceMaxLength: true,
            maxLength: 100,
            width: 300
        }, {
            fieldLabel: Resources.active,
            xtype: 'checkbox',
            name: 'active',
            allowBlank: false,
            width: 300
        },
        {
            fieldLabel: Resources.availableForSale,
            xtype: 'checkbox',
            name: 'availableForSale',
            allowBlank: false,
            width: 300
        },
        {
            fieldLabel: Resources.brand,
            name: 'brand',
            allowBlank: false,
            width: 300
        },
        {
            fieldLabel: Resources.measureUnity,
            name: 'measureUnity',
            allowBlank: false,
            width: 300
        },
        {
            fieldLabel: Resources.weight,
            xtype: 'numberfield',
            name: 'weight',
            allowBlank: false,
            width: 300
        },
        {
            fieldLabel: Resources.height,
            xtype: 'numberfield',
            name: 'height',
            allowBlank: false,
            width: 300
        },
        {
            fieldLabel: Resources.width,
            name: 'width',
            xtype: 'numberfield',
            allowBlank: false,
            width: 300
        },
        {
            fieldLabel: Resources.length,
            name: 'length',
            xtype: 'numberfield',
            allowBlank: false,
            width: 300
        },
        {
            //id: 'productFamiliesCombo',
            queryMode: 'remote',
            autoLoad: 'true',
            store: Ext.getStore('WC.store.ProductFamilies'),
            fieldLabel: 'Family',
            name: 'productFamily_id',
            xtype: 'combo',
            displayField: 'description',
            //valueField: 'productFamily_id',
            valueField: 'id',
            //hiddenValue: 'productFamily_id',
            //hiddenName: 'ProductFamily.Id',
            //  submitValue:true,
            onChange: function (newVal, oldVal) {
                
                var frm = this.up("form");
                var store = this.getStore();
                var index = store.findExact('id', newVal);
                var description = store.getAt(index).data["description"];
                frm.getRecord().set("productFamily_description", description);
            },
            allowBlank: false,
            width: 300
        }
        ]
    }]
});
