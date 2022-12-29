using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.alteracao
{
    public partial class lista_clientes : Page
    {
        private readonly ClienteBLL _clienteBLL = new ClienteBLL();
        private ClienteMDL _clienteMDL = new ClienteMDL();

        private readonly ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private ConexaoMDL _conexaoMDL2 = new ConexaoMDL();

        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();
        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();

        private int _linha;
        private string _coluna;
        private string _parametro;
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
        }

        #endregion

        #region btnPesquisar_Click

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            _parametro = txtPesquisa.Text;
            _tipo = ddltipo.Text;
            _coluna = ddlColuna.SelectedValue;
            CarregaGrid(_parametro, _coluna, _tipo);
        }

        #endregion

        #region btnLimpar_Click

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            Response.Redirect("lista_clientes.aspx");
        }

        #endregion

        #region CarregaGrid

        private void CarregaGrid(string parametro, string coluna, string tipo)
        {
            _conexaoMDL2.Ds.Clear();
            _conexaoMDL2 = _clienteBLL.PesquisaClientes(parametro, coluna, tipo);

            grdClientes.DataSource = _conexaoMDL2.Ds;
            grdClientes.DataBind();

            for (int i = 0; i < _conexaoMDL2.Ds.Tables[0].Rows.Count; i++)
            {
                var lbtNome = (Label) grdClientes.Rows[i].FindControl("lbtNome");

                var lbtDoc = (Label) grdClientes.Rows[i].FindControl("lbtDoc");

                var lbtCep = (Label) grdClientes.Rows[i].FindControl("lbtCEP");
                var lbtEndereco = (Label) grdClientes.Rows[i].FindControl("lbtEndereco");

                var lbtBairro = (Label) grdClientes.Rows[i].FindControl("lbtBairro");
                var lbtEmail = (Label) grdClientes.Rows[i].FindControl("lbtEmail");


                lbtNome.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Cli_Nome"].ToString());
                lbtDoc.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Doc_Cliente"].ToString());

                lbtCep.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Cli_CEP"].ToString());
                lbtEndereco.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Cli_Rua"].ToString());
                lbtBairro.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Cli_Bairro"].ToString());
                lbtEmail.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Cli_Email"].ToString());
            }
        }

        #endregion

        #region grdClientesRowCommand

        protected void grdClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                _linha = Convert.ToInt32(e.CommandArgument);

                var intDoc = (Label) (grdClientes.Rows[_linha].FindControl("lbtDoc"));
                _clienteMDL.Documento = intDoc.Text;
                _conexaoMDL2.Ds.Clear();
                _clienteMDL.Tipo = ddltipo.SelectedValue;
                _clienteMDL = _clienteBLL.CarregaInformacoesCliente(_clienteMDL);

                Session["PassaDadosCli"] = _clienteMDL.Registro;

                Response.Redirect("dados_cliente.aspx");
            }
        }

        #endregion

        #region ddltipo_SelectedIndexChanged

        protected void ddltipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdClientes.DataSource = null;
            grdClientes.DataBind();
            txtPesquisa.Text = string.Empty;

            if (ddltipo.SelectedValue == "1" || ddltipo.SelectedValue == "2")
            {
                btnLimpar.Visible = true;
                pnCadastro.Visible = true;
                _clienteMDL.Tipo = ddltipo.SelectedValue;
            }
            else
            {
                pnCadastro.Visible = false;
                btnLimpar.Visible = false;
            }
        }

        #endregion
    }
}