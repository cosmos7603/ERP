namespace AM.WebSite.Controls.SearchHeader.Models
{
	public class SearchHeaderModel
	{
		public string ID { get; set; }
		public string Title { get; set; }

		public SearchHeaderModel()
		{
			ID = "SearchHeader";
			Title = "Search";
		}

		public SearchHeaderModel(string title)
		{
			ID = "SearchHeader";
			Title = title;
		}
	}
}