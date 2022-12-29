using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL
{
    public partial class cadastro_eati : Page
    {
        private readonly FuncionarioBLL _funcionarioBLL = new FuncionarioBLL();
        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region btnCadastrar_Click

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (ValidaCampos())
            {
                _conexaoMDL.Validador = _funcionarioGLL.ValidaCpf(txtCpf.Text);

                if (_conexaoMDL.Validador == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('CPF inválido!');</script>");
                }
                else
                {
                    CadastraFuncionario();
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
                _funcionarioMDL.Cep = txtCep.Text;

                _funcionarioMDL = _funcionarioGLL.PesquisaCep(_funcionarioMDL);

                txtCidade.Text = _funcionarioMDL.Cidade;
                txtEstado.Text = _funcionarioMDL.Estado;
                txtBairro.Text = _funcionarioMDL.Bairro;
                txtEndereco.Text = _funcionarioMDL.Rua;

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

        #region CadastraFuncionario

        private void CadastraFuncionario()
        {
            _funcionarioMDL.Nome = txtNome.Text;
            _funcionarioMDL.Telefone = txtTelefone.Text;
            _funcionarioMDL.Rg = txtRg.Text;
            _funcionarioMDL.Cpf = txtCpf.Text;
            _funcionarioMDL.Cnh = txtCnh.Text;
            _funcionarioMDL.Email = txtEmail.Text;

            _funcionarioMDL.CTrabalho = (txtN_Ctrabalho.Text + " / " + txtN_Ctrabalho2.Text);
            _funcionarioMDL.Cep = txtCep.Text;
            _funcionarioMDL.Rua = txtEndereco.Text;
            _funcionarioMDL.NEstabelecimento = txtNumero.Text;
            _funcionarioMDL.Bairro = txtBairro.Text;
            _funcionarioMDL.Cidade = txtCidade.Text;
            _funcionarioMDL.Estado = txtEstado.Text;

            _funcionarioMDL.Cargo = ddlCargo.Text;

            try
            {
                _conexaoMDL = _funcionarioBLL.CadastraFuncionario(_funcionarioMDL);
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }

            Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                        _conexaoMDL.Validador
                                                            ? "<script>alert('Cadastro efetuado com sucesso. Utilize o CEP para primeiro acesso ao sistema');</script>"
                                                            : "<script>alert('Já existe documentação semelhante cadastrada na base de funcionários');</script>");

            Page.ClientScript.RegisterStartupScript(GetType(), "0", "<script>window.close();</script>"); 
        }

        #endregion

        #region ValidaCampos

        private Boolean ValidaCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtTelefone.Text) ||
                string.IsNullOrWhiteSpace(txtCpf.Text) || string.IsNullOrWhiteSpace(txtRg.Text) ||
                string.IsNullOrWhiteSpace(txtCep.Text) || string.IsNullOrWhiteSpace(txtEndereco.Text) ||
                string.IsNullOrWhiteSpace(txtNumero.Text) || string.IsNullOrWhiteSpace(txtBairro.Text) ||
                string.IsNullOrWhiteSpace(txtCidade.Text) || string.IsNullOrWhiteSpace(txtEstado.Text) ||
                string.IsNullOrWhiteSpace(txtN_Ctrabalho.Text) || string.IsNullOrWhiteSpace(txtN_Ctrabalho2.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) || ddlCargo.SelectedValue == "0")
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
            txtRg.Text = string.Empty;
            txtCpf.Text = string.Empty;
            txtCnh.Text = string.Empty;
            txtN_Ctrabalho.Text = string.Empty;

            txtCep.Text = string.Empty;
            txtEndereco.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtCidade.Text = string.Empty;
            txtEstado.Text = string.Empty;
        }

        #endregion

        #region ddlCargo_Load

        protected void ddlCargo_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    _conexaoMDL = _funcionarioBLL.CarregaNomeCargo(_conexaoMDL);

                    ddlCargo.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlCargo.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlCargo.DataSource = _conexaoMDL.Ds;
                    ddlCargo.DataBind();

                    ddlCargo.Items.Insert(0, new ListItem("Selecione...", "0"));
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion

        #region btnCancelar_Click

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "0", "<script>window.close();</script>");
        }

        #endregion
    }
}