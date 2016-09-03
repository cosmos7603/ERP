using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebErpExt5.resources;

namespace WebErpExt5
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ;
            // Get a set of resources appropriate to the culture defined by the browser
            ResourceSet Resources = Labels.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            Page.ClientScript.RegisterStartupScript(this.GetType(),
                                        "script", "<script>var Resources = {}</script>");

        }
    }
}