using System;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL
{
    public partial class login : Page
    {
        private readonly FuncionarioBLL _funcionarioBLL = new FuncionarioBLL();
        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();
        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region btnLogar_Click

        protected void btnLogar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCpf.Text) || string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o login corretamente');</script>");
            }
            else
            {
                _conexaoMDL.Ds.Clear();

                _funcionarioMDL.Cpf = txtCpf.Text;
                _funcionarioMDL.Senha = txtSenha.Text;

                _conexaoMDL = _funcionarioBLL.CarregaFuncionario(_funcionarioMDL);

                if (_conexaoMDL.Validador)
                {
                    _conexaoMDL = _funcionarioGLL.ValidaSenha(_conexaoMDL);

                    if (_conexaoMDL.Validador)
                    {
                        pnLogin.Visible = false;
                        pnSenhaN.Visible = true;
                        btnEati.Visible = false;
                        btnNovaSenha.Visible = false;

                        Session["CarregaDS"] = _conexaoMDL.Ds;
                    }
                    else
                    {
                        _conexaoMDL = _funcionarioGLL.ValidaStatus(_conexaoMDL);

                        _conexaoMDL = _funcionarioBLL.ValidaPeriodoFerias(_funcionarioMDL);

                        if (_conexaoMDL.Validador && _conexaoMDL.Validador2)
                        {
                            Session["PassaInfo"] = _conexaoMDL.Ds;

                            Response.Redirect("~/modulos/home/home.aspx");
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                        "<script>alert('Voce não está autorizado(a) a acessar o sistema');location.href='login.aspx';</script>");
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('CPF ou senha incorretos');</script>");
                }
            }
        }

        #endregion

        #region btnConfirmar_Click

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (txtSenhaN.Text.Equals(txtSenhaC.Text))
            {
                _conexaoMDL.Ds = (DataSet) Session["CarregaDS"];

                _funcionarioMDL.Cpf = txtCpf.Text;
                _funcionarioMDL.Senha = txtSenhaN.Text;

                _conexaoMDL = _funcionarioGLL.ValidaDocumentos(_conexaoMDL, _funcionarioMDL);

                if (_conexaoMDL.Validador)
                {
                    Session.Remove("CarregaDS");

                    _conexaoMDL = _funcionarioBLL.AtualizaSenha(_funcionarioMDL);

                    if (string.IsNullOrWhiteSpace(txtSenha.Text))
                    {
                        _funcionarioMDL.Cpf = txtCpf.Text;
                        _funcionarioMDL.Senha = txtSenhaC.Text;

                        _conexaoMDL = _funcionarioBLL.CarregaFuncionario(_funcionarioMDL);
                    }

                    Session["PassaInfo"] = _conexaoMDL.Ds;

                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Senha atualizada com sucesso');location.href='/modulos/home/home.aspx';</script>");
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('A senha não pode ser igual a nenhum de seus documentos cadastrados');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('As senhas não conferem');</script>");
            }
        }

        #endregion

        #region btnEati_Click

        protected void btnEati_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "Erro",
                                                    "window.open('cadastro_eati.aspx', 'janela', 'width=810, height=560, top=50, left=250,scrollbars=no, directories=no, menubar=no,fullscreen=no,maximized=no, resizable=no ');",
                                                    true);
        }

        #endregion

        #region btnNovaSenha_Click

        protected void btnNovaSenha_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCpf.Text))
            {
                pnLogin.Visible = false;
                pnSenhaR.Visible = true;

                txtSenha.Text = string.Empty;
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Informe o CPF para recuperação de senha');</script>");
            }
        }

        #endregion

        #region btnEnviar_Click

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                _funcionarioMDL.Cpf = txtCpf.Text;
                _funcionarioMDL.Email = txtEmail.Text;

                try
                {
                    _funcionarioMDL = _funcionarioBLL.ValidaEmail(_funcionarioMDL);
                }
                catch (Exception)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }

                if (_funcionarioMDL.Validador)
                {
                    hfToken.Value = _funcionarioMDL.Senha.ToLower();
                    hfRG.Value = _funcionarioMDL.Rg;
                    hfCNH.Value = _funcionarioMDL.Cnh;

                    lblEmail.Visible = false;
                    txtEmail.Visible = false;

                    lblToken.Visible = true;
                    txtToken.Visible = true;

                    btnEnviar.Visible = false;
                    btnConfirmar2.Visible = true;

                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Foi enviado para seu email um token de confirmação. Informe o token no campo disponivel para redefinição de senha');</script>");

                    var mailMessage = new MailMessage();

                    var smtpClient = new SmtpClient("smtp.gmail.com", 587) {EnableSsl = true};
                    var networkCredential = new NetworkCredential("laboratoriocsharp@gmail.com", "labcsharp");
                    smtpClient.Credentials = networkCredential;

                    mailMessage.From = new MailAddress("laboratoriocsharp@gmail.com");

                    mailMessage.To.Add(_funcionarioMDL.Email);

                    mailMessage.Subject = "Recuperação de Senha: ManagementRestaurant";

                    mailMessage.Body = "Olá " + _funcionarioMDL.Nome + "! Seu token para recuperação de senha é: " +
                                       _funcionarioMDL.Senha.ToLower();

                    smtpClient.Send(mailMessage);
                    smtpClient.UseDefaultCredentials = true;
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Email informado não pertence a este CPF');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Informe o email para recuperação de senha');</script>");
            }
        }

        #endregion

        #region btnConfirmar2_Click

        protected void btnConfirmar2_Click(object sender, EventArgs e)
        {
            if (txtToken.Text.Equals(hfToken.Value))
            {
                var dc = new DataColumn {DataType = Type.GetType("System.String"), ColumnName = "Fun_RG"};
                _conexaoMDL.Dt.Columns.Add(dc);

                dc = new DataColumn {DataType = Type.GetType("System.String"), ColumnName = "CNH"};
                _conexaoMDL.Dt.Columns.Add(dc);

                DataRow dr = _conexaoMDL.Dt.NewRow();

                dr["Fun_RG"] = hfRG.Value;
                dr["CNH"] = hfCNH.Value;

                _conexaoMDL.Dt.Rows.Add(dr);

                _conexaoMDL.Ds.Merge(_conexaoMDL.Dt);

                Session["CarregaDS"] = _conexaoMDL.Ds;

                RecuperaSenha();
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Token informado não é valido');</script>");
            }
        }

        #endregion

        #region RecuperaSenha

        private void RecuperaSenha()
        {
            pnLogin.Visible = false;
            pnSenhaR.Visible = false;
            pnSenhaN.Visible = true;
            btnEati.Visible = false;
            btnNovaSenha.Visible = false;
        }

        #endregion
    }
}