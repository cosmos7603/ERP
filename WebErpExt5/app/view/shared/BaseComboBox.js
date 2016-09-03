

Ext.define('ERP.view.shared.BaseComboBox', {
    extend: 'Ext.form.field.ComboBox',
    alias:'widget.basecombobox',
    initComponent: function () {
        Ext.apply(this, {
            queryMode: 'local',
            minChars: 0,
            anyMatch: true,
            triggerAction: 'all',
            hideTrigger: true,
            //  autoLoad: 'true',
            emptyText: 'Please select a record',
            autoSelect: true,
            forceSelection: true
        });
        this.callParent(arguments);
    }
});
