/**
 * This class is the view model for the Main view of the application.
 */
Ext.define('ERP.view.product.ProductFormViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.productformviewmodel',
    stores: {
        Products: {
            extend: 'ERP.store.BaseStore',
            alias: 'store.products',
            model: 'ERP.model.Product',
            autoLoad: true,
            pageSize: 10,
            remoteFilter: true,
            remoteSort: true
        },
        ProductFamilies: {
            extend: 'ERP.store.BaseStore',
            model: 'ERP.model.ProductFamily',
            autoLoad: true
        }

        ,
        data: {
            name: 'ERP',
            currentModel: null
        },
        formulas: {
            currentModel: {
                bind: {
                    record: '{form.getModel}',
                    deep: true
                },
                get: function (data)
                {
                    return data.record;
                }
            }
        }

    }
});