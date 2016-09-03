Ext.define('WC.view.shared.BaseFormList', {
    extend: 'Ext.form.Panel',

    jsonSubmit: true,
    height: '100%',
    flex: 2,
    layout: 'fit',
    border: false,
    collapsible: false,
    editable:true,

    initComponent: function () {
        this.callParent(arguments);
    },

    listeners: {
        afterrender: function (obj, eOpts) {
            obj.getDockedItems('toolbar')[0].setHeight(40);
        }
    },

    //setReadOnlyForAll: function (bReadOnly) {
    //    this.getForm().getFields().each(function (field) {
    //        if (field.$className != 'Ext.form.field.FileButton')
    //            field.setReadOnly(bReadOnly);
    //    });
    //},

    setEditMode: function (bEditMode) {
        //this.setReadOnlyForAll(!bEditMode);

        //var toolbar = this.getDockedItems('toolbar')[0];

        //this.dataList.setDisabled(bEditMode);
        //toolbar.getComponent('buttonNew').setVisible(!bEditMode);
        //toolbar.getComponent('buttonEdit').setVisible(!bEditMode);
        //toolbar.getComponent('buttonSave').setVisible(bEditMode);
        //toolbar.getComponent('buttonCancel').setVisible(bEditMode);

        //if (this.afterSetEditMode) {
        //    this.afterSetEditMode(bEditMode);
        //}
    },

    loadMask: true,
    buttons: [{
        text: 'New',
        itemId: 'buttonNew',
        region: 'west',
        handler: function () {
            var newRecord = Ext.create(this.up('panel').modelName);
            this.up('panel').loadRecord(newRecord);
         //   this.up('panel').setEditMode(true);
        }
    }, {
        text: 'Edit',
        region: 'west',
        itemId: 'buttonEdit',
        handler: function () {
          //  this.up('panel').setEditMode(true);
        }
    }, {
        text: 'Save',
        itemId: 'buttonSave',
        handler: function (button, e) {
            var frm = this.up('form').getForm();
            if (frm.isValid()) {
                frm.updateRecord();
                frm.getRecord().save({
                    success: function(rec, op) {
                        var result = op.request.scope.reader.jsonData;
                        if (!result.success)
                            Ext.Msg.alert('Error', result.error.message);

                        var parentStore = button.up('panel').parentPanel.dataStore;
                        parentStore.load();
                  //      button.up('panel').setEditMode(false);
                    },
                    failure: function(rec, op) {
                        var result = op.request.scope.reader.jsonData;
                        if (result)
                            Ext.Msg.alert('Error', result.error ? result.error.message : 'No response');
                    }
                });
            } else {
                Ext.Msg.alert("Error!", "the data is not valid, please review it.");
            }
        }
    }, {
        text: 'Cancel',
        itemId: 'buttonCancel',
        handler: function () {
            this.up('panel').loadRecord(this.up('panel').parentPanel.currentRecord);
         //   this.up('panel').setEditMode(false);
        }
    }]
});
