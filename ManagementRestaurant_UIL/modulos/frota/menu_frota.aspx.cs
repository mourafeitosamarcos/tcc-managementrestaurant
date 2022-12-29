using System;
using System.Web.UI;
using System.Data;

using ManagementRestaurant_MDL;
using ManagementRestaurant_GLL;

namespace ManagementRestaurant_UIL.modulos.frota
{
    public partial class menu_frota : Page
    {
        FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();
        FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            _conexaoMDL.Ds = (DataSet)Session["PassaInfo"];

            _funcionarioMDL = _funcionarioGLL.ValidaUsuario(_conexaoMDL);

            if (_funcionarioMDL.N_Acesso == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "'alertscript",
                    string.Format("window.alert(\"{0}\");history.go(-{1});", "Voce não está autorizado a acessar a página", 1), true);
            }
        }

        #endregion
    }
}