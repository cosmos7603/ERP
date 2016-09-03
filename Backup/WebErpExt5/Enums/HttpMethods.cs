using System.ComponentModel;

namespace WebErpExt5.Enums
{
    public enum HttpMethods
    {
        [Description("GET")]
        Get,
        [Description("POST")]
        Post,
        [Description("PUT")]
        Put,
        [Description("DELETE")]
        Delete
    }
}
