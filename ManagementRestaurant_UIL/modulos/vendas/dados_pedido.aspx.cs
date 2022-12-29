using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.vendas
{
    public partial class dados_pedido : Page
    {
        private ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private readonly ConexaoMDL _conexaoMDL2 = new ConexaoMDL();

        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();
        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private readonly PedidoBLL _pedidoBLL = new PedidoBLL();
        private readonly PedidoGLL _pedidoGLL = new PedidoGLL();
        private readonly PedidoMDL _pedidoMDL = new PedidoMDL();

        private ClienteMDL _clienteMDL = new ClienteMDL();

        private string _pedido;

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PassaInfo"] != null)
                {
                    _conexaoMDL.Ds = (DataSet) Session["PassaInfo"];

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
                        CarregaDadosPedido();
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

        #region CarregaDadosPedido

        private void CarregaDadosPedido()
        {
            _conexaoMDL2.Ds = (DataSet) Session["PassaDadosPed"];
            _clienteMDL = _pedidoGLL.CarregaDadosClientePedido(_conexaoMDL2);
            _pedidoMDL.Registro = (DataSet) Session["PassaDadosPed"];


            txtNome.Text = _clienteMDL.Nome;
            txtTelefone.Text = _clienteMDL.Telefone;
            hfTipo.Value = _clienteMDL.Tipo;

            lbltNome.Text = _clienteMDL.Tipo == "2" ? "Razão Social:" : "Nome:";

            txtCEP.Text = _clienteMDL.Cep;
            txtEndereco.Text = _clienteMDL.Rua;
            txtNumero.Text = _clienteMDL.NEstabelecimento;
            txtBairro.Text = _clienteMDL.Bairro;
            txtCidade.Text = _clienteMDL.Cidade;
            txtEstado.Text = _clienteMDL.Estado;


            txtNPedido.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[0]["Numero_Pedido"].ToString());
            txtPValor.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[0]["Pedido_Valor"].ToString());

            _pedido = txtNPedido.Text;
            grdPratos.DataSource = _conexaoMDL2.Ds;
            grdPratos.DataBind();

            for (int i = 0; i < _conexaoMDL2.Ds.Tables[0].Rows.Count; i++)
            {
                var lblPrato = (Label) grdPratos.Rows[i].FindControl("lblPrato");
                var lblQtd = (Label) grdPratos.Rows[i].FindControl("lblQtd");


                lblPrato.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Nome_Prato"].ToString());
                lblQtd.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Qtde"].ToString());
            }
        }

        #endregion

        #region ddlEntregadores_Load

        protected void ddlEntregadores_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    _conexaoMDL = _pedidoBLL.CarregaNomeEntregadores();

                    ddlEntregadores.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlEntregadores.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlEntregadores.DataSource = _conexaoMDL.Ds;
                    ddlEntregadores.DataBind();

                    ddlEntregadores.Items.Insert(0, new ListItem("Selecione...", "0"));
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion

        #region BtnBack_Click

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("lista_pedidos.aspx");
        }

        #endregion

        #region btnGeraOE_Click

        protected void btnGeraOE_Click(object sender, EventArgs e)
        {
            if (ValidaCampos())
            {
                _conexaoMDL2.Ds = (DataSet)Session["PassaDadosPed"];

                _pedido = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[0]["Numero_Pedido"].ToString());

                _conexaoMDL = _pedidoBLL.BaixaIngredientesEntrega(_pedido);

                if (_conexaoMDL.Validador)
                {
                    _pedidoBLL.EntregaPedido(_pedido);

                    CarregaRelatorio();
                }
            }
        }

        #endregion

        #region CarregaRelatorio

        private void CarregaRelatorio()
        {
            var dc = new DataColumn {DataType = Type.GetType("System.String"), ColumnName = "Cli_Nome"};
            _conexaoMDL.Dt.Columns.Add(dc);

            dc = new DataColumn {DataType = Type.GetType("System.String"), ColumnName = "Cli_Telefone"};
            _conexaoMDL.Dt.Columns.Add(dc);

            dc = new DataColumn {DataType = Type.GetType("System.String"), ColumnName = "Cli_CEP"};
            _conexaoMDL.Dt.Columns.Add(dc);

            dc = new DataColumn {DataType = Type.GetType("System.String"), ColumnName = "Cli_Rua"};
            _conexaoMDL.Dt.Columns.Add(dc);

            dc = new DataColumn {DataType = Type.GetType("System.String"), ColumnName = "Cli_NumEstabelecimento"};
            _conexaoMDL.Dt.Columns.Add(dc);

            dc = new DataColumn {DataType = Type.GetType("System.String"), ColumnName = "Cli_Bairro"};
            _conexaoMDL.Dt.Columns.Add(dc);

            dc = new DataColumn {DataType = Type.GetType("System.String"), ColumnName = "Cli_Cidade"};
            _conexaoMDL.Dt.Columns.Add(dc);

            dc = new DataColumn {DataType = Type.GetType("System.String"), ColumnName = "Cli_Estado"};
            _conexaoMDL.Dt.Columns.Add(dc);

            dc = new DataColumn {DataType = Type.GetType("System.String"), ColumnName = "Numero_Pedido"};
            _conexaoMDL.Dt.Columns.Add(dc);

            dc = new DataColumn {DataType = Type.GetType("System.String"), ColumnName = "Fun_Nome"};
            _conexaoMDL.Dt.Columns.Add(dc);

            dc = new DataColumn { DataType = Type.GetType("System.String"), ColumnName = "Nome_Prato" };
            _conexaoMDL.Dt.Columns.Add(dc);

            dc = new DataColumn { DataType = Type.GetType("System.String"), ColumnName = "Pedido_Quantidade" };
            _conexaoMDL.Dt.Columns.Add(dc);

            dc = new DataColumn { DataType = Type.GetType("System.String"), ColumnName = "Pedido_Valor" };
            _conexaoMDL.Dt.Columns.Add(dc);

            DataRow dr = _conexaoMDL.Dt.NewRow();

            dr["Cli_Nome"] = txtNome.Text;
            dr["Cli_Telefone"] = txtTelefone.Text;
            dr["Cli_CEP"] = txtCEP.Text;
            dr["Cli_Rua"] = txtEndereco.Text;
            dr["Cli_NumEstabelecimento"] = txtNumero.Text;
            dr["Cli_Bairro"] = txtBairro.Text;
            dr["Cli_Cidade"] = txtCidade.Text;
            dr["Cli_Estado"] = txtEstado.Text;
            dr["Numero_Pedido"] = txtNPedido.Text;
            dr["Fun_Nome"] = ddlEntregadores.SelectedItem;
            dr["Pedido_Valor"] = txtPValor.Text;
            dr["Nome_Prato"] = _conexaoMDL2.Ds.Tables[0].Rows[0]["Nome_Prato"].ToString();
            dr["Pedido_Quantidade"] = _conexaoMDL2.Ds.Tables[0].Rows[0]["Qtde"].ToString();

            _conexaoMDL.Dt.Rows.Add(dr);

            for (var i = 1; i <= grdPratos.Rows.Count -1; i++)
            {
                DataRow dr1 = _conexaoMDL.Dt.NewRow();

                dr1["Nome_Prato"] = _conexaoMDL2.Ds.Tables[0].Rows[i]["Nome_Prato"].ToString();
                dr1["Pedido_Quantidade"] = _conexaoMDL2.Ds.Tables[0].Rows[i]["Qtde"].ToString();
                _conexaoMDL.Dt.Rows.Add(dr1);
            }

           
                
            _conexaoMDL.NRelatorio = ("imprime_pedido");
            _conexaoMDL.CRelatorio = ("modulos/relatorios/reporting/imprime_pedido.rdlc");

            Session["CarregaRelatorio"] = _conexaoMDL;

            Page.ClientScript.RegisterStartupScript(GetType(), "Erro", "window.open('../relatorios/visualiza_relatorio.aspx', 'janela', 'width=795, height=520, top=50, left=250,scrollbars=no, directories=no, menubar=no,fullscreen=no,maximized=no, resizable=no ');", true);
        }

        #endregion

        #region ValidaCampos

        private Boolean ValidaCampos()
        {
            if (ddlEntregadores.SelectedValue == ("0"))
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