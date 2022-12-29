using System;
using System.Data;
using System.Data.SqlClient;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_DAL
{
    public class FuncionarioDAL
    {
        private readonly ConexaoDAL _conexaoDAL = new ConexaoDAL();

        private readonly FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();
        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region CarregaFuncionario

        public ConexaoMDL CarregaFuncionario(FuncionarioMDL funcionarioMDL)
        {
            _conexaoMDL = spc_carrega_funcionario(funcionarioMDL);

            try
            {
                _funcionarioMDL.Status = Convert.ToInt16(_conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Status"]);

                _conexaoMDL.Validador = true;
            }
            catch
            {
                _conexaoMDL.Validador = false;
            }

            return _conexaoMDL;
        }

        #endregion

        #region CadastraFuncionario

        public ConexaoMDL CadastraFuncionario(FuncionarioMDL funcionarioMDL)
        {
            _conexaoMDL = spc_valida_documento_funcionario_cadastro(funcionarioMDL);

            try
            {
                _funcionarioMDL.Cpf = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_CPF"].ToString();
                _funcionarioMDL.Rg = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_RG"].ToString();
                _funcionarioMDL.CTrabalho = _conexaoMDL.Ds.Tables[0].Rows[0]["Carteira_de_Trabalho"].ToString();
                _funcionarioMDL.Cnh = _conexaoMDL.Ds.Tables[0].Rows[0]["CNH"].ToString();
            }
            catch
            {
                _conexaoMDL.Validador = false;
            }

            if (funcionarioMDL.Cnh != string.Empty)
            {
                if (funcionarioMDL.Cpf != _funcionarioMDL.Cpf && funcionarioMDL.Rg != _funcionarioMDL.Rg &&
                    funcionarioMDL.CTrabalho != _funcionarioMDL.CTrabalho && funcionarioMDL.Cnh != _funcionarioMDL.Cnh)
                {
                    _conexaoMDL.Ds.Clear();

                    _conexaoMDL = spc_cadastra_funcionario(funcionarioMDL);

                    _conexaoMDL.Validador = true;
                }
                else
                {
                    _conexaoMDL.Validador = false;
                }
            }
            else
            {
                if (funcionarioMDL.Cpf != _funcionarioMDL.Cpf && funcionarioMDL.Rg != _funcionarioMDL.Rg &&
                    funcionarioMDL.CTrabalho != _funcionarioMDL.CTrabalho)
                {
                    _conexaoMDL.Ds.Clear();

                    _conexaoMDL = spc_cadastra_funcionario(funcionarioMDL);

                    _conexaoMDL.Validador = true;
                }
                else
                {
                    _conexaoMDL.Validador = false;
                }
            }

            return _conexaoMDL;
        }

        #endregion

        #region CarregaInformacoesFuncionario

        public ConexaoMDL CarregaInformacoesFuncionario(FuncionarioMDL funcionarioMDL)
        {
            _conexaoMDL = spc_valida_documento_funcionario(funcionarioMDL);

            try
            {
                _funcionarioMDL.Cpf = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_CPF"].ToString();
            }
            catch
            {
                _conexaoMDL.Validador = false;
            }

            if (funcionarioMDL.Cpf == _funcionarioMDL.Cpf)
            {
                _conexaoMDL.Ds.Clear();

                _conexaoMDL = spc_carrega_informacoes_funcionario(funcionarioMDL);

                _conexaoMDL.Validador = true;
            }

            return _conexaoMDL;
        }

        #endregion

        #region CarregaInformacoesPonto

        public ConexaoMDL CarregaInformacoesPonto(FuncionarioMDL funcionarioMDL)
        {
            try
            {
                _conexaoMDL.Ds.Clear();

                _conexaoMDL = spc_carrega_informacoes_ponto(funcionarioMDL);

                _conexaoMDL.Validador = true;
            }
            catch
            {
                _conexaoMDL.Validador = false;
            }

            return _conexaoMDL;
        }

        #endregion

        #region ValidaPeriodoFerias

        public ConexaoMDL ValidaPeriodoFerias(FuncionarioMDL funcionarioMDL)
        {
            _conexaoMDL = spc_valida_periodo_ferias(funcionarioMDL);

            try
            {
                _funcionarioMDL.Ferias =
                    Convert.ToDateTime(_conexaoMDL.Ds.Tables[0].Rows[1]["FeriasData_Inicio"]);
                _funcionarioMDL.FFerias =
                    Convert.ToDateTime(_conexaoMDL.Ds.Tables[0].Rows[1]["FeriasData_Fim"]);
            }
            catch
            {
                _conexaoMDL.Validador2 = true;

                return _conexaoMDL;
            }

            if (DateTime.Today > _funcionarioMDL.Ferias && DateTime.Today < _funcionarioMDL.FFerias)
            {
                _conexaoMDL.Validador2 = false;

                _conexaoMDL.Ds.Tables[0].Rows[1].Delete();
            }
            else
            {
                _conexaoMDL.Validador2 = true;

                _conexaoMDL.Ds.Tables[0].Rows[1].Delete();
            }

            return _conexaoMDL;
        }

        #endregion

        #region AplicaFerias

        public ConexaoMDL AplicaFerias(FuncionarioMDL funcionarioMDL)
        {
            _conexaoMDL = spc_valida_periodo_ferias(funcionarioMDL);

            try
            {
                _funcionarioMDL.Ferias =
                    Convert.ToDateTime(_conexaoMDL.Ds.Tables[0].Rows[0]["FeriasData_Inicio"]);
                _funcionarioMDL.FFerias =
                    Convert.ToDateTime(_conexaoMDL.Ds.Tables[0].Rows[0]["FeriasData_Fim"]);
            }
            catch
            {
                _conexaoMDL.Validador = false;
            }

            if (funcionarioMDL.Ferias > _funcionarioMDL.Ferias && funcionarioMDL.Ferias > _funcionarioMDL.FFerias)
            {
                _conexaoMDL = spc_aplica_ferias(funcionarioMDL);

                _conexaoMDL.Validador = true;
            }
            else
            {
                _conexaoMDL.Validador = false;
            }

            return _conexaoMDL;
        }

        #endregion

        #region AlteraFuncionario

        public ConexaoMDL AlteraFuncionario(FuncionarioMDL funcionarioMDL)
        {
            _conexaoMDL.Ds.Clear();

            _conexaoMDL = spc_altera_funcionario(funcionarioMDL);

            _conexaoMDL.Validador = true;

            return _conexaoMDL;
        }

        #endregion

        #region DesabilitaFuncionario

        public ConexaoMDL DesabilitaFuncionario(FuncionarioMDL funcionarioMDL)
        {
            _conexaoMDL = spc_desabilita_funcionario(funcionarioMDL);

            return _conexaoMDL;
        }

        #endregion

        #region HabilitaFuncionario

        public ConexaoMDL HabilitaFuncionario(FuncionarioMDL funcionarioMDL)
        {
            _conexaoMDL.Ds.Clear();

            _conexaoMDL = spc_habilita_funcionario(funcionarioMDL);

            return _conexaoMDL;
        }

        #endregion

        #region PesquisaFuncionarios

        public ConexaoMDL PesquisaFuncionarios(string parametro, string coluna)
        {
            _conexaoMDL.Ds.Clear();

            _conexaoMDL = spc_pesquisa_funcionarios(parametro, coluna);

            return _conexaoMDL;
        }

        #endregion

        #region PesquisaInativos

        public ConexaoMDL PesquisaInativos(string parametro, string coluna)
        {
            _conexaoMDL.Ds.Clear();

            _conexaoMDL = spc_pesquisa_inativos(parametro, coluna);

            return _conexaoMDL;
        }

        #endregion

        #region ValidaEmail

        public FuncionarioMDL ValidaEmail(FuncionarioMDL funcionarioMDL)
        {
            _conexaoMDL = spc_valida_email(funcionarioMDL);

            try
            {
                funcionarioMDL.Nome = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Nome"].ToString();
                funcionarioMDL.Email = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Email"].ToString();
                funcionarioMDL.Senha = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Senha"].ToString();
                funcionarioMDL.Rg = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_RG"].ToString();
                funcionarioMDL.Cnh = _conexaoMDL.Ds.Tables[0].Rows[0]["CNH"].ToString();

                funcionarioMDL.Validador = true;
            }
            catch
            {
                funcionarioMDL.Validador = false;
            }

            return funcionarioMDL;
        }

        #endregion

        #region spc_carrega_funcionario

        public ConexaoMDL spc_carrega_funcionario(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_funcionario") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@senha_funcionario", funcionarioMDL.Senha));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_nome_cargo

        public ConexaoMDL spc_carrega_nome_cargo(ConexaoMDL conexaoMDL)
        {
            _conexaoDAL.Conexao.Open();

            conexaoMDL.Cmd = new SqlCommand("spc_carrega_nome_cargo")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = _conexaoDAL.Conexao
                };

            conexaoMDL.Da = new SqlDataAdapter(conexaoMDL.Cmd);
            conexaoMDL.Da.Fill(conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return conexaoMDL;
        }

        #endregion

        #region spc_valida_documento_funcionario_cadastro

        public ConexaoMDL spc_valida_documento_funcionario_cadastro(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_documento_funcionario_cadastro")
                {
                    CommandType = CommandType.StoredProcedure
                };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@rg_funcionario", funcionarioMDL.Rg));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cnh_funcionario", funcionarioMDL.Cnh));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@n_trabalho_funcionario", funcionarioMDL.CTrabalho));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_valida_documento_funcionario

        public ConexaoMDL spc_valida_documento_funcionario(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_documento_funcionario")
                {
                    CommandType = CommandType.StoredProcedure
                };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_funcionario

        public ConexaoMDL spc_cadastra_funcionario(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_funcionario") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_funcionario", funcionarioMDL.Nome));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@telefone_funcionario", funcionarioMDL.Telefone));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@rg_funcionario", funcionarioMDL.Rg));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cnh_funcionario", funcionarioMDL.Cnh));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@n_trabalho_funcionario", funcionarioMDL.CTrabalho));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cep_funcionario", funcionarioMDL.Cep));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@endereco_funcionario", funcionarioMDL.Rua));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@numero_funcionario", funcionarioMDL.NEstabelecimento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@bairro_funcionario", funcionarioMDL.Bairro));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cidade_funcionario", funcionarioMDL.Cidade));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@estado_funcionario", funcionarioMDL.Estado));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cargo_funcionario", funcionarioMDL.Cargo));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@email_funcionario", funcionarioMDL.Email));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@senha_funcionario", funcionarioMDL.CepSenha));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@admissao_funcionario", DateTime.Now));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_atualiza_senha

        public ConexaoMDL spc_atualiza_senha(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_atualiza_senha") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@senha_funcionario", funcionarioMDL.Senha));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_informacoes_funcionario

        public ConexaoMDL spc_carrega_informacoes_funcionario(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_informacoes_funcionario")
                {
                    CommandType = CommandType.StoredProcedure
                };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_valida_periodo_ferias

        public ConexaoMDL spc_valida_periodo_ferias(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_periodo_ferias") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_aplica_ferias

        public ConexaoMDL spc_aplica_ferias(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_aplica_ferias") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@inicio_ferias", funcionarioMDL.Ferias));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@fim_ferias", funcionarioMDL.FFerias));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@proventos", funcionarioMDL.Proventos));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_informacoes_ponto

        public ConexaoMDL spc_carrega_informacoes_ponto(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_informacoes_ponto")
                {
                    CommandType = CommandType.StoredProcedure
                };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_ponto_entrada

        public ConexaoMDL spc_cadastra_ponto_entrada(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_ponto_entrada") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@hora_entrada", funcionarioMDL.HEntrada));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_ponto_saida

        public ConexaoMDL spc_cadastra_ponto_saida(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_ponto_saida") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@hora_entrada", funcionarioMDL.HEntrada));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@hora_saida", funcionarioMDL.HSaida));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_almoco_entrada

        public ConexaoMDL spc_cadastra_almoco_entrada(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_almoco_entrada") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@inicio_almoco", funcionarioMDL.Almoco));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@hora_entrada", funcionarioMDL.HEntrada));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_almoco_saida

        public ConexaoMDL spc_cadastra_almoco_saida(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_almoco_saida") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@final_almoco", funcionarioMDL.FAlmoco));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@hora_entrada", funcionarioMDL.HEntrada));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_altera_funcionario

        public ConexaoMDL spc_altera_funcionario(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_altera_dados_funcionario") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_funcionario", funcionarioMDL.Nome));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@telefone_funcionario", funcionarioMDL.Telefone));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cep_funcionario", funcionarioMDL.Cep));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@endereco_funcionario", funcionarioMDL.Rua));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@numero_funcionario", funcionarioMDL.NEstabelecimento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@bairro_funcionario", funcionarioMDL.Bairro));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cidade_funcionario", funcionarioMDL.Cidade));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cnh_funcionario", funcionarioMDL.Cnh));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@estado_funcionario", funcionarioMDL.Estado));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cargo_funcionario", funcionarioMDL.Cargo));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteScalar();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_desabilita_funcionario

        public ConexaoMDL spc_desabilita_funcionario(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_desabilita_funcionario") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteScalar();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_habilita_funcionario

        public ConexaoMDL spc_habilita_funcionario(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_habilita_funcionario") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteScalar();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_pesquisa_funcionarios

        public ConexaoMDL spc_pesquisa_funcionarios(string parametro, string coluna)
        {
            if (parametro == "" || coluna == null)
            {
                parametro = "*";
                coluna = "*";
            }

            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_pesquisa_funcionarios") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@parametro", parametro));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@coluna", coluna));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_pesquisa_inativos

        public ConexaoMDL spc_pesquisa_inativos(string parametro, string coluna)
        {
            _conexaoDAL.Conexao.Open();

            if (parametro == "" || coluna == null)
            {
                parametro = "*";
                coluna = "*";
            }

            _conexaoMDL.Cmd = new SqlCommand("spc_pesquisa_inativos") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@parametro", parametro));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@coluna", coluna));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_registra_log

        public void spc_registra_log(FuncionarioMDL funcionarioMDL, int i, string j)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_registra_log") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_alteracao", j));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@data_alteracao", DateTime.Now));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@tipo_alteracao", i));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();
        }

        #endregion

        #region spc_valida_email

        public ConexaoMDL spc_valida_email(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_email") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@email_funcionario", funcionarioMDL.Email));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_feedback

        public ConexaoMDL spc_cadastra_feedback (FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_feedback") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@assunto", funcionarioMDL.Assunto));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@mensagem", funcionarioMDL.Mensagem));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion
    }
}