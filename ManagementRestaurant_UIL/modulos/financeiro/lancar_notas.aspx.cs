using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.financeiro
{
    public partial class lancar_notas : Page
    {
        private readonly FinanceiroBLL _financeiroBLL = new FinanceiroBLL();
        private FinanceiroMDL _financeiroMDL = new FinanceiroMDL();
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

                    if (_funcionarioMDL.NAcesso != 1)
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

        #region btnCadastrar_Click

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (ValidaCampos())
            {
                _financeiroMDL.NConta = txtConta.Text;
                _financeiroMDL.CDepartamento = Convert.ToInt32(ddlDepartamento.SelectedValue);
                _financeiroMDL.DPagamento = Convert.ToDateTime(txtPagamento.Text);

                if (!string.IsNullOrWhiteSpace(txtObservacao.Text))
                {
                    _financeiroMDL.Observacao = txtObservacao.Text;
                }

                if (hfTipoPagamento.Value == ("1"))
                {
                    _financeiroMDL.CCusto = Convert.ToInt64(hfOrigemPagamento.Value);
                }
                else
                {
                    _financeiroMDL.CContrato = Convert.ToInt64(hfOrigemPagamento.Value);
                }

                if (ValidaDatas(_financeiroMDL))
                {
                    try
                    {
                        _conexaoMDL = _financeiroBLL.CadastraConta(_financeiroMDL);
                    }
                    catch
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                    }

                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                _conexaoMDL.Validador == false
                                                                    ? "<script>alert('Cadastro efetuado com sucesso');location.href='../home/home.aspx';</script>"
                                                                    : "<script>alert('Conta já consta no sistema');</script>");
                }
            }
        }

        #endregion

        #region btnLimpar_Click

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            Response.Redirect("lancar_notas.aspx");
        }

        #endregion

        #region ddlDepartamento_Load

        protected void ddlDepartamento_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    _conexaoMDL.Ds = new DataSet();

                    _conexaoMDL = _financeiroBLL.CarregaDepartamento(_conexaoMDL);

                    ddlDepartamento.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlDepartamento.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlDepartamento.DataSource = _conexaoMDL.Ds;
                    ddlDepartamento.DataBind();

                    ddlDepartamento.Items.Insert(0, new ListItem("Selecione...", "0"));
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion

        #region ddlDepartamento_SelectedIndexChanged

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDepartamento.SelectedValue != "0")
            {
                _financeiroMDL.CDepartamento = Convert.ToInt32(ddlDepartamento.SelectedValue);

                _financeiroMDL = _financeiroBLL.CarregaTipoDepartamento(_financeiroMDL);

                if (_financeiroMDL.CCusto != 0)
                {
                    hfTipoPagamento.Value = ("1");
                    hfOrigemPagamento.Value = Convert.ToString(_financeiroMDL.CCusto);
                }
                else
                {
                    hfTipoPagamento.Value = ("2");
                    hfOrigemPagamento.Value = Convert.ToString(_financeiroMDL.CContrato);
                }
            }
        }

        #endregion

        #region ValidaDatas

        private Boolean ValidaDatas(FinanceiroMDL financeiroMDL)
        {
            int validaReserva = DateTime.Compare(financeiroMDL.DPagamento, DateTime.Today);

            if (validaReserva < 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('A data inicial ou final da reserva não pode ser anterior ao dia atual');</script>");

                return false;
            }

            return true;
        }

        #endregion

        #region ValidaCampos

        private Boolean ValidaCampos()
        {
            if (!string.IsNullOrWhiteSpace(txtObservacao.Text))
            {
                if (string.IsNullOrWhiteSpace(txtConta.Text) || ddlDepartamento.SelectedValue == "0" ||
                    string.IsNullOrWhiteSpace(txtPagamento.Text) || txtObservacao.MaxLength > 500)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");

                    return false;
                }
            }
            else if (string.IsNullOrWhiteSpace(txtConta.Text) || ddlDepartamento.SelectedValue == "0" ||
                    string.IsNullOrWhiteSpace(txtPagamento.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");

                return false;
            }

            return true;
        }

        #endregion
    }
}