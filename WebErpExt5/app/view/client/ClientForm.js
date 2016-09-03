Ext.define('ERP.view.client.ClientForm', {
    extend: 'ERP.view.shared.BaseForm',
    // extend:'Ext.form.Panel',
    width: '60%',
    hideEmptyLabel: false,

    items: [{
        xtype: 'fieldset',
        title: 'Formulario client',
        bodyPadding: '5 5 5',
        defaults: {
            border: false,
            xtype: 'panel',
            flex: 1,
            layout: 'anchor'
        },
        layout: 'hbox',
        items: [{
            defaults: {
                anchor: '-5',
                xtype: 'textfield',
                width: '200px'
            },
            items: [{
                name: 'Active',
                fieldLabel: Resources.Active,
                xtype: 'checkbox',
                infoMsg: "Si está activo, será acc...larios de la aplicación",
                width:'100%'

            }, {
                reference: 'clientTypeCombo',
                bind: {
                    store: '{ClientTypes}',
                    value: '{currentModel.ClientTypeId}'
                },
                fieldLabel: Resources.ClientType,
                xtype: 'basecombobox',
                displayField: 'Description',
                valueField: 'Id'
            }, {
                name: 'clientCode',
                fieldLabel: Resources.ClientCode,
                bind: {
                    value: '{currentModel.ClientCode}'
                },
            }, {
                name: 'Language',
                fieldLabel: Resources.Language,
                bind: {
                    value: '{currentModel.Language}'
                },

            }, {
                name: 'WebSite',
                fieldLabel: Resources.WebSite,
                bind: {
                    value: '{currentModel.WebSite}'
                },

            }, {
                xtype: 'datefield',
                name: 'BirthDate',
                fieldLabel: Resources.BirthDate,
                bind: {
                    value: '{currentModel.BirthDate}'
                },

            }, {
                name: 'Observations',
                fieldLabel: Resources.Observations,
                xtype: 'textarea',
                bind: {
                    value: '{currentModel.Observations}'
                },

            }, {
                reference: 'sectorCombo',
                bind: {
                    store: '{Sectors}',
                    value: '{currentModel.SectorId}'
                },
                fieldLabel: Resources.Sector,
                xtype: 'basecombobox',
                displayField: 'Description',
                valueField: 'Id'
            }]
        }, {
            defaults: {
                anchor: '100%',
                xtype: 'textfield',
                width: '200px'
            },
            items: [
                {
                name: 'CorporateName',
                fieldLabel: Resources.CorporateName,
                bind: {
                    value: '{currentModel.CorporateName}'
                },
            }, {
                name: 'ComercialName',
                fieldLabel: Resources.ComercialName,
                bind: {
                    value: '{currentModel.ComercialName}'
                },
            }, {
                name: 'Email',
                fieldLabel: Resources.Email,
                bind: {
                    value: '{currentModel.Email}'
                },

            }, {
                reference: 'comercialAgentCombo',
                tpl: Ext.create('Ext.XTemplate',
                        '<tpl for=".">', '<div class="x-boundlist-item">',
                        '{FirstName} {LastName}',
                        '</div></tpl>'
                ),
                displayTpl: Ext.create('Ext.XTemplate',
                        '<tpl for=".">',
                        '{FirstName} {LastName}', '</tpl>'

                ),
                bind: {
                    store: '{ComercialAgents}',
                    value: '{currentModel.ComercialAgentId}'
                },
                fieldLabel: Resources.ComercialAgent,
                xtype: 'basecombobox',
                displayField: 'FirstName',

                valueField: 'Id'
            }]
        }]
    }, {
        autoHeight: true,
        collapsed: false,
        //height:'200px',
        collapsible: true,
        //checkboxToggle: true,
        title: 'Direccion de facturacion',
        xtype: 'fieldset',
        bodyPadding: '5 5 5',
        defaults: {
            border: false,
            xtype: 'panel',
            flex: 1,
            layout: 'anchor'
        },
        layout: 'hbox',
        items: [{
            defaults: {
                anchor: '-5',
                xtype: 'textfield',
                width: '200px'
            },
            items: [{
                name: 'Address1',
                fieldLabel: Resources.Address1,
                bind: {
                    value: '{currentModel.Address1}'
                },

            }, {
                name: 'Address2',
                fieldLabel: Resources.Address2,
                bind: {
                    value: '{currentModel.Address2}'
                },
            }, {
                name: 'Country',
                fieldLabel: Resources.Country,
                bind: {
                    value: '{currentModel.Country}'
                },
            }, {
                name: 'Province',
                fieldLabel: Resources.Province,
                bind: {
                    value: '{currentModel.Province}'
                },
            }]
        }, {
            defaults: {
                anchor: '-5',
                xtype: 'textfield',
                width: '200px'
            },
            items: [{
                name: 'Zipcode',
                fieldLabel: Resources.Zipcode,
                bind: {
                    value: '{currentModel.Zipcode}'
                },

            }, {
                name: 'Telephone1',
                fieldLabel: Resources.Telephone1,
                bind: {
                    value: '{currentModel.Telephone1}'
                },
            }, {
                name: 'Telephone2',
                fieldLabel: Resources.Telephone2,
                bind: {
                    value: '{currentModel.Telephone2}'
                },
            }]
        }]
    }, {
        autoHeight: true,
        collapsed: false,
        //height:'200px',
        collapsible: true,
        //checkboxToggle: true,
        title: 'Datos de facturacion',
        xtype: 'fieldset',
        bodyPadding: '5 5 5',
        defaults: {
            border: false,
            xtype: 'panel',
            flex: 1,
            layout: 'anchor'
        },
        layout: 'hbox',
        items: [{
            defaults: {
                anchor: '-5',
                xtype: 'textfield',
                width: '200px'
            },
            items: [{
                reference: 'chargeMethodCombo',
                bind: {
                    store: '{ChargeMethods}',
                    value: '{currentModel.ChargeMethodId}'
                },
                fieldLabel: Resources.ChargeMethod,
                xtype: 'basecombobox',
                displayField: 'Description',
                valueField: 'Id'
            },
            {
                reference: 'dueDateTypeCombo',
                bind: {
                    store: '{PaymentDueDateTypes}',
                    value: '{currentModel.PaymentDueDateTypeId}'
                },
                fieldLabel: Resources.PaymentDueDateType,
                xtype: 'basecombobox',
                displayField: 'Description',
                valueField: 'Id'
            }]
        }, {
            defaults: {
                anchor: '-5',
                xtype: 'textfield',
                width: '200px'
            },
            items: [ {
                name: 'PaymentDay',
                fieldLabel: Resources.PaymentDay,
                xtype: 'numberfield',
                bind: {
                    value: '{currentModel.PaymentDay}'
                },
            }, {
                name: 'PaymentDay2',
                fieldLabel: Resources.PaymentDay2,
                xtype: 'numberfield',
                bind: {
                    value: '{currentModel.PaymentDay2}'
                },
            }, {
                reference: 'taxCombo',
                bind: {
                    store: '{Taxes}',
                    value: '{currentModel.TaxId}'
                },
                fieldLabel: Resources.Tax,
                xtype: 'basecombobox',
                displayField: 'Description',
                valueField: 'Id'
            }
            ]
        }]
    }]


//    xtype: 'fieldset',
//border: 2,
//padding: '10 0 0 15',
//defaultType: 'textfield',
//items: [

});