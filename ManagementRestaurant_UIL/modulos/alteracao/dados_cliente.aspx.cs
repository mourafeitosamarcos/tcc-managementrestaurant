using System;
using System.Data;
using System.Web.UI;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.alteracao
{
    public partial class dados_cliente : Page
    {
        private readonly ClienteBLL _clienteBLL = new ClienteBLL();
        private readonly ClienteGLL _clienteGLL = new ClienteGLL();
        private readonly ConexaoMDL _conexaoMDL2 = new ConexaoMDL();

        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();
        private ClienteMDL _clienteMDL = new ClienteMDL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();
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
                    else
                    {
                        CarregaDadosCliente();
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
            txtNome.Enabled = true;
            txtTelefone.Enabled = true;


            txtCep.Enabled = true;
            txtEndereco.Enabled = true;
            txtNumero.Enabled = true;
            txtBairro.Enabled = true;
            txtCidade.Enabled = true;
            txtEstado.Enabled = true;
            btnBuscar.Enabled = true;
            btnConcluirAlteracao.Visible = true;
            btnAlterar.Visible = false;
            btnVoltar.Visible = false;
        }

        #endregion

        #region CarregaDadosCliente

        private void CarregaDadosCliente()
        {
            _conexaoMDL2.Ds = (DataSet) Session["PassaDadosCli"];
            _clienteMDL = _clienteGLL.CarregaDadosCliente(_conexaoMDL2);


            txtNome.Text = _clienteMDL.Nome;
            txtTelefone.Text = _clienteMDL.Telefone;
            hfTipo.Value = _clienteMDL.Tipo;
            if (_clienteMDL.Tipo == "2")
            {
                lblNome.Text = "Razão Social:";

                txtCNPJ.Text = _clienteMDL.Documento;
                txtCNPJ.Visible = true;
            }
            else
            {
                lblNome.Text = "Nome:";

                txtCPF.Text = _clienteMDL.Documento;
                txtCPF.Visible = true;
            }
            txtCep.Text = _clienteMDL.Cep;
            txtEndereco.Text = _clienteMDL.Rua;
            txtNumero.Text = _clienteMDL.NEstabelecimento;
            txtBairro.Text = _clienteMDL.Bairro;
            txtCidade.Text = _clienteMDL.Cidade;
            txtEstado.Text = _clienteMDL.Estado;
        }

        #endregion

        #region AlteraCliente

        private void AlteraCliente()
        {
            if (ValidaCampos())
            {
                _clienteMDL.Nome = txtNome.Text;
                _clienteMDL.Telefone = txtTelefone.Text;
                _clienteMDL.Tipo = hfTipo.Value;

                _clienteMDL.Documento = _clienteMDL.Tipo == "2" ? txtCNPJ.Text : txtCPF.Text;

                _clienteMDL.Cep = txtCep.Text;
                _clienteMDL.Rua = txtEndereco.Text;
                _clienteMDL.NEstabelecimento = txtNumero.Text;
                _clienteMDL.Bairro = txtBairro.Text;
                _clienteMDL.Cidade = txtCidade.Text;
                _clienteMDL.Estado = txtEstado.Text;

                try
                {
                    _conexaoMDL = _clienteBLL.AlteraCliente(_clienteMDL);
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }

                if (_conexaoMDL.Validador)
                {
                    _clienteMDL = _clienteBLL.CarregaInformacoesCliente(_clienteMDL);

                    Session["PassaDadosCli"] = _clienteMDL.Registro;

                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Alteração efetuada com sucesso');location.href='../alteracao/dados_cliente.aspx';</script>");
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Já existe documentação semelhante cadastrada na base de funcionários');</script>");
                }
            }
        }

        #endregion

        #region ValidaCampos

        private Boolean ValidaCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtTelefone.Text) ||
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

        #region btnConcluirAlteracao_Click

        protected void btnConcluirAlteracao_Click(object sender, EventArgs e)
        {
            AlteraCliente();

            _conexaoMDL2.Ds.Clear();


            Session["PassaDadosFunc"] = _clienteMDL.Registro;
        }

        #endregion

        #region btnBuscar_Click

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                _clienteMDL.Cep = txtCep.Text;

                _clienteMDL = _clienteGLL.PesquisaCep(_clienteMDL);

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
    }
}