using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManagementRestaurant_UIL.modulos.vendas
{
    public partial class mapa_mesas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            mesa1.Attributes["class"] = "Ocupado";
            mesa50.Attributes["class"] = "Ocupado";
            mesa8.Attributes["class"] = "Ocupado";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            mesa1.Attributes["class"] = "Livre";
            mesa50.Attributes["class"] = "Livre";
            mesa8.Attributes["class"] = "Livre";
        }
    }
}