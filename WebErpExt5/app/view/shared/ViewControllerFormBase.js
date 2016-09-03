Ext.define('ERP.view.shared.ViewControllerFormBase', {
    extend: 'Ext.app.ViewController',
    className: '',
    alias: 'controller.viewcontrollerformbase',
    onCancel: function (btn, e)
    {
        var tabContainer = btn.up("form").up("panel");
        var tabForm = btn.up("form").up("container");
        tabContainer.remove(tabForm);

    },
    onCommit: function (btn, e) {

        var storeName = this.className.ucfirst().pluralize();
        var store = Ext.ComponentQuery.query(this.className+'listtab')[0].getViewModel().getStore(storeName);
        var tabContainer = btn.up("form").up("panel");
        var tabForm = btn.up("form").up("container");
        var form = tabForm.down("form");
        var scope =
        {
            tabContainer: tabContainer,
            tabForm: tabForm,
            store: store,
            form: form,
            closeTab: function ()
            {
                //  tabForm.setViewModel(null);
                this.tabContainer.remove(tabForm);
            }
        };
        var frm = this.getView().down('form');
        if (frm.isValid())
        {
            var model = this.getViewModel().get('currentModel');
            model.save({
                callback: function (record, operation)
                {
                    var response = Ext.decode(operation._response.responseText);
                    if (operation.wasSuccessful)
                    {
                        var msgContent = "";
                        var msgTitle = "";
                        if (this.form.mode == "edit")
                        {
                            msgContent = Resources.EditionSuccessMessage;
                            msgTitle = Resources.EditionSuccessTitle;
                        }
                        else if (this.form.mode == "create")
                        {
                            msgContent = Resources.CreationSuccessMessage;
                            msgTitle = Resources.CreationSuccessTitle;
                        }
                        Ext.Msg.alert(msgTitle, msgContent, function ()
                        {
                            this.closeTab();
                            this.store.load();
                        }, this);

                    } else
                    {
                        Ext.Msg.alert('Error', response.error.message);
                    }
                }, scope: scope
            });
        } else
        {
            Ext.Msg.alert("Error!", "the data is not valid, please review it.");
        }
    }
});
