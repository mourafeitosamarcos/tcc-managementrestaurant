using System.Data;
using System.Data.SqlClient;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_DAL
{
    public class FrotaDAL
    {
        private readonly ConexaoDAL _conexaoDAL = new ConexaoDAL();
        private readonly ConexaoMDL _conexaoMDL2 = new ConexaoMDL();

        private readonly FrotaMDL _frotaMDL = new FrotaMDL();

        private readonly FuncionarioDAL _funcionarioDAL = new FuncionarioDAL();
        private readonly FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();
        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region CadastraVeiculo

        public ConexaoMDL CadastraVeiculo(FrotaMDL frotaMDL, FuncionarioMDL funcionarioMDL)
        {
            _conexaoMDL = spc_valida_documento_veiculo(funcionarioMDL);

            try
            {
                _funcionarioMDL.Cpf = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_CPF"].ToString();

                _conexaoMDL.Validador2 = true;
            }
            catch
            {
                if (funcionarioMDL.Cpf != _funcionarioMDL.Cpf)
                {
                    _conexaoMDL = spc_valida_placa_veiculo_interno(frotaMDL);

                    try
                    {
                        _frotaMDL.Placa = _conexaoMDL.Ds.Tables[0].Rows[0]["Placa"].ToString();
                    }
                    catch
                    {
                        if (frotaMDL.Placa != _frotaMDL.Placa)
                        {
                            _conexaoMDL = spc_cadastra_veiculo_interno(frotaMDL, funcionarioMDL);

                            _conexaoMDL.Validador = true;
                        }
                        else
                        {
                            _conexaoMDL.Validador2 = false;
                        }
                    }
                }
                else
                {
                    _conexaoMDL.Validador2 = true;
                }
            }

            return _conexaoMDL;
        }

        #endregion

        #region CarregaInformacoesFuncionarioVeiculo

        public ConexaoMDL CarregaInformacoesFuncionarioVeiculo(FrotaMDL frotaMDL, FuncionarioMDL funcionarioMDL)
        {
            _conexaoMDL = _funcionarioDAL.spc_valida_documento_funcionario(funcionarioMDL);

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

                _conexaoMDL = spc_carrega_informacoes_funcionario_veiculo(funcionarioMDL);

                _conexaoMDL.Validador = true;
            }

            return _conexaoMDL;
        }

        #endregion

        #region AlteraVeiculo

        public ConexaoMDL AlteraVeiculo(FrotaMDL frotaMDL, FuncionarioMDL funcionarioMDL)
        {
            return _conexaoMDL = spc_altera_veiculo(frotaMDL, funcionarioMDL);
        }

        #endregion

        #region CarregaPlacaVeiculoInterno

        public ConexaoMDL CarregaPlacaVeiculoInterno(FrotaMDL frotaMDL, FuncionarioMDL funcionarioMDL)
        {
            _conexaoMDL = spc_valida_placa_veiculo_interno(frotaMDL);

            try
            {
                _frotaMDL.Placa = _conexaoMDL.Ds.Tables[0].Rows[0]["Placa"].ToString();
            }
            catch
            {
                frotaMDL.Validador = false;
            }

            if (frotaMDL.Placa == _frotaMDL.Placa)
            {
                _conexaoMDL.Ds.Clear();

                _conexaoMDL = spc_carrega_veiculo_interno(frotaMDL, funcionarioMDL);
            }

            return _conexaoMDL;
        }

        #endregion

        #region CarregaVagaPrisma

        public ConexaoMDL CarregaVagaPrisma(FrotaMDL frotaMDL, FuncionarioMDL funcionarioMDL)
        {
            _conexaoMDL = spc_valida_prisma(frotaMDL);

            try
            {
                frotaMDL.Placa = _conexaoMDL.Ds.Tables[0].Rows[0]["Placa"].ToString();

                frotaMDL.Validador = true;
            }
            catch
            {
                frotaMDL.Validador = false;
            }

            return _conexaoMDL;
        }

        #endregion

        #region spc_valida_placa_veiculo_interno

        public ConexaoMDL spc_valida_placa_veiculo_interno(FrotaMDL frotaMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_placa_veiculo_interno")
                {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@placa_veiculo", frotaMDL.Placa));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_valida_prisma

        public ConexaoMDL spc_valida_prisma(FrotaMDL frotaMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_prisma") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@prisma", frotaMDL.Prisma));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_valida_documento_funcionario_veiculo

        public ConexaoMDL spc_valida_documento_funcionario_veiculo(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_documento_funcionario_veiculo")
                {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_informacoes_funcionario_veiculo

        public ConexaoMDL spc_carrega_informacoes_funcionario_veiculo(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_informacoes_funcionario_veiculo")
                {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_valida_documento_veiculo

        public ConexaoMDL spc_valida_documento_veiculo(FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_documento_veiculo") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_veiculo_interno

        public ConexaoMDL spc_cadastra_veiculo_interno(FrotaMDL frotaMDL, FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_veiculo_interno") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@placa_veiculo", frotaMDL.Placa));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@tipo_veiculo", frotaMDL.Tipo));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@marca_veiculo", frotaMDL.Marca));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@modelo_veiculo", frotaMDL.Modelo));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_veiculo_interno

        public ConexaoMDL spc_carrega_veiculo_interno(FrotaMDL frotaMDL, FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_veiculo_interno") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@placa_veiculo", frotaMDL.Placa));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_altera_veiculo

        public ConexaoMDL spc_altera_veiculo(FrotaMDL frotaMDL, FuncionarioMDL funcionarioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_altera_veiculo") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@placa_veiculo", frotaMDL.Placa));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@tipo_veiculo", frotaMDL.Tipo));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@marca_veiculo", frotaMDL.Marca));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@modelo_veiculo", frotaMDL.Modelo));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteScalar();
            _conexaoDAL.Conexao.Close();
            _conexaoMDL.Validador = true;
            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_entrada_veiculo

        public ConexaoMDL spc_carrega_entrada_veiculo(FrotaMDL frotaMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_entrada_veiculo") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@placa_veiculo", frotaMDL.Placa));

            _conexaoMDL.Ds.Clear();

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_entrada_veiculo

        public ConexaoMDL spc_cadastra_entrada_veiculo(FrotaMDL frotaMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_entrada_veiculo")
                {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@placa_veiculo", frotaMDL.Placa));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@prisma", frotaMDL.Prisma));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@entrada_veiculo", frotaMDL.DEntrada));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_saida_veiculo

        public ConexaoMDL spc_cadastra_saida_veiculo(FrotaMDL frotaMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_saida_veiculo")
                {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@placa_veiculo", frotaMDL.Placa));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@prisma", frotaMDL.Prisma));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@entrada_veiculo", frotaMDL.DEntrada));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@saida_veiculo", frotaMDL.DSaida));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_prisma

        public ConexaoMDL spc_carrega_prisma()
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL2.Cmd = new SqlCommand("spc_carrega_prisma")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = _conexaoDAL.Conexao
                };

            _conexaoMDL2.Da = new SqlDataAdapter(_conexaoMDL2.Cmd);
            _conexaoMDL2.Da.Fill(_conexaoMDL2.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL2;
        }

        #endregion

        #region spc_carrega_vagas

        public ConexaoMDL spc_carrega_vagas()
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL2.Cmd = new SqlCommand("spc_carrega_vagas")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = _conexaoDAL.Conexao
                };

            _conexaoMDL2.Da = new SqlDataAdapter(_conexaoMDL2.Cmd);
            _conexaoMDL2.Da.Fill(_conexaoMDL2.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL2;
        }

        #endregion
    }
}