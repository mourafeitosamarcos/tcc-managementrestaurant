using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.alteracao
{
    public partial class lista_inativos : Page
    {
        private readonly ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private readonly FuncionarioBLL _funcionarioBLL = new FuncionarioBLL();
        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private ConexaoMDL _conexaoMDL2 = new ConexaoMDL();
        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();

        private int _linha;
        private string _coluna;
        private string _parametro;

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
            _coluna = ddlColuna.SelectedValue;
            CarregaGridInativos(_parametro, _coluna);
        }

        #endregion

        #region btnLimpar_Click

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            Response.Redirect("lista_inativos.aspx");
        }

        #endregion

        #region CarregaGridInativos

        private void CarregaGridInativos(string parametro, string coluna)
        {
            _conexaoMDL2.Ds.Clear();
            _conexaoMDL2 = _funcionarioBLL.PesquisaInativos(parametro, coluna);

            grdFuncionarios.DataSource = _conexaoMDL2.Ds;
            grdFuncionarios.DataBind();

            for (int i = 0; i < _conexaoMDL2.Ds.Tables[0].Rows.Count; i++)
            {
                var lbtNome = (Label) grdFuncionarios.Rows[i].FindControl("lbtNome");
                var lbtCargo = (Label) grdFuncionarios.Rows[i].FindControl("lbtCargo");
                var lbtCpf = (Label) grdFuncionarios.Rows[i].FindControl("lbtCpf");
                var lbtRg = (Label) grdFuncionarios.Rows[i].FindControl("lbtRG");

                var lbtCep = (Label) grdFuncionarios.Rows[i].FindControl("lbtCEP");
                var lbtEndereco = (Label) grdFuncionarios.Rows[i].FindControl("lbtEndereco");

                var lbtBairro = (Label) grdFuncionarios.Rows[i].FindControl("lbtBairro");
                var lbtDtAdm = (Label) grdFuncionarios.Rows[i].FindControl("lbtDtAdm");


                lbtNome.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Fun_Nome"].ToString());
                lbtCargo.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Cargo_Nome"].ToString());
                lbtCpf.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Fun_CPF"].ToString());
                lbtRg.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Fun_RG"].ToString());
                lbtCep.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Fun_CEP"].ToString());
                lbtEndereco.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Fun_Rua"].ToString());
                lbtBairro.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Fun_Bairro"].ToString());
                lbtDtAdm.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Admissao_Data"].ToString());
            }
        }

        #endregion

        #region grdFuncionariosRowCommand

        protected void grdFuncionarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Habilitar")
            {
                _linha = Convert.ToInt32(e.CommandArgument);

                var intCpf = (Label) (grdFuncionarios.Rows[_linha].FindControl("lbtCpf"));

                _funcionarioMDL.Cpf = intCpf.Text;

                _conexaoMDL2 = _funcionarioBLL.HabilitaFuncionario(_funcionarioMDL);

                CarregaGridInativos(_parametro, _coluna);

                _conexaoMDL.Ds = (DataSet) Session["PassaInfo"];

                const int i = 2;
                string j = intCpf.Text;

                _funcionarioBLL.RegistraLog(_conexaoMDL, i, j);

                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Conta habilitada com sucesso');</script>");
            }
            else if (e.CommandName == "Ver")
            {
                _linha = Convert.ToInt32(e.CommandArgument);

                var intCpf = (Label) (grdFuncionarios.Rows[_linha].FindControl("lbtCpf"));

                _funcionarioMDL.Cpf = intCpf.Text;
                _conexaoMDL2.Ds.Clear();

                _funcionarioMDL = _funcionarioBLL.CarregaInformacoesFuncionario(_funcionarioMDL);

                Session["PassaDadosFunc"] = _funcionarioMDL.Registro;
                Response.Redirect("dados_funcionario.aspx");
            }
        }

        #endregion
    }
}