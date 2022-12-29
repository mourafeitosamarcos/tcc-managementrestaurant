using System;
using System.Web.UI;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.cliente
{
    public partial class cadastro_cliente : Page
    {
        private readonly ClienteBLL _clienteBLL = new ClienteBLL();
        private readonly ClienteGLL _clienteGLL = new ClienteGLL();
        private ClienteMDL _clienteMDL = new ClienteMDL();
        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

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
                if (ddlCliente.SelectedValue == "1")
                {
                    _conexaoMDL.Validador = _clienteGLL.ValidaCPF(txtCpf.Text);

                    if (_conexaoMDL.Validador == false)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('CPF inválido!');</script>");
                    }
                    else
                    {
                        CadastraCliente();
                    }
                }
                else if (ddlCliente.SelectedValue == "2")
                {
                    _conexaoMDL.Validador = _clienteGLL.ValidaCNPJ(txtCnpj.Text);

                    if (_conexaoMDL.Validador == false)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('CNPJ inválido!');</script>");
                    }
                    else
                    {
                        CadastraCliente();
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Selecione um tipo de cliente');</script>");
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
                _clienteMDL.Cep = txtCep.Text;

                _clienteMDL = _clienteGLL.PesquisaCEP(_clienteMDL);

                txtCidade.Text = _clienteMDL.Cidade;
                txtEstado.Text = _clienteMDL.Estado;
                txtBairro.Text = _clienteMDL.Bairro;
                txtEndereco.Text = _clienteMDL.Rua;

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

        #region CadastraCliente

        private void CadastraCliente()
        {
            _clienteMDL.Nome = txtNome.Text;
            _clienteMDL.Telefone = txtTelefone.Text;

            _clienteMDL.Cpf = txtCpf.Text;
            _clienteMDL.Cnpj = txtCnpj.Text;

            _clienteMDL.Email = txtEmail.Text;
            _clienteMDL.Cep = txtCep.Text;
            _clienteMDL.Rua = txtEndereco.Text;
            _clienteMDL.N_Estabelecimento = txtNumero.Text;
            _clienteMDL.Bairro = txtBairro.Text;
            _clienteMDL.Cidade = txtCidade.Text;
            _clienteMDL.Estado = txtEstado.Text;

            try
            {
                _conexaoMDL = _clienteBLL.CadastraCliente(_clienteMDL);
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }

            Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                        _conexaoMDL.Validador
                                                            ? "<script>alert('Cadastro efetuado com sucesso');location.href='menu_cliente.aspx';</script>"
                                                            : "<script>alert('CPF ou CNPJ já constam no sistema');</script>");
        }

        #endregion

        #region ValidaCampos

        private Boolean ValidaCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtTelefone.Text) ||
                string.IsNullOrWhiteSpace(txtCpf.Text) &&
                string.IsNullOrWhiteSpace(txtCnpj.Text) && string.IsNullOrWhiteSpace(txtEmail.Text) ||
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
            txtCpf.Text = string.Empty;
            txtCnpj.Text = string.Empty;
            txtEmail.Text = string.Empty;

            txtCep.Text = string.Empty;
            txtEndereco.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtCidade.Text = string.Empty;
            txtEstado.Text = string.Empty;
        }

        #endregion

        #region ddlCliente_SelectedIndexChanged

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCliente.SelectedValue == "1")
            {
                LimpaCampos();

                lblNome.Text = "Nome:";
                lblDocumento.Text = "CPF:";

                txtCpf.Visible = true;
                txtCnpj.Visible = false;

                pnCadastro.Visible = true;
            }
            else if (ddlCliente.SelectedValue == "2")
            {
                LimpaCampos();

                lblNome.Text = "Razão Social:";
                lblDocumento.Text = "CNPJ:";

                txtCnpj.Visible = true;
                txtCpf.Visible = false;

                pnCadastro.Visible = true;
            }
            else
            {
                pnCadastro.Visible = false;

                LimpaCampos();
            }
        }

        #endregion
    }
}