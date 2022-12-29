using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.vendas
{
    public partial class lista_pedidos : Page
    {
        private readonly ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private ConexaoMDL _conexaoMDL2 = new ConexaoMDL();

        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();
        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();

        private readonly PedidoBLL _pedidoBLL = new PedidoBLL();
        private PedidoMDL _pedidoMDL = new PedidoMDL();

        private int _linha;
        private string _tipo;

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PassaInfo"] != null)
                {
                    _conexaoMDL.Ds = (DataSet) Session["PassaInfo"];

                    _funcionarioMDL = _funcionarioGLL.ValidaUsuario(_conexaoMDL);

                    if (_funcionarioMDL.NAcesso != 1 && _funcionarioMDL.NAcesso != 2)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "'alertscript",
                                                                string.Format(
                                                                    "window.alert(\"{0}\");history.go(-{1});",
                                                                    "Voce não está autorizado a acessar a página", 1),
                                                                true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Sua sessão foi encerrada automaticamente por inatividade. Faça o login novamente para iniciar uma nova sessão');location.href='../../login.aspx';</script>");
                }
            }
        }

        #endregion

        #region CarregaGrid

        private void CarregaGrid(string tipo)
        {
            if (ddltipo.SelectedValue == "1")
            {
                _conexaoMDL2.Ds.Clear();
                _conexaoMDL2 = _pedidoBLL.PesquisaPedidos(tipo);

                grdPedidosFisico.DataSource = _conexaoMDL2.Ds;
                grdPedidosFisico.DataBind();

                for (int i = 0; i < _conexaoMDL2.Ds.Tables[0].Rows.Count; i++)
                {
                    var lblNome = (Label) grdPedidosFisico.Rows[i].FindControl("lblNome");
                    var lblNPedido = (Label) grdPedidosFisico.Rows[i].FindControl("lblNPedido");


                    lblNome.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Cli_Nome"].ToString());
                    lblNPedido.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Numero_Pedido"].ToString());
                }
                grdPedidosFisico.Visible = true;
                pGridPesquisaFisico.Visible = true;
            }
            else if (ddltipo.SelectedValue == "2")
            {
                _conexaoMDL2.Ds.Clear();
                _conexaoMDL2 = _pedidoBLL.PesquisaPedidos(tipo);
                grdPedidosJuridico.DataSource = _conexaoMDL2.Ds;
                grdPedidosJuridico.DataBind();

                for (int i = 0; i < _conexaoMDL2.Ds.Tables[0].Rows.Count; i++)
                {
                    var lblNomeJ = (Label) grdPedidosJuridico.Rows[i].FindControl("lblNomeJ");
                    var lblNPedidoJuridico = (Label) grdPedidosJuridico.Rows[i].FindControl("lblNPedidoJuridico");
                    //    var lblDocJ = (Label)grdPedidosJuridico.Rows[i].FindControl("lblDocJ");
                    //var lblDtPedido = (Label)grdPedidosJuridico.Rows[i].FindControl("lblDtPedido");
                    var lblPrevisao = (Label) grdPedidosJuridico.Rows[i].FindControl("lblPrevisao");


                    lblNomeJ.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[0]["Cli_Nome"].ToString());
                    lblNPedidoJuridico.Text =
                        Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Numero_Pedido"].ToString());

                    lblPrevisao.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Previsao_Entrega"].ToString());
                }
                grdPedidosJuridico.Visible = true;
                pGridPesquisaJuridico.Visible = true;
            }
        }

        #endregion

        #region grdPedidosFisicoRowCommand

        protected void grdPedidosFisico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                _linha = Convert.ToInt32(e.CommandArgument);

                var intDoc = (Label) (grdPedidosFisico.Rows[_linha].FindControl("lblNPedido"));
                string nPedido = intDoc.Text;
                _conexaoMDL2.Ds.Clear();
                _pedidoMDL.Tipo = ddltipo.SelectedValue;
                _pedidoMDL = _pedidoBLL.CarregaInformacoesPedido(nPedido);

                Session["PassaDadosPed"] = _pedidoMDL.Registro;

                Response.Redirect("dados_pedido.aspx");
            }
        }

        #endregion

        #region grdPedidosJuridicoRowCommand

        protected void grdPedidosJuridico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                _linha = Convert.ToInt32(e.CommandArgument);

                var intDoc = (Label) (grdPedidosJuridico.Rows[_linha].FindControl("lblNPedidoJuridico"));
                string nPedido = intDoc.Text;
                _conexaoMDL2.Ds.Clear();
                _pedidoMDL.Tipo = ddltipo.SelectedValue;
                _pedidoMDL = _pedidoBLL.CarregaInformacoesPedido(nPedido);

                Session["PassaDadosPed"] = _pedidoMDL.Registro;

                Response.Redirect("dados_pedido.aspx");
            }
        }

        #endregion

        #region ddltipo_SelectedIndexChanged

        protected void ddltipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                grdPedidosFisico.DataSource = null;
                grdPedidosJuridico.DataSource = null;
                grdPedidosFisico.DataBind();
                grdPedidosJuridico.DataBind();
            }

            if (ddltipo.SelectedValue == "1")
            {
                _tipo = ddltipo.SelectedValue;
                CarregaGrid(_tipo);

                pGridPesquisaJuridico.Visible = false;
            }
            else if (ddltipo.SelectedValue == "2")
            {
                _tipo = ddltipo.SelectedValue;
                CarregaGrid(_tipo);

                pGridPesquisaFisico.Visible = false;
            }
            else
            {
                pGridPesquisaFisico.Visible = false;
                pGridPesquisaJuridico.Visible = false;
            }
        }

        #endregion
    }
}