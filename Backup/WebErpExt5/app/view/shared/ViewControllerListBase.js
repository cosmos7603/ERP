Ext.define('ERP.view.shared.ViewControllerListBase', {
    extend: 'Ext.app.ViewController',
    className: '',
    alias: 'controller.viewcontrollerlistbase',

    getNewEmptyModel: function (viewModel)
    {
        return viewModel.data[this.getStoreNameFromClassName(this.className)].model.create();
    },
    addNewModelToStore: function (store, model)
    {
        store.add(model);
    },

    getStoreNameFromClassName: function(className) {
        return className.ucfirst().pluralize();
    },

    getNewForm: function (mode)
    {
        var form = Ext.create('ERP.view.' + this.className + '.' + this.className.ucfirst() + 'Form', {
            modelValidation: true,
            mode: mode
            //layout: { type: 'column' }
        });
        return form;
    },

    getNewFormTab: function (tabId, title)
    {


        var tab = Ext.create('ERP.view.' + this.className + '.' + this.className.ucfirst() + 'FormTab', {
            id: tabId,
            //  items: items,
            //  viewModel: createFormViewModel,
            // iconCls: record.get('iconCls'),
            title: title

        });
        return tab;
    },
    addFormToTab: function (form, tab)
    {
        var items = [];
        items.push(form);
        tab.add(items);
    },
    loadDataToForm: function (tab, form, model)
    {
        tab.getViewModel().set('currentModel', model);
        form.loadRecord(model);
    },
    getSelectedModelFromGrid: function ()
    {
        var tabGrid = this.getView();
        var grid = tabGrid.down("grid");
        var model = grid.selection;
        return model;
    },
    addTabToTabContainer: function (tab)
    {
        var tabContainer = Ext.ComponentQuery.query("tabcontainer")[0];
        tabContainer.add(tab);
    },
    showTab: function (tab)
    {
        tab.show();
    }
    ,

    openFormCreate: function ()
    {

        var tabContainer = Ext.ComponentQuery.query("tabcontainer")[0];
        var tabId = 'tab-' + this.className + '-create';
        var formCreateTab = tabContainer.items.get(tabId);
        if (!formCreateTab)
        {
            var title = this.className + ' - Create';
            var form = this.getNewForm("create");
            formCreateTab = this.getNewFormTab(tabId, title);
            this.addFormToTab(form, formCreateTab);
            var model = this.getNewEmptyModel(formCreateTab.getViewModel());
            this.addNewModelToStore(formCreateTab.viewModel.data[this.getStoreNameFromClassName(this.className)], model);
            this.loadDataToForm(formCreateTab, form, model);
            this.addTabToTabContainer(formCreateTab);
            this.showTab(formCreateTab);
        } else
        {
            this.showTab(formCreateTab);
        }
    },
    openFormEdit: function ()
    {
        var tabContainer = Ext.ComponentQuery.query("tabcontainer")[0];
        var tabGrid = this.getView();
        var grid = tabGrid.down("grid");
        var tabId = 'tab-' + this.className + '-edit' + '-' + grid.selection.data.Id;
        var formEditTab = tabContainer.items.get(tabId);
        if (!formEditTab)
        {
            var title = this.className + ' - Edit - ' + grid.selection.data.Id;
            var form = this.getNewForm("edit");
            formEditTab = this.getNewFormTab(tabId, title);
            this.addFormToTab(form, formEditTab);
            var model = this.getSelectedModelFromGrid();
            this.loadDataToForm(formEditTab, form, model);
            this.addTabToTabContainer(formEditTab);
            this.showTab(formEditTab);
        } else
        {
            this.showTab(formEditTab);
        }
    },
    onCreate: function ()
    {
        this.openFormCreate();
    },

    onEdit: function ()
    {
        this.openFormEdit();
    },
    onDelete: function (btn, e)
    {
        var title = Resources.DeleteConfirmTitle;
        var content = Resources.DeleteConfirmContent;
        Ext.MessageBox.show({
            title: title,
            msg: content,
            buttons: Ext.MessageBox.YESNOCANCEL,
            scope: btn,
            fn: function (choise)
            {
                if (choise == "yes")
                {
                    var grid = btn.up('gridpanel');
                    var store = grid.getStore();
                    var record = grid.getSelection();
                    store.remove(record);
                    store.save();
                }
            },
            animateTarget: 'mb4',
            icon: Ext.MessageBox.QUESTION
        });

    }

    //onDeleteItem: function (grid, rowIndex) {

    //    var rec = grid.getStore().getAt(rowIndex);
    //    if (rec)
    //    {
    //        grid.getStore().remove(rec);
    //        grid.getStore().sync();
    //    }
    //}
});
