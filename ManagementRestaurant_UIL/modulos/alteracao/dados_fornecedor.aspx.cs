using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.alteracao
{
    public partial class dados_fornecedor : Page
    {
        private readonly ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private readonly EstoqueBLL _estoqueBLL = new EstoqueBLL();
        private readonly EstoqueGLL _estoqueGLL = new EstoqueGLL();
        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private ConexaoMDL _conexaoMDL2 = new ConexaoMDL();
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

        #region btnAlterar_Click

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            txtBairro.Enabled = true;
            txtCep.Enabled = true;
            txtCidade.Enabled = true;
            txtComplemento.Enabled = true;
            txtEmail.Enabled = true;
            txtEndereco.Enabled = true;
            txtEstado.Enabled = true;
            txtNome.Enabled = true;
            txtNumero.Enabled = true;
            txtTelefone.Enabled = true;
            btnConcluirAlteracao.Visible = true;
            btnVoltar.Visible = true;
            btnAlterar.Visible = false;
        }

        #endregion

        #region btnBuscar_Click

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                _estoqueMDL.FCep = txtCep.Text;

                _estoqueMDL = _estoqueGLL.PesquisaCep(_estoqueMDL);

                txtCidade.Text = _estoqueMDL.FCidade;
                txtEstado.Text = _estoqueMDL.FEstado;
                txtBairro.Text = _estoqueMDL.FBairro;
                txtEndereco.Text = _estoqueMDL.FRua;

                txtNumero.Focus();
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('O serviço de preenchimento automático não está disponivel no momento');</script>");

                txtEndereco.Focus();
            }
        }

        #endregion

        #region AlteraFornecedor

        private void AlteraFornecedor()
        {
            _estoqueMDL.FNome = txtNome.Text;
            _estoqueMDL.FTelefone = txtTelefone.Text;
            _estoqueMDL.FEmail = txtEmail.Text;
            _estoqueMDL.FCnpj = txtCnpj.Text;
            _estoqueMDL.FCep = txtCep.Text;
            _estoqueMDL.FRua = txtEndereco.Text;
            _estoqueMDL.FnEstabelecimento = txtNumero.Text;
            _estoqueMDL.FBairro = txtBairro.Text;
            _estoqueMDL.FCidade = txtCidade.Text;
            _estoqueMDL.FEstado = txtEstado.Text;
            _estoqueMDL.FComplemento = txtComplemento.Text;
            _estoqueMDL.FEmail = txtEmail.Text;

            try
            {
                _conexaoMDL2 = _estoqueBLL.AlteraFornecedor(_estoqueMDL);

                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Alteração efetuada com sucesso');location.href='../alteracao/dados_fornecedor.aspx';</script>");
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }
        }

        #endregion

        #region ValidaCampos

        private Boolean ValidaCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtTelefone.Text) ||
                string.IsNullOrWhiteSpace(txtCnpj.Text) ||
                string.IsNullOrWhiteSpace(txtCep.Text) || string.IsNullOrWhiteSpace(txtEndereco.Text) ||
                string.IsNullOrWhiteSpace(txtNumero.Text) || string.IsNullOrWhiteSpace(txtBairro.Text) ||
                string.IsNullOrWhiteSpace(txtCidade.Text) || string.IsNullOrWhiteSpace(txtEstado.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");

                return false;
            }

            return true;
        }

        #endregion

        #region ddlFornecedor_Load

        protected void ddlFornecedor_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ddlFornecedor.Items.Count == 0)
                {
                    try
                    {
                        _conexaoMDL2 = _estoqueBLL.CarregaFornecedor(_conexaoMDL2);

                        ddlFornecedor.DataTextField = _conexaoMDL2.Ds.Tables[0].Columns[0].ToString();
                        ddlFornecedor.DataValueField = _conexaoMDL2.Ds.Tables[0].Columns[1].ToString();
                        ddlFornecedor.DataSource = _conexaoMDL2.Ds;
                        ddlFornecedor.DataBind();

                        ddlFornecedor.Items.Insert(0, new ListItem("Selecione...", "0"));
                    }
                    catch
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                    }
                }
            }
        }

        #endregion

        #region ddlFornecedor_SelectedIndexChanged

        protected void ddlFornecedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            _estoqueMDL.FCnpj = ddlFornecedor.SelectedValue;

            _estoqueMDL = _estoqueBLL.CarregaDadosFornecedor(_estoqueMDL);

            txtNome.Text = _estoqueMDL.FNome;
            txtTelefone.Text = _estoqueMDL.FTelefone;
            txtEmail.Text = _estoqueMDL.FEmail;
            txtCnpj.Text = _estoqueMDL.FCnpj;
            txtCep.Text = _estoqueMDL.FCep;
            txtEndereco.Text = _estoqueMDL.FRua;
            txtNumero.Text = _estoqueMDL.FnEstabelecimento;
            txtBairro.Text = _estoqueMDL.FBairro;
            txtCidade.Text = _estoqueMDL.FCidade;
            txtEstado.Text = _estoqueMDL.FEstado;
            txtComplemento.Text = _estoqueMDL.FComplemento;
            txtEmail.Text = _estoqueMDL.FEmail;

            pnAlteracao.Visible = true;
            btnAlterar.Visible = true;
            btnVoltar.Visible = true;
        }

        #endregion

        #region btnConcluirAlteracao_Click

        protected void btnConcluirAlteracao_Click(object sender, EventArgs e)
        {
            {
                if (ValidaCampos())
                {
                    {
                        AlteraFornecedor();
                    }
                }
            }
        }

        #endregion
    }
}