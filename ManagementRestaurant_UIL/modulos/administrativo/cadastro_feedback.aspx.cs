using System;
using System.Data;
using System.Web.UI;
using ManagementRestaurant_BLL;
using ManagementRestaurant_MDL;
using ManagementRestaurant_GLL;

namespace ManagementRestaurant_UIL.modulos.administrativo
{
    public partial class cadastro_feedback : Page
    {
        private readonly FuncionarioBLL _funcionarioBLL = new FuncionarioBLL();
        
        private readonly ConexaoMDL _conexaoMDL = new ConexaoMDL();

        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();
        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

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
                    else
                    {
                        hfCpf.Value = _funcionarioMDL.Cpf;
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
                _funcionarioMDL.Cpf = hfCpf.Value;
                _funcionarioMDL.Assunto = Convert.ToInt16(ddlAssunto.SelectedValue);
                _funcionarioMDL.Mensagem = txtMensagem.Text;

                try
                {
                    _funcionarioBLL.CadastraFeedback(_funcionarioMDL);

                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                            "<script>alert('Feedback enviado com sucesso');</script>");

                    Page.ClientScript.RegisterStartupScript(GetType(), "0", "<script>window.close();</script>");
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
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

        #region ValidaCampos

        private Boolean ValidaCampos()
        {
            if (ddlAssunto.SelectedValue == "0" || string.IsNullOrWhiteSpace(txtMensagem.Text) || txtMensagem.MaxLength > 500)
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
            Response.Redirect("cadastro_feedback.aspx");
        }

        #endregion

        #region btnCancelar_Click

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "0", "<script>window.close();</script>");
        }

        #endregion
    }
}