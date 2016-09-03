using System.ComponentModel;

namespace WebERP.Enums
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
