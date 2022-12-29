using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.administrativo
{
    public partial class cadastro_produto : Page
    {
        private readonly EstoqueBLL _estoqueBLL = new EstoqueBLL();
        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private EstoqueMDL _estoqueMDL = new EstoqueMDL();
        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PassaInfo"] != null)
                {
                    _conexaoMDL.Ds = (DataSet) Session["PassaInfo"];

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

        #region ddlTipoProduto_Load

        protected void ddlTipoProduto_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    _conexaoMDL = _estoqueBLL.CarregaTipoProduto(_conexaoMDL);

                    ddlTipoProduto.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlTipoProduto.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlTipoProduto.DataSource = _conexaoMDL.Ds;
                    ddlTipoProduto.DataBind();

                    ddlTipoProduto.Items.Insert(0, new ListItem("Selecione...", "0"));
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion

        #region btnCadastrar_Click

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (ValidaCampos())
            {
                _estoqueMDL.NProduto = txtNome.Text;
                _estoqueMDL.Produto = ddlTipoProduto.Text;

                try
                {
                    _estoqueMDL = _estoqueBLL.CadastraProduto(_estoqueMDL);
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }

                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            _estoqueMDL.Validador
                                                                ? "<script>alert('Cadastro efetuado com sucesso. O codigo do produto é: " +
                                                                  _estoqueMDL.CProduto +
                                                                  "');location.href='../home/home.aspx';</script>"
                                                                : "<script>alert('Produto já consta no sistema');</script>");
            }
        }

        #endregion

        #region ValidaCampos

        private Boolean ValidaCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || ddlTipoProduto.SelectedValue == "0")
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");

                return false;
            }

            return true;
        }

        #endregion

        #region LimpaCampos

        private void LimpaCampos()
        {
            txtNome.Text = string.Empty;
            ddlTipoProduto.SelectedValue = "0";
        }

        #endregion

        #region btnLimpar_Click

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        #endregion
    }
}