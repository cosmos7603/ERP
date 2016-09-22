namespace AM.DAL.QueryResults
{
    public class CodeGetListResult
    {
        public int CodeId { get; set; }
        public int StoreId { get; set; }
        public string CodeTypeCode { get; set; }
        public bool AllowBrandLevel { get; set; }
        public string CodeName { get; set; }
        public bool Deleted { get; set; }
        public bool DefaultCode { get; set; }
        public string HoCode { get; set; }
    }
}
