using System;
using System.Web.UI;

namespace ManagementRestaurant_UIL.master
{
    public partial class login : MasterPage
    {
        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region imgLogo_Click

        protected void imgLogo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/login.aspx");
        }

        #endregion
    }
}