


Ext.define('WC.model.BaseModel', {
    extend: 'Ext.data.Model',
    
    proxy: {
        type: 'rest',
        //type : 'dynamikarest',
        url : 'DynamikaERP/rest/',
        reader: {
            type: 'json',
            root: 'Items',
            totalProperty: 'TotalCount'
        },
        writer: {
        	type: 'json',
		    writeAllFields: true,
		    nameProperty: 'mapping',
		    root: 'model'
        }
        ,buildUrl: function() {
            arguments[0].url = this.url + this.model.modelName.replace('WC.model.', '');
            return Ext.data.proxy.Rest.prototype.buildUrl.apply(this, arguments);
        }
    }
});
