using System;
using System.Data;
using System.Web.UI;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.administrativo
{
    public partial class cadastro_fornecedor : Page
    {
        private readonly EstoqueBLL _estoqueBLL = new EstoqueBLL();
        private readonly EstoqueGLL _estoqueGLL = new EstoqueGLL();
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

        #region btnCadastrar_Click

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (ValidaCampos())
            {
                _conexaoMDL.Validador = _estoqueGLL.ValidaCnpj(txtCnpj.Text);

                if (_conexaoMDL.Validador == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('CNPJ inválido!');</script>");
                }
                else
                {
                    CadastraFornecedor();
                }
            }
        }

        #endregion

        #region btnLimpar_Click

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
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

        #region CadastraFornecedor

        private void CadastraFornecedor()
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
                _conexaoMDL = _estoqueBLL.CadastraFornecedor(_estoqueMDL);
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }

            Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                        _conexaoMDL.Validador
                                                            ? "<script>alert('Cadastro efetuado com sucesso');location.href='../home/home.aspx';</script>"
                                                            : "<script>alert('CNPJ já consta no sistema');</script>");
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

        #region LimpaCampos

        private void LimpaCampos()
        {
            txtNome.Text = string.Empty;
            txtTelefone.Text = string.Empty;
            txtCnpj.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtCep.Text = string.Empty;
            txtEndereco.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtCidade.Text = string.Empty;
            txtEstado.Text = string.Empty;
            txtComplemento.Text = string.Empty;
        }

        #endregion
    }
}