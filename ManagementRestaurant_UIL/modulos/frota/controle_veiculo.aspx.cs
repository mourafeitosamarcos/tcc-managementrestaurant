using System;
using System.Web.UI;
using ManagementRestaurant_BLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.frota
{
    public partial class controle_veiculo : Page
    {
        private FrotaMDL _frotaMDL = new FrotaMDL();
        private readonly FrotaBLL _frotaBLL = new FrotaBLL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaVagas();
        }

        #endregion

        #region btnBuscar_Click

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlaca.Text) || ddlSentido.SelectedValue == "0")
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");
            }
            else
            {
                _frotaMDL.Placa = txtPlaca.Text;

                ddlSentido.Enabled = false;

                try
                {
                    _frotaMDL = _frotaBLL.CarregaPlacaVeiculoInterno(_frotaMDL);
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }

                if (_frotaMDL.Validador)
                {
                    CarregaInformacoesFuncionario();
                }
                else
                {
                    CarregaInformacoesCliente();
                }
            }
        }

        #endregion

        #region CarregaInformacoesFuncionario

        private void CarregaInformacoesFuncionario()
        {
            if (ddlSentido.SelectedValue == "1")
            {
                txtNome.Text = _frotaMDL.Fun_Nome;
                txtMarca.Text = _frotaMDL.Marca;
                txtTipo.Text = _frotaMDL.Tipo;
                txtModelo.Text = _frotaMDL.Modelo;

                pnFuncionario.Visible = true;

                txtPlaca.Enabled = false;

                txtEntrada.Text = Convert.ToString(DateTime.Now.TimeOfDay).Substring(0, 8);
            }
            else
            {
                txtNome.Text = _frotaMDL.Fun_Nome;
                txtMarca.Text = _frotaMDL.Marca;
                txtTipo.Text = _frotaMDL.Tipo;
                txtModelo.Text = _frotaMDL.Modelo;

                pnFuncionario.Visible = true;

                txtPlaca.Enabled = false;

                _frotaMDL = _frotaBLL.CarregaEntradaVeiculo(_frotaMDL);

                txtEntrada.Text = Convert.ToString(_frotaMDL.D_Entrada.TimeOfDay);

                if (_frotaMDL.D_Entrada.Ticks == 0)
                {
                    LimpaCampos();

                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Veiculo não tem entrada registrada');</script>");
                }
                else
                {
                    txtSaida.Text = Convert.ToString(DateTime.Now.TimeOfDay).Substring(0, 8);
                }
            }
        }

        #endregion

        #region CarregaInformacoesCliente

        private void CarregaInformacoesCliente()
        {
            if (ddlSentido.SelectedValue == "1")
            {
                pnCliente.Visible = true;

                txtPlaca.Enabled = false;

                txtEntradaExterno.Text = Convert.ToString(DateTime.Now.TimeOfDay).Substring(0, 8);
            }
            else
            {
                pnCliente.Visible = true;

                txtPlaca.Enabled = false;

                _frotaMDL = _frotaBLL.CarregaEntradaVeiculo(_frotaMDL);

                txtEntradaExterno.Text = Convert.ToString(_frotaMDL.D_Entrada.TimeOfDay);

                if (_frotaMDL.D_Entrada.Ticks == 0)
                {
                    LimpaCampos();

                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Veiculo não tem entrada registrada');</script>");
                }
                else
                {
                    txtSaidaExterno.Text = Convert.ToString(DateTime.Now.TimeOfDay).Substring(0, 8);
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

        #region btnLimparExterno_Click

        protected void btnLimparExterno_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        #endregion

        #region LimpaCampos

        private void LimpaCampos()
        {
            pnFuncionario.Visible = false;
            pnCliente.Visible = false;

            txtPlaca.Text = string.Empty;

            txtEntrada.Text = string.Empty;
            txtSaida.Text = string.Empty;

            txtEntradaExterno.Text = string.Empty;
            txtSaidaExterno.Text = string.Empty;

            txtPlaca.Enabled = true;
            ddlSentido.Enabled = true;

            ddlSentido.SelectedValue = "0";
        }

        #endregion

        #region btnAplicar_Click

        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlSentido.SelectedValue == "1" && txtSaida.Text == "")
                {
                    _frotaMDL.Placa = txtPlaca.Text;
                    _frotaMDL.D_Entrada = Convert.ToDateTime(txtEntrada.Text);

                    _conexaoMDL = _frotaBLL.CadastraHoraEntradaInternos(_frotaMDL);

                    CarregaVagas();
                }
                else
                {
                    _frotaMDL.Placa = txtPlaca.Text;
                    _frotaMDL.D_Entrada = Convert.ToDateTime(txtEntrada.Text);
                    _frotaMDL.D_Saida = Convert.ToDateTime(txtSaida.Text);

                    _conexaoMDL = _frotaBLL.CadastraHoraSaidaInternos(_frotaMDL);

                    CarregaVagas();
                }
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }

            if (_conexaoMDL.Validador == false)
            {
                _conexaoMDL.Ds.Clear();

                LimpaCampos();

                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Hora registrada com sucesso');</script>");
            }
        }

        #endregion

        #region btnAplicarExterno_Click

        protected void btnAplicarExterno_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlSentido.SelectedValue == "1" && txtSaida.Text == "")
                {
                    _frotaMDL.Placa = txtPlaca.Text;
                    _frotaMDL.D_Entrada = Convert.ToDateTime(txtEntradaExterno.Text);

                    _conexaoMDL = _frotaBLL.CadastraHoraEntradaExternos(_frotaMDL);

                    CarregaVagas();
                }
                else
                {
                    _frotaMDL.Placa = txtPlaca.Text;
                    _frotaMDL.D_Entrada = Convert.ToDateTime(txtEntradaExterno.Text);
                    _frotaMDL.D_Saida = Convert.ToDateTime(txtSaidaExterno.Text);

                    _conexaoMDL = _frotaBLL.CadastraHoraSaidaExternos(_frotaMDL);

                    CarregaVagas();
                }
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }

            if (_conexaoMDL.Validador == false)
            {
                _conexaoMDL.Ds.Clear();

                LimpaCampos();

                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Hora registrada com sucesso');</script>");
            }
        }

        #endregion

        #region CarregaVagas

        private void CarregaVagas()
        {
            _frotaMDL = _frotaBLL.CarregaVagas(_frotaMDL);

            txtVaga.Text = Convert.ToString(_frotaMDL.Vagas);
        }

        #endregion
    }
}