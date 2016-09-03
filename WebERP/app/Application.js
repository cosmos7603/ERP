Ext.define('WC.Application', {
    extend: 'Ext.app.Application',
    
    name: 'WC',
    appFolder: 'DynamikaERP/app',

    views: [
        'shared.MainMenu',
        'shared.BaseFormList',
        'shared.BaseList',
        'shared.TabContainer'
    ],

    controllers: [
        'Main'
    ],

    stores: [
        'WC.store.ProductFamilies',
        'WC.store.Products'
    ]
});

Ext.require(['Ext.form.DateField', 'Ext.grid.PropertyColumnModel']);
//Ext.require(['Ext.form.DateField', 'Ext.grid.PropertyColumnModel', 'WC.model.DynamikaRest']);


Ext.onReady(function() {
    Ext.grid.PropertyColumnModel.prototype.dateFormat = 'd/m/Y';
    Ext.form.DateField.prototype.format = 'd/m/Y';
    Ext.data.Field.prototype.dateFormat = 'd/m/Y';
//    Ext.ux.grid.filter.DateFilter.prototype.dateFormat = 'd/m/Y';
    Ext.DatePicker.prototype.format = 'd/m/Y';
    Ext.Date.defaultFormat = 'd/m/Y';
});