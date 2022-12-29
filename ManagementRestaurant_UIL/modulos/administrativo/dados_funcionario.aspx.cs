using System;
using System.Data;
using System.Web.UI;

using ManagementRestaurant_BLL;
using ManagementRestaurant_MDL;
using ManagementRestaurant_GLL;

namespace ManagementRestaurant_UIL.modulos.administrativo
{
    public partial class dados_funcionario : Page
    {
        private ConexaoMDL _conexaoMDL3 = new ConexaoMDL();
        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        private FuncionarioBLL _funcionarioBLL = new FuncionarioBLL();
        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();
        private FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();
        private FuncionarioMDL _funcionarioMDL2 = new FuncionarioMDL();

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _conexaoMDL.Ds = (DataSet)Session["PassaInfo"];

                _funcionarioMDL = _funcionarioGLL.ValidaUsuario(_conexaoMDL);

                if (_funcionarioMDL.N_Acesso != 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "'alertscript",
                        string.Format("window.alert(\"{0}\");history.go(-{1});", "Voce não está autorizado a acessar a página", 1), true);
                }
                else
                {
                    CarregaDadosFuncionario();
                }
            }
        }

        #endregion

        #region btnAlterar_Click

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            txtNome.Enabled = true;
            txtTelefone.Enabled = true;

            if (txtCnh.Text == string.Empty)
            {
                txtCnh.Enabled = true;
            }
        
            txtCep.Enabled = true;
            txtEndereco.Enabled = true;
            txtNumero.Enabled = true;
            txtBairro.Enabled = true;
            txtCidade.Enabled = true;
            txtEstado.Enabled = true;
            ddlCargo.Enabled = true;
            btnConcluirAlteracao.Visible = true;
            btnCancelar.Visible = true;
            btnAlterar.Visible = false;
            btnBack.Visible = false;
        }

        #endregion

        #region CarregaDadosFuncionario

        private void CarregaDadosFuncionario()
        {
            _conexaoMDL3.Ds = (DataSet) Session["PassaDadosFunc"];

            _funcionarioMDL = _funcionarioGLL.CarregaDadosFuncionario(_conexaoMDL3);
            
            txtNome.Text = _funcionarioMDL.Nome;
            txtTelefone.Text = _funcionarioMDL.Telefone;
            txtRg.Text = _funcionarioMDL.Rg;
            txtCpf.Text = _funcionarioMDL.Cpf;
            txtCnh.Text = _funcionarioMDL.Cnh;
            txtN_Ctrabalho.Text = _funcionarioMDL.C_Trabalho;
            txtN_Ctrabalho2.Text = _funcionarioMDL.C_Trabalho2;
            txtCep.Text = _funcionarioMDL.Cep;
            txtEndereco.Text = _funcionarioMDL.Rua;
            txtNumero.Text = _funcionarioMDL.N_Estabelecimento;
            txtBairro.Text = _funcionarioMDL.Bairro;
            txtCidade.Text = _funcionarioMDL.Cidade;
            txtEstado.Text = _funcionarioMDL.Estado;
            ddlCargo.Text = _funcionarioMDL.Cargo;
        }

        #endregion

        #region AlteraFuncionario

        private void AlteraFuncionario()
        {
            _funcionarioMDL.Nome = txtNome.Text;
            _funcionarioMDL.Telefone = txtTelefone.Text;
            _funcionarioMDL.Rg = txtRg.Text;
            _funcionarioMDL.Cpf = txtCpf.Text;
            _funcionarioMDL.Cnh = txtCnh.Text;
            _funcionarioMDL.C_Trabalho = (txtN_Ctrabalho.Text + " / " + txtN_Ctrabalho2.Text);
            _funcionarioMDL.Cep = txtCep.Text;
            _funcionarioMDL.Rua = txtEndereco.Text;
            _funcionarioMDL.N_Estabelecimento = txtNumero.Text;
            _funcionarioMDL.Bairro = txtBairro.Text;
            _funcionarioMDL.Cidade = txtCidade.Text;
            _funcionarioMDL.Estado = txtEstado.Text;
            _funcionarioMDL.Cargo = ddlCargo.Text;

            try
            {
                _conexaoMDL = _funcionarioBLL.AlteraFuncionario(_funcionarioMDL);
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }

            if (_conexaoMDL.Validador)
            {
                var i = 1;

                _conexaoMDL.Ds = (DataSet)Session["PassaInfo"];

                _funcionarioBLL.RegistraLog(_conexaoMDL, i);

                ScriptManager.RegisterClientScriptBlock(this, GetType(), "'alertscript",
                    string.Format("window.alert(\"{0}\");history.go(-{1});", "Alteração efetuada com sucesso", 3), true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Já existe documentação semelhante cadastrada na base de funcionários');</script>");
            }
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
                string.IsNullOrWhiteSpace(txtN_Ctrabalho.Text) || string.IsNullOrWhiteSpace(txtN_Ctrabalho2.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");

                return false;
            }

            return true;
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

                    ddlCargo.DataSource = _conexaoMDL.Ds.Tables[0];
                    ddlCargo.DataTextField = "Cargo_Nome";
                    ddlCargo.DataBind();
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion

        #region btnConcluirAlteracao_Click

        protected void btnConcluirAlteracao_Click(object sender, EventArgs e)
        {
            AlteraFuncionario();

            _conexaoMDL3.Ds.Clear();

            _funcionarioMDL2 = _funcionarioBLL.CarregaInformacoesFuncionario(_funcionarioMDL);

            Session["PassaDadosFunc"] = _funcionarioMDL2.Registro;
        }

        #endregion

        #region btnCancelar_Click

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("dados_funcionario.aspx");
        }

        #endregion

        #region BtnBack_Click

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            _conexaoMDL3.Ds = (DataSet)Session["PassaDadosFunc"];

            _conexaoMDL3 = _funcionarioGLL.VoltaLista(_conexaoMDL3);

            if (_conexaoMDL3.Validador == true)
            {
                Response.Redirect("lista_ativos.aspx");
            }
            else
            {
                Response.Redirect("lista_inativos.aspx");
            }
        }

        #endregion
    }
}