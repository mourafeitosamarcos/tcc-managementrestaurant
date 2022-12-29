using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.home
{
    public partial class Home : Page
    {
        private readonly ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private  ConexaoMDL _conexaoMDL2 = new ConexaoMDL();

        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();
        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private readonly PedidoBLL _pedidoBLL = new PedidoBLL();

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PassaInfo"] != null)
                {
                    _conexaoMDL.Ds = (DataSet)Session["PassaInfo"];

                    _funcionarioMDL = _funcionarioGLL.ValidaUsuario(_conexaoMDL);

                    if (_funcionarioMDL.NAcesso == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "'alertscript",
                                                                string.Format(
                                                                    "window.alert(\"{0}\");history.go(-{1});",
                                                                    "Voce não está autorizado(a) a acessar a página", 1),
                                                                true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Sua sessão foi encerrada automaticamente por inatividade. Faça o login novamente para iniciar uma nova sessão');location.href='../../login.aspx';</script>");
                }
            }

            CarregaGrid();
        }

        #endregion

        #region Timer1_Tick

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        #endregion

        #region CarregaGrid

        public void CarregaGrid()
        {
            //_conexaoMDL2.Ds.Clear();
            //_conexaoMDL2 = _pedidoBLL.PesquisaIngredientes();

            //grdIngredientes.DataSource = _conexaoMDL2.Ds;
            //grdIngredientes.DataBind();

            //for (int i = 0; i < _conexaoMDL2.Ds.Tables[0].Rows.Count; i++)
            //{
            //    var lblIngrediente = (Label)grdIngredientes.Rows[i].FindControl("lblIngrediente");
            //    var lblQuantidade = (Label)grdIngredientes.Rows[i].FindControl("lblQuantidade");

            //    lblIngrediente.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Ingrediente"].ToString());
            //    lblQuantidade.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Qtde"].ToString());
            //}
        }

        #endregion
    }
}
           
            
        

        
        
        
        
        
        






