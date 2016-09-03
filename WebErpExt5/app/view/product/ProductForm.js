
Ext.define('ERP.view.product.ProductForm', {
    extend: 'ERP.view.shared.BaseForm',
    title: 'Formulario product',
    reference: 'form',
    items: [
    {
        xtype: 'fieldset',
        border: 0,
        padding: '10 0 0 15',

        defaultType: 'textfield',
        items: [
            //{
            //    xtype: 'hiddenfield',
            //    name: 'id'
            //},
            {
                fieldLabel: Resources.ProductCode,
                name: 'ProductCode',
                bind: {
                    value: '{currentModel.ProductCode}'
                },
                allowBlank: false,
                enforceMaxLength: true,
                maxLength: 100,
                width: 300
            },
            {
                reference: 'familyCombo',
                bind: {
                    store: '{ProductFamilies}',
                    value: '{currentModel.ProductFamilyId}'
                },
                fieldLabel: Resources.ProductFamily,
                xtype: 'basecombobox',
                displayField: 'Description',
                valueField: 'Id',
                width: 300
            },

        {
            fieldLabel: Resources.ShortDescription,
            name: 'ShortDescription',
            bind: {
                value: '{currentModel.ShortDescription}'
            },
            allowBlank: true,
            enforceMaxLength: true,
            maxLength: 100,
            width: 300
        }, {
            fieldLabel: Resources.LongDescription,
            name: 'LongDescription',
            bind: {
                value: '{currentModel.LongDescription}'
            },
            allowBlank: true,
            enforceMaxLength: true,
            maxLength: 100,
            width: 300
        }, {
            fieldLabel: Resources.Active,
            xtype: 'checkbox',
            name: 'Active',
            bind: {
                value: '{currentModel.Active}'
            },
            allowBlank: false,
            width: 300
        },
        {
            fieldLabel: Resources.AvailableForSale,
            xtype: 'checkbox',
            name: 'AvailableForSale',
            bind: {
                value: '{currentModel.AvailableForSale}'
            },
            allowBlank: false,
            width: 300
        },
        {
            fieldLabel: Resources.Brand,
            name: 'Brand',
            bind: {
                value: '{currentModel.Brand}'
            },
            allowBlank: false,
            width: 300
        },
        {
            fieldLabel: Resources.MeasureUnity,
            name: 'MeasureUnity',
            bind: {
                value: '{currentModel.MeasureUnity}'
            },
            allowBlank: false,
            width: 300
        },
        {
            fieldLabel: Resources.Weight,
            xtype: 'numberfield',
            name: 'Weight',
            bind: {
                value: '{currentModel.Weight}'
            },
            allowBlank: false,
            width: 300
        },
        {
            fieldLabel: Resources.Height,
            xtype: 'numberfield',
            name: 'Height',
            bind: {
                value: '{currentModel.Height}'
            },
            allowBlank: false,
            width: 300
        },
        {
            fieldLabel: Resources.Width,
            name: 'Width',
            bind: {
                value: '{currentModel.Width}'
            },
            xtype: 'numberfield',
            allowBlank: false,
            width: 300
        },
        {
            fieldLabel: Resources.Length,
            name: 'Length',
            bind: {
                value: '{currentModel.Length}'
            },
            xtype: 'numberfield',
            allowBlank: false,
            width: 300
        }

        ]
    }]
});
