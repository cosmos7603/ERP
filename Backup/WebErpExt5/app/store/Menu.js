Ext.define('ERP.store.Menu', {
    extend: 'Ext.data.TreeStore',
    root: {
        expanded: false,
        expandable: false,
        children: [{
            text: 'Datos Maestros',
            leaf: false,
            expanded: true,
            expandable: true,
            children: [
                {
                    id: 'product',
                    text: Resources.products,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                },
                {
                    id: 'client.Tab',
                    text: Resources.clients,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                }, {
                    id: 'potentialClient.Tab',
                    text: Resources.potentialClients,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                }, {
                    id: 'provider.Tab',
                    text: Resources.providers,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                }, {
                    id: 'partner.Tab',
                    text: Resources.partners,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                }, {
                    id: 'comercialAgent.Tab',
                    text: Resources.comercialAgents,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                }, {
                    id: 'transporter.Tab',
                    text: Resources.transporters,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                }, {
                    id: 'project.Tab',
                    text: Resources.projects,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                }, {
                    id: 'documentsRepository.Tab',
                    text: Resources.documentsRepository,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                }, {
                    id: 'billing.Tab',
                    text: Resources.billing,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: false,
                    children: [{
                        id: 'bankAccount.Tab',
                        text: Resources.bankAccounts,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'cashRegister.Tab',
                        text: Resources.cashRegisters,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'chargePaymentMethod.Tab',
                        text: Resources.chargePaymentMethod,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'expirationType.Tab',
                        text: Resources.expirationTypes,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'invoiceSeries.Tab',
                        text: Resources.invoiceSeries,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'invoiceType.Tab',
                        text: Resources.invoiceTypes,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'tax.Tab',
                        text: Resources.taxes,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'chargePaymentConcept.Tab',
                        text: Resources.chargePaymentConcepts,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }]
                }, {
                    id: 'warehouse.Tab',
                    text: Resources.documentsRepository,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: false,
                    children: [{
                        id: 'family.Tab',
                        text: Resources.families,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'brand.Tab',
                        text: Resources.brands,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'measurmentUnit.Tab',
                        text: Resources.measurmentUnits,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }]
                }, {
                    id: 'purchases.Tab',
                    text: Resources.purchases,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: false,
                    children: [
                        {
                            id: 'providerType.Tab',
                            text: Resources.providerTypes,
                            iconCls: 'icon icon-paciente',
                            cls: 'clickable',
                            leaf: true
                        }, {
                            id: 'providerRating.Tab',
                            text: Resources.providerRatings,
                            iconCls: 'icon icon-paciente',
                            cls: 'clickable',
                            leaf: true
                        }
                    ]
                }, {
                    id: 'sales.Tab',
                    text: Resources.sales,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: false,
                    expanded: true,
                    expandable: true,
                    children: [{
                        id: 'offerType.Tab',
                        text: Resources.offerTypes,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }, {
                        id: 'price.Tab',
                        text: Resources.prices,
                        iconCls: 'icon icon-paciente',
                        cls: 'clickable',
                        leaf: true
                    }]
                }, {
                    id: 'crm.Tab',
                    text: Resources.crm,
                    iconCls: 'icon icon-paciente',
                    cls: 'clickable',
                    leaf: true
                }]
        }, {
            text: Resources.billing,
            leaf: false,
            expanded: true,
            expandable: true
        }, {
            text: Resources.warehouse,
            leaf: false,
            expanded: true,
            expandable: true
        }, {
            text: Resources.purchases,
            leaf: false,
            expanded: true,
            expandable: true
        }, {
            text: Resources.sales,
            leaf: false,
            expanded: true,
            expandable: true
        }, {
            text: Resources.crm,
            leaf: false,
            expanded: true,
            expandable: true
        }, {
            text: Resources.reports,
            leaf: false,
            expanded: true,
            expandable: true
        }, {
            text: Resources.administration,
            leaf: false,
            expanded: true,
            expandable: true
        }]
    }
});