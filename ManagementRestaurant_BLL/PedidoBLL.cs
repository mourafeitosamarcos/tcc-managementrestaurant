using System.Data;
using ManagementRestaurant_DAL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_BLL
{
    public class PedidoBLL
    {
        private readonly ClienteGLL _clienteGLL = new ClienteGLL();

        private readonly ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();
        private readonly PedidoDAL _pedidoDAL = new PedidoDAL();

        #region CadastraPedido

        public ConexaoMDL CadastraPedido(FuncionarioMDL funcionarioMDL, ClienteMDL clienteMDL, PedidoMDL pedidoMDL,
                                         DataSet CadPratos)
        {
            clienteMDL = _clienteGLL.TrataDados(clienteMDL);

            return _pedidoDAL.CadastraPedido(funcionarioMDL, clienteMDL, pedidoMDL, CadPratos);
        }

        #endregion

        #region PesquisaPedidos

        public ConexaoMDL PesquisaPedidos(string tipo)
        {
            return _pedidoDAL.PesquisaPedidos(tipo);
        }

        #endregion

        #region CarregaNomeEntregadores

        public ConexaoMDL CarregaNomeEntregadores()
        {
            return _pedidoDAL.spc_carrega_nome_entregadores();
        }

        #endregion

        #region ValidaMesa

        public ConexaoMDL ValidaMesa(PedidoMDL pedidoMDL)
        {
            return _pedidoDAL.spc_valida_mesa(pedidoMDL);
        }

        #endregion

        #region BaixaIngredientesEntrega

        public ConexaoMDL BaixaIngredientesEntrega(string pedido)
        {
            return _pedidoDAL.BaixaIngredientesEntrega(pedido);
        }

        #endregion

        #region EntregaPedido

        public void EntregaPedido(string pedido)
        {
            _pedidoDAL.EntregaPedido(pedido);
        }

        #endregion

        #region CadastraReserva

        public ConexaoMDL CadastraReserva(FuncionarioMDL funcionarioMDL, ClienteMDL clienteMDL, PedidoMDL pedidoMDL)
        {
            funcionarioMDL = _funcionarioGLL.TrataDados(funcionarioMDL);
            clienteMDL = _clienteGLL.TrataDados(clienteMDL);

            return _pedidoDAL.spc_cadastra_reserva(funcionarioMDL, clienteMDL, pedidoMDL);
        }

        #endregion

        #region CarregaInformacoesPedido

        public PedidoMDL CarregaInformacoesPedido(string nPedido)
        {
            return _pedidoDAL.CarregaInformacoesPedido(nPedido);
        }

        #endregion

        #region CarregaPrevisaoEntrega

        public ConexaoMDL CarregaPrevisaoEntrega(ConexaoMDL conexaoMDL)
        {
            return _pedidoDAL.spc_carrega_previsao_entrega(_conexaoMDL);
        }

        #endregion

        #region PesquisaIngredientes

        public ConexaoMDL PesquisaIngredientes()
        {
            return _pedidoDAL.PesquisaIngredientes();
        }

        #endregion
    }
}