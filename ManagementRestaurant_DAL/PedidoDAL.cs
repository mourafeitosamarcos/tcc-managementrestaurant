using System;
using System.Data;
using System.Data.SqlClient;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_DAL
{
    public class PedidoDAL
    {
        private readonly ClienteDAL _clienteDAL = new ClienteDAL();
        private readonly ClienteMDL _clienteMDL = new ClienteMDL();

        private readonly ConexaoDAL _conexaoDAL = new ConexaoDAL();
        private readonly PedidoMDL _pedidoMDL = new PedidoMDL();
        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        private int _i, _j;

        #region CadastraPedido

        public ConexaoMDL CadastraPedido(FuncionarioMDL funcionarioMDL, ClienteMDL clienteMDL, PedidoMDL pedidoMDL,
                                         DataSet CadPratos)
        {
            _conexaoMDL = _clienteDAL.spc_valida_documento_cliente(clienteMDL);

            try
            {
                _clienteMDL.Documento = _conexaoMDL.Ds.Tables[0].Rows[0]["Doc_Cliente"].ToString();
            }
            catch
            {
                _conexaoMDL.Validador = false;
            }

            if (clienteMDL.Documento == _clienteMDL.Documento)
            {
                _conexaoMDL.Ds.Clear();

                _conexaoMDL = spc_cadastra_pedido(funcionarioMDL, clienteMDL, pedidoMDL);

                pedidoMDL.CPedido = Convert.ToInt32(_conexaoMDL.Ds.Tables[0].Rows[0]["Numero_Pedido"]);
                var pQuantidade = new string[Convert.ToInt32(CadPratos.Tables[0].Rows.Count)];
                var prato = new string[Convert.ToInt32(CadPratos.Tables[0].Rows.Count)];
                for (int a = 0; a <= CadPratos.Tables[0].Rows.Count - 1; a++)
                {
                    pQuantidade[Convert.ToInt32(a)] = CadPratos.Tables[0].Rows[a]["Quantidade"].ToString();
                    prato[Convert.ToInt32(a)] = CadPratos.Tables[0].Rows[a]["IdPrato"].ToString();
                }
                for (_i = 0; _i <= prato.Length - 1; _i++)
                {
                    _conexaoMDL = spc_cadastra_item_pedido(pedidoMDL, prato, pQuantidade);

                    _conexaoMDL = spc_cadastra_baixa_ingrediente(_conexaoMDL, pQuantidade);
                }

                _conexaoMDL.Validador = true;
            }

            return _conexaoMDL;
        }

        #endregion

        #region EntregaPedido

        public void EntregaPedido(string pedido)
        {
            spc_entrega_pedido(pedido);
        }

        #endregion

        #region BaixaIngredientesEntrega

        public ConexaoMDL BaixaIngredientesEntrega(string pedido)
        {
            _conexaoMDL = spc_pesquisa_ingredientes_entrega(pedido);

            var pQuantidade = new string[Convert.ToInt32(1)];
            pQuantidade[Convert.ToInt32(0)] = "1";
            _i = 0;

            _conexaoMDL = spc_cadastra_baixa_ingrediente(_conexaoMDL, pQuantidade);


            return _conexaoMDL;
        }

        #endregion

        #region CarregaInformacoesPedido

        public PedidoMDL CarregaInformacoesPedido(String nPedido)
        {
            return spc_carrega_informacoes_pedido(nPedido);
        }

        #endregion

        #region PesquisaPedidos

        public ConexaoMDL PesquisaPedidos(string tipo)
        {
            return spc_pesquisa_pedidos(tipo);
        }

        #endregion

        #region PesquisaIngredientes

        public ConexaoMDL PesquisaIngredientes()
        {
            return spc_pesquisa_ingredientes();
        }

        #endregion

        #region spc_pesquisa_ingredientes

        public ConexaoMDL spc_pesquisa_ingredientes()
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_pesquisa_ingredientes") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();


            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_nome_entregadores

        public ConexaoMDL spc_carrega_nome_entregadores()
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_nome_entregadores")
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

        #region spc_pesquisa_pedidos

        public ConexaoMDL spc_pesquisa_pedidos(string tipo)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_pesquisa_pedidos") {CommandType = CommandType.StoredProcedure};
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@tipo", tipo));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_pedido

        public ConexaoMDL spc_cadastra_pedido(FuncionarioMDL funcionarioMDL, ClienteMDL clienteMDL, PedidoMDL pedidoMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_pedido") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@doc_cliente", clienteMDL.Documento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcioanario", funcionarioMDL.Cpf));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@pedido_tipo", pedidoMDL.Compra));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cliente_tipo", pedidoMDL.Cliente));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@pedido_valor", pedidoMDL.ValorTotal));

            if (pedidoMDL.Contrato.Ticks == 0 && pedidoMDL.FContrato.Ticks == 0)
            {
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@pedido_quantidade", null));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@data_inicio", null));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@data_fim", null));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@previsao_entrega", null));
            }
            else
            {
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@pedido_quantidade", pedidoMDL.QDia));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@data_inicio", pedidoMDL.Contrato));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@data_fim", pedidoMDL.FContrato));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@previsao_entrega", pedidoMDL.Previsao.TimeOfDay));
            }

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_item_pedido

        public ConexaoMDL spc_cadastra_item_pedido(PedidoMDL pedidoMDL, string[] prato, string[] pQuantidade)
        {
            _conexaoMDL.Ds2.Clear();

            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_item_pedido") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@prato", prato[_i]));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@numero_pedido", pedidoMDL.CPedido));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@pedido_data", DateTime.Now));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@quantidade_prato", Convert.ToDouble(pQuantidade[_i])));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds2);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_baixa_ingrediente

        public ConexaoMDL spc_cadastra_baixa_ingrediente(ConexaoMDL conexaoMDL, string[] pQuantidade)
        {
            for (_j = 0; _j <= conexaoMDL.Ds2.Tables[0].Rows.Count - 1; _j++)
            {
                _conexaoDAL.Conexao.Open();

                conexaoMDL.Cmd = new SqlCommand("spc_cadastra_baixa_ingrediente")
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@ingrediente",
                                                               conexaoMDL.Ds2.Tables[0].Rows[_j]["Cod_Produto"]));
                conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@quantidade_ingrediente",
                                                               conexaoMDL.Ds2.Tables[0].Rows[_j]["Quantidade"]));
                conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@quantidade_prato", Convert.ToDouble(pQuantidade[_i])));

                conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
                conexaoMDL.Cmd.ExecuteNonQuery();
                _conexaoDAL.Conexao.Close();
            }
            conexaoMDL.Validador = true;
            return conexaoMDL;
        }

        #endregion

        #region spc_entrega_pedido

        public void spc_entrega_pedido(string pedido)
        {
            _conexaoDAL.Conexao.Open();
            _conexaoMDL.Cmd = new SqlCommand("spc_entrega_pedido") {CommandType = CommandType.StoredProcedure};
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@numero_pedido", pedido));
            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Validador = Convert.ToBoolean(_conexaoMDL.Cmd.ExecuteScalar());
            _conexaoDAL.Conexao.Close();
        }

        #endregion

        #region spc_valida_mesa

        public ConexaoMDL spc_valida_mesa(PedidoMDL pedidoMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_mesa") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@data_reserva", pedidoMDL.Reserva));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@numero_mesa", pedidoMDL.Mesa));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Validador = Convert.ToBoolean(_conexaoMDL.Cmd.ExecuteScalar());

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_reserva

        public ConexaoMDL spc_cadastra_reserva(FuncionarioMDL funcionarioMDL, ClienteMDL clienteMDL, PedidoMDL pedidoMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_reserva") {CommandType = CommandType.StoredProcedure};

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@doc_cliente", clienteMDL.Documento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@data_reserva", pedidoMDL.Reserva));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@numero_mesa", pedidoMDL.Mesa));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_pesquisa_pedidos_entrega

        public ConexaoMDL spc_pesquisa_ingredientes_entrega(string pedido)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_pesquisa_ingredientes_entrega")
                {
                    CommandType = CommandType.StoredProcedure
                };
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@numero_pedido", pedido));
            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds2);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_informacoes_pedido

        public PedidoMDL spc_carrega_informacoes_pedido(string nPedido)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_informacoes_pedido")
                {
                    CommandType = CommandType.StoredProcedure
                };
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@NPedido", nPedido));
            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            _pedidoMDL.Registro = _conexaoMDL.Ds;

            return _pedidoMDL;
        }

        #endregion

        #region spc_carrega_previsao_entrega

        public ConexaoMDL spc_carrega_previsao_entrega(ConexaoMDL conexaoMDL)
        {
            _conexaoDAL.Conexao.Open();

            conexaoMDL.Cmd = new SqlCommand("spc_carrega_previsao_entrega")
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
    }
}