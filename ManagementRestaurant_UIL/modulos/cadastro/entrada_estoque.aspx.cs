using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.cadastro
{
    public partial class entrada_estoque : Page
    {
        private readonly EstoqueBLL _estoqueBLL = new EstoqueBLL();
        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private EstoqueMDL _estoqueMDL = new EstoqueMDL();
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

                    if (_funcionarioMDL.NAcesso != 1 && _funcionarioMDL.NAcesso != 5)
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

        #region btnAplicar_Click

        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            if (ValidaCampos())
            {
                _estoqueMDL.CProduto = Convert.ToInt32(txtCodProduto.Text);
                _estoqueMDL.NFiscal = txtNotaF.Text;

                if (txtLote.Text != string.Empty && ddlFornecedor.SelectedValue != "0")
                {
                    _estoqueMDL.Lote = Convert.ToInt32(txtLote.Text);
                    _estoqueMDL.FCnpj = ddlFornecedor.SelectedValue;
                }
                else
                {
                    _estoqueMDL.Lote = 0;
                    _estoqueMDL.FCnpj = "0";
                }

                _estoqueMDL.Validade = Convert.ToDateTime(txtValidade.Text);
                _estoqueMDL.Quantidade = Convert.ToInt32(txtQuantidade.Text);

                _estoqueMDL.Compra = chkAvulso.Checked == false ? 1 : 2;

                if (_estoqueMDL.Quantidade != 0)
                {
                    TimeSpan validaUteis = _estoqueMDL.Validade - DateTime.Today;
                    int intervalo = validaUteis.Days;

                    if (intervalo > 0)
                    {
                        if (intervalo > 30)
                        {
                            CadastraEntradaProduto();
                        }
                        else
                        {
                            //Aplicar confirm button antes da execução do método 
                            CadastraEntradaProduto();
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Não é permitido o cadastro de produtos com a validade vencida');</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Não é permitido o cadastro de produtos com a quantidade igual a 0');</script>");
                }
            }
        }

        #endregion

        #region CadastraEntradaProduto

        public void CadastraEntradaProduto()
        {
            try
            {
                _conexaoMDL = _estoqueBLL.CadastraEntradaEstoque(_estoqueMDL);

                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            _conexaoMDL.Validador
                                                                ? "<script>alert('Cadastro efetuado com sucesso');location.href='../home/home.aspx';</script>"
                                                                : "<script>alert('Nota fiscal já consta no sistema');</script>");
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
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
            if (chkAvulso.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtNotaF.Text) || string.IsNullOrWhiteSpace(txtValidade.Text) ||
                    string.IsNullOrWhiteSpace(txtQuantidade.Text))
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");

                    return false;
                }

                return true;
            }

            if (string.IsNullOrWhiteSpace(txtNotaF.Text) || string.IsNullOrWhiteSpace(txtLote.Text) ||
                string.IsNullOrWhiteSpace(txtValidade.Text) || string.IsNullOrWhiteSpace(txtQuantidade.Text) ||
                ddlFornecedor.SelectedValue == "0")
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
            txtCodProduto.Text = string.Empty;
            txtCodProduto.Enabled = true;
            txtNotaF.Text = string.Empty;
            txtProduto.Text = string.Empty;
            txtLote.Text = string.Empty;
            txtLote.Enabled = true;
            txtValidade.Text = string.Empty;
            txtQuantidade.Text = string.Empty;
            ddlFornecedor.SelectedValue = "0";
            ddlFornecedor.Enabled = true;
            chkAvulso.Checked = false;

            pnProduto.Visible = false;
        }

        #endregion

        #region ddlFornecedor_Load

        protected void ddlFornecedor_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    _conexaoMDL = _estoqueBLL.CarregaFornecedor(_conexaoMDL);

                    ddlFornecedor.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlFornecedor.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlFornecedor.DataSource = _conexaoMDL.Ds;
                    ddlFornecedor.DataBind();

                    ddlFornecedor.Items.Insert(0, new ListItem("Selecione...", "0"));
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion

        #region chkAvulso_CheckedChanged

        protected void chkAvulso_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAvulso.Checked)
            {
                ddlFornecedor.Enabled = false;
                ddlFornecedor.SelectedValue = "0";
                txtLote.Enabled = false;

                txtLote.Text = string.Empty;
            }
            else
            {
                ddlFornecedor.Enabled = true;
                txtLote.Enabled = true;
            }
        }

        #endregion

        #region btnBuscar_Click

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodProduto.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Digite o codigo para consulta');</script>");
            }
            else
            {
                _estoqueMDL.CProduto = Convert.ToInt16(txtCodProduto.Text);

                try
                {
                    _estoqueMDL = _estoqueBLL.CarregaNomeProduto(_estoqueMDL);
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }

                if (_estoqueMDL.Validador == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Codigo não consta no sistema');</script>");
                }
                else
                {
                    pnProduto.Visible = true;
                    txtCodProduto.Enabled = false;

                    txtProduto.Text = _estoqueMDL.NProduto;
                }
            }
        }

        #endregion
    }
}