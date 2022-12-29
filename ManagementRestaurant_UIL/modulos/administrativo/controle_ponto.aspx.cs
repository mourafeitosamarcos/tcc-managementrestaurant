using System;
using System.Web.UI;
using System.Data;

using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.administrativo
{
    public partial class controle_ponto : Page
    {
        private readonly FuncionarioBLL _funcionarioBLL = new FuncionarioBLL();
        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();
        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            _conexaoMDL.Ds = (DataSet)Session["PassaInfo"];

            _funcionarioMDL = _funcionarioGLL.ValidaUsuario(_conexaoMDL);

            if (_funcionarioMDL.N_Acesso == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "'alertscript",
                    string.Format("window.alert(\"{0}\");history.go(-{1});", "Voce não está autorizado a acessar a página", 1), true);
            }
        }

        #endregion

        #region btnBuscar_Click

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (ValidaCampos())
            {
                _funcionarioMDL.Cpf = txtCpf.Text;

                _conexaoMDL.Validador = _funcionarioGLL.ValidaCPF(txtCpf.Text);

                if (_conexaoMDL.Validador == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('CPF inválido!');</script>");
                }
                else
                {
                    try
                    {
                        _funcionarioMDL = _funcionarioBLL.CarregaInformacoesPonto(_funcionarioMDL);
                    }
                    catch
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                    }

                    if (_funcionarioMDL.Validador == false)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('CPF não consta no sistema');</script>");
                    }
                    else
                    {
                        if (_funcionarioMDL.H_Saida.DayOfYear != DateTime.Now.DayOfYear)
                        {
                            if (ddlSentido.SelectedValue == "1")
                            {
                                txtEntrada.Text = Convert.ToString(DateTime.Now.TimeOfDay).Substring(0, 8);

                                ddlSentido.Enabled = false;
                                txtCpf.Enabled = false;
                                pnCadastro.Visible = true;
                            }
                            else if (ddlSentido.SelectedValue == "2" &&
                                     _funcionarioMDL.H_Entrada.DayOfYear != DateTime.Now.DayOfYear)
                            {
                                LimpaCampos();

                                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                            "<script>alert('Não consta saida pendente para este CPF');</script>");
                            }
                            else
                            {
                                txtEntrada.Text = Convert.ToString(_funcionarioMDL.H_Entrada.TimeOfDay);
                                txtSaida.Text = Convert.ToString(DateTime.Now.TimeOfDay).Substring(0, 8);

                                ddlSentido.Enabled = false;
                                txtCpf.Enabled = false;
                                pnCadastro.Visible = true;
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                        "<script>alert('Já consta no sistema entrada e saida para o dia atual');location.href='menu_administrativo.aspx';</script>");
                        }
                    }
                }
            }
        }

        #endregion

        #region btnAplicar_Click

        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            CadastraPonto();
        }

        #endregion

        #region CadastraPonto

        private void CadastraPonto()
        {
            try
            {
                _funcionarioMDL.H_Entrada = Convert.ToDateTime(txtEntrada.Text);

                if (txtSaida.Text != string.Empty)
                    _funcionarioMDL.H_Saida = Convert.ToDateTime(txtSaida.Text);

                if (txtSaida.Text == string.Empty)
                {
                    _funcionarioMDL.Cpf = txtCpf.Text;
                    _funcionarioMDL.H_Entrada = Convert.ToDateTime(txtEntrada.Text);

                    _conexaoMDL = _funcionarioBLL.CadastraPontoEntrada(_funcionarioMDL);
                }
                else
                {
                    _funcionarioMDL.Cpf = txtCpf.Text;
                    _funcionarioMDL.H_Entrada = Convert.ToDateTime(txtEntrada.Text);
                    _funcionarioMDL.H_Saida = Convert.ToDateTime(txtSaida.Text);

                    _conexaoMDL = _funcionarioBLL.CadastraPontoSaida(_funcionarioMDL);
                }
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }

            Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                        _conexaoMDL.Validador == false
                                                            ? "<script>alert('Hora registrada com sucesso');location.href='menu_administrativo.aspx';</script>"
                                                            : "<script>alert('Ocorreu um erro, tente novamente mais tarde');</script>");
        }

        #endregion

        #region ValidaCampos

        private Boolean ValidaCampos()
        {
            if (string.IsNullOrWhiteSpace(txtCpf.Text) || ddlSentido.SelectedValue == "0")
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");

                return false;
            }

            return true;
        }

        #endregion

        #region btnLimpar_Click

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        #endregion

        #region LimpaCampos

        private void LimpaCampos()
        {
            pnCadastro.Visible = false;
            txtCpf.Enabled = true;
            txtCpf.Text = string.Empty;
            ddlSentido.Enabled = true;
            ddlSentido.SelectedValue = "0";
        }

        #endregion
    }
}