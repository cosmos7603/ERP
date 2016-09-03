Ext.define('ERP.view.shared.BaseForm', {
    extend: 'Ext.form.Panel',
    jsonSubmit: true,
    height: '100%',
    flex: 2,
   // layout: 'fit',
    border: false,
  //  collapsible: false,
    editable: true,
    autoDestroy: true,
    mode:"",
    setRecord: function (record)
    {
        this.loadRecord(record);
    },

    listeners: {
        afterrender: function (obj, eOpts) {
            obj.getDockedItems('toolbar')[0].setHeight(40);
        }
    },
    loadMask: true,
    buttons: [{
        text: Resources.Save,
        itemId: 'buttonSave',
        handler: 'onCommit'
    }, {
        text: Resources.Cancel,
        itemId: 'buttonCancel',
        handler: 'onCancel'
    }]
   
});
