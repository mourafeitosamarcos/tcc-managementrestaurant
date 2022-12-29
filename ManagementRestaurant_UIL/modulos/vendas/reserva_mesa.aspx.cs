using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.vendas
{
    public partial class reserva_mesa : Page
    {
        private readonly ClienteBLL _clienteBLL = new ClienteBLL();
        private readonly ClienteGLL _clienteGLL = new ClienteGLL();
        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private readonly PedidoBLL _pedidoBLL = new PedidoBLL();
        private readonly PedidoMDL _pedidoMDL = new PedidoMDL();
        private ClienteMDL _clienteMDL = new ClienteMDL();

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
                        hfCpfFuncionario.Value = _funcionarioMDL.Cpf;
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
                _clienteMDL.Documento = txtCpf.Text;

                _conexaoMDL.Validador = _clienteGLL.ValidaCpf(txtCpf.Text);

                if (_conexaoMDL.Validador == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('CPF inválido!');</script>");
                }
                else
                {
                    _clienteMDL.Documento = txtCpf.Text;

                    try
                    {
                        _clienteMDL = _clienteBLL.CarregaInformacoesClienteReserva(_clienteMDL);
                    }
                    catch
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                    }

                    if (_clienteMDL.Validador == false)
                    {
                        txtCpf.Text = string.Empty;

                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('CPF não consta no sistema');</script>");
                    }
                    else
                    {
                        txtNome.Text = _clienteMDL.Nome;
                        txtTelefone.Text = _clienteMDL.Telefone;

                        txtCpf.Enabled = false;
                        pnReserva.Visible = true;
                    }
                }
            }
        }

        #endregion

        #region btnLimpar_Click

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            Response.Redirect("reserva_mesa.aspx");
        }

        #endregion

        #region ddlMesa_SelectedIndexChanged

        protected void ddlMesa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtData.Text) && ddlHora.SelectedValue != "0")
            {
                _pedidoMDL.DReserva = Convert.ToDateTime(txtData.Text).Date;
                _pedidoMDL.HReserva = Convert.ToDateTime(ddlHora.SelectedValue);

                _pedidoMDL.Reserva = new DateTime(_pedidoMDL.DReserva.Year, _pedidoMDL.DReserva.Month,
                                                  _pedidoMDL.DReserva.Day,
                                                  _pedidoMDL.HReserva.Hour, _pedidoMDL.HReserva.Minute,
                                                  _pedidoMDL.HReserva.Second);

                _pedidoMDL.Mesa = Convert.ToInt32(ddlMesa.SelectedValue);

                _conexaoMDL = _pedidoBLL.ValidaMesa(_pedidoMDL);

                if (_conexaoMDL.Validador == false)
                {
                    txtData.Enabled = false;
                    ddlHora.Enabled = false;
                    ddlMesa.Enabled = false;
                    imgData.Enabled = false;

                    btnAplicar.Enabled = true;
                    lblStatusMesa.Visible = true;
                    lblStatusMesa.ForeColor = Color.Green;
                    lblStatusMesa.Text = ("Mesa disponível");
                }
                else
                {
                    lblStatusMesa.Visible = true;
                    lblStatusMesa.ForeColor = Color.Red;
                    lblStatusMesa.Text = ("Mesa indisponível");
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar a validação corretamente');</script>");

                LimpaCampos();
            }
        }

        #endregion

        #region btnAplicar_Click

        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            _funcionarioMDL.Cpf = hfCpfFuncionario.Value;
            _clienteMDL.Documento = txtCpf.Text;

            _pedidoMDL.DReserva = Convert.ToDateTime(txtData.Text).Date;
            _pedidoMDL.HReserva = Convert.ToDateTime(ddlHora.SelectedValue);

            _pedidoMDL.Reserva = new DateTime(_pedidoMDL.DReserva.Year, _pedidoMDL.DReserva.Month,
                                              _pedidoMDL.DReserva.Day,
                                              _pedidoMDL.HReserva.Hour, _pedidoMDL.HReserva.Minute,
                                              _pedidoMDL.HReserva.Second);

            _pedidoMDL.Mesa = Convert.ToInt32(ddlMesa.SelectedValue);

            if (ValidaDatas(_pedidoMDL))
            {
                try
                {
                    _pedidoBLL.CadastraReserva(_funcionarioMDL, _clienteMDL, _pedidoMDL);

                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Cadastro efetuado com sucesso');location.href='../home/home.aspx';</script>");
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion

        #region ValidaDatas

        private Boolean ValidaDatas(PedidoMDL pedidoMDL)
        {
            int validaReserva = DateTime.Compare(pedidoMDL.DReserva, DateTime.Today);

            if (validaReserva < 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('A data inicial ou final da reserva não pode ser anterior ao dia atual');</script>");

                return false;
            }

            return true;
        }

        #endregion

        #region LimpaCampos

        public void LimpaCampos()
        {
            txtData.Enabled = true;
            ddlHora.Enabled = true;
            ddlMesa.Enabled = true;
            imgData.Enabled = true;

            ddlHora.SelectedValue = ("0");
            ddlMesa.SelectedValue = ("0");

            lblStatusMesa.Visible = false;
            btnAplicar.Enabled = false;
        }

        #endregion
    }
}