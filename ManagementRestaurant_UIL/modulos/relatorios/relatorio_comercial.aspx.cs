using System;
using System.Data;
using System.Web.UI;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.relatorios
{
    public partial class RelatorioComercial : Page
    {
        private readonly RelatorioBLL _relatorioBLL = new RelatorioBLL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();
        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();
        private readonly RelatorioMDL _relatorioMDL = new RelatorioMDL();

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PassaInfo"] != null)
                {
                    _conexaoMDL.Ds = (DataSet)Session["PassaInfo"];

                    _funcionarioMDL = _funcionarioGLL.ValidaUsuario(_conexaoMDL);

                    if (_funcionarioMDL.NAcesso == 0)
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

        protected void lbtnQuantidadeCli_Click(object sender, EventArgs e)
        {
            CarregaCliEstado();
        }

        private void CarregaCliEstado()
        {
            _relatorioMDL.Tipo = "";
            _conexaoMDL = _relatorioBLL.quantidadeClienteEstado(_relatorioMDL);

            _conexaoMDL.NRelatorio = ("quantidadeClienteEstado");
            _conexaoMDL.CRelatorio = ("modulos/relatorios/reporting/quantidadeClienteEstado.rdlc");

            Session["CarregaRelatorio"] = _conexaoMDL;

            Page.ClientScript.RegisterStartupScript(GetType(), "Erro", "window.open('../relatorios/visualiza_relatorio2.aspx', 'janela', 'width=795, height=520, top=50, left=250,scrollbars=no, directories=no, menubar=no,fullscreen=no,maximized=no, resizable=no ');", true);
        }

        private void CarregaPedidoCliente()
        {
            _relatorioMDL.Tipo = "";
            _conexaoMDL = _relatorioBLL.quantidadePedidoTpCliente(_relatorioMDL);

            _conexaoMDL.NRelatorio = ("quantidadePedTipoCliente");
            _conexaoMDL.CRelatorio = ("modulos/relatorios/reporting/quantidadePedTipoCliente.rdlc");

            Session["CarregaRelatorio"] = _conexaoMDL;

            Page.ClientScript.RegisterStartupScript(GetType(), "Erro", "window.open('../relatorios/visualiza_relatorio2.aspx', 'janela', 'width=795, height=520, top=50, left=250,scrollbars=no, directories=no, menubar=no,fullscreen=no,maximized=no, resizable=no ');", true);
        }

        private void CarregaReservasdia()
        {
            _relatorioMDL.Tipo = "";
            _conexaoMDL = _relatorioBLL.quantidadePedidoTpCliente(_relatorioMDL);

            _conexaoMDL.NRelatorio = ("quantidadeReservasDia");
            _conexaoMDL.CRelatorio = ("modulos/relatorios/reporting/quantidadeReservasDia.rdlc");

            Session["CarregaRelatorio"] = _conexaoMDL;

            Page.ClientScript.RegisterStartupScript(GetType(), "Erro", "window.open('../relatorios/visualiza_relatorio2.aspx', 'janela', 'width=795, height=520, top=50, left=250,scrollbars=no, directories=no, menubar=no,fullscreen=no,maximized=no, resizable=no ');", true);
        }

        protected void lbtnQtdPedTPCliente_Click(object sender, EventArgs e)
        {
            CarregaPedidoCliente();
        }

        protected void lblquantReservas_Click(object sender, EventArgs e)
        {
            CarregaReservasdia();
        }
    }
}