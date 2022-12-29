using System;
using System.Data;
using System.Web.UI;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;
using Microsoft.Reporting.WebForms;

namespace ManagementRestaurant_UIL.modulos.relatorios
{
    public partial class VisualizaRelatorio : Page
    {
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
                    _conexaoMDL.Ds = (DataSet)Session["PassaInfo"];

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
                        CarregaRelatorio();
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

        #region CarregaRelatorio

        public void CarregaRelatorio()
        {
            _conexaoMDL = (ConexaoMDL)Session["CarregaRelatorio"];

            var rptData = new ReportDataSource { Name = (_conexaoMDL.NRelatorio), Value = _conexaoMDL.Dt };

            rptPedidos.LocalReport.DataSources.Clear();
            rptPedidos.LocalReport.DataSources.Add(rptData);

            rptPedidos.LocalReport.ReportPath = (_conexaoMDL.CRelatorio);

            rptPedidos.LocalReport.Refresh();
        }

        #endregion
    }
}