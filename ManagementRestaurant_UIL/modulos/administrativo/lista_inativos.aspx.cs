using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using ManagementRestaurant_BLL;
using ManagementRestaurant_MDL;
using ManagementRestaurant_GLL;

namespace ManagementRestaurant_UIL.modulos.administrativo
{
    public partial class lista_inativos : Page
    {
        private readonly FuncionarioBLL _funcionarioBLL = new FuncionarioBLL();
        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();
        private FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private ConexaoMDL _conexaoMDL3 = new ConexaoMDL();
        
        private FuncionarioMDL _funcionarioMDL2 = new FuncionarioMDL();
        private int _linha;
        private string coluna;
        private string parametro;
       
        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _conexaoMDL.Ds = (DataSet)Session["PassaInfo"];

                _funcionarioMDL = _funcionarioGLL.ValidaUsuario(_conexaoMDL);

                if (_funcionarioMDL.N_Acesso != 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "'alertscript",
                        string.Format("window.alert(\"{0}\");history.go(-{1});", "Voce não está autorizado a acessar a página", 1), true);
                }
                else
                {
                    var _dt = (DataTable)Session["SalvaISearch"];

                    if (_dt != null)
                    {

                        CarregaGridInativos(_dt.Rows[0]["param"].ToString(), _dt.Rows[0]["coluna"].ToString());
                    }
                }
            }
        }

        #endregion

        #region btnPesquisar_Click

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            DataTable _dt = new DataTable("DT0");
            _dt = (DataTable)Session["SalvaISearch"];
            if (_dt != null)
            {
                _dt.Clear();
            }
            parametro = txtPesquisa.Text;
            coluna = ddlColuna.SelectedValue;
            DataTable _dt1= new DataTable("DT0");
            DataColumn Col = _dt1.Columns.Add("ID", typeof(Int32));
            _dt1.Columns.Add("param", typeof(String));
            _dt1.Columns.Add("coluna", typeof (String));
            _dt1.Rows.Add(0);
            _dt1.Rows[0]["param"] = parametro;
            _dt1.Rows[0]["coluna"] = coluna;
            Session["SalvaISearch"] = _dt1;

            CarregaGridInativos(parametro, coluna);

        }

        #endregion

        #region CarregaGridInativos

        private void CarregaGridInativos(string parametro, string coluna)
        {          
            _conexaoMDL3.Ds.Clear();
            _conexaoMDL3 = _funcionarioBLL.PesquisaInativos(parametro, coluna);
       
            grdFuncionarios.DataSource = _conexaoMDL3.Ds;
            grdFuncionarios.DataBind();

            for (int i = 0; i < _conexaoMDL3.Ds.Tables[0].Rows.Count; i++)
            {
                var lbtNome = (LinkButton) grdFuncionarios.Rows[i].FindControl("lbtNome");
                var lbtCargo = (LinkButton) grdFuncionarios.Rows[i].FindControl("lbtCargo");
                var lbtCPF = (LinkButton) grdFuncionarios.Rows[i].FindControl("lbtCpf");
                var lbtRG = (LinkButton) grdFuncionarios.Rows[i].FindControl("lbtRG");
         
                var lbtCEP = (LinkButton) grdFuncionarios.Rows[i].FindControl("lbtCEP");
                var lbtEndereco = (LinkButton) grdFuncionarios.Rows[i].FindControl("lbtEndereco");
         
                var lbtBairro = (LinkButton) grdFuncionarios.Rows[i].FindControl("lbtBairro");
                var lbtDtAdm = (LinkButton) grdFuncionarios.Rows[i].FindControl("lbtDtAdm");


                lbtNome.Text = Convert.ToString(_conexaoMDL3.Ds.Tables[0].Rows[i]["Fun_Nome"].ToString());
                lbtCargo.Text = Convert.ToString(_conexaoMDL3.Ds.Tables[0].Rows[i]["Cargo_Nome"].ToString());
                lbtCPF.Text = Convert.ToString(_conexaoMDL3.Ds.Tables[0].Rows[i]["Fun_CPF"].ToString());
                lbtRG.Text = Convert.ToString(_conexaoMDL3.Ds.Tables[0].Rows[i]["Fun_RG"].ToString());
                lbtCEP.Text = Convert.ToString(_conexaoMDL3.Ds.Tables[0].Rows[i]["Fun_CEP"].ToString());
                lbtEndereco.Text = Convert.ToString(_conexaoMDL3.Ds.Tables[0].Rows[i]["Fun_Rua"].ToString());
                lbtBairro.Text = Convert.ToString(_conexaoMDL3.Ds.Tables[0].Rows[i]["Fun_Bairro"].ToString());
                lbtDtAdm.Text = Convert.ToString(_conexaoMDL3.Ds.Tables[0].Rows[i]["Admissao_Data"].ToString());
            }
        }

        #endregion

        #region grdFuncionariosRowCommand

        protected void grdFuncionarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Habilitar")
            {
                _linha = Convert.ToInt32(e.CommandArgument);

                var intCPF = (LinkButton)(grdFuncionarios.Rows[_linha].FindControl("lbtCpf"));
                _funcionarioMDL.Cpf = intCPF.Text;

                _conexaoMDL3 = _funcionarioBLL.HabilitaFuncionario(_funcionarioMDL);

                CarregaGridInativos(parametro, coluna);

                var i = 2;

                _conexaoMDL.Ds = (DataSet)Session["PassaInfo"];

                _funcionarioBLL.RegistraLog(_conexaoMDL, i);

                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Conta habilitada com sucesso');</script>");
            }
            else if (e.CommandName == "Ver")
            {
                _linha = Convert.ToInt32(e.CommandArgument);

                var intCPF = (LinkButton)(grdFuncionarios.Rows[_linha].FindControl("lbtCpf"));
                _funcionarioMDL.Cpf = intCPF.Text;
                _conexaoMDL3.Ds.Clear();

                _funcionarioMDL2 = _funcionarioBLL.CarregaInformacoesFuncionario(_funcionarioMDL);
               
                Session["PassaDadosFunc"] = _funcionarioMDL2.Registro;
                Response.Redirect("dados_funcionario.aspx");
            }
        }

        #endregion
    }
}