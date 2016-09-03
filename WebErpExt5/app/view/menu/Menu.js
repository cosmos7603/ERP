Ext.define('ERP.view.menu.Menu', {
    extend: 'Ext.tree.Panel',
    alias: 'widget.menu',
    controller: 'menu',
    //    store: 'ERP.store.Menu',
    title: 'Menu',
    split: true,
    width: 200,
    minWidth: 200,
    rootVisible: false,
    autoScroll: true,
    collapsible: true,
    floatable: false,
    listeners: {
        itemClick: 'onSelectItem'
    },
    root: {
        expanded: true,
        expandable: true,
        children: [{
            text: 'Datos Maestros',
            leaf: false,
            expanded: true,
            expandable: true,
            children: [
                {
                    id: 'product',
                    text: Resources.Products,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                },
                {
                    id: 'client',
                    text: Resources.Clients,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                }, {
                    id: 'potentialClient.Tab',
                    text: Resources.PotentialClients,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                }, {
                    id: 'provider.Tab',
                    text: Resources.Providers,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                }, {
                    id: 'partner.Tab',
                    text: Resources.Partners,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                }, {
                    id: 'comercialAgent.Tab',
                    text: Resources.ComercialAgents,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                }, {
                    id: 'transporter.Tab',
                    text: Resources.Transporters,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                }, {
                    id: 'project.Tab',
                    text: Resources.Projects,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                }, {
                    id: 'documentsRepository.Tab',
                    text: Resources.DocumentsRepository,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                }, {
                    id: 'billing.Tab',
                    text: Resources.Billing,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: false,
                    children: [{
                        id: 'bankAccount.Tab',
                        text: Resources.BankAccounts,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'cashRegister.Tab',
                        text: Resources.CashRegisters,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'chargePaymentMethod.Tab',
                        text: Resources.ChargePaymentMethod,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'expirationType.Tab',
                        text: Resources.ExpirationTypes,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'invoiceSeries.Tab',
                        text: Resources.InvoiceSeries,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'invoiceType.Tab',
                        text: Resources.InvoiceTypes,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'tax.Tab',
                        text: Resources.Taxes,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'chargePaymentConcept.Tab',
                        text: Resources.ChargePaymentConcepts,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }]
                }, {
                    id: 'warehouse.Tab',
                    text: Resources.DocumentsRepository,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: false,
                    children: [{
                        id: 'family.Tab',
                        text: Resources.Families,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'brand.Tab',
                        text: Resources.Brands,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'measurmentUnit.Tab',
                        text: Resources.MeasurmentUnits,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }]
                }, {
                    id: 'purchases.Tab',
                    text: Resources.Purchases,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: false,
                    children: [
                        {
                            id: 'providerType.Tab',
                            text: Resources.ProviderTypes,
                            iconCls: 'icon icon-paciente',
                            cls: 'clickable',
                            leaf: true
                        }, {
                            id: 'providerRating.Tab',
                            text: Resources.ProviderRatings,
                            iconCls: 'icon icon-paciente',
                            cls: 'clickable',
                            leaf: true
                        }
                    ]
                }, {
                    id: 'sales.Tab',
                    text: Resources.Sales,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: false,
                    expanded: true,
                    expandable: true,
                    children: [{
                        id: 'offerType.Tab',
                        text: Resources.OfferTypes,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'price.Tab',
                        text: Resources.Prices,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }]
                }, {
                    id: 'crm.Tab',
                    text: Resources.Crm,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                }]
        }
        , {
            text: Resources.Billing,
            leaf: false,
            expanded: true,
            expandable: true
        }, {
            text: Resources.Warehouse,
            leaf: false,
            expanded: true,
            expandable: true
        }, {
            text: Resources.Purchases,
            leaf: false,
            expanded: true,
            expandable: true
        }, {
            //     text: Resources.Sales,
            leaf: false,
            expanded: true,
            expandable: true
        }, {
            //  text: Resources.Crm,
            leaf: false,
            expanded: true,
            expandable: true
        }, {
            //   text: Resources.Reports,
            leaf: false,
            expanded: true,
            expandable: true
        }
            , {
                //  text: Resources.Administration,
                leaf: false,
                expanded: true,
                expandable: true
            }
        ]
    }

    //initComponent: function () {
    //    this.store = Ext.create('ERP.store.Menu', { id: 'menuStore' });
    //    this.callParent(arguments);
    //}
});
