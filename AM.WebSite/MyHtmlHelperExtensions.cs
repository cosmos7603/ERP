using System.Web.Mvc;
using Kendo.Mvc.UI;

namespace AM.WebSite
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
				.Selectable(selectable => selectable
					.Mode(GridSelectionMode.Single))
				.Navigatable()
				.Events(events =>
						{
							events.Change("onRowSelected");
							events.DataBound("onGridLoad");
						}
				)
				.Filterable(ftb => ftb.Mode(GridFilterMode.Menu))
				.HtmlAttributes(new {style = "height:550px;"})
				.DataSource(dataSource => dataSource
					.WebApi()
					.Events(events => events.Error("error_handler").Sync("sync_handler"))
					.PageSize(10)
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
				.Resizable(resize => resize.Columns(true))
				.Reorderable(reorder => reorder.Columns(true))
				.Pageable(pageable => pageable
					.Refresh(true)
					.PageSizes(true)
					.ButtonCount(5))
				.Groupable();


		}


	}

}