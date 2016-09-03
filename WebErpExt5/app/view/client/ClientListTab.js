Ext.define('ERP.view.client.ClientListTab', {
    extend: 'ERP.view.shared.ListTabBase',
    alias: 'widget.clientlisttab',
    controller: 'clientlistcontroller',
    viewModel: { type: 'clientlistviewmodel' },
    requires: [
        'ERP.model.Client*',
        'ERP.view.client.*',
        'ERP.model.Sector',
        'ERP.model.ComercialAgent',
        'ERP.model.ChargeMethod',
        'ERP.model.PaymentDueDateType',
        'ERP.model.Tax'
    ]
});
