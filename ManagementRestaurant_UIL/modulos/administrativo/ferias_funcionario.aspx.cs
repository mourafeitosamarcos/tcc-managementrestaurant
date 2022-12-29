using System;
using System.Data;
using System.Web.UI;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.administrativo
{
    public partial class ferias_funcionario : Page
    {
        private readonly FuncionarioBLL _funcionarioBLL = new FuncionarioBLL();
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

        #region btnLimpar_Click

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ferias_funcionario.aspx");
        }

        #endregion

        #region btnAplicar_Click

        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            if (ValidaCampos())
            {
                if (string.IsNullOrWhiteSpace(txtProventos.Text))
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Preencha inicial e final nos calendários');</script>");
                }
                else
                {
                    AplicaFerias();
                }
            }
        }

        #endregion

        #region ValidaCampos

        private Boolean ValidaCampos()
        {
            if (string.IsNullOrWhiteSpace(txtDias.Text) || string.IsNullOrWhiteSpace(txtHora.Text) ||
                string.IsNullOrWhiteSpace(txtI_Ferias.Text) || string.IsNullOrWhiteSpace(txtF_Ferias.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");

                return false;
            }

            return true;
        }

        #endregion

        #region LimpaCampos

        public void LimpaCampos()
        {
            txtI_Ferias.Text = string.Empty;
            txtF_Ferias.Text = string.Empty;
            txtDias.Text = string.Empty;
            txtHora.Text = string.Empty;
            txtProventos.Text = string.Empty;

            txtI_Ferias.Enabled = true;
            txtF_Ferias.Enabled = true;
            txtDias.Enabled = true;
            txtHora.Enabled = true;
            imgI_Ferias.Enabled = true;
            imgF_Ferias.Enabled = true;
        }

        #endregion

        #region AplicaFerias

        private void AplicaFerias()
        {
            _funcionarioMDL.Ferias = Convert.ToDateTime(txtI_Ferias.Text).Date;
            _funcionarioMDL.FFerias = Convert.ToDateTime(txtF_Ferias.Text).Date;

            _funcionarioMDL.Cpf = txtCpf.Text;
            _funcionarioMDL.Cargo = txtCargo.Text;

            _funcionarioMDL.DAdmissao = Convert.ToDateTime(txtAdmissao.Text);

            _funcionarioMDL.DUteis = Convert.ToInt16(txtDias.Text);
            _funcionarioMDL.Proventos = Convert.ToDouble(txtProventos.Text);

            int validaFeriasI = DateTime.Compare(_funcionarioMDL.Ferias, DateTime.Today);
            int validaFeriasF = DateTime.Compare(_funcionarioMDL.FFerias, DateTime.Today);

            if (validaFeriasI < 0 || validaFeriasF < 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('A data inicial ou final das férias não pode ser anterior ao dia atual');</script>");
            }
            else
            {
                TimeSpan validaUteis = _funcionarioMDL.FFerias - _funcionarioMDL.Ferias;
                int intervalo = validaUteis.Days;

                if (_funcionarioMDL.DUteis > intervalo)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Os dias úteis não podem ser maiores que o intervalo das férias');</script>");

                    LimpaCampos();
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(txtProventos.Text) || txtProventos.Text == "0")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('É necessário calcular os proventos antes de aplicar as férias');</script>");

                        LimpaCampos();
                    }

                    TimeSpan validaPermanencia = _funcionarioMDL.Ferias -
                                                 Convert.ToDateTime(_funcionarioMDL.DAdmissao);

                    int permanencia = validaPermanencia.Days;

                    if (permanencia < 364)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Só é possivel aplicar férias a funcionários com permanencia minima de um ano');</script>");

                        LimpaCampos();
                    }
                    else if (Convert.ToInt16(txtHora.Text) > 24)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('A quantidade máxima de horas trabalhadas não podem exceder 24 horas');</script>");

                        LimpaCampos();
                    }
                    else
                    {
                        try
                        {
                            _conexaoMDL = _funcionarioBLL.AplicaFerias(_funcionarioMDL);

                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                        _conexaoMDL.Validador
                                                                            ? "<script>alert('Férias aplicadas com sucesso');location.href='../home/home.aspx';</script>"
                                                                            : "<script>alert('Já existe férias cadastradas para este funcionário nesse periodo');</script>");
                        }
                        catch
                        {
                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                        "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");

                            LimpaCampos();
                        }
                    }
                }
            }
        }

        #endregion

        #region btnCalcular_Click

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            if (ValidaCampos())
            {
                _funcionarioMDL.SHora = Convert.ToDouble(txtS_Hora.Text);
                _funcionarioMDL.DUteis = Convert.ToInt16(txtDias.Text);
                _funcionarioMDL.Proventos = _funcionarioMDL.DUteis*Convert.ToInt16(txtHora.Text)*_funcionarioMDL.SHora;

                txtProventos.Text = Convert.ToString(_funcionarioMDL.Proventos);

                txtI_Ferias.Enabled = false;
                txtF_Ferias.Enabled = false;
                txtDias.Enabled = false;
                txtHora.Enabled = false;
                imgI_Ferias.Enabled = false;
                imgF_Ferias.Enabled = false;
            }
        }

        #endregion

        #region btnBuscar_Click

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCpf.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Digite o CPF para consulta');</script>");
            }
            else
            {
                _funcionarioMDL.Cpf = txtCpf.Text;

                _conexaoMDL.Validador = _funcionarioGLL.ValidaCpf(txtCpf.Text);

                if (_conexaoMDL.Validador == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('CPF inválido!');</script>");
                }
                else
                {
                    _funcionarioMDL.Cpf = txtCpf.Text;

                    try
                    {
                        _funcionarioMDL = _funcionarioBLL.CarregaInformacoesFuncionario(_funcionarioMDL);
                    }
                    catch
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                    }

                    if (_funcionarioMDL.Validador == false)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('CPF não consta no sistema');</script>");
                    }
                    else
                    {
                        txtNome.Text = _funcionarioMDL.Nome;
                        txtTelefone.Text = _funcionarioMDL.Telefone;
                        txtRg.Text = _funcionarioMDL.Rg;
                        txtCnh.Text = _funcionarioMDL.Cnh;
                        txtAdmissao.Text = Convert.ToString(_funcionarioMDL.DAdmissao).Substring(0, 10);
                        txtN_Ctrabalho.Text = _funcionarioMDL.CTrabalho;
                        txtCargo.Text = _funcionarioMDL.Cargo;
                        txtS_Hora.Text = Convert.ToString(_funcionarioMDL.SHora);

                        txtCep.Text = _funcionarioMDL.Cep;
                        txtEndereco.Text = _funcionarioMDL.Rua;
                        txtNumero.Text = _funcionarioMDL.NEstabelecimento;
                        txtBairro.Text = _funcionarioMDL.Bairro;
                        txtCidade.Text = _funcionarioMDL.Cidade;
                        txtEstado.Text = _funcionarioMDL.Estado;

                        txtCpf.Enabled = false;
                        pnCadastro.Visible = true;
                    }
                }
            }
        }

        #endregion
    }
}