using System;
using System.Data;
using System.Data.SqlClient;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_DAL
{
    public class FinanceiroDAL
    {
        private readonly ConexaoDAL _conexaoDAL = new ConexaoDAL();
        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

       
        #region CadastraCentroCusto

        public ConexaoMDL CadastraCentroCusto(FinanceiroMDL financeiroMDL)
        {
            _conexaoMDL = spc_valida_centro_custo(financeiroMDL);

            if (_conexaoMDL.Validador == false)
            {
                _conexaoMDL = spc_cadastra_centro_custo(financeiroMDL);
            }

            return _conexaoMDL;
        }

        #endregion

        #region AtualizaCentroCusto

        public ConexaoMDL AtualizaCentroCusto(FinanceiroMDL financeiroMDL)
        {
            if (_conexaoMDL.Validador == false)
            {
                _conexaoMDL = spc_atualiza_centro_custo(financeiroMDL);
            }

            return _conexaoMDL;
        }

        #endregion

        #region CadastraContrato

        public ConexaoMDL CadastraContrato(FinanceiroMDL financeiroMDL)
        {
            _conexaoMDL = spc_valida_contrato(financeiroMDL);

            if (_conexaoMDL.Validador == false)
            {
                _conexaoMDL = spc_cadastra_contrato(financeiroMDL);
            }

            return _conexaoMDL;
        }

        #endregion

        #region AtualizaContrato

        public ConexaoMDL AtualizaContrato(FinanceiroMDL financeiroMDL)
        {
            if (_conexaoMDL.Validador == false)
            {
                _conexaoMDL = spc_atualiza_contrato(financeiroMDL);
            }

            return _conexaoMDL;
        }

        #endregion

        #region CadastraDepartamento

        public ConexaoMDL CadastraDepartamento(FinanceiroMDL financeiroMDL)
        {
            _conexaoMDL = spc_valida_departamento(financeiroMDL);

            if (_conexaoMDL.Validador == false)
            {
                _conexaoMDL = spc_cadastra_departamento(financeiroMDL);
            }

            return _conexaoMDL;
        }

        #endregion

        #region AtualizaDepartamento

        public ConexaoMDL AtualizaDepartamento(FinanceiroMDL financeiroMDL)
        {
        
        
        
                _conexaoMDL = spc_atualiza_departamento(financeiroMDL);
        
            return _conexaoMDL;
        }

        #endregion

        #region CadastraConta

        public ConexaoMDL CadastraConta(FinanceiroMDL financeiroMDL)
        {
            _conexaoMDL = spc_valida_conta(financeiroMDL);

            if (_conexaoMDL.Validador == false)
            {
                _conexaoMDL = spc_cadastra_conta(financeiroMDL);
            }

            return _conexaoMDL;
        }

        #endregion

        #region spc_valida_centro_custo

        public ConexaoMDL spc_valida_centro_custo(FinanceiroMDL financeiroMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_centro_custo") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_ccusto", financeiroMDL.CCusto));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_ccusto", financeiroMDL.NCusto));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Validador = Convert.ToBoolean(_conexaoMDL.Cmd.ExecuteScalar());

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion
       
        #region spc_cadastra_centro_custo

        public ConexaoMDL spc_cadastra_centro_custo(FinanceiroMDL financeiroMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_centro_custo") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_ccusto", financeiroMDL.CCusto));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_ccusto", financeiroMDL.NCusto));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_atualiza_centro_custo

        public ConexaoMDL spc_atualiza_centro_custo(FinanceiroMDL financeiroMDL)
        {
          try
          {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_atualiza_centro_custo") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_ccusto", financeiroMDL.CCusto));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_ccusto", financeiroMDL.NCusto));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();
     _conexaoMDL.Validador = true;
            }
            catch { _conexaoMDL.Validador = false; }
                return _conexaoMDL;
        }

        #endregion

        #region spc_valida_contrato

        public ConexaoMDL spc_valida_contrato(FinanceiroMDL financeiroMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_contrato") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_contrato", financeiroMDL.CContrato));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_contrato", financeiroMDL.NContrato));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Validador = Convert.ToBoolean(_conexaoMDL.Cmd.ExecuteScalar());

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion
        
        #region spc_pesquisa_centro_custo

        public ConexaoMDL spc_pesquisa_centro_custo()
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_pesquisa_centro_custo") { CommandType = CommandType.StoredProcedure };
            
            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);
            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_pesquisa_contrato

        public ConexaoMDL spc_pesquisa_contrato()
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_pesquisa_contrato") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);
            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_pesquisa_departamento

        public ConexaoMDL spc_pesquisa_departamento()
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_pesquisa_departamento") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);
            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_dados_contrato

        public ConexaoMDL spc_carrega_dados_contrato(FinanceiroMDL financeiroMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_dados_contrato") { CommandType = CommandType.StoredProcedure };
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@Num_Contrato",financeiroMDL.NContrato));
            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);
            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_dados_departamento

        public ConexaoMDL spc_carrega_dados_departamento(FinanceiroMDL financeiroMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_dados_departamento") { CommandType = CommandType.StoredProcedure };
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@Num_departamento", financeiroMDL.NDepartamento));
            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);
            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_dados_centro_custo

        public ConexaoMDL spc_carrega_dados_centro_custo(FinanceiroMDL financeiroMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_dados_centro_custo") { CommandType = CommandType.StoredProcedure };
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@Num_CC", financeiroMDL.NCusto));
            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);
            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_contrato

        public ConexaoMDL spc_cadastra_contrato(FinanceiroMDL financeiroMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_contrato") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_contrato", financeiroMDL.CContrato));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_contrato", financeiroMDL.NContrato));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cliente_consolidado", financeiroMDL.CConsolidado));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_atualiza_contrato

        public ConexaoMDL spc_atualiza_contrato(FinanceiroMDL financeiroMDL)
        {
            try
            {
                _conexaoDAL.Conexao.Open();

                _conexaoMDL.Cmd = new SqlCommand("spc_atualiza_contrato") { CommandType = CommandType.StoredProcedure };

                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_contrato", financeiroMDL.CContrato));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_contrato", financeiroMDL.NContrato));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cliente_consolidado", financeiroMDL.CConsolidado));

                _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
                _conexaoMDL.Cmd.ExecuteNonQuery();

                _conexaoDAL.Conexao.Close();
                _conexaoMDL.Validador = true;
            }
            catch { _conexaoMDL.Validador = false; }
                return _conexaoMDL;
            
        }

        #endregion

        #region spc_valida_departamento

        public ConexaoMDL spc_valida_departamento(FinanceiroMDL financeiroMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_departamento") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_departamento", financeiroMDL.NDepartamento));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Validador = Convert.ToBoolean(_conexaoMDL.Cmd.ExecuteScalar());

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_departamento

        public ConexaoMDL spc_cadastra_departamento(FinanceiroMDL financeiroMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_departamento") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_departamento", financeiroMDL.NDepartamento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@responsavel", financeiroMDL.Responsavel));

            if (financeiroMDL.CCusto != 0)
            {
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_ccusto", financeiroMDL.CCusto));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_contrato", null));
            }
            else
            {
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_ccusto", null));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_contrato", financeiroMDL.CContrato));
            }

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_atualiza_departamento

        public ConexaoMDL spc_atualiza_departamento(FinanceiroMDL financeiroMDL)
        {
            try
            {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_atualiza_departamento") { CommandType = CommandType.StoredProcedure };
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_departamento", financeiroMDL.CDepartamento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_departamento", financeiroMDL.NDepartamento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@responsavel", financeiroMDL.Responsavel));

            if (financeiroMDL.CCusto != 0)
            {
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_ccusto", financeiroMDL.CCusto));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_contrato", null));
            }
            else
            {
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_ccusto", null));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_contrato", financeiroMDL.CContrato));
            }

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();
            _conexaoMDL.Validador = true;
            }
            catch { _conexaoMDL.Validador = false; }
            return _conexaoMDL;
            
        }

        #endregion
        
        #region spc_carrega_responsavel

        public ConexaoMDL spc_carrega_responsavel(ConexaoMDL conexaoMDL)
        {
            _conexaoDAL.Conexao.Open();

            conexaoMDL.Cmd = new SqlCommand("spc_carrega_responsavel")
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

        #region spc_carrega_centro_custo

        public ConexaoMDL spc_carrega_centro_custo(ConexaoMDL conexaoMDL)
        {
            _conexaoDAL.Conexao.Open();

            conexaoMDL.Cmd = new SqlCommand("spc_carrega_centro_custo")
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

        #region spc_carrega_contrato

        public ConexaoMDL spc_carrega_contrato(ConexaoMDL conexaoMDL)
        {
            _conexaoDAL.Conexao.Open();

            conexaoMDL.Cmd = new SqlCommand("spc_carrega_contrato")
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

        #region spc_carrega_departamento

        public ConexaoMDL spc_carrega_departamento(ConexaoMDL conexaoMDL)
        {
            _conexaoDAL.Conexao.Open();

            conexaoMDL.Cmd = new SqlCommand("spc_carrega_departamento")
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

        #region spc_valida_conta

        public ConexaoMDL spc_valida_conta(FinanceiroMDL financeiroMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_conta") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@numero_conta", financeiroMDL.NConta));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Validador = Convert.ToBoolean(_conexaoMDL.Cmd.ExecuteScalar());

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_conta

        public ConexaoMDL spc_cadastra_conta(FinanceiroMDL financeiroMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_conta") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@numero_conta", financeiroMDL.NConta));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_departamento", financeiroMDL.CDepartamento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@data_pagamento", financeiroMDL.DPagamento));

            if (financeiroMDL.CCusto != 0)
            {
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_ccusto", financeiroMDL.CCusto));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_contrato", null));
            }
            else
            {
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_ccusto", null));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_contrato", financeiroMDL.CContrato));
            }

            _conexaoMDL.Cmd.Parameters.Add(financeiroMDL.Observacao != null
                                               ? new SqlParameter("@observacao", financeiroMDL.Observacao)
                                               : new SqlParameter("@observacao", null));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_tipo_departamento

        public ConexaoMDL spc_carrega_tipo_departamento(FinanceiroMDL financeiroMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_tipo_departamento")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = _conexaoDAL.Conexao
                };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codigo_departamento", financeiroMDL.CDepartamento));

            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion
    }
}