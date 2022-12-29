using System;
using System.Data;
using System.Web.UI;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.operacional
{
    public partial class controle_veiculo : Page
    {
        private readonly FrotaBLL _frotaBLL = new FrotaBLL();
        private FrotaMDL _frotaMDL = new FrotaMDL();

        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();
        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PassaInfo"] != null)
                {
                    _conexaoMDL.Ds = (DataSet) Session["PassaInfo"];

                    _funcionarioMDL = _funcionarioGLL.ValidaUsuario(_conexaoMDL);

                    if (_funcionarioMDL.NAcesso != 1 && _funcionarioMDL.NAcesso != 5)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "'alertscript",
                                                                string.Format(
                                                                    "window.alert(\"{0}\");history.go(-{1});",
                                                                    "Voce não está autorizado(a) a acessar a página", 1),
                                                                true);
                    }
                    else
                    {
                        CarregaVagas();
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

        #region btnBuscar_Click

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlaca.Text) && string.IsNullOrWhiteSpace(txtPrismaP.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(txtPrismaP.Text))
                {
                    _frotaMDL.Prisma = Convert.ToInt16(txtPrismaP.Text);
                }

                try
                {
                    _funcionarioMDL.Cpf = string.Empty;

                    if (!string.IsNullOrWhiteSpace(txtPlaca.Text) && !string.IsNullOrWhiteSpace(txtPrismaP.Text))
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Informe o número da placa ou o número do prisma para consulta');</script>");

                        txtPlaca.Text = string.Empty;
                        txtPrismaP.Text = string.Empty;
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(txtPlaca.Text))
                        {
                            _frotaMDL.Placa = txtPlaca.Text;

                            _frotaMDL = _frotaBLL.CarregaPlacaVeiculoInterno(_frotaMDL, _funcionarioMDL);

                            if (_frotaMDL.Validador)
                            {
                                CarregaInformacoesFuncionario();
                            }
                            else
                            {
                                CarregaInformacoesCliente();
                            }
                        }
                        else
                        {
                            _frotaMDL.Placa = string.Empty;

                            _frotaMDL = _frotaBLL.CarregaVagaPrisma(_frotaMDL, _funcionarioMDL);

                            if (_frotaMDL.Validador)
                            {
                                if (_frotaMDL.Validador2)
                                {
                                    CarregaInformacoesFuncionario();
                                }
                                else
                                {
                                    CarregaInformacoesCliente();
                                }
                            }
                            else
                            {
                                LimpaCampos();

                                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                            "<script>alert('Não consta veiculo estacionado com o prisma informado');</script>");
                            }
                        }
                    }
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion

        #region CarregaInformacoesFuncionario

        private void CarregaInformacoesFuncionario()
        {
            try
            {
                _frotaMDL = _frotaBLL.CarregaEntradaVeiculo(_frotaMDL);
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }

            if (_frotaMDL.DEntrada.Ticks != 0 && _frotaMDL.DSaida.Ticks != 0 || _frotaMDL.DEntrada.Ticks == 0)
            {
                CarregaPrisma();

                if (txtPlaca.Text == string.Empty)
                {
                    lblPlaca2.Visible = true;
                    txtPlaca2.Visible = true;

                    txtPlaca2.Text = _frotaMDL.Placa;
                }

                txtNome.Text = _frotaMDL.FunNome;
                txtMarca.Text = _frotaMDL.Marca;
                txtTipo.Text = _frotaMDL.Tipo;
                txtModelo.Text = _frotaMDL.Modelo;

                pnFuncionario.Visible = true;

                txtPlaca.Enabled = false;
                txtPrismaP.Enabled = false;
            }
            else if (_frotaMDL.DEntrada.Ticks != 0 && _frotaMDL.DSaida.Ticks == 0)
            {
                txtEntrada.Text = Convert.ToString(_frotaMDL.DEntrada.TimeOfDay);

                txtPrisma.Text = Convert.ToString(_frotaMDL.Prisma);

                if (txtPlaca.Text == string.Empty)
                {
                    lblPlaca2.Visible = true;
                    txtPlaca2.Visible = true;

                    txtPlaca2.Text = _frotaMDL.Placa;
                }

                txtNome.Text = _frotaMDL.FunNome;
                txtMarca.Text = _frotaMDL.Marca;
                txtTipo.Text = _frotaMDL.Tipo;
                txtModelo.Text = _frotaMDL.Modelo;

                pnFuncionario.Visible = true;

                txtPlaca.Enabled = false;
                txtPrismaP.Enabled = false;
            }
        }

        #endregion

        #region CarregaInformacoesCliente

        private void CarregaInformacoesCliente()
        {
            try
            {
                _frotaMDL = _frotaBLL.CarregaEntradaVeiculo(_frotaMDL);
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }

            if (_frotaMDL.DEntrada.Ticks != 0 && _frotaMDL.DSaida.Ticks != 0 || _frotaMDL.DEntrada.Ticks == 0)
            {
                CarregaPrisma();

                if (txtPlaca.Text == string.Empty)
                {
                    lblPlaca3.Visible = true;
                    txtPlaca3.Visible = true;

                    txtPlaca3.Text = _frotaMDL.Placa;
                }

                pnCliente.Visible = true;
                txtPlaca.Enabled = false;
                txtPrismaP.Enabled = false;
            }
            else if (_frotaMDL.DEntrada.Ticks != 0 && _frotaMDL.DSaida.Ticks == 0)
            {
                txtEntradaExterno.Text = Convert.ToString(_frotaMDL.DEntrada.TimeOfDay);

                txtPrisma2.Text = Convert.ToString(_frotaMDL.Prisma);

                if (txtPlaca.Text == string.Empty)
                {
                    lblPlaca3.Visible = true;
                    txtPlaca3.Visible = true;

                    txtPlaca3.Text = _frotaMDL.Placa;
                }

                pnCliente.Visible = true;
                txtPlaca.Enabled = false;
                txtPrismaP.Enabled = false;

                _frotaMDL = _frotaBLL.CarregaEntradaVeiculo(_frotaMDL);
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
            txtPrismaP.Text = string.Empty;

            txtEntrada.Text = string.Empty;
            txtSaida.Text = string.Empty;

            txtEntradaExterno.Text = string.Empty;
            txtSaidaExterno.Text = string.Empty;

            txtPlaca2.Text = string.Empty;
            txtPlaca3.Text = string.Empty;

            lblPlaca2.Visible = false;
            txtPlaca2.Visible = false;

            lblPlaca3.Visible = false;
            txtPlaca3.Visible = false;

            txtPlaca.Enabled = true;
            txtPrismaP.Enabled = true;
        }

        #endregion

        #region btnAplicar_Click

        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEntrada.Text == string.Empty)
                {
                    _frotaMDL.Placa = txtPlaca.Text != string.Empty ? txtPlaca.Text : txtPlaca2.Text;

                    _frotaMDL.Prisma = Convert.ToInt16(txtPrisma.Text);
                    _frotaMDL.DEntrada = DateTime.Now;

                    _frotaMDL.DEntrada = new DateTime(_frotaMDL.DEntrada.Year, _frotaMDL.DEntrada.Month,
                                                       _frotaMDL.DEntrada.Day,
                                                       _frotaMDL.DEntrada.Hour, _frotaMDL.DEntrada.Minute,
                                                       _frotaMDL.DEntrada.Second);

                    _conexaoMDL = _frotaBLL.CadastraHoraEntradaInternos(_frotaMDL);

                    if (_conexaoMDL.Validador == false)
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Hora de entrada registrada com sucesso');</script>");

                    CarregaVagas();

                    LimpaCampos();
                }
                else
                {
                    _frotaMDL.Placa = txtPlaca.Text != string.Empty ? txtPlaca.Text : txtPlaca2.Text;

                    _frotaMDL.Prisma = Convert.ToInt16(txtPrisma.Text);
                    _frotaMDL.DEntrada = Convert.ToDateTime(txtEntrada.Text);
                    _frotaMDL.DSaida = DateTime.Now;

                    _frotaMDL.DSaida = new DateTime(_frotaMDL.DSaida.Year, _frotaMDL.DSaida.Month,
                                                     _frotaMDL.DSaida.Day,
                                                     _frotaMDL.DSaida.Hour, _frotaMDL.DSaida.Minute,
                                                     _frotaMDL.DSaida.Second);

                    _conexaoMDL = _frotaBLL.CadastraHoraSaidaInternos(_frotaMDL);

                    if (_conexaoMDL.Validador == false)
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Hora de saida registrada com sucesso');</script>");

                    CarregaVagas();

                    LimpaCampos();
                }
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }
        }

        #endregion

        #region btnAplicarExterno_Click

        protected void btnAplicarExterno_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEntradaExterno.Text == string.Empty)
                {
                    _frotaMDL.Placa = txtPlaca.Text != string.Empty ? txtPlaca.Text : txtPlaca3.Text;

                    _frotaMDL.Prisma = Convert.ToInt16(txtPrisma2.Text);
                    _frotaMDL.DEntrada = DateTime.Now;

                    _frotaMDL.DEntrada = new DateTime(_frotaMDL.DEntrada.Year, _frotaMDL.DEntrada.Month,
                                                       _frotaMDL.DEntrada.Day,
                                                       _frotaMDL.DEntrada.Hour, _frotaMDL.DEntrada.Minute,
                                                       _frotaMDL.DEntrada.Second);

                    _conexaoMDL = _frotaBLL.CadastraHoraEntradaExternos(_frotaMDL);

                    if (_conexaoMDL.Validador == false)
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Hora de entrada registrada com sucesso');</script>");

                    CarregaVagas();

                    LimpaCampos();
                }
                else
                {
                    _frotaMDL.Placa = txtPlaca.Text != string.Empty ? txtPlaca.Text : txtPlaca3.Text;

                    _frotaMDL.Prisma = Convert.ToInt16(txtPrisma2.Text);
                    _frotaMDL.DEntrada = Convert.ToDateTime(txtEntradaExterno.Text);
                    _frotaMDL.DSaida = DateTime.Now;

                    _frotaMDL.DSaida = new DateTime(_frotaMDL.DSaida.Year, _frotaMDL.DSaida.Month,
                                                     _frotaMDL.DSaida.Day,
                                                     _frotaMDL.DSaida.Hour, _frotaMDL.DSaida.Minute,
                                                     _frotaMDL.DSaida.Second);

                    _conexaoMDL = _frotaBLL.CadastraHoraSaidaExternos(_frotaMDL);

                    if (_conexaoMDL.Validador == false)
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Hora de saida registrada com sucesso');</script>");

                    CarregaVagas();

                    LimpaCampos();
                }
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }
        }

        #endregion

        #region CarregaPrisma

        private void CarregaPrisma()
        {
            _frotaMDL = _frotaBLL.CarregaPrisma(_frotaMDL);

            txtPrisma.Text = Convert.ToString(_frotaMDL.Prisma);
            txtPrisma2.Text = Convert.ToString(_frotaMDL.Prisma);
        }

        #endregion

        #region CarregaVagas

        private void CarregaVagas()
        {
            _frotaMDL = _frotaBLL.CarregaVagas(_frotaMDL);

            txtVaga.Text = Convert.ToString(_frotaMDL.Vaga);
        }

        #endregion
    }
}