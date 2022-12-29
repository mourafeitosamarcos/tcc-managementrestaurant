using System;
using System.Data;
using System.Web.UI;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.operacional
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
            if (!IsPostBack)
            {
                if (Session["PassaInfo"] != null)
                {
                    _conexaoMDL.Ds = (DataSet) Session["PassaInfo"];

                    _funcionarioMDL = _funcionarioGLL.ValidaUsuario(_conexaoMDL);

                    if (_funcionarioMDL.NAcesso == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "'alertscript",
                                                                string.Format(
                                                                    "window.alert(\"{0}\");history.go(-{1});",
                                                                    "Voce não está autorizado(a) a acessar a página", 1),
                                                                true);
                    }
                    else
                    {
                        hfCpf.Value = _funcionarioMDL.Cpf;

                        CarregaInformacoesPonto();
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

        #region CarregaInformacoesPonto

        public void CarregaInformacoesPonto()
        {
            try
            {
                _funcionarioMDL.Cpf = hfCpf.Value;

                _funcionarioMDL = _funcionarioBLL.CarregaInformacoesPonto(_funcionarioMDL);
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }

            if (_funcionarioMDL.HSaida.DayOfYear != DateTime.Now.DayOfYear)
            {
                if (_funcionarioMDL.HEntrada.Ticks == 0)
                {
                    lblStatus.Text = ("Horario de ponto");

                    pnCadastro.Visible = true;

                    goto Final;
                }

                if (_funcionarioMDL.FAlmoco.DayOfYear == DateTime.Now.DayOfYear)
                {
                    lblStatus.Text = ("Horario de ponto");

                    txtEntrada.Text = Convert.ToString(_funcionarioMDL.HEntrada.TimeOfDay);

                    pnCadastro.Visible = true;

                    goto Final;
                }

                if (_funcionarioMDL.Almoco.DayOfYear == DateTime.Now.DayOfYear)
                {
                    lblStatus.Text = ("Horario de almoço");

                    lblEntrada.Text = ("Inicio de almoço:");
                    lblSaida.Text = ("Final de almoço:");

                    txtEntrada.Text = Convert.ToString(_funcionarioMDL.Almoco.TimeOfDay);

                    pnCadastro.Visible = true;

                    Session["PassaData"] = _funcionarioMDL.HEntrada;

                    goto Final;
                }

                if (_funcionarioMDL.HEntrada.DayOfYear == DateTime.Now.DayOfYear)
                {
                    lblStatus.Text = ("Horario de almoço");

                    lblEntrada.Text = ("Inicio de almoço:");
                    lblSaida.Text = ("Final de almoço:");

                    pnCadastro.Visible = true;

                    Session["PassaData"] = _funcionarioMDL.HEntrada;
                }

                Final:
                {}
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Já consta no sistema entrada e saida para o dia atual');location.href='../home/home.aspx';</script>");
            }
        }

        #endregion

        #region btnAplicar_Click

        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblStatus.Text == ("Horario de ponto"))
                {
                    if (txtEntrada.Text == string.Empty)
                    {
                        _funcionarioMDL.Cpf = hfCpf.Value;
                        _funcionarioMDL.HEntrada = DateTime.Now;

                        _funcionarioMDL.HEntrada = new DateTime(_funcionarioMDL.HEntrada.Year,
                                                                 _funcionarioMDL.HEntrada.Month,
                                                                 _funcionarioMDL.HEntrada.Day,
                                                                 _funcionarioMDL.HEntrada.Hour,
                                                                 _funcionarioMDL.HEntrada.Minute,
                                                                 _funcionarioMDL.HEntrada.Second);

                        _conexaoMDL = _funcionarioBLL.CadastraPontoEntrada(_funcionarioMDL);

                        if (_conexaoMDL.Validador == false)
                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                        "<script>alert('Hora de entrada registrada com sucesso');location.href='../home/home.aspx';</script>");
                    }
                    else
                    {
                        _funcionarioMDL.Cpf = hfCpf.Value;
                        _funcionarioMDL.HEntrada = Convert.ToDateTime(txtEntrada.Text);
                        _funcionarioMDL.HSaida = DateTime.Now;

                        _funcionarioMDL.HSaida = new DateTime(_funcionarioMDL.HSaida.Year,
                                                               _funcionarioMDL.HSaida.Month,
                                                               _funcionarioMDL.HSaida.Day,
                                                               _funcionarioMDL.HSaida.Hour,
                                                               _funcionarioMDL.HSaida.Minute,
                                                               _funcionarioMDL.HSaida.Second);

                        _conexaoMDL = _funcionarioBLL.CadastraPontoSaida(_funcionarioMDL);

                        if (_conexaoMDL.Validador == false)
                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                        "<script>alert('Hora de saida registrada com sucesso');location.href='../home/home.aspx';</script>");
                    }
                }
                else
                {
                    _funcionarioMDL.HEntrada = (DateTime) Session["PassaData"];

                    if (txtEntrada.Text == string.Empty)
                    {
                        _funcionarioMDL.Cpf = hfCpf.Value;
                        _funcionarioMDL.Almoco = DateTime.Now;

                        _funcionarioMDL.Almoco = new DateTime(_funcionarioMDL.Almoco.Year,
                                                                _funcionarioMDL.Almoco.Month,
                                                                _funcionarioMDL.Almoco.Day,
                                                                _funcionarioMDL.Almoco.Hour,
                                                                _funcionarioMDL.Almoco.Minute,
                                                                _funcionarioMDL.Almoco.Second);

                        _conexaoMDL = _funcionarioBLL.CadastraAlmocoEntrada(_funcionarioMDL);

                        if (_conexaoMDL.Validador == false)
                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                        "<script>alert('Inicio de almoço registrado com sucesso');location.href='../home/home.aspx';</script>");
                    }
                    else
                    {
                        _funcionarioMDL.Cpf = hfCpf.Value;
                        _funcionarioMDL.FAlmoco = DateTime.Now;

                        _funcionarioMDL.FAlmoco = new DateTime(_funcionarioMDL.FAlmoco.Year,
                                                                _funcionarioMDL.FAlmoco.Month,
                                                                _funcionarioMDL.FAlmoco.Day,
                                                                _funcionarioMDL.FAlmoco.Hour,
                                                                _funcionarioMDL.FAlmoco.Minute,
                                                                _funcionarioMDL.FAlmoco.Second);

                        _conexaoMDL = _funcionarioBLL.CadastraAlmocoSaida(_funcionarioMDL);

                        if (_conexaoMDL.Validador == false)
                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                        "<script>alert('Final de almoço registrado com sucesso');location.href='../home/home.aspx';</script>");
                    }
                }
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }
        }

        #endregion
    }
}