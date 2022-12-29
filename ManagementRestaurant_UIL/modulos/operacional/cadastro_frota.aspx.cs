using System;
using System.Data;
using System.Web.UI;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.operacional
{
    public partial class cadastro_frota : Page
    {
        private readonly FrotaBLL _frotaBLL = new FrotaBLL();
        private readonly FrotaMDL _frotaMDL = new FrotaMDL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();
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

                    if (_funcionarioMDL.NAcesso != 1 && _funcionarioMDL.NAcesso != 2 && _funcionarioMDL.NAcesso != 5)
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
                CadastraVeiculo();
            }
        }

        #endregion

        #region btnLimpar_Click

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        #endregion

        #region CadastraVeiculo

        private void CadastraVeiculo()
        {
            _funcionarioMDL.Cpf = txtCpf.Text;
            _frotaMDL.Placa = txtPlaca.Text;
            _frotaMDL.Tipo = ddlTipo.Text;
            _frotaMDL.Marca = txtMarca.Text;
            _frotaMDL.Modelo = txtModelo.Text;

            try
            {
                _conexaoMDL = _frotaBLL.CadastraVeiculo(_frotaMDL, _funcionarioMDL);
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }

            if (_conexaoMDL.Validador)
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Cadastro efetuado com sucesso');location.href='../home/home.aspx';</script>");
            }
            else if (_conexaoMDL.Validador2 == false)
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Veiculo já está cadastrado no sistema');</script>");
            }
            else if (_conexaoMDL.Validador == false)
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Funcionário já tem veiculo cadastrado no sistema');</script>");
            }
        }

        #endregion

        #region ValidaCampos

        private Boolean ValidaCampos()
        {
            if (ddlTipo.SelectedValue == "0" || string.IsNullOrWhiteSpace(txtPlaca.Text) ||
                string.IsNullOrWhiteSpace(txtModelo.Text))
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
            txtPlaca.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtModelo.Text = string.Empty;

            ddlTipo.SelectedValue = "0";

            txtCpf.Text = string.Empty;
            txtCpf.Enabled = true;
            pnCadastro.Visible = false;
        }

        #endregion

        #region btnBuscar_Click

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCpf.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Digite o CPF para consulta');</script>");
            }
            else
            {
                _funcionarioMDL.Cpf = txtCpf.Text;

                _conexaoMDL.Validador = _funcionarioGLL.ValidaCpf(txtCpf.Text);

                if (_conexaoMDL.Validador == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('CPF inválido!');</script>");
                }
                else
                {
                    _funcionarioMDL.Cpf = txtCpf.Text;

                    try
                    {
                        _funcionarioMDL = _frotaBLL.CarregaInformacoesFuncionario(_funcionarioMDL);
                    }
                    catch
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                    }

                    if (_funcionarioMDL.Cnh == "")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Funcionário não tem CNH cadastrada no sistema');location.href='../home/home.aspx';</script>");
                    }
                    else if (_funcionarioMDL.Validador == false)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('CPF não consta no sistema');</script>");
                    }
                    else
                    {
                        txtNome.Text = _funcionarioMDL.Nome;
                        txtTelefone.Text = _funcionarioMDL.Telefone;
                        txtCnh.Text = _funcionarioMDL.Cnh;

                        pnCadastro.Visible = true;

                        txtCpf.Enabled = false;
                    }
                }
            }
        }

        #endregion
    }
}