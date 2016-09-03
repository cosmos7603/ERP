Ext.define('ERP.view.product.ProductListTab', {
    extend: 'ERP.view.shared.ListTabBase',
    alias: 'widget.productlisttab',
    controller: 'productlistcontroller',
    viewModel: { type: 'productlistviewmodel' },
    requires: [
        'ERP.store.Product*',
        'ERP.view.product.*'
    ]
});
