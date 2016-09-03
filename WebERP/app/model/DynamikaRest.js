Ext.define('WC.model.DynamikaRest', {
    extend: 'Ext.data.proxy.Rest',
    alias: 'proxy.dynamikarest',
    buildUrl: function() {
        //arguments[0].url = this.url + this.model.modelName.replace('WC.model.', '');
        arguments[0].url = this.url + 'product';
        return Ext.data.proxy.Rest.prototype.buildUrl.apply(this, arguments);
    }
})