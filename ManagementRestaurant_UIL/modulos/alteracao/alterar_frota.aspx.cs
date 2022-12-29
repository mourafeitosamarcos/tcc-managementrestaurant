using System;
using System.Data;
using System.Web.UI;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.alteracao
{
    public partial class alterar_frota : Page
    {
        private readonly FrotaBLL _frotaBLL = new FrotaBLL();

        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();
        private ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private FrotaMDL _frotaMDL = new FrotaMDL();
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

        #region btnConcluir_Click

        protected void btnConcluir_Click(object sender, EventArgs e)
        {
            if (ValidaCampos())
            {
                AlteraVeiculo();
            }
        }

        #endregion

        #region AlteraVeiculo

        private void AlteraVeiculo()
        {
            _funcionarioMDL.Cpf = txtCpf.Text;
            _frotaMDL.Placa = txtPlaca.Text;
            _frotaMDL.Tipo = ddlTipo.Text;
            _frotaMDL.Marca = txtMarca.Text;
            _frotaMDL.Modelo = txtModelo.Text;

            try
            {
                _conexaoMDL = _frotaBLL.AlteraVeiculo(_frotaMDL, _funcionarioMDL);
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }

            if (_conexaoMDL.Validador)
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Alteração efetuada com sucesso');location.href='../home/home.aspx';</script>");

                btnBuscar_Click(null, null);
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
                                                                    "<script>alert('Funcionário não tem CNH cadastrada no sistema');location.href='menu_frota.aspx';</script>");
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
                    try
                    {
                        _frotaMDL.Placa = String.Empty;
                        _frotaMDL = _frotaBLL.CarregaVeiculoFuncionario(_frotaMDL, _funcionarioMDL);

                        txtMarca.Text = _frotaMDL.Marca;
                        txtModelo.Text = _frotaMDL.Modelo;
                        txtPlaca.Text = _frotaMDL.Placa;
                        ddlTipo.Text = _frotaMDL.Tipo;
                    }

                    catch
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Não consta vínculo de veículo com o CPF pesquisado!'); location.href='../operacional/alterar_frota.aspx';</script>");
                    }
                }
            }
        }

        #endregion

        #region btnAlterar_Click

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            txtMarca.Enabled = true;
            txtModelo.Enabled = true;
            txtPlaca.Enabled = true;
            btnConcluir.Visible = true;
            btnVoltar.Visible = false;
            btnAlterar.Visible = false;
            ddlTipo.Enabled = true;
        }

        #endregion
    }
}