Ext.define('ERP.view.client.ClientFormTab', {
    alias:'widget.clientformtab',
    extend: 'ERP.view.shared.FormTabBase',
    viewModel: {
        type: 'clientformviewmodel'
    },
    controller: 'clientformcontroller'

});
