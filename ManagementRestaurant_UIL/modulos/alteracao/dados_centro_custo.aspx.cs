using System;
using System.Data;
using System.Web.UI;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;
using System.Web.UI.WebControls;

namespace ManagementRestaurant_UIL.modulos.alteracao
{
    public partial class dados_centro_custo : Page
    {
        private readonly FinanceiroBLL _financeiroBLL = new FinanceiroBLL();
        private  FinanceiroMDL _financeiroMDL = new FinanceiroMDL();
        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PassaInfo"] != null)
                {
                    _conexaoMDL.Ds = (DataSet)Session["PassaInfo"];

                    _funcionarioMDL = _funcionarioGLL.ValidaUsuario(_conexaoMDL);

                    if (_funcionarioMDL.NAcesso != 1)
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

        #region btnConcluir_Click

        protected void btnConcluir_Click(object sender, EventArgs e)
        {
            if (ValidaCampos())
            {
                _financeiroMDL.CCusto = Convert.ToInt64(txtCodigo.Text);
                _financeiroMDL.NCusto = txtNome.Text;

                try
                {
                    _conexaoMDL = _financeiroBLL.AtualizaCentroCusto(_financeiroMDL);
                    
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                               _conexaoMDL.Validador
                                                                   ? "<script>alert('Atualização efetuada com sucesso');location.href='../home/home.aspx';</script>"
                                                                   : "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                
                                              
            }
        }

        #endregion

        #region btnCancelar

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("dados_centro_custo.aspx");
        }

        #endregion

        #region ValidaCampos

        private Boolean ValidaCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para atualizar o cadastro corretamente');</script>");

                return false;
            }

            return true;
        }

        #endregion

        #region ddlCC_Load
        protected void ddlCC_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                try
                {
                    _conexaoMDL = _financeiroBLL.PesquisaCentroCusto();

                    ddlCC.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlCC.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlCC.DataSource = _conexaoMDL.Ds;
                    ddlCC.DataBind();

                    ddlCC.Items.Insert(0, new ListItem("Selecione...", "0"));
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }
        #endregion

        #region ddlCC_SelectedIndexChanged
        protected void ddlCC_SelectedIndexChanged(object sender, EventArgs e)
        {
             if (IsPostBack == true)
            {
                _financeiroMDL.NCusto = (ddlCC.SelectedItem).ToString();
   
                _financeiroMDL = _financeiroBLL.CarregaDadosCentroCusto(_financeiroMDL);

                txtNome.Text = _financeiroMDL.NCusto;
                txtCodigo.Text = Convert.ToString(_financeiroMDL.CCusto);
                pnCC.Visible = true;
            }
        }
        #endregion

        #region btnAlterar_Click

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            txtNome.Enabled = true;
            btnAlterar.Visible = false;
            btnConcluir.Visible = true;
        }

        #endregion
    }
}