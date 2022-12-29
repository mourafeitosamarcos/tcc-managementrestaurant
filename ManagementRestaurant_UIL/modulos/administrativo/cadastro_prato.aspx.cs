using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.administrativo
{
    public partial class cadastro_prato : Page
    {
        private readonly EstoqueBLL _estoqueBLL = new EstoqueBLL();
        private readonly EstoqueMDL _estoqueMDL = new EstoqueMDL();
        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();
       private DataTable CadIng = new DataTable();
        private ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();
        
                

     
       

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

            {
                if ((Session["Acumulador"])!=null)
                {
                    Session.Remove("Acumulador");
                }
                Session["Acumulador"] = null;
               
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
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Sua sessão foi encerrada automaticamente por inatividade. Faça o login novamente para iniciar uma nova sessão');location.href='../../login.aspx';</script>");
                }
            }

            
            
            
        }

        #endregion

        #region ddlIngrediente_Load

        protected void ddlIngrediente_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                try
                {
                    _conexaoMDL = _estoqueBLL.CarregaNomeProdutoDropdown();

                    ddlIngrediente.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlIngrediente.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlIngrediente.DataSource = _conexaoMDL.Ds.Tables[0];
                    ddlIngrediente.DataBind();

                    ddlIngrediente.Items.Insert(0, new ListItem("Selecione...", "0"));
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion

       #region btnCadastrar_Click

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            CadIng = (DataTable)Session["Acumulador"];

            if (CadIng.Rows.Count != 0)
            {
                if (ValidaCampos())
                {
                    _estoqueMDL.NPrato = txtNomePrato.Text;
                    _estoqueMDL.VPrato = Convert.ToDouble(txtValorPrato.Text);

                   


                    try
                    {
                        DataSet CadPrato = new DataSet();
                        CadPrato.Tables.Add(CadIng);

                        _conexaoMDL = _estoqueBLL.CadastraPrato(_estoqueMDL,CadPrato);

                        CadIng.Clear();
                        CadIng.Dispose();

                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    _conexaoMDL.Validador
                                                                        ? "<script>alert('Cadastro efetuado com sucesso');location.href='../home/home.aspx';</script>"
                                                                        : "<script>alert('Prato já consta no sistema');</script>");

                    }
                    catch
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                    }
                    

                }
            }
            else {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                  "<script>alert('Cadastre os Ingredientes do Prato');</script>");

            }
        }

        #endregion

        #region btnLimpar_Click

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNomePrato.Text = string.Empty;
            txtValorPrato.Text = string.Empty;
            ddlIngrediente.SelectedValue = "0";
            txtQuantidade.Text = string.Empty;
            grdIngPrato.DataSource = null;
            grdIngPrato.DataBind();
            CadIng.Dispose();
            
        }

        #endregion

        #region ValidaCampos

        private Boolean ValidaCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNomePrato.Text) || string.IsNullOrWhiteSpace(txtValorPrato.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");

                return false;
            }

            return true;
        }

        #endregion



        protected void btnItem_Click(object sender, EventArgs e)
        {


            if (ddlIngrediente.SelectedIndex == 0 || txtQuantidade.Text == "")
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Favor cadastrar todos os dados sobre o ingrediente utilizado');</script>");


            }
            else
            {
               
                CadIng.Columns.Add("Ingrediente", Type.GetType("System.String"));
                CadIng.Columns.Add("IdProd", Type.GetType("System.Int32"));
                CadIng.Columns.Add("Qtd", Type.GetType("System.Decimal"));
                if ((Session["Acumulador"]) != null)
                {
                    CadIng = (DataTable)Session["Acumulador"];
                }
                
                DataRow dr = CadIng.NewRow();

                dr["Ingrediente"] = ddlIngrediente.SelectedItem;
                dr["IdProd"] = ddlIngrediente.Text;
                dr["Qtd"] = txtQuantidade.Text;

                CadIng.Rows.Add(dr);

                Session["Acumulador"] = CadIng;

                grdIngPrato.DataSource = CadIng;
                grdIngPrato.DataBind();

                for (int a = 0; a <= CadIng.Rows.Count -1; a++)
                {
                    var lblIngrediente = (Label)grdIngPrato.Rows[a].FindControl("lblIngrediente");
                    var lblQuantidade = (Label)grdIngPrato.Rows[a].FindControl("lblQuantidade");


                    lblIngrediente.Text = CadIng.Rows[a]["Ingrediente"].ToString();
                    lblQuantidade.Text = CadIng.Rows[a]["Qtd"].ToString();

                    
                }
                ddlIngrediente.SelectedValue = "0";
                txtQuantidade.Text = "";
            }
        }

        protected void grdIngPrato_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Apagar")
            {
               int _linha = Convert.ToInt32(e.CommandArgument);
               CadIng = (DataTable)Session["Acumulador"];
               CadIng.Rows[_linha].Delete();
               grdIngPrato.DataSource = null;
               Session["Acumulador"] = CadIng;
               grdIngPrato.DataSource = CadIng;
               grdIngPrato.DataBind();

               for (int a = 0; a <= CadIng.Rows.Count - 1; a++)
               {
                   var lblIngrediente = (Label)grdIngPrato.Rows[a].FindControl("lblIngrediente");
                   var lblQuantidade = (Label)grdIngPrato.Rows[a].FindControl("lblQuantidade");


                   lblIngrediente.Text = CadIng.Rows[a]["Ingrediente"].ToString();
                   lblQuantidade.Text = CadIng.Rows[a]["Qtd"].ToString();


               }
            }
        }
    }
}