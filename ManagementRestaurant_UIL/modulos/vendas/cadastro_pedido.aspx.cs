using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.vendas
{
    public partial class cadastro_pedido : Page
    {
        private readonly ClienteGLL _clienteGLL = new ClienteGLL();
        private readonly ClienteMDL _clienteMDL = new ClienteMDL();

        private readonly EstoqueBLL _estoqueBLL = new EstoqueBLL();
        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private readonly PedidoBLL _pedidoBLL = new PedidoBLL();
        private readonly PedidoMDL _pedidoMDL = new PedidoMDL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();
        private DataTable CadPratos = new DataTable();
       
        private double _i;

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((Session["Acumulador"]) != null)
                {
                    Session.Remove("Acumulador");
                }

                Session["Acumulador"] = null;
               
                if (Session["PassaInfo"] != null)
                {
                    _conexaoMDL.Ds = (DataSet) Session["PassaInfo"];

                    _funcionarioMDL = _funcionarioGLL.ValidaUsuario(_conexaoMDL);

                    if (_funcionarioMDL.NAcesso != 1 && _funcionarioMDL.NAcesso != 2 && _funcionarioMDL.NAcesso != 3)
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

        #region ddlCliente_SelectedIndexChanged

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCliente.SelectedValue == "1")
            {
                btnAplicar.Visible = true;
                btnVoltar.Visible = true;

                btnAplicarExterno.Visible = false;
                btnVoltar2.Visible = false;

                pnPratos.Visible = true;
                pnClienteF.Visible = true;
                pnClienteJ.Visible = false;
            }
            else if (ddlCliente.SelectedValue == "2")
            {
                btnAplicarExterno.Visible = true;
                btnVoltar2.Visible = true;

                btnAplicar.Visible = false;
                btnVoltar.Visible = false;

                pnPratos.Visible = true;
                pnClienteJ.Visible = true;
                pnClienteF.Visible = false;
            }
            else
            {
                Response.Redirect("cadastro_pedido.aspx");
            }
        }

        #endregion

        #region ddlPrato_Load

        protected void ddlPrato_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    _conexaoMDL = _estoqueBLL.CarregaNomePratoDropdown();

                    ddlPrato.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlPrato.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlPrato.DataSource = _conexaoMDL.Ds;
                    ddlPrato.DataBind();

                    ddlPrato.Items.Insert(0, new ListItem("Selecione...", "0"));

                    Session["CarregaDS"] = _conexaoMDL.Ds;
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion

        #region ddlPrato_SelectedIndexChanged

        protected void ddlPrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            _conexaoMDL.Ds = (DataSet) Session["CarregaDS"];

            txtValor.Text = ddlPrato.SelectedValue != "0" ? Convert.ToString(_conexaoMDL.Ds.Tables[0].Rows[ddlPrato.SelectedIndex - 1]["Valor_Prato"]) : string.Empty;

               }

        #endregion

        #region btnCalcular_Click

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            CadPratos = (DataTable)Session["Acumulador"];
            if (ddlCompra.SelectedValue != "0" && grdIngPrato.Rows.Count != 0)
            {
                _pedidoMDL.ValorTotal = 0;
                _pedidoMDL.Valor = 0;
                for (int a = 0; a <= CadPratos.Rows.Count - 1; a++)
                {
                    _pedidoMDL.Valor = _pedidoMDL.Valor + ((Convert.ToDouble(CadPratos.Rows[a]["Valor"])) * (Convert.ToDouble(CadPratos.Rows[a]["Quantidade"])));
                }
                _pedidoMDL.ValorTotal = _pedidoMDL.Valor;

                if (ddlCompra.SelectedValue == "1")
                {
                    txtValorPrato.Text = Convert.ToString(_pedidoMDL.ValorTotal * 1.1);
                }
                else if (ddlCompra.SelectedValue == "3")
                {
                    txtValorPrato.Text = Convert.ToString(_pedidoMDL.ValorTotal * 1.15);
                }
                else
                {
                    txtValorPrato.Text = Convert.ToString(_pedidoMDL.ValorTotal);
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                           "<script>alert('Preencha todos os campos para efetuar o pedido corretamente');</script>");
            }
        }
        

        #endregion

        #region btnAplicar_Click

        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            CadPratos = (DataTable)Session["Acumulador"];

            if (ValidaCamposF() && txtValorPrato.Text != string.Empty)
            {
                _conexaoMDL.Validador = _clienteGLL.ValidaCpf(txtCpfCliente.Text);

                if (_conexaoMDL.Validador == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('CPF inválido!');</script>");
                }
                else
                {
                    _funcionarioMDL.Cpf = hfCpf.Value;
                    _clienteMDL.Documento = txtCpfCliente.Text;
                    _pedidoMDL.Compra = ddlCompra.Text;
                    _pedidoMDL.Cliente = ddlCliente.Text;
                    _pedidoMDL.ValorTotal = Convert.ToDouble(txtValorPrato.Text);
                
                }

                var cadPed = new DataSet();
                cadPed.Tables.Add(CadPratos);

                try
                {
                    _conexaoMDL = _pedidoBLL.CadastraPedido(_funcionarioMDL, _clienteMDL, _pedidoMDL, cadPed);

                    CadPratos.Clear();
                    CadPratos.Dispose();

                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                _conexaoMDL.Validador
                                                                    ? "<script>alert('Cadastro efetuado com sucesso');location.href='../home/home.aspx';</script>"
                                                                    : "<script>alert('CPF não consta no sistema');</script>");
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }
        
        #endregion

        #region btnCalcularJ_Click

        protected void btnCalcularJ_Click(object sender, EventArgs e)
        {
            CadPratos = (DataTable)Session["Acumulador"];
            _pedidoMDL.QDia = Convert.ToInt32(txtQuantidadeDia.Text);
            _pedidoMDL.Contrato = Convert.ToDateTime(txtI_Contrato.Text);
            _pedidoMDL.FContrato = Convert.ToDateTime(txtF_Contrato.Text);

            if (ddlCompra2.SelectedValue != "0" && grdIngPrato.Rows.Count != 0 && !string.IsNullOrWhiteSpace(txtQuantidadeDia.Text) && !string.IsNullOrWhiteSpace(txtI_Contrato.Text) && !string.IsNullOrWhiteSpace(txtF_Contrato.Text) && ddlEntrega.SelectedIndex != 0 && ValidaDatas(_pedidoMDL))
            {
                ddlCliente.Enabled = false;

                txtCnpjCliente.Enabled = false;
                ddlCompra2.Enabled = false;
                ddlEntrega.Enabled = false;

                ddlPrato.Enabled = false;
                txtQuantidade.Enabled = false;

                txtQuantidadeDia.Enabled = false;
                txtI_Contrato.Enabled = false;
                txtF_Contrato.Enabled = false;

                _pedidoMDL.ValorTotal = 0;
                _pedidoMDL.Valor = 0;
                for (int a = 0; a <= CadPratos.Rows.Count - 1; a++)
                {
                    _pedidoMDL.Valor = _pedidoMDL.Valor + ((Convert.ToDouble(CadPratos.Rows[a]["Valor"])) * (Convert.ToDouble(CadPratos.Rows[a]["Quantidade"])));
                }
                _pedidoMDL.ValorTotal = _pedidoMDL.Valor;

                TimeSpan validaUteis = _pedidoMDL.FContrato - _pedidoMDL.Contrato;
                int intervalo = validaUteis.Days;

                txtValorPrato2.Text = ddlCompra.SelectedValue == "1" ? Convert.ToString((_pedidoMDL.ValorTotal*(_pedidoMDL.QDia*intervalo))) : Convert.ToString((_pedidoMDL.ValorTotal*(_pedidoMDL.QDia*intervalo))*1.15);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                           "<script>alert('Preencha todos os campos para efetuar o pedido corretamente');</script>");
            }
        }

        #endregion

        #region btnAplicarExterno_Click

        protected void btnAplicarExterno_Click(object sender, EventArgs e)
        {
            CadPratos = (DataTable)Session["Acumulador"];

            if (ValidaCamposJ() && txtValorPrato2.Text != string.Empty)
            {
                _conexaoMDL.Validador = _clienteGLL.ValidaCnpj(txtCnpjCliente.Text);

                if (_conexaoMDL.Validador == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('CNPJ inválido!');</script>");
                }
                else
                {
                    _funcionarioMDL.Cpf = hfCpf.Value;
                    _clienteMDL.Documento = txtCnpjCliente.Text;
                    _pedidoMDL.Compra = ddlCompra2.Text;
                    _pedidoMDL.Cliente = ddlCliente.Text;
                    _pedidoMDL.ValorTotal = Convert.ToDouble(txtValorPrato2.Text);
                    _pedidoMDL.QDia = Convert.ToInt32(txtQuantidadeDia.Text);
                    _pedidoMDL.Previsao = Convert.ToDateTime(ddlEntrega.SelectedValue);
                    _pedidoMDL.Contrato = Convert.ToDateTime(txtI_Contrato.Text);
                    _pedidoMDL.FContrato = Convert.ToDateTime(txtF_Contrato.Text);

                    DataSet CadPed = new DataSet();
                    CadPed.Tables.Add(CadPratos);

                    try
                    {
                        _conexaoMDL = _pedidoBLL.CadastraPedido(_funcionarioMDL, _clienteMDL, _pedidoMDL, CadPed);

                        CadPratos.Clear();
                        CadPratos.Dispose();

                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                        _conexaoMDL.Validador
                                                                            ? "<script>alert('Cadastro efetuado com sucesso');location.href='../home/home.aspx';</script>"
                                                                            : "<script>alert('CNPJ não consta no sistema');</script>");
                        }
                        catch
                        {
                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                        "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                        }
                    }
                }
}
        #endregion

        #region BtnItem_Click
        protected void btnItem_Click(object sender, EventArgs e)
        {
        if (ddlPrato.SelectedIndex == 0 || string.IsNullOrWhiteSpace(txtQuantidade.Text) || string.IsNullOrWhiteSpace(txtValor.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Favor cadastrar todos os dados sobre o ingrediente utilizado');</script>");

            }
            else
            {
                CadPratos.Columns.Add("Prato", Type.GetType("System.String"));
                CadPratos.Columns.Add("IdPrato", Type.GetType("System.Int32"));
                CadPratos.Columns.Add("Quantidade", Type.GetType("System.Int32"));
                CadPratos.Columns.Add("Valor", Type.GetType("System.Decimal"));
                
            if ((Session["Acumulador"]) != null)
                {
                    CadPratos = (DataTable)Session["Acumulador"];
                }

                DataRow dr = CadPratos.NewRow();

                dr["Prato"] = ddlPrato.SelectedItem;
                dr["IdPrato"] = ddlPrato.Text;
                dr["Quantidade"] = txtQuantidade.Text;
                dr["Valor"] = txtValor.Text;

                CadPratos.Rows.Add(dr);
                
                
                CarregaGrid(CadPratos);
            }
        }
        #endregion

        #region btnLimpar_Click

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            Response.Redirect("cadastro_pedido.aspx");
        }

        #endregion

        #region ValidaCamposF

        private Boolean ValidaCamposF()
        {
            if (string.IsNullOrWhiteSpace(txtCpfCliente.Text) || ddlCompra.SelectedValue == "0" || grdIngPrato.Rows.Count == 0  || string.IsNullOrWhiteSpace((txtValorPrato.Text)))
            {

                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");

                return false;
            }

            return true;
        }

        #endregion

        #region ValidaCamposJ

        private Boolean ValidaCamposJ()
        {
            if (string.IsNullOrWhiteSpace(txtCnpjCliente.Text) || ddlCompra2.SelectedValue == "0" || grdIngPrato.Rows.Count == 0 || string.IsNullOrWhiteSpace(txtQuantidadeDia.Text) || string.IsNullOrWhiteSpace(txtI_Contrato.Text) || string.IsNullOrWhiteSpace(txtF_Contrato.Text) || ddlEntrega.SelectedIndex == 0 || string.IsNullOrWhiteSpace((txtValorPrato2.Text)))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                           "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");

                return false;
            }

            return true;
        }

        #endregion

        #region ValidaDatas

        private Boolean ValidaDatas(PedidoMDL pedidoMDL)
        {
            int validaContratoI = DateTime.Compare(pedidoMDL.Contrato, DateTime.Today);
            int validaContratoF = DateTime.Compare(pedidoMDL.FContrato, DateTime.Today);

            if (validaContratoI < 0 || validaContratoF < 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('A data inicial ou final do contrato não pode ser anterior ao dia atual');</script>");

                return false;
            }

            return true;
        }

        #endregion

        #region CarregaGrid
        public void CarregaGrid(DataTable CadPratos) 
        {
            grdIngPrato.DataSource = null;
            Session["Acumulador"] = CadPratos;

            grdIngPrato.DataSource = CadPratos;
            grdIngPrato.DataBind();

            for (int a = 0; a <= CadPratos.Rows.Count - 1; a++)
            {
                var lblPrato = (Label)grdIngPrato.Rows[a].FindControl("lblPrato");
                var lblQuantidade = (Label)grdIngPrato.Rows[a].FindControl("lblQuantidade");
                var lblValor = (Label)grdIngPrato.Rows[a].FindControl("lblValor");

                lblPrato.Text = CadPratos.Rows[a]["Prato"].ToString();
                lblQuantidade.Text = CadPratos.Rows[a]["Quantidade"].ToString();
                lblValor.Text = CadPratos.Rows[a]["Valor"].ToString();


            }
            ddlPrato.SelectedValue = "0";
            txtQuantidade.Text = "";
            txtValor.Text = "";
        
        }
        #endregion

        #region grdIngPrato_RowCommand
        protected void grdIngPrato_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Apagar")
            {
                var linha = Convert.ToInt32(e.CommandArgument);
                CadPratos = (DataTable)Session["Acumulador"];
                CadPratos.Rows[linha].Delete();

                CarregaGrid(CadPratos);
            }
        }
        #endregion

        #region ddlEntrega_Load

        protected void ddlEntrega_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    _conexaoMDL = _pedidoBLL.CarregaPrevisaoEntrega(_conexaoMDL);

                    ddlEntrega.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlEntrega.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlEntrega.DataSource = _conexaoMDL.Ds;
                    ddlEntrega.DataBind();

                    ddlEntrega.Items.Insert(0, new ListItem("Selecione...", "0"));
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion
    }
}