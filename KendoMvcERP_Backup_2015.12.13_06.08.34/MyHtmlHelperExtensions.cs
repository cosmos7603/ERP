using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;

namespace KendoMvcERP
{
    public static class MyHtmlHelperExtensions
    {
        public static Kendo.Mvc.UI.Fluent.GridBuilder<T> MyGrid<T>(this HtmlHelper helper, string name, string className)
            where T : class
        {

            //System.Reflection.Assembly assembly;
            //assembly = System.Reflection.Assembly.Load("ResourceFiles");

            //        System.Resources.ResourceManager myManager = new
            //System.Resources.ResourceManager("ResourceNamespace.myResources", assembly);
            //        string title = myManager.GetString()

            //        @Resources.Product.NewProduct
            //var resourceManager = new ResourceManager(className, assembly);
            //string title = resourceManager.GetString("New" + className);

            
            return helper.Kendo().Grid<T>()
                
                //.Editable(editable => editable.Mode(GridEditMode.PopUp).TemplateName(className + "Editor").Window(window => window.Width(600).Resizable().Scrollable(true).Title(title)))
                .Name(name)
                .Groupable()
                .Pageable()
                .Sortable()
                .Scrollable()
                .Filterable(ftb => ftb.Mode(GridFilterMode.Menu))
        .HtmlAttributes(new { style = "height:550px;" })
        .DataSource(dataSource => dataSource
        .Ajax()
        .Events(events => events.Error("error_handler").Sync("sync_handler"))
        .PageSize(20)
        .Model(model =>
        {
            model.Id("Id");
        }
            )
        .Read(read => read.Action("Get", className))
        .Update(update => update.Action("Update", className))
        .Destroy(update => update.Action("Delete", className))
        .Create(update => update.Action("Add", className))
        )
                .Pageable();
        }

        
    }

}