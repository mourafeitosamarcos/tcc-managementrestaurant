using System;
using System.Data;

using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.master
{
    public partial class principal : System.Web.UI.MasterPage
    {
        readonly FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();

        readonly ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Timeout = 120;

                if (Session["PassaInfo"] != null)
                {
                    _conexaoMDL.Ds = (DataSet)Session["PassaInfo"];

                    _funcionarioMDL.Nome = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Nome"].ToString();

                    lblNome.Text = _funcionarioMDL.Nome + "!";
                }
                else
                {
                    Session.Timeout = 120;

                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Sua sessão foi encerrada automaticamente por inatividade. Faça o login novamente para iniciar uma nova sessão');location.href='../../login.aspx';</script>");
                }
            }
        }

        #endregion

        #region lbtSair_Click

        protected void lbtSair_Click(object sender, EventArgs e)
        {
            Session.Abandon();

            Response.Redirect("~/login.aspx");
        }

        #endregion

        #region btnFeed_Click

        protected void btnFeed_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "Erro",
                                                    "window.open('../administrativo/cadastro_feedback.aspx', 'janela', 'width=810, height=560, top=50, left=250,scrollbars=no, directories=no, menubar=no,fullscreen=no,maximized=no, resizable=no ');",
                                                    true);
        }

        #endregion
    }
}