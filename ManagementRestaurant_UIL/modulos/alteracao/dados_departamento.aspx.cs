﻿using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.alteracao
{
    public partial class dados_departamento : Page
    {
        private readonly FinanceiroBLL _financeiroBLL = new FinanceiroBLL();
        private  FinanceiroMDL _financeiroMDL = new FinanceiroMDL();
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

        #region btnConcluir_Click

        protected void btnConcluir_Click(object sender, EventArgs e)
        {
            if (ValidaCampos())
            {
                _financeiroMDL.NDepartamento = txtNome.Text;
                _financeiroMDL.Responsavel = ddlResponsavel.SelectedValue;
                _financeiroMDL.CDepartamento = Convert.ToInt32(ddlDep.SelectedValue);
                if (ddlContrato.Text == "0")
                {
                    _financeiroMDL.CCusto = Convert.ToInt64(ddlCentroCusto.Text);
                    _financeiroMDL.CContrato = 0;
                }
                else
                {
                    _financeiroMDL.CContrato = Convert.ToInt64(ddlContrato.Text);
                    _financeiroMDL.CCusto = 0;
                }
                try
                {
                    _conexaoMDL = _financeiroBLL.AtualizaDepartamento(_financeiroMDL);
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }

                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            _conexaoMDL.Validador
                                                                ? "<script>alert('Atualização efetuada com sucesso');location.href='../home/home.aspx';</script>"
                                                                : "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }
        }

        #endregion

        #region btnCancelar_Click

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("dados_departamento.aspx");
        }

        #endregion

        #region rbCentroCusto_CheckedChanged

        protected void rbCentroCusto_CheckedChanged(object sender, EventArgs e)
        {ddlContrato.SelectedValue = ("0");
        ddlContrato.Enabled = false;

            ddlCentroCusto.Enabled = true;
        }

        #endregion

        #region rbContrato_CheckedChanged

        protected void rbContrato_CheckedChanged(object sender, EventArgs e)
        {
            ddlCentroCusto.SelectedValue = ("0");
            ddlCentroCusto.Enabled = false;

            ddlContrato.Enabled = true;
        }

        #endregion

        #region ddlResponsavel_Load

        protected void ddlResponsavel_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    _conexaoMDL.Ds = new DataSet();

                    _conexaoMDL = _financeiroBLL.CarregaResponsavel(_conexaoMDL);

                    ddlResponsavel.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlResponsavel.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlResponsavel.DataSource = _conexaoMDL.Ds;
                    ddlResponsavel.DataBind();

                    ddlResponsavel.Items.Insert(0, new ListItem("Selecione...", "0"));
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion

        #region ddlCentroCusto_Load

        protected void ddlCentroCusto_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    _conexaoMDL.Ds = new DataSet();

                    _conexaoMDL = _financeiroBLL.CarregaCentroCusto(_conexaoMDL);

                    ddlCentroCusto.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlCentroCusto.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlCentroCusto.DataSource = _conexaoMDL.Ds;
                    ddlCentroCusto.DataBind();

                    ddlCentroCusto.Items.Insert(0, new ListItem("Selecione...", "0"));
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion

        #region ddlDep_Load
        protected void ddlDep_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                try
                {
                    _conexaoMDL = _financeiroBLL.PesquisaDepartamento();

                    ddlDep.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlDep.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlDep.DataSource = _conexaoMDL.Ds;
                    ddlDep.DataBind();

                    ddlDep.Items.Insert(0, new ListItem("Selecione...", "0"));


                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }
        #endregion

        #region ddlContrato_Load

        protected void ddlContrato_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    _conexaoMDL.Ds = new DataSet();

                    _conexaoMDL = _financeiroBLL.CarregaContrato(_conexaoMDL);

                    ddlContrato.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlContrato.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlContrato.DataSource = _conexaoMDL.Ds;
                    ddlContrato.DataBind();

                    ddlContrato.Items.Insert(0, new ListItem("Selecione...", "0"));
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion

        #region ValidaCampos

        private Boolean ValidaCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || ddlResponsavel.SelectedValue == "0" ||
                ddlCentroCusto.SelectedValue == "0" && ddlDep.SelectedValue == "0")
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");

                return false;
            }

            return true;
        }

        #endregion

        #region ddlDep_SelectedIndexChanged
        protected void ddlDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            _financeiroMDL.NDepartamento = (ddlDep.SelectedValue).ToString();
            _financeiroMDL = _financeiroBLL.CarregaDadosDepartamento(_financeiroMDL);
            txtNome.Text = (_financeiroMDL.NDepartamento);
            ddlCentroCusto.SelectedValue = (_financeiroMDL.CCusto).ToString();
            ddlContrato.SelectedValue=(_financeiroMDL.CContrato).ToString();
            ddlResponsavel.Text=(_financeiroMDL.Responsavel);
            if (ddlCentroCusto.SelectedValue == ("0"))
            {
                rbCentroCusto.Checked = false;
                rbContrato.Checked = true;
                rbContrato_CheckedChanged(null,null);
            }
            else
            {
                rbContrato.Checked = false;
                rbCentroCusto.Checked = true;
                rbCentroCusto_CheckedChanged(null, null);
            }
            PnDep.Visible = true;
        }
        #endregion
    }
}