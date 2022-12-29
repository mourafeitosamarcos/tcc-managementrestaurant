using System;
using System.Web.UI;

namespace ManagementRestaurant_UIL
{
    public partial class _default : Page
    {
        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        #endregion
    }
}