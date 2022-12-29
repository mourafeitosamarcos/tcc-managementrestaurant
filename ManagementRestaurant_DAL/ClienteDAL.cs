using System.Data;
using System.Data.SqlClient;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_DAL
{
    public class ClienteDAL
    {
        private readonly ClienteMDL _clienteMDL = new ClienteMDL();

        private readonly ConexaoDAL _conexaoDAL = new ConexaoDAL();
        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region AlteraCliente

        public ConexaoMDL AlteraCliente(ClienteMDL clienteMDL)
        {
            _conexaoMDL.Ds.Clear();

            _conexaoMDL = spc_altera_dados_cliente(clienteMDL);

            _conexaoMDL.Validador = true;

            return _conexaoMDL;
        }

        #endregion

        #region CadastraCliente

        public ConexaoMDL CadastraCliente(ClienteMDL clienteMDL)
        {
            _conexaoMDL = spc_valida_documento_cliente(clienteMDL);

            try
            {
                _clienteMDL.Documento = _conexaoMDL.Ds.Tables[0].Rows[0]["Doc_Cliente"].ToString();
            }
            catch
            {
                _conexaoMDL.Validador = false;
            }

            if (clienteMDL.Documento != _clienteMDL.Documento)
            {
                _conexaoMDL = spc_cadastra_cliente(clienteMDL);

                _conexaoMDL.Validador = true;
            }
            else
            {
                _conexaoMDL.Validador = false;
            }

            return _conexaoMDL;
        }

        #endregion

        #region PesquisaClientes

        public ConexaoMDL PesquisaClientes(string parametro, string coluna, string tipo)
        {
            _conexaoMDL.Ds.Clear();

            _conexaoMDL = spc_pesquisa_clientes(parametro, coluna, tipo);

            return _conexaoMDL;
        }

        #endregion

        #region spc_valida_documento_cliente

        public ConexaoMDL spc_valida_documento_cliente(ClienteMDL clienteMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_documento_cliente") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@doc_cliente", clienteMDL.Documento));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_altera_dados_cliente

        public ConexaoMDL spc_altera_dados_cliente(ClienteMDL clienteMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_altera_dados_cliente") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_cliente", clienteMDL.Nome));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@telefone_cliente", clienteMDL.Telefone));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cep_cliente", clienteMDL.Cep));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@endereco_cliente", clienteMDL.Rua));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@numero_cliente", clienteMDL.NEstabelecimento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@bairro_cliente", clienteMDL.Bairro));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cidade_cliente", clienteMDL.Cidade));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@estado_cliente", clienteMDL.Estado));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@doc_cliente", clienteMDL.Documento));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteScalar();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_cliente

        public ConexaoMDL spc_cadastra_cliente(ClienteMDL clienteMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_cliente") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_cliente", clienteMDL.Nome));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@telefone_cliente", clienteMDL.Telefone));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@doc_cliente", clienteMDL.Documento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@email_cliente", clienteMDL.Email));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cep_cliente", clienteMDL.Cep));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@endereco_cliente", clienteMDL.Rua));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@numero_cliente", clienteMDL.NEstabelecimento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@bairro_cliente", clienteMDL.Bairro));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cidade_cliente", clienteMDL.Cidade));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@estado_cliente", clienteMDL.Estado));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@tipo_cliente", clienteMDL.Tipo));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_informacoes_cliente

        public ConexaoMDL spc_carrega_informacoes_cliente(ClienteMDL clienteMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_informacoes_cliente")
                {
                    CommandType = CommandType.StoredProcedure
                };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cod_cliente", clienteMDL.Documento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@tipo", clienteMDL.Tipo));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_pesquisa_clientes

        public ConexaoMDL spc_pesquisa_clientes(string parametro, string coluna, string tipo)
        {
            if (parametro == "" || coluna == null)
            {
                parametro = "*";
                coluna = "*";
            }

            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_pesquisa_clientes") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@parametro", parametro));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@coluna", coluna));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@tipo", tipo));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();


            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_informacoes_cliente_reserva

        public ConexaoMDL spc_carrega_informacoes_cliente_reserva(ClienteMDL clienteMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_informacoes_cliente_reserva")
                {
                    CommandType = CommandType.StoredProcedure
                };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@doc_cliente", clienteMDL.Documento));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion
    }
}