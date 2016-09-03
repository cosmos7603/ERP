/**
 * This class is the view model for the Main view of the application.
 */
Ext.define('ERP.view.product.ProductListViewModel', {
    extend: 'ERP.view.shared.BaseListViewModel',
    alias: 'viewmodel.productlistviewmodel',
    stores: {
        Products: {
            extend: 'ERP.store.BaseStore',
            alias: 'store.products',
            model: 'ERP.model.Product',
            autoLoad: true,
            pageSize: 10,
            remoteFilter: true,
            remoteSort: true
        }
        ,
        ProductFamilies: {
            extend: 'ERP.store.BaseStore',
            model: 'ERP.model.ProductFamily',
            autoLoad: true
        }
    }
});