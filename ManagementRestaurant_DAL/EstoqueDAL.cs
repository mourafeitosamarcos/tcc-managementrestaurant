using System;
using System.Data;
using System.Data.SqlClient;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_DAL
{
    public class EstoqueDAL
    {
        private readonly ConexaoDAL _conexaoDAL = new ConexaoDAL();
        private readonly EstoqueMDL _estoqueMDL = new EstoqueMDL();
        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region CadastraFornecedor

        public ConexaoMDL CadastraFornecedor(EstoqueMDL estoqueMDL)
        {
            _conexaoMDL = spc_valida_documento_fornecedor(estoqueMDL);

            try
            {
                _estoqueMDL.FCnpj = _conexaoMDL.Ds.Tables[0].Rows[0]["For_CNPJ"].ToString();
            }
            catch
            {
                _conexaoMDL.Validador = false;
            }

            if (estoqueMDL.FCnpj != _estoqueMDL.FCnpj)
            {
                _conexaoMDL = spc_cadastra_fornecedor(estoqueMDL);

                _conexaoMDL.Validador = true;
            }
            else
            {
                _conexaoMDL.Validador = false;
            }

            return _conexaoMDL;
        }

        #endregion

        #region AlteraFornecedor

        public ConexaoMDL AlteraFornecedor(EstoqueMDL estoqueMDL)
        {
            _conexaoMDL = spc_altera_fornecedor(estoqueMDL);

            _conexaoMDL.Validador = true;

            return _conexaoMDL;
        }

        #endregion

        #region CarregaDadosFornecedor

        public ConexaoMDL CarregaDadosFornecedor(EstoqueMDL estoqueMDL)
        {
            _conexaoMDL = spc_carrega_dados_fornecedor(estoqueMDL);


            return _conexaoMDL;
        }

        #endregion

        #region CadastraProduto

        public ConexaoMDL CadastraProduto(EstoqueMDL estoqueMDL)
        {
            _conexaoMDL = spc_valida_nome_produto(estoqueMDL);

            try
            {
                _estoqueMDL.NProduto = _conexaoMDL.Ds.Tables[0].Rows[0]["Pro_Nome"].ToString();
            }
            catch
            {
                _conexaoMDL.Validador = false;
            }

            if (estoqueMDL.NProduto != _estoqueMDL.NProduto)
            {
                _conexaoMDL = spc_cadastra_produto(estoqueMDL);

                _conexaoMDL.Validador = true;
            }
            else
            {
                _conexaoMDL.Validador = false;
            }

            return _conexaoMDL;
        }

        #endregion

        #region CadastraEntradaEstoque

        public ConexaoMDL CadastraEntradaEstoque(EstoqueMDL estoqueMDL)
        {
            _conexaoMDL = spc_valida_nota_fiscal(estoqueMDL);

            try
            {
                _estoqueMDL.NFiscal = _conexaoMDL.Ds.Tables[0].Rows[0]["NF_Produto"].ToString();
            }
            catch
            {
                _conexaoMDL.Validador = false;
            }

            if (estoqueMDL.NFiscal != _estoqueMDL.NFiscal)
            {
                _conexaoMDL = spc_cadastra_entrada_estoque(estoqueMDL);

                _conexaoMDL.Validador = true;
            }
            else
            {
                _conexaoMDL.Validador = false;
            }

            return _conexaoMDL;
        }

        #endregion

        #region CadastraPrato

        public ConexaoMDL CadastraPrato(EstoqueMDL estoqueMDL, DataSet CadIng)
        {
            _conexaoMDL = spc_valida_nome_prato(estoqueMDL);

            try
            {
                _estoqueMDL.NPrato = _conexaoMDL.Ds.Tables[0].Rows[0]["Nome_Prato"].ToString();
            }
            catch
            {
                _conexaoMDL.Validador = false;
            }

            if (estoqueMDL.NPrato != _estoqueMDL.NPrato)
            {
                _conexaoMDL = spc_cadastra_prato(estoqueMDL);

                estoqueMDL.CPrato = Convert.ToInt32(_conexaoMDL.Ds.Tables[0].Rows[0]["ID_Prato"].ToString());

                _conexaoMDL = spc_cadastra_ingrediente_prato(estoqueMDL, CadIng);

                _conexaoMDL.Validador = true;
            }
            else
            {
                _conexaoMDL.Validador = false;
            }

            return _conexaoMDL;
        }

        #endregion

        #region spc_valida_documento_fornecedor

        public ConexaoMDL spc_valida_documento_fornecedor(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_documento_fornecedor")
                {
                    CommandType = CommandType.StoredProcedure
                };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cnpj_fornecedor", estoqueMDL.FCnpj));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_valida_nome_produto

        public ConexaoMDL spc_valida_nome_produto(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_nome_produto") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_produto", estoqueMDL.NProduto));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_fornecedor

        public ConexaoMDL spc_cadastra_fornecedor(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_fornecedor") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_fornecedor", estoqueMDL.FNome));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@telefone_fornecedor", estoqueMDL.FTelefone));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cnpj_fornecedor", estoqueMDL.FCnpj));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cep_fornecedor", estoqueMDL.FCep));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@endereco_fornecedor", estoqueMDL.FRua));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@numero_fornecedor", estoqueMDL.FnEstabelecimento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@bairro_fornecedor", estoqueMDL.FBairro));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cidade_fornecedor", estoqueMDL.FCidade));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@estado_fornecedor", estoqueMDL.FEstado));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@complemento_fornecedor", estoqueMDL.FComplemento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@email_fornecedor", estoqueMDL.FEmail));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_altera_fornecedor

        public ConexaoMDL spc_altera_fornecedor(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_altera_fornecedor") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_fornecedor", estoqueMDL.FNome));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@telefone_fornecedor", estoqueMDL.FTelefone));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cnpj_fornecedor", estoqueMDL.FCnpj));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cep_fornecedor", estoqueMDL.FCep));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@endereco_fornecedor", estoqueMDL.FRua));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@numero_fornecedor", estoqueMDL.FnEstabelecimento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@bairro_fornecedor", estoqueMDL.FBairro));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cidade_fornecedor", estoqueMDL.FCidade));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@estado_fornecedor", estoqueMDL.FEstado));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@complemento_fornecedor", estoqueMDL.FComplemento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@email_fornecedor", estoqueMDL.FEmail));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_nome_fornecedor

        public ConexaoMDL spc_carrega_nome_fornecedor(ConexaoMDL conexaoMDL)
        {
            _conexaoDAL.Conexao.Open();

            conexaoMDL.Cmd = new SqlCommand("spc_carrega_nome_fornecedor")
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

        #region spc_carrega_tipo_produto

        public ConexaoMDL spc_carrega_tipo_produto(ConexaoMDL conexaoMDL)
        {
            _conexaoDAL.Conexao.Open();

            conexaoMDL.Cmd = new SqlCommand("spc_carrega_tipo_produto")
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

        #region spc_carrega_nome_produto

        public ConexaoMDL spc_carrega_nome_produto(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_nome_produto") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cod_produto", estoqueMDL.CProduto));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();


            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_produto

        public ConexaoMDL spc_cadastra_produto(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_produto") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_produto", estoqueMDL.NProduto));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@tipo_produto", estoqueMDL.Produto));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_valida_nota_fiscal

        public ConexaoMDL spc_valida_nota_fiscal(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_nota_fiscal") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nf_produto", estoqueMDL.NFiscal));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_dados_fornecedor

        public ConexaoMDL spc_carrega_dados_fornecedor(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_dados_fornecedor") {CommandType = CommandType.StoredProcedure};
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cnpj_fornecedor", estoqueMDL.FCnpj));
            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();


            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_entrada_estoque

        public ConexaoMDL spc_cadastra_entrada_estoque(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_entrada_estoque") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nf_produto", estoqueMDL.NFiscal));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@tipo_compra", estoqueMDL.Compra));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@lote", estoqueMDL.Lote));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cnpj_fornecedor", estoqueMDL.FCnpj));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cod_produto", estoqueMDL.CProduto));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@validade", estoqueMDL.Validade));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@quantidade", estoqueMDL.Quantidade));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_nome_produto_dropdown

        public ConexaoMDL spc_carrega_nome_produto_dropdown()
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_nome_produto_dropdown")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = _conexaoDAL.Conexao
                };

            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();


            return _conexaoMDL;
        }

        #endregion

        #region spc_valida_nome_prato

        public ConexaoMDL spc_valida_nome_prato(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_nome_prato") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_prato", estoqueMDL.NPrato));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_prato

        public ConexaoMDL spc_cadastra_prato(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_prato") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_prato", estoqueMDL.NPrato));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@valor_prato", estoqueMDL.VPrato));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_ingrediente_prato

        public ConexaoMDL spc_cadastra_ingrediente_prato(EstoqueMDL estoqueMDL, DataSet CadIng)
        {
            int i;

            for (i = 0; i <= CadIng.Tables[0].Rows.Count - 1; i++)
            {
                _conexaoDAL.Conexao.Open();

                _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_ingrediente_prato")
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@ingrediente_prato",
                                                                CadIng.Tables[0].Rows[i]["IdProd"].ToString()));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@id_prato", estoqueMDL.CPrato));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@quantidade_ingrediente",
                                                                Convert.ToDouble(CadIng.Tables[0].Rows[i]["Qtd"])));

                _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
                _conexaoMDL.Cmd.ExecuteNonQuery();

                _conexaoDAL.Conexao.Close();
            }

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_nome_prato_dropdown

        public ConexaoMDL spc_carrega_nome_prato_dropdown()
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_nome_prato_dropdown")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = _conexaoDAL.Conexao
                };

            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();


            return _conexaoMDL;
        }

        #endregion
    }
}