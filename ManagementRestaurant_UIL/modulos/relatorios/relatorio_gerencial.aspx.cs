using System;
using System.Data;
using System.Web.UI;
using ManagementRestaurant_BLL;
using ManagementRestaurant_MDL;
using ManagementRestaurant_GLL;

namespace ManagementRestaurant_UIL.modulos.relatorios
{
    public partial class RelatorioGerencial : Page
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

        public void RelQtdVeiculos()
        {

        }

        protected void lbtnProdutoTipo_Click(object sender, EventArgs e)
        {
            CarregaRelTipoProd();
        }

        protected void lbtnProdutoEntrada_Click(object sender, EventArgs e)
        {
            CarregaRelProdEntrada();
        }

        protected void lbtnVeiculosCadastrados_Click(object sender, EventArgs e)
        {
            CarregaRelVeiculosCad();
        }

        protected void lbtnCliEstado_Click(object sender, EventArgs e)
        {
            
        }

        protected void lbtnDocDepart_Click(object sender, EventArgs e)
        {
            CarregaRelDocDepart();
        }

        protected void lbnFuncionarios_Click(object sender, EventArgs e)
        {
            CarregaRelGeralFunc();
        }

        private void CarregaRelTipoProd()
        {
            _relatorioMDL.Tipo = "";
            _conexaoMDL = _relatorioBLL.quantidadePedidoTp(_relatorioMDL);

            _conexaoMDL.NRelatorio = ("quantidadeProdutoTp");
            _conexaoMDL.CRelatorio = ("modulos/relatorios/reporting/quantidadeProdutoTp.rdlc");

            Session["CarregaRelatorio"] = _conexaoMDL;

            Page.ClientScript.RegisterStartupScript(GetType(), "Erro", "window.open('../relatorios/visualiza_relatorio2.aspx', 'janela', 'width=795, height=520, top=50, left=250,scrollbars=no, directories=no, menubar=no,fullscreen=no,maximized=no, resizable=no ');", true);
        }

        private void CarregaRelProdEntrada()
        {
            _relatorioMDL.dtInicio = "";
            _relatorioMDL.dtFim = "";
            _relatorioMDL.Tipo = "";
            _conexaoMDL = _relatorioBLL.quantidadePedidoEntrada(_relatorioMDL);

            _conexaoMDL.NRelatorio = ("quantidadeProdutoEnt");
            _conexaoMDL.CRelatorio = ("modulos/relatorios/reporting/quantidadeProdutoEnt.rdlc");

            Session["CarregaRelatorio"] = _conexaoMDL;

            Page.ClientScript.RegisterStartupScript(GetType(), "Erro", "window.open('../relatorios/visualiza_relatorio2.aspx', 'janela', 'width=795, height=520, top=50, left=250,scrollbars=no, directories=no, menubar=no,fullscreen=no,maximized=no, resizable=no ');", true);
        }

        private void CarregaRelVeiculosCad()
        {
            _relatorioMDL.dtInicio = "";
            _relatorioMDL.dtFim = "";
            _conexaoMDL = _relatorioBLL.quantidadeVeiculoCad(_relatorioMDL);

            _conexaoMDL.NRelatorio = ("quantidadeVeiculoCad");
            _conexaoMDL.CRelatorio = ("modulos/relatorios/reporting/quantidadeVeiculoCad.rdlc");

            Session["CarregaRelatorio"] = _conexaoMDL;

            Page.ClientScript.RegisterStartupScript(GetType(), "Erro", "window.open('../relatorios/visualiza_relatorio2.aspx', 'janela', 'width=795, height=520, top=50, left=250,scrollbars=no, directories=no, menubar=no,fullscreen=no,maximized=no, resizable=no ');", true);
        }       

        private void CarregaRelDocDepart()
        {
            _relatorioMDL.Tipo = "";
            _conexaoMDL = _relatorioBLL.totalDocumentoElemento(_relatorioMDL);

            _conexaoMDL.NRelatorio = ("totalDocElemento");
            _conexaoMDL.CRelatorio = ("modulos/relatorios/reporting/totalDocElemento.rdlc");

            Session["CarregaRelatorio"] = _conexaoMDL;

            Page.ClientScript.RegisterStartupScript(GetType(), "Erro", "window.open('../relatorios/visualiza_relatorio2.aspx', 'janela', 'width=795, height=520, top=50, left=250,scrollbars=no, directories=no, menubar=no,fullscreen=no,maximized=no, resizable=no ');", true);
        }

        private void CarregaRelGeralFunc()
        {
            _relatorioMDL.Tipo = "";
            _conexaoMDL = _relatorioBLL.funcionarioGeral(_relatorioMDL);

            _conexaoMDL.NRelatorio = ("funcionarioGeral");
            _conexaoMDL.CRelatorio = ("modulos/relatorios/reporting/funcionarioGeral.rdlc");

            Session["CarregaRelatorio"] = _conexaoMDL;

            Page.ClientScript.RegisterStartupScript(GetType(), "Erro", "window.open('../relatorios/visualiza_relatorio2.aspx', 'janela', 'width=795, height=520, top=50, left=250,scrollbars=no, directories=no, menubar=no,fullscreen=no,maximized=no, resizable=no ');", true);
        }

        private void CarregaRelTunoverFunc()
        {
            _relatorioMDL.Tipo = "";
            _conexaoMDL = _relatorioBLL.funcionarioTunOver(_relatorioMDL);

            _conexaoMDL.NRelatorio = ("funcionarioTurnOver");
            _conexaoMDL.CRelatorio = ("modulos/relatorios/reporting/funcionarioTurnOver.rdlc");

            Session["CarregaRelatorio"] = _conexaoMDL;

            Page.ClientScript.RegisterStartupScript(GetType(), "Erro", "window.open('../relatorios/visualiza_relatorio2.aspx', 'janela', 'width=795, height=520, top=50, left=250,scrollbars=no, directories=no, menubar=no,fullscreen=no,maximized=no, resizable=no ');", true);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            CarregaRelTunoverFunc();
        }
    }
}